using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Cybertek.Apis.Common.Mappers;

public class CustomMapper : IMapper
{
    private readonly IServiceProvider _serviceProvider;
    public IConfigurationProvider ConfigurationProvider => throw new NotImplementedException();

    public CustomMapper(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TDestination Map<TDestination>(object source)
    {
        return Convert<TDestination>(source);
    }

    public TDestination Map<TSource, TDestination>(TSource source)
    {
        throw new NotImplementedException();
    }

    public TDestination Map<TSource, TDestination>(TSource source,
        TDestination destination)
    {
        throw new NotImplementedException();
    }

    public object Map(object source,
        Type sourceType,
        Type destinationType)
    {
        throw new NotImplementedException();
    }

    public object Map(object source,
        object destination,
        Type sourceType,
        Type destinationType)
    {
        throw new NotImplementedException();
    }

    public TDestination Map<TDestination>(object source,
        Action<IMappingOperationOptions<object, TDestination>> opts)
    {
        throw new NotImplementedException();
    }

    public TDestination Map<TSource, TDestination>(TSource source,
        Action<IMappingOperationOptions<TSource, TDestination>> opts)
    {
        throw new NotImplementedException();
    }

    public TDestination Map<TSource, TDestination>(TSource source,
        TDestination destination,
        Action<IMappingOperationOptions<TSource, TDestination>> opts)
    {
        throw new NotImplementedException();
    }

    public object Map(object source,
        Type sourceType,
        Type destinationType,
        Action<IMappingOperationOptions<object, object>> opts)
    {
        throw new NotImplementedException();
    }

    public object Map(object source,
        object destination,
        Type sourceType,
        Type destinationType,
        Action<IMappingOperationOptions<object, object>> opts)
    {
        throw new NotImplementedException();
    }

    public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source,
        object parameters = null,
        params Expression<Func<TDestination, object>>[] membersToExpand)
    {
        throw new NotImplementedException();
    }

    public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source,
        IDictionary<string, object> parameters,
        params string[] membersToExpand)
    {
        throw new NotImplementedException();
    }

    public IQueryable ProjectTo(IQueryable source,
        Type destinationType,
        IDictionary<string, object> parameters = null,
        params string[] membersToExpand)
    {
        throw new NotImplementedException();
    }

    private TDestination Convert<TDestination>(object source)
    {
        return (TDestination) _serviceProvider.GetService<IDomainContractConverter>().Convert(typeof(TDestination), source);
    }
}
