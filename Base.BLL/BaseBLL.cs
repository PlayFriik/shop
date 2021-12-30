using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.BLL.Contracts;
using Base.DAL.Interfaces;

namespace Base.BLL
{
    public class BaseBLL<TUnitOfWork> : IBaseBLL
        where TUnitOfWork : IBaseUnitOfWork
    {
        protected readonly TUnitOfWork BaseUnitOfWork;

        public BaseBLL(TUnitOfWork baseUnitOfWork)
        {
            BaseUnitOfWork = baseUnitOfWork;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await BaseUnitOfWork.SaveChangesAsync();
        }

        private readonly Dictionary<Type, object> _cache = new();
        public TService GetService<TService>(Func<TService> instantiation)
            where TService : class
        {
            if (_cache.TryGetValue(typeof(TService), out var result))
            {
                return (TService) result;
            }

            var service = instantiation();
            _cache.Add(typeof(TService), service);
            
            return service;
        }
    }
}