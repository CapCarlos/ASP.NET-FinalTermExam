using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET_FinalTermExam.Controllers
{
    public class EmployeeController : Controller
    {
        Models.CodeService codeService = new Models.CodeService();
        Models.EmployeeService employeeService = new Models.EmployeeService();
       
        /// <summary>
        /// 訂單管理首頁
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.EmpCodeData = this.codeService.GetJob(-1);
            
            return View();
        }


        /// <summary>
        /// 取得員工查詢結果
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult Index(Models.EmployeeSearchArg arg)
        {
            ViewBag.SCodeData = this.codeService.GetJob(-1);
            
            Models.EmployeeService employeeService = new Models.EmployeeService();
            ViewBag.SearchResult = employeeService.GetEmployeeByCondtioin(arg);
            return View("Index");
        }










    }
}