using MovieTickets.Domain.Relations;
using System.Collections.Generic;

namespace MovieTickets.Domain.DTO
{
    public class ShoppingCartDTO
    {
        public List<MovieInshoppingCart> Movies { get; set; }

        public double TotalPrice { get; set; }

    }
}
