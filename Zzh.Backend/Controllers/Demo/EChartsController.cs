using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Zzh.Lib.DB.Repositorys;

namespace Zzh.Backend.Controllers.Demo
{
    public class EChartsController : Controller
    {
        // GET: ECharts
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetPriceJson()
        {
            using (jlgRepository repo = new jlgRepository())
            {
                var list = await repo.GetListAsync();
                string[] legend = { "单价", "批发价" };
                var xData = list.Select(p => p.rq).ToList().Select(p=>p.ToString("yyyy-MM-dd"));
                List<object> objResult = new List<object>();
                foreach (string item in legend)
                {
                    objResult.Add(new {
                        name=item,
                        type= "line",
                        data= item== "单价"? list.Select(p=>p.priceNum1).ToList(): list.Select(p => p.priceNum2).ToList()
                    });
                }
                return Json(new { legend = legend , xData = xData , ySeries=objResult });
            }
        }
    }
}