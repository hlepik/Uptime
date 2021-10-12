using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class PlanetRepository : BaseRepository<Planet, Domain.App.Planet, AppDbContext>,
        IPlanetRepository
    {

        public PlanetRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new PlanetMapper(mapper))
        {
        }

        public List<Planet> GetAllPlanets(Route routeObject)
        {
            var planetNames = new List<Planet>();
            foreach (var planet in routeObject.Legs!)
            {
                if (planetNames.Find(p => p.Name == planet.RouteInfo!.From!.Name) == null)
                {
                    planetNames.Add(new Planet(planet.RouteInfo!.From!.Id, planet.RouteInfo.From.Name));
                }
                if (planetNames.Find(p => p.Name == planet.RouteInfo!.To!.Name) == null)
                {
                    planetNames.Add(new Planet(planet.RouteInfo!.To!.Id, planet.RouteInfo.To.Name));
                }

            }

            return planetNames.OrderBy(x => x.Name).ToList();
        }

    }
}