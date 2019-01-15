using Zzh.Lib.DB.Repositorys;
using Zzh.Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Zzh.Lib.DB.Context;

namespace Zzh.Backend.Controllers
{
    public class CodesController : BaseController
    {
        // GET: Codes
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetCodeListByTypeID(int typeId)
        {
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                var list = await CodeRepository.GetCodesListAsync(visiter,typeId);
                return Json(new { rows = list });
            }
        }
        public async Task<ActionResult> CodeEdit(int codeID,int typeID)
        {
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                var model = await CodeRepository.GetCodeAsync(visiter, codeID);
                if (model == null)
                {
                    model = new Codes() { Status = 1, TypeId = typeID };
                }
                var StatusList = new List<SelectListItem>();
                StatusList.Add(new SelectListItem() { Value = "1", Text = "正常显示", Selected = true });
                StatusList.Add(new SelectListItem() { Value = "0", Text = "隐藏" });
                ViewBag.StatusList = StatusList;
                return View(model);
            }
        }
        public async Task<JsonResult> SaveCode(Codes codePara)
        {
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                var result = await CodeRepository.AddOrUpdateCode(visiter, codePara);
                return Json(new { isOk = result });
            }
        }
        #region CodeType
        public async Task<JsonResult> GetCodeTypeList(int page, int rows, string typeName)
        {
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                var tuple = await CodeTypeRepository.GetCodeTypeListAsync(visiter, page, rows, typeName);
                return Json(new { total = tuple.Item1, rows = tuple.Item2 });
            }
        }
        public async Task<ActionResult> CodeTypeEdit(int typeId)
        {
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                var model = await CodeTypeRepository.GetCodeTypeAsync(visiter, typeId);
                if (model == null)
                    model = new CodeType();
                return View(model);
            }
        }
        public async Task<JsonResult> SaveCodeType(CodeType modelPara)
        {
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                var result = await CodeTypeRepository.AddOrUpdateAsync(visiter, modelPara);
                return Json(new { isOk = result });
            }
        }
        public async Task<JsonResult> DelCodeType(int typeID)
        {
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                var result = await CodeTypeRepository.DelCodeTypeAsync(visiter, typeID);
                return Json(new { isOk = result });
            }
        }
        #endregion
    }
}