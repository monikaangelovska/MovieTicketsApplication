using Microsoft.EntityFrameworkCore;
using MovieTickets.Domain.DomainModels;
using MovieTickets.Repository.Interface;
using System.Collections.Generic;

namespace MovieTickets.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Order> entities;
        string errorMessage = string.Empty;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Order>();
        }
        public List<Order> GetAllOrders()
        {
            return entities
                .Include(z => z.MovieInOrders)
                .Include(z => z.User)
                .Include("MovieInOrders.Movie")
                .ToListAsync().Result;
        }

        public Order GetOrderDetails(BaseEntity model)
        {
            return entities
                .Include(z => z.MovieInOrders)
                .Include(z => z.User)
                .Include("MovieInOrders.Movie")
                .SingleOrDefaultAsync(z => z.Id == model.Id).Result;
        }
    }
}
