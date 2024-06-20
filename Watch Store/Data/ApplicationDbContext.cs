using Microsoft.EntityFrameworkCore;
using Watch_Store.Models.Entities;

namespace Watch_Store.Data
{
    public class ApplicationDbContext: DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public DbSet<Watch> Watches { get; set; }
    }
}
