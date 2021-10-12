using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace DAL.App.EF.Repositories
{
    public class ReservationRepository : BaseRepository<Reservation, Domain.App.Reservation, AppDbContext>,
        IReservationRepository
    {

        public ReservationRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new ReservationMapper(mapper))
        {
        }

        public async Task<List<Reservation>> GetAllReservationsAsync(DateTime validDate)
        {

            var query = CreateQuery();

            var resQuery = query
                .Select(p => new Reservation()
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Price = p.Price,
                    TravelTime = p.TravelTime,
                    CompanyNames = p.CompanyNames,
                    Routes = p.Routes,
                    ValidUntil = p.ValidUntil

                }).OrderBy(p => p.LastName);


            return await resQuery.ToListAsync();
        }

        public List<Reservation> GetAllByValidDate(List<Reservation> reservations, DateTime date)
        {
            List<Reservation> res = new List<Reservation>();
            foreach (var each in reservations)
            {
                if (each.ValidUntil.ToString("G") == date.ToString("G"))
                {

                    res.Add(each);
                }
            }

            return res;
        }


        public List<Reservation> GetLists(List<Reservation?> reservations)
        {
            List<Reservation> allReservations = new List<Reservation>();


            foreach (var each in reservations)
            {
                Reservation res = new Reservation();
                res.Companies = JsonConvert.DeserializeObject<List<string>>(each!.CompanyNames);
                res.Route = JsonConvert.DeserializeObject<List<string>>(each.Routes);
                res.FirstName = each.FirstName;
                res.LastName = each.LastName;
                res.TravelTime = each.TravelTime;
                res.Price = each.Price;
                 allReservations.Add(res);
            }


            return allReservations;
        }

    }
}