using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DAL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class RouteViewModels
    {
        public string From { get; set; } = default!;
        public string To { get; set; } = default!;
        public Provider? Providers { get; set; }
        public string? RouteInfo { get; set; }
        public IEnumerable<Leg>? Legs { get; set; }
        public string? ErrorMessage { get; set; }

        [Display(Name = "Maximum price", Prompt = "Maximum price")]
        public string? MessageMain { get; set; }
        public string? SessionMessage { get; set; }
        [DisplayName("Maximum price")]
        public double ShortByPrice { get; set; }
        [DisplayName("Short by")]
        public int ShortBy { get; set; }

        [DisplayName("Start date")]
        [DataType(DataType.Date)]
        public DateTime ShortByDate { get; set; } = DateTime.Now;

        [DisplayName("Company")]
        public string? CompanyName { get; set; }
        public SelectList? CompanySelectList { get; set; }
        public SelectList? PlanetSelectList { get; set; }

    }
}