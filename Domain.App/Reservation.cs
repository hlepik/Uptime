using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;


namespace Domain.App
{
    public class Reservation : DomainEntityId
    {
        [MaxLength(256)] public string FirstName { get; set; } = default!;
        [MaxLength(256)] public string LastName { get; set; } = default!;
        public double Price { get; set; }
        public TimeSpan TravelTime { get; set; }
        public DateTime ValidUntil { get; set; }
        [MaxLength(5000)] public string CompanyNames { get; set; } = default!;
        [MaxLength(5000)] public string Routes { get; set; } = default!;


    }
}