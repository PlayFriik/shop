namespace Base.Application.Mappers;

public interface IBaseMapper<TApplicationEntity, TDomainEntity>
{
    TApplicationEntity? Map(TDomainEntity? entity);
    TDomainEntity? Map(TApplicationEntity? entity);
}