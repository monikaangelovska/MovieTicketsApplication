using Microsoft.EntityFrameworkCore;
using MovieTickets.Domain.Identity;
using MovieTickets.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieTickets.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<User> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<User>();
        }

        public IEnumerable<User> GetAll()
        {
            return entities.AsEnumerable();
        }

        public User Get(string id)
        {
            return entities
            .Include(z => z.UserCart)
            .Include("UserCart.MovieInshoppingCarts")
            .Include("UserCart.MovieInshoppingCarts.CurrentMovie")
            .SingleOrDefault(s => s.Id == id);
        }
        public void Insert(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}
