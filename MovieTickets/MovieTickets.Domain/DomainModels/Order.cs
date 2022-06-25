using MovieTickets.Domain.Identity;
using MovieTickets.Domain.Relations;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MovieTickets.Domain.DomainModels
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public virtual ICollection<MovieInOrder> MovieInOrders { get; set; }
    }
}
