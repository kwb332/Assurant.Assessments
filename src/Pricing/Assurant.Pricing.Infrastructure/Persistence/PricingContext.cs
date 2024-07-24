
using Assurant.Pricing.Infrastructure.Contract.DataModel;
using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

namespace Assurant.Pricing.Infrastructure.Persistence
{
    public class PricingContext : DbContext
    {
        public PricingContext(DbContextOptions<PricingContext> options) :base(options)
        { 
        }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Holiday> Holiday { get; set; }
    }
}
