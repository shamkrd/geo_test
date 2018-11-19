using GeoIP.CORE.Helpers;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using dapper = GeoIP.CORE.Repositories.Dapper;

namespace GeoIP.DbUpdater
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var geoDb = new dapper.GeoInfoRepository(configuration.GetConnectionString("GeoDb"));

            string url = configuration["ArchiveUrl"];

            var urls = await HtmlParser.GetDowloadUrls(url);

            var geoInfo = (await FileHelper.UnzipFile(urls[0])).Split("\n");
          
            await geoDb.InsertMany(SqlHelper.GetManyInsertQuery(ref geoInfo));

        }
    }
}
