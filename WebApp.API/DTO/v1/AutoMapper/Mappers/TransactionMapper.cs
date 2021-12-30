using AutoMapper;

namespace WebApp.API.DTO.v1.AutoMapper.Mappers;

public class TransactionMapper : BaseMapper<WebApp.API.DTO.v1.Transaction, WebApp.BLL.DTO.Transaction>
{
    public TransactionMapper(IMapper mapper) : base(mapper)
    {
            
    }
}