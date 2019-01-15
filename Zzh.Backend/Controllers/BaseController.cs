using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zzh.Backend.Controllers.Filter;
using System.Web.Optimization;

namespace Zzh.Backend.Controllers
{
    [ActionFilter]
    [ExceptionFilter]
    public class BaseController : Controller
    {
    }
}