using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IReservationRepository :IBaseRepository<Reservation>, IReservationRepositoryCustom<Reservation>
    {

    }
    public interface IReservationRepositoryCustom<TEntity>
    {

        Task<List<Reservation>> GetAllReservationsAsync(DateTime validDate);
        List<Reservation> GetLists(List<Reservation?> reservations);
        List<Reservation> GetAllByValidDate(List<Reservation> reservations, DateTime date);
    }
}