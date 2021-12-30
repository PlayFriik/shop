using AutoMapper;
using Base.DAL.Interfaces.DTO.AutoMapper.Mappers;

namespace Base.DAL.EF.Mappers;

public class BaseMapper<TLeftEntity, TRightEntity> : IBaseMapper<TLeftEntity, TRightEntity>
{
    private readonly IMapper _mapper;
        
    public BaseMapper(IMapper mapper)
    {
        _mapper = mapper;
    }
        
    public virtual TLeftEntity? Map(TRightEntity? inObject)
    {
        return _mapper.Map<TLeftEntity>(inObject);
    }

    public virtual TRightEntity? Map(TLeftEntity? inObject)
    {
        return _mapper.Map<TRightEntity>(inObject);
    }
}