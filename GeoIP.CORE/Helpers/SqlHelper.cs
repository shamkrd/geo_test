using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeoIP.CORE.Helpers
{
   public abstract  class SqlHelper
    {
        public static string GetManyInsertQuery( ref string[]geoInfo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("CREATE TABLE temp_ueser_info  ( " +
                "id bigserial NOT NULL PRIMARY KEY," +
                "ip character varying(20) NOT NULL," +
                "latitude double precision NOT NULL," +
                "longitude double precision NOT NULL);");

            sb.Append("INSERT INTO temp_ueser_info (ip, latitude, longitude) VALUES ");

            for (int i = 1; i < geoInfo.Length - 1; i++)
            {
                if (string.IsNullOrEmpty(geoInfo[i].Split(",")[7]) ||
                    string.IsNullOrEmpty(geoInfo[i].Split(",")[8]) ||
                    string.IsNullOrEmpty(geoInfo[i].Split(",")[0].Split("/")[0])
                    )
                {
                    continue;
                }

                if (i == geoInfo.Length - 2)
                {

                    sb.Append($"('{geoInfo[i].Split(",")[0].Split("/")[0]}'," +
                       $"{geoInfo[i].Split(",")[7]}," +
                       $"{geoInfo[i].Split(",")[8]});");
                }
                else
                {
                    sb.Append($"('{geoInfo[i].Split(",")[0].Split("/")[0]}'," +
                        $"{geoInfo[i].Split(",")[7]}," +
                        $"{geoInfo[i].Split(",")[8]}),");
                }

            }

            sb.Append("BEGIN;");
            sb.Append("TRUNCATE geo_infos;");
            sb.Append("INSERT INTO geo_infos(ip,latitude,longitude)" +
                       "SELECT ip, latitude, longitude FROM temp_ueser_info;");
            sb.Append("DROP TABLE temp_ueser_info;");
            sb.Append("COMMIT;");
            return sb.ToString();
        }
    }
}
