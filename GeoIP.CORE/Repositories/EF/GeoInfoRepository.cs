using System.Threading.Tasks;
using GeoIP.CORE.Models;
using Microsoft.EntityFrameworkCore;

namespace GeoIP.CORE.Repositories.EF
{
   public class GeoInfoRepository : IGeoInfoRepository
    {
        private GeoContext context;

        public GeoInfoRepository(GeoContext ctx) => context = ctx;

        public async Task<GeoInfo> Get(string ip)
        {
            var geoInfo = await context
                .GeoInfos
                .FirstOrDefaultAsync(x => x.Ip == ip);

            return geoInfo;
        }

        public async Task Insert(GeoInfo geoInfo)
        {
            await context.GeoInfos.AddAsync(geoInfo);
            await context.SaveChangesAsync();
        }

        public async Task Update(GeoInfo geoInfo)
        {
            context.GeoInfos.Update(geoInfo);
            await context.SaveChangesAsync();
        }
    }
}
