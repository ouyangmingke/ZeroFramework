using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using RabbitMQ.Client;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

using Zero.Core.DependencyInjection;
using Zero.EventBus.Abstractions.Domain;
using Zero.EventBus.Domain;
using Zero.EventBus.Domain.Distributed;
using Zero.RabbitMQ.Domain;

namespace Zero.EventBus.RabbitMQ.Domain;

public class RabbitMqDistributedEventBus : DistributedEventBusBase, ISingletonDependency
{
    protected ZeroRabbitMqEventBusOptions ZeroRabbitMqEventBusOptions { get; }
    protected IConnectionPool ConnectionPool { get; }
    protected IRabbitMqSerializer Serializer { get; }
    protected ConcurrentDictionary<Type, List<IEventHandlerFactory>> HandlerFactories { get; }
    protected ConcurrentDictionary<string, Type> EventTypes { get; }
    protected IRabbitMqMessageConsumerFactory MessageConsumerFactory { get; }
    protected IRabbitMqMessageConsumer Consumer { get; private set; }

    private bool _exchangeCreated;

    public RabbitMqDistributedEventBus(
        IOptions<ZeroRabbitMqEventBusOptions> options,
        IConnectionPool connectionPool,
              IRabbitMqSerializer serializer,
        IServiceScopeFactory serviceScopeFactory,
        IRabbitMqMessageConsumerFactory messageConsumerFactory
        )
        : base(
            serviceScopeFactory)
    {
        ConnectionPool = connectionPool;
        Serializer = serializer;
        MessageConsumerFactory = messageConsumerFactory;
        ZeroRabbitMqEventBusOptions = options.Value;
        HandlerFactories = new ConcurrentDictionary<Type, List<IEventHandlerFactory>>();
        EventTypes = new ConcurrentDictionary<string, Type>();
    }

    public void Initialize()
    {
        Consumer = MessageConsumerFactory.Create(
            new ExchangeDeclareConfiguration(
                ZeroRabbitMqEventBusOptions.ExchangeName,
                type: ZeroRabbitMqEventBusOptions.GetExchangeTypeOrDefault(),
                durable: true
            ),
            new QueueDeclareConfiguration(
                ZeroRabbitMqEventBusOptions.ClientName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                prefetchCount: ZeroRabbitMqEventBusOptions.PrefetchCount
            ),
            ZeroRabbitMqEventBusOptions.ConnectionName
        );

        // Todo: 订阅消息
        //Consumer.OnMessageReceived(ProcessEventAsync);
    }

    protected override async Task PublishToEventBusAsync(Type eventType, object eventData)
    {
        await PublishAsync(eventType, eventData, null);
    }

    public Task PublishAsync(
    Type eventType,
    object eventData,
    IBasicProperties properties,
    Dictionary<string, object> headersArguments = null)
    {
        var eventName = EventNameAttribute.GetNameOrDefault(eventType);
        var body = Serializer.Serialize(eventData);

        return PublishAsync(eventName, body, properties, headersArguments);
    }
    protected virtual Task PublishAsync(
    string eventName,
    byte[] body,
    IBasicProperties properties,
    Dictionary<string, object> headersArguments = null,
    Guid? eventId = null)
    {
        using (var channel = ConnectionPool.Get(ZeroRabbitMqEventBusOptions.ConnectionName).CreateModel())
        {
            return PublishAsync(channel, eventName, body, properties, headersArguments, eventId);
        }
    }

    /// <summary>
    /// 发布消息 默认消息持久化
    /// </summary>
    /// <param name="channel"></param>
    /// <param name="eventName"></param>
    /// <param name="body"></param>
    /// <param name="properties"></param>
    /// <param name="headersArguments"></param>
    /// <param name="eventId"></param>
    /// <returns></returns>
    protected virtual Task PublishAsync(
    IModel channel,
    string eventName,
    byte[] body,
    IBasicProperties properties,
    Dictionary<string, object> headersArguments = null,
    Guid? eventId = null)
    {
        EnsureExchangeExists(channel);

        if (properties == null)
        {
            properties = channel.CreateBasicProperties();
            properties.DeliveryMode = RabbitMqConsts.DeliveryModes.Persistent;
        }

        if (string.IsNullOrEmpty(properties.MessageId))
        {
            properties.MessageId = (eventId ?? Guid.NewGuid()).ToString("N");
        }

        SetEventMessageHeaders(properties, headersArguments);

        channel.BasicPublish(
            exchange: ZeroRabbitMqEventBusOptions.ExchangeName,
            routingKey: eventName,
            mandatory: true,
            basicProperties: properties,
            body: body
        );

        return Task.CompletedTask;
    }

    /// <summary>
    /// 确保交换器存在
    /// </summary>
    /// <param name="channel"></param>
    private void EnsureExchangeExists(IModel channel)
    {
        if (_exchangeCreated)
        {
            return;
        }

        try
        {
            channel.ExchangeDeclarePassive(ZeroRabbitMqEventBusOptions.ExchangeName);
        }
        catch (Exception)
        {
            channel.ExchangeDeclare(
                ZeroRabbitMqEventBusOptions.ExchangeName,
                ZeroRabbitMqEventBusOptions.GetExchangeTypeOrDefault(),
                durable: true
            );
        }
        _exchangeCreated = true;
    }

    /// <summary>
    /// 设置事件消息头
    /// </summary>
    /// <param name="properties"></param>
    /// <param name="headersArguments"></param>
    private void SetEventMessageHeaders(IBasicProperties properties, Dictionary<string, object> headersArguments)
    {
        if (headersArguments == null)
        {
            return;
        }

        properties.Headers ??= new Dictionary<string, object>();

        foreach (var header in headersArguments)
        {
            properties.Headers[header.Key] = header.Value;
        }
    }

}
