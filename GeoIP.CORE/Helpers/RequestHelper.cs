using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace GeoIP.CORE.Helpers
{
    public class RequestHelper
    {
        public static async Task<Stream> SendRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            Stream resp;
            HttpWebResponse response = (HttpWebResponse)await (request.GetResponseAsync());
            resp = response.GetResponseStream();
            return resp;
        }
    }
}
