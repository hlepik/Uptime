using AutoMapper;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;
using DAL.Base.EF;

namespace DAL.App.EF
{
    public class AppUnitOfWork : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        protected IMapper Mapper;
        public AppUnitOfWork(AppDbContext uowDbContext, IMapper mapper) : base(uowDbContext)
        {
            Mapper = mapper;
        }

        public ICompanyRepository Company => GetRepository(() => new CompanyRepository(UowDbContext, Mapper));
        public ILegRepository Leg => GetRepository(() => new LegRepository(UowDbContext, Mapper));
        public IPlanetRepository Planet => GetRepository(() => new PlanetRepository(UowDbContext, Mapper));
        public IReservationRepository Reservation => GetRepository(() => new ReservationRepository(UowDbContext, Mapper));
        public IRouteRepository Route => GetRepository(() => new RouteRepository(UowDbContext, Mapper));


    }
}