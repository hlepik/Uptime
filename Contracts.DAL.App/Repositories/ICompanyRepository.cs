using System.Collections.Generic;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ICompanyRepository :IBaseRepository<Company>, ICompanyRepositoryCustom<Company>
    {

    }
    public interface ICompanyRepositoryCustom<TEntity>
    {

        List<Company> GetAllCompanies(Route routeObject);
        List<string> GetAllCompanyNames(Leg leg);
    }
}