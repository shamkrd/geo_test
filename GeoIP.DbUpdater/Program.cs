using GeoIP.CORE.Helpers;
using Microsoft.Extensions.Configuration;
using System.IO;
using dapper = GeoIP.CORE.Repositories.Dapper;

namespace GeoIP.DbUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var geoDb = new dapper.GeoInfoRepository(configuration.GetConnectionString("GeoDb"));

            string url = configuration["ArchiveUrl"];

            var urls = HtmlParser.GetDowloadUrls(url).GetAwaiter().GetResult();

            var geoInfo = FileHelper.UnzipFile(urls[0]).GetAwaiter().GetResult().Split("\n");
          
            geoDb.InsertMany(SqlHelper.GetManyInsertQuery(ref geoInfo)).GetAwaiter().GetResult();

        }
    }
}
