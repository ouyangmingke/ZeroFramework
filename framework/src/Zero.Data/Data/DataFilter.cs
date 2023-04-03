using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

using Zero.Core.DependencyInjection;

namespace Zero.Data;

public class DataFilter : IDataFilter, ISingletonDependency
{
    private readonly ConcurrentDictionary<Type, object> _filters;

    private readonly IServiceProvider _serviceProvider;

    public DataFilter(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _filters = new ConcurrentDictionary<Type, object>();
    }

    public IDisposable Enable<TFilter>()
        where TFilter : class
    {
        return GetFilter<TFilter>().Enable();
    }

    public IDisposable Disable<TFilter>()
        where TFilter : class
    {
        return GetFilter<TFilter>().Disable();
    }

    public bool IsEnabled<TFilter>()
        where TFilter : class
    {
        return GetFilter<TFilter>().IsEnabled;
    }

    private IDataFilter<TFilter> GetFilter<TFilter>()
        where TFilter : class
    {
        return _filters.GetOrAdd(
            typeof(TFilter),
             factory: () => _serviceProvider.GetRequiredService<IDataFilter<TFilter>>()
        ) as IDataFilter<TFilter>;
    }
}

public class DataFilter<TFilter> : IDataFilter<TFilter>
    where TFilter : class
{
    public bool IsEnabled => throw new NotImplementedException();

    public IDisposable Disable()
    {
        throw new NotImplementedException();
    }

    public IDisposable Enable()
    {
        throw new NotImplementedException();
    }
}
