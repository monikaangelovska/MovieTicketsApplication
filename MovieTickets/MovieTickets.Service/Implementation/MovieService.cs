using Microsoft.Extensions.Logging;
using MovieTickets.Domain.DomainModels;
using MovieTickets.Domain.DTO;
using MovieTickets.Domain.Relations;
using MovieTickets.Repository.Interface;
using MovieTickets.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieTickets.Service.Implementation
{
    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _movieRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<MovieInshoppingCart> _movieInshoppingCartRepository;
        private readonly ILogger<MovieService> _logger;

        public MovieService(IRepository<Movie> movieRepository, ILogger<MovieService> logger, IUserRepository userRepository, IRepository<MovieInshoppingCart> movieInshoppingCartRepository)
        {
            _movieRepository = movieRepository;
            _userRepository = userRepository;
            _movieInshoppingCartRepository = movieInshoppingCartRepository;
            _logger = logger;
        }
        public bool AddMovieToShoppingCart(AddMovieToShoppingCartDTO item, string userID)
        {
            var user = this._userRepository.Get(userID);

            var userShoppingCart = user.UserCart;

            if (item.SelectedMovieId != null && userShoppingCart != null)
            {
                var movie = this.GetDetailsForMovie(item.SelectedMovieId);

                if (movie != null)
                {
                    MovieInshoppingCart itemToAdd = new MovieInshoppingCart
                    {
                        Id = Guid.NewGuid(),
                        CurrentMovie = movie,
                        MovieId = movie.Id,
                        UserCart = userShoppingCart,
                        ShoppingCartId = userShoppingCart.Id,
                        Amount = item.AmountOfTickets
                    };

                    this._movieInshoppingCartRepository.Insert(itemToAdd);
                    _logger.LogInformation("Movie was succesfully added to ShoppingCart!");
                    return true;
                }
                return false;
            }
            _logger.LogInformation("Something was wrong! MovieId or UserShoppingCart are unavailable.");
            return false;
        }
        public void CreateNewMovie(Movie m)
        {
            this._movieRepository.Insert(m);
        }
        public void DeleteMovie(Guid id)
        {
            var movie = this.GetDetailsForMovie(id);
            this._movieRepository.Delete(movie);
        }
        public List<Movie> GetAllMovies()
        {
            _logger.LogInformation("GetAllMovies was called!");
            return this._movieRepository.GetAll().ToList();
        }
        public Movie GetDetailsForMovie(Guid? id)
        {
            return this._movieRepository.Get(id);
        }
        public AddMovieToShoppingCartDTO GetShoppingCartInfo(Guid? id)
        {
            var movie = this.GetDetailsForMovie(id);

            AddMovieToShoppingCartDTO model = new AddMovieToShoppingCartDTO
            {
                SelectedMovie = movie,
                SelectedMovieId = movie.Id,
                AmountOfTickets = 1
            };

            return model;
        }
        public void UpdeteExistingMovie(Movie m)
        {
            this._movieRepository.Update(m);
        }
    }
}
