using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GentsOutfits_Authentication_.Models;

namespace GentsOutfits_Authentication_.Data
{
    public class ApplicationDbContext : IdentityDbContext<MyUsers>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<GentsOutfits_Authentication_.Models.Product> Product { get; set; } = default!;
    }
}
