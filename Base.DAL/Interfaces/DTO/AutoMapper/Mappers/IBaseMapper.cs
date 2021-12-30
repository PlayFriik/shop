namespace Base.DAL.Interfaces.DTO.AutoMapper.Mappers;

public interface IBaseMapper<TLeftObject, TRightObject>
{
    TLeftObject? Map(TRightObject? inObject);
    TRightObject? Map(TLeftObject? inObject);
}