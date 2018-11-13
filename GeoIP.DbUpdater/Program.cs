using GeoIP.CORE.Models;
using Microsoft.Extensions.Configuration;
using System;
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

            var geoInfo = new GeoInfo { Ip = "123.223.223.1", Latitude = 25.9988448, Longitude = 45.8383800 };

            geoDb.Insert(geoInfo).GetAwaiter().GetResult();


        }
    }
}
