using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieTickets.Domain.DomainModels;
using MovieTickets.Domain.Identity;
using MovieTickets.Domain.Relations;
using System;

namespace MovieTickets.Repository
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<MovieInshoppingCart> MovieInshoppingCarts { get; set; }
        public virtual DbSet<EmailMessage> EmailMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Movie>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<ShoppingCart>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<MovieInshoppingCart>()
                .HasOne(z => z.CurrentMovie)
                .WithMany(z => z.MovieInshoppingCarts)
                .HasForeignKey(z => z.ShoppingCartId);

            builder.Entity<MovieInshoppingCart>()
                .HasOne(z => z.UserCart)
                .WithMany(z => z.MovieInshoppingCarts)
                .HasForeignKey(z => z.MovieId);

            builder.Entity<ShoppingCart>()
                .HasOne<User>(z => z.Owner)
                .WithOne(z => z.UserCart)
                .HasForeignKey<ShoppingCart>(z => z.OwnerId);

            builder.Entity<MovieInOrder>()
                .HasOne(z => z.Order)
                .WithMany(z => z.MovieInOrders)
                .HasForeignKey(z => z.MovieId);

            builder.Entity<MovieInOrder>()
                .HasOne(z => z.Movie)
                .WithMany(z => z.MovieInOrders)
                .HasForeignKey(z => z.OrderId);
        }
    }
}
