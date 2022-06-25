using MovieTickets.Domain.Relations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieTickets.Domain.DomainModels
{
    public class Movie : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Duration { get; set; }
        [Required]
        public DateTime PlayingDate { get; set; }
        [Required]
        public DateTime PlayingTime { get; set; }
        [Required]
        public double TicketPrice { get; set; }
        [Required]
        public double Rating { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public string Image { get; set; }
        public virtual ICollection<MovieInshoppingCart> MovieInshoppingCarts { get; set; }
        public virtual ICollection<MovieInOrder> MovieInOrders { get; set; }
    }
}