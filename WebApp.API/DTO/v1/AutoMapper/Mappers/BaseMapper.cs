using AutoMapper;
using Base.BLL.Interfaces.DTO.AutoMapper.Mappers;

namespace WebApp.API.DTO.v1.AutoMapper.Mappers;

public class BaseMapper<TLeftEntity, TRightEntity> : IBaseMapper<TLeftEntity, TRightEntity>
{
    protected IMapper Mapper;
        
    public BaseMapper(IMapper mapper)
    {
        Mapper = mapper;
    }
        
    public TLeftEntity Map(TRightEntity? inObject)
    {
        return Mapper.Map<TLeftEntity>(inObject);
    }
        
    public TRightEntity Map(TLeftEntity? inObject)
    {
        return Mapper.Map<TRightEntity>(inObject);
    }
}