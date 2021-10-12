using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Newtonsoft.Json;

namespace DAL.App.EF.Repositories
{
    public class LegRepository : BaseRepository<Leg, Domain.App.Leg, AppDbContext>,
        ILegRepository
    {

        public LegRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new LegMapper(mapper))
        {
        }

        public List<Leg> GetAllRoutes(Route routeObject, string from, string to, string companyName, DateTime date, double price, int shortBy)
        {
            var allRoute = new List<Leg>();
            List<Leg> result = new List<Leg>();
            var counter = 0;
            var searchPlanet = from;
            var route = new Leg();

            do
            {

                var onlyDestinationObj = routeObject.Legs!.Where(x => x.RouteInfo!.From!.Name == searchPlanet
                                                                      && !route.AllStops!.ContainsKey(x.RouteInfo.To!
                                                                          .Name)
                                                                      && x.RouteInfo!.To!.Name != route.VisitedRouteTo)
                    .ToList();
                var routeCount = onlyDestinationObj.Count;
                if (routeCount == 0)
                {
                    result.Remove(route);
                }

                foreach (var each in onlyDestinationObj)
                {

                    if (routeCount > 1 && counter > 0)
                    {
                        route.VisitedRouteFrom = each.RouteInfo!.From!.Name;
                        route.VisitedRouteTo = each.RouteInfo!.To!.Name;
                        route.ValidUntil = routeObject.ValidUntil;
                        result.Add(Clone(route)!);
                    }

                    each.RouteInfo!.RoadCount++;
                    var routeInfo = new RouteInfo();
                    route.Id = each.Id;
                    routeInfo.Distance = each.RouteInfo!.Distance;
                    route.From = each.RouteInfo.From!.Name;
                    route.To = each.RouteInfo.To!.Name;
                    route.AllStops!.Add(route.From, route.To);
                    route.VisitedRouteFrom = "";
                    route.VisitedRouteTo = "";

                    var providers = companyName != null ? each.Providers
                        .Where(x => x.Company!.Name == companyName).ToList() : each.Providers;

                    foreach (var allProviders in providers)
                    {

                        var provider = new Provider();
                        provider.Id = allProviders.Id;
                        provider.From = each.RouteInfo.From!.Name;
                        provider.To = each.RouteInfo.To!.Name;
                        provider.Price = allProviders.Price;
                        provider.FlightEnd = allProviders.FlightEnd;
                        provider.FlightStart = allProviders.FlightStart;
                        provider.TravelTime = allProviders.FlightEnd - allProviders.FlightStart;
                        provider.FlightEndString = allProviders.FlightEnd.ToString("dd/MM/yyyy HH:mm:ss");
                        provider.FlightStartString = allProviders.FlightStart.ToString("dd/MM/yyyy HH:mm:ss");
                        provider.CompanyName = allProviders.Company!.Name;
                        provider.Distance = each.RouteInfo.Distance;
                        route.Providers.Add(provider);
                    }

                    route.Providers = route.Providers.OrderBy(x => x.FlightStart).ToList();

                    if (!route.IsChecked)
                    {
                        if (route.To == to)
                        {
                            allRoute.Add(route);
                        }
                        else
                        {
                            result.Add(route);
                        }
                        route = new Leg();
                    }


                    if (counter != 0)
                    {
                        break;
                    }
                }

                if (route.To == to)
                {
                    allRoute.Add(route);
                    result.Remove(route);
                }


                if (result.Count == 0) continue;
                searchPlanet = result[0].To;
                result[0].From = result[0].To;
                route = result[0];
                route.IsChecked = true;
                counter++;
            } while (result.Count != 0 );


            return GetAllRouteTogether(allRoute, date, shortBy, price);
        }
        public T? Clone<T>(T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }

        public List<Leg> GetAllRouteTogether(List<Leg> routes, DateTime date, int shortBy, double totalPrice)
        {

            List<Leg> allRoutes = new List<Leg>();

            foreach (var each in routes)
            {
                var count = 0;
                var routeStops = each.AllStops!.Keys.Count;
                var keyFrom = each.AllStops.Keys.First();
                var startTime = date;
                var allProviders = each.Providers.Where(x => x.From == keyFrom && x.FlightStart >= startTime).ToList();


                foreach (var provider in allProviders)
                {
                    double price = 0;
                    Int64 distance = 0;
                    var timeTogether = new TimeSpan();
                    var providerGroup = new List<Provider>();
                    var nextStop = provider;
                    Leg leg = new Leg();

                    while (count != routeStops)
                    {
                        startTime = nextStop!.FlightEnd;
                        price += nextStop.Price;
                        distance += nextStop.Distance;
                        timeTogether += nextStop.TravelTime;
                        count++;
                        providerGroup.Add(nextStop);

                        if (count == routeStops)
                        {
                            leg.Id = Guid.NewGuid();
                            leg.AllStops = each.AllStops;
                            leg.Providers = providerGroup;
                            leg.PriceTogether = Math.Round(price, 2);
                            leg.DistanceTogether = distance;
                            leg.TimeTogether = timeTogether;
                            leg.ValidUntil = each.ValidUntil;
                            leg.Companies = GetAllCompanies(providerGroup);
                            leg.Route = each.AllStops.Keys.ToList();
                            leg.Route.Add(each.AllStops.Values.Last());

                            allRoutes.Add(leg);

                        }else
                        {
                            keyFrom = each.AllStops.ElementAt(count).Key;
                            nextStop = each.Providers.FirstOrDefault(x => x.From == keyFrom && x.FlightStart > startTime);
                            if (nextStop == null)
                            {
                                count = routeStops;
                            }
                        }
                    }

                    count = 0;
                }
            }
            if (totalPrice > 0)
            {
                allRoutes = allRoutes.Where(x => x.PriceTogether <= totalPrice).ToList();
            }

            if (shortBy != 0)
            {
                allRoutes = ShortRoutes(allRoutes, shortBy);
            }

            return allRoutes;

        }

        public List<Leg> ShortRoutes(List<Leg> allRoutes, int shortBy)
        {
            List<Leg> routes;

            switch (shortBy)
            {
                case 1:
                    routes = allRoutes.OrderBy(x => x.PriceTogether).ToList();
                    break;
                case 2:
                    routes = allRoutes.OrderBy(x => x.TimeTogether).ToList();
                    break;
                default:
                    routes = allRoutes.OrderBy(x => x.DistanceTogether).ToList();
                    break;
            }

            return routes;

        }

        public Leg? GetLegById(Guid? id, IEnumerable<Leg>? legsObject)
        {
            var leg = legsObject!.FirstOrDefault(x => x.Id == id);

            return leg;

        }

        public List<string> GetAllCompanies(List<Provider> providers)
        {

            List<string> companies = new List<string>();

            foreach (var each in providers.Where(each => !companies.Contains(each.CompanyName)))
            {
                companies.Add(each.CompanyName);
            }

            return companies;
        }



    }


}