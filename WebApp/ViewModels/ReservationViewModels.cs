using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WebApp.ViewModels
{
    public class ReservationViewModels
    {
        public Guid Id { get; set; }
        [DisplayName("First name")]
        public string FirstName { get; set; } = default!;
        [DisplayName("Last name")]
        public string LastName { get; set; } = default!;
        [DisplayName("Route(s)")]
        public List<string>? Route { get; set; }
        public DateTime ValidUntil { get; set; }
        public double Price { get; set; }
        [DisplayName("Travel time")]
        public TimeSpan TotalTravelTime { get; set; }
        [DisplayName("Company name(s)")]
        public List<string>? Companies { get; set; }


    }
}