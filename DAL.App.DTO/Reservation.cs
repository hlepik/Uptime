using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;
using Domain.Base;


namespace DAL.App.DTO
{
    public class Reservation : DomainEntityId
    {
        [DisplayName("First name")]
        public string FirstName { get; set; } = default!;
        [DisplayName("Last name")]
        public string LastName { get; set; } = default!;

        public List<string>? Companies { get; set; } = new List<string>();
        public List<string>? Route { get; set; } = new List<string>();
        public double Price { get; set; }
        [DisplayName("Travel time")]
        public TimeSpan TravelTime { get; set; }

        public DateTime ValidUntil { get; set; }
        [DisplayName("Company name(s)")]
        public string CompanyNames { get; set; } = default!;
        [DisplayName("Route(s)")]
        public string Routes { get; set; } = default!;

    }
}