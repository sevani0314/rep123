using Demo_20230215.Models;
using Demo_20230215.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace Demo_20230215.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            EmpMethods empMethods = new EmpMethods();
            List<EmployeeModel> empList = empMethods.GetEmpData();

            return View(empList);
        }

        public PartialViewResult EmpData(string EmpNo = "")
        {
            List<EmployeeModel> empList = new List<EmployeeModel>();

            if (Session["EmpData"] == null)
            {
                EmpMethods empMethods = new EmpMethods();
                empList = empMethods.GetEmpData();
                Session["EmpData"] = empList;
            }
            else
            {
                empList = Session["EmpData"] as List<EmployeeModel>;
                //empList = (List<EmployeeModel>)Session["EmpData"]
            }

            if (string.IsNullOrEmpty(EmpNo))
            {
                return PartialView(empList);
            }

            var result = new List<EmployeeModel>();
            //foreach(var item in empList)
            //{
            //  if(item.EmpNo == EmpNo)
            //  {
            //       result.Add(item);
            //    }
            // }

            result = empList.Where(x => x.EmpNo == EmpNo).ToList();

            //40-47與49行作用一樣
            return PartialView(empList.Where(x => x.EmpNo == EmpNo).ToList());
        }

        public ActionResult About()
        {
            //一些功能
            Session["D1"] = "hELLO SESSION";
            ViewBag.D1 = "hELLO viewbag";
            ViewData["D1"] = "hELLO viewdata";
            TempData["D1"] = "hELLO tempdata";
            ViewBag.Message = "Your application description page.";
            return View();
        }




        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Edit(string EmpNo)
        {
            //判斷輸入參數
            if (string.IsNullOrEmpty(EmpNo))
            {
                return View(new EmployeeModel());
            }

            if (Session["EmpData"] == null)
            {
                return View(new EmployeeModel());
            }

            EmployeeModel result = ((List<EmployeeModel>)Session["EmpData"]).Where(x => x.EmpNo == EmpNo).FirstOrDefault();
            return View(result);


        }

        [HttpPost]//請求只能是http
        public JsonResult EditPost(EmployeeModel empData)
        {
            MessageInfo messageInfo = new MessageInfo() { IsSuccess = true, Msg = "修改成功" };
            //todo 檢核資料
            if (Session["EmpData"] == null)
            {
                messageInfo.IsSuccess = false;
                messageInfo.Msg = "No data";
                return Json(messageInfo);
            }


            List<EmployeeModel> result = (List<EmployeeModel>)Session["EmpData"];

            if (result.Where(x => x.EmpNo == empData.EmpNo).Count() == 0)
            {
                //return fail
                messageInfo.IsSuccess = false;
                messageInfo.Msg = "No data";
                return Json(messageInfo);
            }

            var emp = result.Where(x => x.EmpNo == empData.EmpNo).FirstOrDefault();
            emp.Name = empData.Name;
            emp.Ext = empData.Ext;
            Session["EmpData"] = result;
            return Json(messageInfo);
        }

        [HttpPost]
        public JsonResult DeletePost(string EmpNo)
        {
            MessageInfo messageInfo = new MessageInfo() { IsSuccess = true, Msg = "" };
            if (Session["EmpData"] == null)
            {
                messageInfo.IsSuccess = false;
                messageInfo.Msg = "查無資料";
                return Json(messageInfo);
            }

            List<EmployeeModel> empDatas = Session["EmpData"] as List<EmployeeModel>;

            if (empDatas.Where(x => x.EmpNo == EmpNo).Count() == 0)
            {
                messageInfo.IsSuccess = false;
                messageInfo.Msg = "查無資料";
                return Json(messageInfo);
            }

            empDatas.Remove(empDatas.Where(x => x.EmpNo == EmpNo).FirstOrDefault());
            Session["EmpData"] = empDatas;

            return Json(messageInfo);
        }

        //表達動作-新增
        public ActionResult Adda()
        {
            
            return View();
        }


        //json-新增
        [HttpPost]
        public JsonResult Add(EmployeeModel employee)
        {
            MessageInfo messageInfo = new MessageInfo() { IsSuccess = true, Msg = "新增成功" };
            
            List<EmployeeModel> result = (List<EmployeeModel>)Session["EmpData"];
          
            //index btn新增
            //儲存後確定key直沒有一樣的 int的分機號碼
            //jquery判斷
            if (result.Where(x => x.EmpNo == employee.EmpNo).Count() != 0)
            {
                messageInfo.IsSuccess = false;
                messageInfo.Msg = "資料庫內已有資料";
                return Json(messageInfo);
            }

            result.Add(employee);
            Session["EmpData"] = result;
            return Json(messageInfo);

        }
    }
}