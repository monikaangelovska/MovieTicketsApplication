using MovieTickets.Domain.DomainModels;
using System;

namespace MovieTickets.Domain.DTO
{
    public class AddMovieToShoppingCartDTO
    {
        public Movie SelectedMovie { get; set; }

        public Guid SelectedMovieId { get; set; }

        public int AmountOfTickets { get; set; }

    }
}
