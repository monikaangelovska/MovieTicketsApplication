using MovieTickets.Domain.DomainModels;
using MovieTickets.Domain.DTO;
using MovieTickets.Domain.Relations;
using MovieTickets.Repository.Interface;
using MovieTickets.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieTickets.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<EmailMessage> _mailRepository;
        private readonly IRepository<MovieInOrder> _movieInOrderRepository;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IRepository<EmailMessage> mailRepository, IUserRepository userRepository, IRepository<Order> orderRepository, IRepository<MovieInOrder> movieInOrderRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _movieInOrderRepository = movieInOrderRepository;
            _mailRepository = mailRepository;
        }

        public bool deleteMovieFromShoppingCart(string userId, Guid id)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.Get(userId);

                var userCard = loggedInUser.UserCart;

                var itemToDelete = userCard.MovieInshoppingCarts.Where(z => z.MovieId.Equals(id)).FirstOrDefault();

                userCard.MovieInshoppingCarts.Remove(itemToDelete);

                this._shoppingCartRepository.Update(userCard);

                return true;
            }
            return false;
        }

        public ShoppingCartDTO getShoppingCartInfo(string userId)
        {
            var loggedInUser = this._userRepository.Get(userId);

            var userCard = loggedInUser.UserCart;

            var allMoviesTickets = userCard.MovieInshoppingCarts.ToList();

            var allMovieTicketsPrice = allMoviesTickets.Select(z => new
            {
                TicketPrice = z.CurrentMovie.TicketPrice,
                Amount = z.Amount
            }).ToList();

            double totalPrice = 0.0;

            foreach (var item in allMovieTicketsPrice)
            {
                totalPrice += item.Amount * item.TicketPrice;
            }

            ShoppingCartDTO result = new ShoppingCartDTO
            {
                Movies = allMoviesTickets,
                TotalPrice = totalPrice,
            };

            return result;
        }

        public bool orderNow(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.Get(userId);

                var userCard = loggedInUser.UserCart;

                EmailMessage message = new EmailMessage();
                message.MailTo = loggedInUser.Email;
                message.Subject = "Successfully created order";
                message.Status = false;

                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    User = loggedInUser,
                    UserId = userId
                };

                this._orderRepository.Insert(order);

                List<MovieInOrder> movieInOrders = new List<MovieInOrder>();

                var result = userCard.MovieInshoppingCarts.Select(z => new MovieInOrder
                {
                    Id = Guid.NewGuid(),
                    MovieId = z.CurrentMovie.Id,
                    Movie = z.CurrentMovie,
                    OrderId = order.Id,
                    Order = order,
                    Amount = z.Amount
                }).ToList();

                StringBuilder sb = new StringBuilder();

                sb.AppendLine("Your order is completed. The order contains: ");

                var totalPrice = 0.0;

                for (int i = 1; i <= result.Count(); i++)
                {
                    var item = result[i - 1];
                    totalPrice += item.Amount * item.Movie.TicketPrice;
                    sb.AppendLine(i.ToString() + ". " + item.Movie.Name + " with price of: " + item.Movie.TicketPrice + " and amount of: " + item.Amount);
                }

                sb.AppendLine("Total price: " + totalPrice.ToString());

                message.Content = sb.ToString();

                movieInOrders.AddRange(result);

                foreach (var element in movieInOrders)
                {
                    this._movieInOrderRepository.Insert(element);
                }

                loggedInUser.UserCart.MovieInshoppingCarts.Clear();

                this._userRepository.Update(loggedInUser);
                this._mailRepository.Insert(message);

                return true;

            }
            return false;
        }
    }
}
