using MovieTickets.Domain.DomainModels;
using MovieTickets.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTickets.Service.Interface
{
    public interface IMovieService
    {
        List<Movie> GetAllMovies();
        Movie GetDetailsForMovie(Guid? id);
        void CreateNewMovie(Movie p);
        void UpdeteExistingMovie(Movie p);
        AddMovieToShoppingCartDTO GetShoppingCartInfo(Guid? id);
        void DeleteMovie(Guid id);
        bool AddMovieToShoppingCart(AddMovieToShoppingCartDTO item, string userID);
    }
}
