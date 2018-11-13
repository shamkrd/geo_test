using GeoIP.CORE.Models;
using System.Threading.Tasks;

namespace GeoIP.CORE.Repositories
{
   public interface IGeoInfoRepository
    {
        Task<GeoInfo> Get(string ip);

        Task Insert(GeoInfo geoInfo);

        Task Update(GeoInfo geoInfo);
    }
}
