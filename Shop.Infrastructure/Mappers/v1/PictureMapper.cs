using AutoMapper;
using Base.Infrastructure.Mappers;
using Shop.Application.Models.v1;

namespace Shop.Infrastructure.Mappers.v1;

public class PictureMapper : BaseMapper<Picture, Domain.Models.Picture>
{
    public PictureMapper(IMapper mapper) : base(mapper)
    {
    }
}