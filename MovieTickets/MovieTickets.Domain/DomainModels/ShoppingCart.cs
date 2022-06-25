using MovieTickets.Domain.Identity;
using MovieTickets.Domain.Relations;
using System;
using System.Collections.Generic;

namespace MovieTickets.Domain.DomainModels
{
    public class ShoppingCart : BaseEntity
    {
        public string OwnerId { get; set; }
        public virtual User Owner { get; set; }
        public virtual ICollection<MovieInshoppingCart> MovieInshoppingCarts { get; set; }

    }
}