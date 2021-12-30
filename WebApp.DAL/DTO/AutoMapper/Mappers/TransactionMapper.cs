using AutoMapper;
using Base.DAL.EF.Mappers;

namespace WebApp.DAL.DTO.AutoMapper.Mappers;

public class TransactionMapper : BaseMapper<WebApp.DAL.DTO.Transaction, WebApp.Domain.Transaction>
{
    public TransactionMapper(IMapper mapper) : base(mapper)
    {
            
    }
}