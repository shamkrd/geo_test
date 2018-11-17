using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoIP.CORE.Models;
using GeoIP.CORE.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeoIP.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class GeoInfoController : Controller
    {
        private IGeoInfoRepository geoDb;
        public GeoInfoController(IGeoInfoRepository geoRep)
        {
            geoDb = geoRep;
        }

        [HttpGet("{ip}")]
        public async Task<IActionResult> Get(string ip)
        {
            try
            {
                GeoInfo info = await geoDb.Get(ip);
                if (info == null)
                {
                    return Json("По вашему запросу ничего не найдено, возможно вы допустили ошибку.");
                }
                return Json(info);
            }
            catch
            {
                return Json("Во время выполнения запроса произошла ошибка, попробуйте повторить позже.");
            }
        }
    }
}