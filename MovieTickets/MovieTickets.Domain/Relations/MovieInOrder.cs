using MovieTickets.Domain.DomainModels;
using System;

namespace MovieTickets.Domain.Relations
{
    public class MovieInOrder : BaseEntity
    {
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public int Amount { get; set; }
    }
}
