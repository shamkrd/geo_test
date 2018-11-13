using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoIP.CORE.Models
{
    /// <summary>
    /// Информация о геолокации
    /// </summary>
    public class GeoInfo
    {
        #region Properties
        [Column("id")]
        [JsonIgnore]
        public long Id { get; set; }

        /// <summary>
        /// IP адрес
        /// </summary>
        [Column("ip")]
        [JsonProperty("ip")]
        public string Ip { get; set; }

        /// <summary>
        /// Широта
        /// </summary>
        [Column("latitude")]
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        /// <summary>
        /// Долгота
        /// </summary>
        [Column("longitude")]
        [JsonProperty("longitude")]
        public double Longitude { get; set; }
        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор для созданеия экземпляра класса используя
        /// ответ от базы данных
        /// </summary>
        /// <param name="dbResuls">ответ полученный от базы данных (набор полей)</param>
        public GeoInfo(dynamic dbResuls)
        {
            Id = dbResuls.id;
            Ip = dbResuls.Ip;
            Latitude = dbResuls.latitude;
            Longitude = dbResuls.longitude;
        }

        public GeoInfo(){}

        #endregion

    }
}
