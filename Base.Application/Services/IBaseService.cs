using Base.Application.Repositories;

namespace Base.Application.Services;

public interface IBaseService<TApplicationEntity> : IBaseRepository<TApplicationEntity>
    where TApplicationEntity : class
{
}