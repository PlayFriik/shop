using AutoMapper;
using Base.BLL.Mappers;

namespace WebApp.BLL.DTO.AutoMapper.Mappers;

public class TransactionMapper : BaseMapper<WebApp.BLL.DTO.Transaction, WebApp.DAL.DTO.Transaction>
{
    public TransactionMapper(IMapper mapper) : base(mapper)
    {
            
    }
}