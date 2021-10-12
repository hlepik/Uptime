using System;
using System.Collections.Generic;
using System.Numerics;
using Contracts.Domain.Base;
using Domain.Base;


namespace DAL.App.DTO
{
    public class RouteInfo : DomainEntityId
    {
        public Planet? From { get; set; }
        public Planet? To { get; set; }
        public Int64 Distance { get; set; }
        public int RoadCount { get; set; }

    }
}