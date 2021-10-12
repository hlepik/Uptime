using System.Net;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Newtonsoft.Json;

namespace DAL.App.EF.Repositories
{
    public class RouteRepository : BaseRepository<Route, Domain.App.Route, AppDbContext>,
        IRouteRepository
    {

        public RouteRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new RouteMapper(mapper))
        {
        }

        public Route? GetRouteObject()
        {
            var json = new WebClient().DownloadString(
                "https://cosmos-odyssey.azurewebsites.net/api/v1.0/TravelPrices");
            var routeObject = JsonConvert.DeserializeObject<DAL.App.DTO.Route>(json);

            return routeObject;

        }

    }
}