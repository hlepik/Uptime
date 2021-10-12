using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using DAL.App.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAppUnitOfWork _uow;
        private string SessionLeg = "";

        public HomeController(ILogger<HomeController> logger, IAppUnitOfWork uow)
        {
            _logger = logger;
            _uow = uow;

        }

        public  ViewResult Index()
        {
            var routeObject = _uow.Route.GetRouteObject();
            var companyNames = _uow.Company.GetAllCompanies(routeObject!);
            var planetNames = _uow.Planet.GetAllPlanets(routeObject!);

            var vm = new RouteViewModels();
            vm.PlanetSelectList = new SelectList(planetNames, nameof(Planet.Name), nameof(Planet.Name));
            vm.CompanySelectList = new SelectList(companyNames, nameof(Company.Name), nameof(Company.Name));

            vm.Legs = new List<Leg>();
            return View(vm);
        }

        // POST: Home/Find
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Find(RouteViewModels vm)
        {
            var routeObject = _uow.Route.GetRouteObject();
            vm.PlanetSelectList = new SelectList(_uow.Planet.GetAllPlanets(routeObject!), nameof(Planet.Name), nameof(Planet.Name));
            vm.CompanySelectList = new SelectList(_uow.Company.GetAllCompanies(routeObject!), nameof(Company.Name), nameof(Company.Name));
            if (ModelState.IsValid && vm.From != vm.To)
            {
                vm.Legs = _uow.Leg.GetAllRoutes(routeObject!, vm.From, vm.To, vm.CompanyName!,
                vm.ShortByDate, vm.ShortByPrice, vm.ShortBy).Take(15);
                vm.RouteInfo = JsonConvert.SerializeObject(vm.Legs);
                if (vm.Legs.Count() == 0)
                {
                    vm.SessionMessage = "No routes were found!";
                }

                HttpContext.Session.SetString(SessionLeg, JsonConvert.SerializeObject(vm.Legs));
                return View("~/Views/Home/Index.cshtml", vm);
            }

            if (vm.From == vm.To)
            {
                vm.ErrorMessage = "Starting point and destination cannot be same!";
            }

            vm.Legs = new List<Leg>();
            return View("~/Views/Home/Index.cshtml", vm);

        }


        // GET: Home/Reserve/5
        public IActionResult Reserve(Guid? id)
        {
            var session = HttpContext.Session.GetString(SessionLeg);

            if (session == null)
            {
                var companyNames = _uow.Company.GetAllCompanies(_uow.Route.GetRouteObject()!);
                var planetNames = _uow.Planet.GetAllPlanets(_uow.Route.GetRouteObject()!);

                var vm = new RouteViewModels();
                vm.PlanetSelectList = new SelectList(planetNames, nameof(Planet.Name), nameof(Planet.Name));
                vm.CompanySelectList = new SelectList(companyNames, nameof(Company.Name), nameof(Company.Name));

                vm.Legs = new List<Leg>();
                vm.SessionMessage = "Session expired!";
                return View("~/Views/Home/Index.cshtml", vm);
            }

            var data = JsonConvert.DeserializeObject<IEnumerable<Leg>>(session);


            var leg = _uow.Leg.GetLegById(id, data);
            ReservationViewModels vmReservation = new ReservationViewModels();
            vmReservation.Price = leg!.PriceTogether;
            vmReservation.Companies = leg.Companies;
            vmReservation.Id = leg.Id;
            var destination = leg.AllStops!.Values.Last();
            leg.AllStops.Add(destination, "");
            vmReservation.Route = leg.Route;
            vmReservation.ValidUntil = leg.ValidUntil;
            vmReservation.TotalTravelTime = leg.TimeTogether;

            return View(vmReservation);
        }

        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reserve(ReservationViewModels vm)
        {

            var session = HttpContext.Session.GetString(SessionLeg);
            var data = JsonConvert.DeserializeObject<IEnumerable<Leg>>(session);

            var leg = _uow.Leg.GetLegById(vm.Id, data);

            if (ModelState.IsValid)
            {
                var reservation = new Reservation()
                {
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    ValidUntil = vm.ValidUntil,
                    Price = vm.Price,
                    TravelTime = vm.TotalTravelTime,
                    CompanyNames = JsonConvert.SerializeObject(leg!.Companies),
                    Routes = JsonConvert.SerializeObject(leg.Route)

                };
                _uow.Reservation.Add(reservation);
                await _uow.SaveChangesAsync();
                var routeObject = _uow.Route.GetRouteObject();
                var companyNames = _uow.Company.GetAllCompanies(routeObject!);
                var planetNames = _uow.Planet.GetAllPlanets(routeObject!);

                var routeVm = new RouteViewModels();
                routeVm.PlanetSelectList = new SelectList(planetNames, nameof(Planet.Name),nameof(Planet.Name));
                routeVm.CompanySelectList = new SelectList(companyNames, nameof(Company.Name),nameof(Company.Name));

                routeVm.Legs = new List<Leg>();
                routeVm.MessageMain = "Reservation is confirmed!";

                return View("~/Views/Home/Index.cshtml", routeVm);
            }

            return View(vm);


        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }


    }
}