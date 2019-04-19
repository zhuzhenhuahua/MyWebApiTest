using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Zzh.Lib.DB.Context;
using Zzh.Lib.DB.Repositorys;
using Zzh.Lib.Util;

namespace Zzh.Backend.Controllers
{
    public class TestController : ApiController
    {
        public HttpResponseMessage addUser(int userId)
        {
            if (userId == 0)
                return JsonResponseHelper.HttpRMtoJson("该用户名已存在", HttpStatusCode.OK, ECustomStatus.Fail);
            return JsonResponseHelper.HttpRMtoJson(true, HttpStatusCode.OK, ECustomStatus.Success);
        }
        public async Task<HttpResponseMessage> getAllUser()
        {
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                var result = await Sys_UserRepository.GetListAsync(visiter, 1, 2, "");
                return JsonResponseHelper.HttpRMtoJson(new { total = result.Item1, rows = result.Item2 }, HttpStatusCode.OK, ECustomStatus.Success);
            }
        }
        public async Task<HttpResponseMessage> getUserById(int userId)
        {
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                if(userId==0)
                    return JsonResponseHelper.HttpRMtoJson("不存在id为0的用户", HttpStatusCode.OK, ECustomStatus.Fail);
                var result = await Sys_UserRepository.GetUserAsync(visiter, userId);
                return JsonResponseHelper.HttpRMtoJson(result, HttpStatusCode.OK, ECustomStatus.Success);
            }
        }
    }
}
