using System;
using Base.DAL.Interfaces.Repositories;
using Base.Domain.Interfaces;

namespace Base.BLL.Interfaces.Services;

public interface IBaseService<TBLLEntity, TDALEntity> : IBaseService<TBLLEntity, TDALEntity, Guid>
    where TBLLEntity : class, IDomainEntityId
    where TDALEntity : class, IDomainEntityId
{
        
}
    
public interface IBaseService<TBLLEntity, TDALEntity, TKey> : IBaseRepository<TBLLEntity, TKey>
    where TBLLEntity : class, IDomainEntityId<TKey> 
    where TDALEntity : class, IDomainEntityId<TKey> 
    where TKey : IEquatable<TKey>
{
        
}