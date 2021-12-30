using Base.BLL.Interfaces.Services;
using WebApp.DAL.Interfaces.Repositories;

namespace WebApp.BLL.Interfaces.Services;

public interface ITransactionService : IBaseService<WebApp.BLL.DTO.Transaction, WebApp.DAL.DTO.Transaction>, ITransactionRepositoryCustom<WebApp.BLL.DTO.Transaction>
{
        
}