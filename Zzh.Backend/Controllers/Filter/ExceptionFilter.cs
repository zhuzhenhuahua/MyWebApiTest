using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zzh.Lib.Log;

namespace Zzh.Backend.Controllers.Filter
{
    public class ExceptionFilterAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            SHLog4net.LogError(ex);
        }
    }
}