using System;
using Domain.Base;


namespace DAL.App.DTO
{
    public class Provider: DomainEntityId
    {
        public string CompanyName { get; set; } = default!;
        public TimeSpan TravelTime { get; set; }
        public Company? Company { get; set; }
        public Int64 Distance { get; set; }
        public string From { get; set; } = default!;
        public string To { get; set; } = default!;
        public double Price { get; set; }
        public string?  FlightStartString { get; set; }
        public string? FlightEndString { get; set; }
        public DateTime FlightStart { get; set; }
        public DateTime FlightEnd { get; set; }
    }
}