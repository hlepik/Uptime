using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class CompanyRepository : BaseRepository<Company, Domain.App.Company, AppDbContext>,
        ICompanyRepository
    {

        public CompanyRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new CompanyMapper(mapper))
        {
        }

        public List<Company> GetAllCompanies(Route routeObject)
        {
            var companyNames = new List<Company>();
            foreach (var provider in routeObject!.Legs!.SelectMany(each => each.Providers
                .Where(provider => companyNames
                    .Find(p => p.Name == provider.Company!.Name) == null)))
            {
                companyNames.Add(new Company(provider.Company!.Id, provider.Company.Name));
            }

            return companyNames.OrderBy(x => x.Name).ToList();
        }

        public List<string> GetAllCompanyNames(Leg leg)
        {
            List<string> companies = new List<string>();

            foreach (var each in leg.Providers)
            {
                if (!companies.Contains(each.CompanyName))
                {
                    companies.Add(each.CompanyName);
                }

            }
            return companies;
        }

    }
}