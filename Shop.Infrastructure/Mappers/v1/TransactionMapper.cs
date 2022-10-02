using AutoMapper;
using Base.Infrastructure.Mappers;
using Shop.Application.Models.v1;

namespace Shop.Infrastructure.Mappers.v1;

public class TransactionMapper : BaseMapper<Transaction, Domain.Models.Transaction>
{
    public TransactionMapper(IMapper mapper) : base(mapper)
    {
    }
}