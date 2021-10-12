using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class LegMapper : BaseMapper<DAL.App.DTO.Leg, Domain.App.Leg>,
        IBaseMapper<DAL.App.DTO.Leg, Domain.App.Leg>
    {
        public LegMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}