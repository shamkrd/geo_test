using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using GeoIP.CORE.Models;
using Npgsql;

namespace GeoIP.CORE.Repositories.Dapper
{
    public class GeoInfoRepository : IGeoInfoRepository
    {
        private string connectionString;
        private const string TABLE_NAME = "geo_infos";
        public GeoInfoRepository(string conStr)
        {
            connectionString = conStr;
        }

        public async Task<GeoInfo> Get(string ip)
        {
            GeoInfo geoInfo = null;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                try
                {
                    var sql = $"SELECT * FROM {TABLE_NAME} WHERE ip = @ip";
                    var param = new { ip };

                    var result = (await db.QueryAsync<dynamic>(sql, param)).FirstOrDefault();
                    try
                    {
                        geoInfo = new GeoInfo(result);

                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Во время преобразования ответа произошла ошибка.", ex);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Во время получения ответа из базы данных произошла ошибка", ex);
                }
            }
            return geoInfo;
        }

        public async Task Insert(GeoInfo geoInfo)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                try
                {
                    string sql = $"INSERT INTO {TABLE_NAME} " +
                        "(ip, latitude, longitude) " +
                        "VALUES " +
                        "(@ip, @latitude, @longitude);";

                    var param = new
                    {
                        ip = geoInfo.Ip,
                        latitude = geoInfo.Latitude,
                        longitude = geoInfo.Longitude
                    };

                    await db.ExecuteAsync(sql, param);
                }
                catch (Exception ex)
                {
                    throw new Exception("Во время выполнения sql запроса произошла ошибка", ex);
                }
            }
        }

        public async Task Update(GeoInfo geoInfo)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                try
                {
                    string sql = $"UPDATE {TABLE_NAME} SET" +
                        "ip = @ip, " +
                        "latitude = @latitude, " +
                        "longitude = @longitude " +
                        "WHERE " +
                        "id = @id;";

                    var param = new
                    {
                        ip = geoInfo.Ip,
                        latitude = geoInfo.Latitude,
                        longitude = geoInfo.Longitude,
                        id = geoInfo.Id
                    };

                    await db.ExecuteAsync(sql, param);
                }
                catch (Exception ex)
                {
                    throw new Exception("Во время выполнения sql запроса произошла ошибка", ex);
                }
            }
        }
    }
}
