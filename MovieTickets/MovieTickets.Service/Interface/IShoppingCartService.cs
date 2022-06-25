using MovieTickets.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTickets.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDTO getShoppingCartInfo(string userId);
        bool deleteMovieFromShoppingCart(string userId, Guid id);

        bool orderNow(string userId);
    }
}
