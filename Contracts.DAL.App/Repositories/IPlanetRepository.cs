using System.Collections.Generic;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPlanetRepository
    {
        List<Planet> GetAllPlanets(Route routeObject);
    }
}