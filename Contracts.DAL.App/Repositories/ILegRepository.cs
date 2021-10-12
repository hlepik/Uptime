using System;
using System.Collections.Generic;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ILegRepository :IBaseRepository<Leg>, ILegRepositoryCustom<Leg>
    {

    }
    public interface ILegRepositoryCustom<TEntity>
    {
        T? Clone<T>(T source);
        Leg? GetLegById(Guid? id, IEnumerable<Leg>? legsObject);
        List<Leg> GetAllRoutes(Route routeObject, string from, string to, string companyName, DateTime date, double price, int shortBy);
    }
}