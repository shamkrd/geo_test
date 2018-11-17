using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GeoIP.CORE.Helpers
{
    public abstract class HtmlParser
    {
        /// <summary>
        /// Получаем html страницу по указоному url адресу
        /// </summary>
        /// <param name="url">Адрес страницы</param>
        private static async Task<string> GetHtml(string url)
        {
            string html = string.Empty;
            var response = await RequestHelper.SendRequest(url);

            using (StreamReader reader = new StreamReader(response))
            {
                html = await reader.ReadToEndAsync();
            }
            return html;
        }

        /// <summary>
        /// Получаем все url'ы на станице
        /// </summary>
        /// <param name="html">HTML страница</param>
        /// <returns>Список url'ов</returns>
        private static List<string> GetUrls(string html)
        {
            List<string> urls = new List<string>();

            //Регулярное выражение для получения всех ссылкок
            Regex regex = new Regex("<a\\s+(?:[^>]*?\\s+)?href=([\"'])(.*?)\\1");
            MatchCollection matches = Regex.Matches(html, @"(<a.*?>.*?</a>)", RegexOptions.Singleline);
            foreach (Match item in matches)
            {
                string value = item.Groups[1].Value;
                //Регулярное выражение для получения ссылок на zip архивы
                Match m = Regex.Match(value, @"\""(.*?).zip(.*?)\""",RegexOptions.Singleline);
                if (m.Success && !m.Value.Contains("md5"))
                {
                    urls.Add(m.Value.Substring(1, m.Value.Length - 2));
                }
            }
            return urls;
        }

        /// <summary>
        /// Получает списко урлов для загрузки архивов с указанной страницы
        /// </summary>
        /// <param name="url">Url страницы на которой находятся архивы</param>
        public static async Task<List<string>> GetDowloadUrls(string url)
        {
            var html = await GetHtml(url);
            var dowloadUrls =  GetUrls(html);
            return dowloadUrls;
        }
    }
}
