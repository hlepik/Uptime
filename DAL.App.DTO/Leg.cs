using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domain.Base;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Leg : DomainEntityId
    {

        public DateTime ValidUntil { get; set; }
        public Boolean IsChecked { get; set; }
        public RouteInfo? RouteInfo { get; set; }
        public string From { get; set; } = default!;
        public string To { get; set; } = default!;
        public string? VisitedRouteFrom { get; set; }
        [DisplayName("Total price")]
        public double PriceTogether { get; set; }
        [DisplayName("Total distance")]
        public Int64 DistanceTogether { get; set; }
        [DisplayName("Total time")]
        public TimeSpan TimeTogether { get; set; }
        public string? VisitedRouteTo { get; set; }
        public List<string> Route { get; set; } = new List<string>();
        public List<string> Companies { get; set; } = default!;
        [NotMapped]
        public Dictionary<string, string> AllStops { get; set; } = new Dictionary<string, string>();

        public List<Provider> Providers { get; set; } =  new List<Provider>();


    }
}