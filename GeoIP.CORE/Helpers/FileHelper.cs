
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoIP.CORE.Helpers
{
   public class FileHelper
    {
        /// <summary>
        /// Разорхивирует архив по url адресу
        /// </summary>
        /// <param name="url">Адрес архива</param>
        /// <returns></returns>
        public async static Task<string> UnzipFile (string url)
        {
            var resp = await RequestHelper.SendRequest(url);
                using (var archive = new ZipArchive(resp))
                {
                    //Проверяем что файл в апхиаве, содержит IPv4 адреса 
                    var entry = archive.Entries.FirstOrDefault(x=>x.FullName.Contains("IPv4"));

                    if (entry != null)
                    {
                        using (var unzippedEntryStream = entry.Open())
                        {
                            using (var ms = new MemoryStream())
                            {
                                unzippedEntryStream.CopyTo(ms);
                                var unzippedArray = ms.ToArray();

                                return Encoding.Default.GetString(unzippedArray);
                            }
                    }
                }
            }
            return "";
        }
    }
}
