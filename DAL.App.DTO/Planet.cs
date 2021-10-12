using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;
using Domain.Base;


namespace DAL.App.DTO
{
    public class Planet : DomainEntityId
    {

        public Planet()
        {
        }

        public Planet(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public String Name { get; set; } = default!;

    }
}