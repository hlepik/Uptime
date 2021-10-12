using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class PlanetMapper : BaseMapper<DAL.App.DTO.Planet, Domain.App.Planet>,
        IBaseMapper<DAL.App.DTO.Planet, Domain.App.Planet>
    {
        public PlanetMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}