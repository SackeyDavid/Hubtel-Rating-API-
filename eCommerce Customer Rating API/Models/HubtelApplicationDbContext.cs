using Microsoft.EntityFrameworkCore;

namespace eCommerce_Customer_Rating_API.Models
{
    public class HubtelApplicationDbContext:DbContext
    {
        public HubtelApplicationDbContext(DbContextOptions<HubtelApplicationDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Auth>? Auth { get; set; }
        public virtual DbSet<Ratings>? Ratings { get; set; }
    }
}
