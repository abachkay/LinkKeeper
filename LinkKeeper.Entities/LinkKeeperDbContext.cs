using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace LinkKeeper.Entities
{
    public class LinkKeeperDbContext : IdentityDbContext<ApplicationUser>
    {   
        public LinkKeeperDbContext() : base("DefaultConnection", throwIfV1Schema: false) { }
        public static LinkKeeperDbContext Create()
        {
            return new LinkKeeperDbContext();
        }
        public DbSet<Link> Links { get; set; }        
    }
}
