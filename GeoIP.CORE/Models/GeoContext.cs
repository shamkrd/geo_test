using Microsoft.EntityFrameworkCore;

namespace GeoIP.CORE.Models
{
   public class GeoContext:DbContext
    {
        public GeoContext(DbContextOptions<GeoContext> opts) : 
            base(opts) { }

        public DbSet<GeoInfo> GeoInfos { get; set; }
    }
}
