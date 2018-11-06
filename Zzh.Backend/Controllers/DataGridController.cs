using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Zzh.Lib.DB.Repositorys;
using Zzh.Backend.Controllers.Filter;
using Zzh.Model.DB.Configuration;
using System.Configuration;
using Zzh.Lib.Util;
using Zzh.Utility;
using Newtonsoft.Json;

namespace Zzh.Backend.Controllers
{
    public class DataGridController : BaseController
    {
        // GET: DataGrid
        public ActionResult BasicGrid()
        {
            return View();
        }
        public ActionResult Editor()
        {
            return View();
        }
        public ActionResult MergeCells()
        {
            return View();
        }
        public ActionResult DataCharts(string city = "武汉")
        {
            Dictionary<string, string> para = new Dictionary<string, string>();
            para.Add("city", city);
            var json = RequestUtility.HttpGet("https://www.apiopen.top/weatherApi", para);
            var Root = JsonConvert.DeserializeObject<Weather>(json);
            if (Root.data == null)
                Root.data = new Data() { forecast = new List<ForecastItem>(), yesterday = new Yesterday() };
            return View(Root);
        }
        public class Yesterday
        {
            /// <summary>
            /// 5日星期一
            /// </summary>
            public string date { get; set; }
            /// <summary>
            /// 高温 15℃
            /// </summary>
            public string high { get; set; }
            /// <summary>
            /// 无持续风向
            /// </summary>
            public string fx { get; set; }
            /// <summary>
            /// 低温 9℃
            /// </summary>
            public string low { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string fl { get; set; }
            /// <summary>
            /// 阴
            /// </summary>
            public string type { get; set; }
        }

        public class ForecastItem
        {
            /// <summary>
            /// 6日星期二
            /// </summary>
            public string date { get; set; }
            /// <summary>
            /// 高温 15℃
            /// </summary>
            public string high { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string fengli { get; set; }
            /// <summary>
            /// 低温 9℃
            /// </summary>
            public string low { get; set; }
            /// <summary>
            /// 无持续风向
            /// </summary>
            public string fengxiang { get; set; }
            /// <summary>
            /// 多云
            /// </summary>
            public string type { get; set; }
        }

        public class Data
        {
            /// <summary>
            /// 
            /// </summary>
            public Yesterday yesterday { get; set; }
            /// <summary>
            /// 成都
            /// </summary>
            public string city { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string aqi { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<ForecastItem> forecast { get; set; }
            /// <summary>
            /// 昼夜温差较大，较易发生感冒，请适当增减衣服。体质较弱的朋友请注意防护。
            /// </summary>
            public string ganmao { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string wendu { get; set; }
        }
    }

    public class Weather
    {
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 成功!
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DataGridController.Data data { get; set; }
    }
}