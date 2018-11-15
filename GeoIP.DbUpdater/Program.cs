using GeoIP.CORE.Helpers;
using GeoIP.CORE.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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
