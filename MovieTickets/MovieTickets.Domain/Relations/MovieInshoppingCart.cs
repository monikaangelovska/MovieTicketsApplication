using MovieTickets.Domain.DomainModels;
using System;

namespace MovieTickets.Domain.Relations
{
    public class MovieInshoppingCart : BaseEntity
    {
        public Guid MovieId { get; set; }
        public virtual Movie CurrentMovie { get; set; }
        public Guid ShoppingCartId { get; set; }
        public virtual ShoppingCart UserCart { get; set; }
        public int Amount { get; set; }
    }
}
