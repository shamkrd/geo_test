using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GeoIP.CORE.Helpers
{
    public abstract class HtmlParser
    {
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

        private static List<string> GetUrls(string html)
        {
            List<string> urls = new List<string>();

            Regex regex = new Regex("<a\\s+(?:[^>]*?\\s+)?href=([\"'])(.*?)\\1");
            MatchCollection matches = Regex.Matches(html, @"(<a.*?>.*?</a>)", RegexOptions.Singleline);
            foreach (Match item in matches)
            {
                string value = item.Groups[1].Value;
                Match m = Regex.Match(value, @"\""(.*?).zip(.*?)\""",
                RegexOptions.Singleline);
                if (m.Success && !m.Value.Contains("md5"))
                {
                    urls.Add(m.Value.Substring(1, m.Value.Length - 2));
                }
            }
            return urls;
        }

        public static async Task<List<string>> GetDowloadUrls(string url)
        {
            var html = await GetHtml(url);
            var dowloadUrls =  GetUrls(html);
            return dowloadUrls;
        }
    }
}
