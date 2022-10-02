using AutoMapper;
using Base.Application.Mappers;

namespace Base.Infrastructure.Mappers;

public class BaseMapper<TApplicationEntity, TDomainEntity> : IBaseMapper<TApplicationEntity, TDomainEntity>
{
    protected IMapper Mapper;

    public BaseMapper(IMapper mapper)
    {
        Mapper = mapper;
    }

    public TApplicationEntity Map(TDomainEntity? entity)
    {
        return Mapper.Map<TApplicationEntity>(entity);
    }

    public TDomainEntity Map(TApplicationEntity? entity)
    {
        return Mapper.Map<TDomainEntity>(entity);
    }
}