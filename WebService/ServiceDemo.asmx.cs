using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using Zzh.Lib.DB.Repositorys;
using Zzh.Model.DB;

namespace WebService
{
    /// <summary>
    /// ServiceDemo 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class ServiceDemo : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod(Description = "测试调用数据库")]
        public string GetStr(string str)
        {
            try
            {
                using (Sys_UserRepository repo = new Sys_UserRepository())
                {
                    var list = repo.GetListAsync();
                    string json = JsonConvert.SerializeObject(list);
                    return json;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
