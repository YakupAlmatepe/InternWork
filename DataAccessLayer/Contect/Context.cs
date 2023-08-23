using Microsoft.EntityFrameworkCore;
using EntityLayer.Concrete;

namespace DataAccessLayer.Contect
{
    
    public class Context :DbContext
    {
        
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public Context()
        {
        }

        public DbSet<RandomTemperature> RandomTemperatures { get; set; }
    }
}
