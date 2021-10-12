using System;
using System.Collections.Generic;
using Contracts.Domain.Base;
using Domain.Base;


namespace DAL.App.DTO
{
    public class Company : DomainEntityId
    {
        public Company()
        {
        }

        public Company(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public String Name { get; set; } = default!;
    }
}