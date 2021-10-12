using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class RouteMapper : BaseMapper<DAL.App.DTO.Route, Domain.App.Route>,
        IBaseMapper<DAL.App.DTO.Route, Domain.App.Route>
    {
        public RouteMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}