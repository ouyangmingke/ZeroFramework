namespace Zero.RabbitMQ.Domain
{
    public class ZeroRabbitMqOptions
    {
        public RabbitMqConnections Connections { get; }

        public ZeroRabbitMqOptions()
        {
            Connections = new RabbitMqConnections();
        }
    }
}