using Microsoft.AspNetCore.Identity;
using MovieTickets.Domain.DomainModels;

namespace MovieTickets.Domain.Identity
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public virtual ShoppingCart UserCart { get; set; }
    }
}
