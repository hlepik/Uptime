using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork
    {
        ICompanyRepository Company { get; }
        ILegRepository Leg { get; }
        IPlanetRepository Planet { get; }
        IReservationRepository Reservation { get; }
        IRouteRepository Route { get; }

    }
}