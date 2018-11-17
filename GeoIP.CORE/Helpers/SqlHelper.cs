using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeoIP.CORE.Helpers
{
   public abstract  class SqlHelper
    {
        /// <summary>
        /// Генерирует запрос для записи большого кол-ва строк в таблицу
        /// </summary>
        /// <param name="geoInfo">Массив строк переданный по ссылке</param>
        /// <returns>Возвращает строку запроса для добавления большого кол-ва строк</returns>
        public static string GetManyInsertQuery( ref string[]geoInfo)
        {
            #region Подготовка запроса для заполнения временной таблицы
            StringBuilder sb = new StringBuilder();
            sb.Append("CREATE TABLE temp_ueser_info  ( " +
                "id bigserial NOT NULL PRIMARY KEY," +
                "ip character varying(20) NOT NULL," +
                "latitude double precision NOT NULL," +
                "longitude double precision NOT NULL);");

            sb.Append("INSERT INTO temp_ueser_info (ip, latitude, longitude) VALUES ");

            string ip, latitude, longitude;

            for (int i = 1; i < geoInfo.Length - 1; i++)
            {
                ip = geoInfo[i].Split(",")[0].Split("/")[0];
                latitude = geoInfo[i].Split(",")[7];
                longitude = geoInfo[i].Split(",")[8];
                //Проверяем значения строки пере
                if (string.IsNullOrEmpty(ip) || string.IsNullOrEmpty(latitude) ||
                    string.IsNullOrEmpty(longitude))
                {
                    continue;
                }

                if (i == geoInfo.Length - 2)
                {

                    sb.Append($"('{ip}'," +
                       $"{latitude}," +
                       $"{longitude});");
                }
                else
                {
                    sb.Append($"('{ip}'," +
                        $"{latitude}," +
                        $"{longitude}),");
                }

            }
            #endregion

            #region Создания транзакции для гарантии целостности
            sb.Append("BEGIN;");
            sb.Append("TRUNCATE geo_infos;");
            sb.Append("INSERT INTO geo_infos(ip,latitude,longitude)" +
                       "SELECT ip, latitude, longitude FROM temp_ueser_info;");
            sb.Append("DROP TABLE temp_ueser_info;");
            sb.Append("COMMIT;");
            #endregion

            return sb.ToString();
        }
    }
}
