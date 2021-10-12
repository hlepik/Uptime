using System;
using System.Collections.Generic;
using Contracts.Domain.Base;
using Domain.Base;


namespace DAL.App.DTO
{
    public class Route : DomainEntityId
    {
        public DateTime ValidUntil { get; set; }
        public List<Leg>? Legs { get; set; }

    }
}