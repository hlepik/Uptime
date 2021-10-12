using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App;
using DAL.App.DTO;
using Microsoft.AspNetCore.Mvc;


namespace WebApp.Controllers
{
    public class Reservations : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public Reservations(IAppUnitOfWork uow)
        {

            _uow = uow;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var data = _uow.Route.GetRouteObject();
            var reservations = await _uow.Reservation.GetAllReservationsAsync(data!.ValidUntil);

            reservations = _uow.Reservation.GetAllByValidDate(reservations, data!.ValidUntil);

            var res = _uow.Reservation.GetLists(reservations!);

            return View(res);
        }

    }
}
