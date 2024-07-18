using EmployeeApplication.Data;
using EmployeeApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System;

namespace EmployeeApplication.Controllers
{
    public class EmployeeController : Controller
    {
        private ApplicationDbContext _Db;

        public EmployeeController( ApplicationDbContext db)
        {
            _Db = db;
        }

        public IActionResult Index()
        {
            try
            {
                var book = _Db.Employee.ToList();

                return View(book);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View();
            }
            
        }

        public IActionResult Create()
        {
            try
            {
                var list1 = new List<SelectListItem>
                {
                    new SelectListItem {Text="Information Technology", Value="Information Technology"},
                    new SelectListItem {Text="Human Resource", Value="Human Resource"},
                    new SelectListItem {Text="Finance", Value="Finance"},
                    new SelectListItem {Text="Sales", Value="Sales"}
                };

                var list2 = new List<SelectListItem>
                {
                new SelectListItem {Text="Manager of Information Technology", Value="Manager of Information Technology"},
                    new SelectListItem {Text="System Analysis", Value="System Analysis"},
                    new SelectListItem {Text="Software Engineer", Value="Software Engineer"},
                    new SelectListItem {Text="Manager of Sales", Value="Manager of Sales"},
                    new SelectListItem {Text="Sales Executive", Value="Sales Executive"},
                    new SelectListItem {Text="Purchasing", Value="Purchasing"},
                    new SelectListItem {Text="Manager of Human Resource", Value="Manager of Human Resource"},
                    new SelectListItem {Text="Human Resource Employee", Value="Human Resource Employee"},
                    new SelectListItem {Text="Manager of Finance", Value="Manager of Finance"},
                    new SelectListItem {Text="Finance Consultant", Value="Finance Consultant"}
                };

                var list3 = new List<SelectListItem>
                {
                new SelectListItem {Text="Malaysian", Value="Malaysian"},
                    new SelectListItem {Text="Singapore", Value="Singapore"},
                    new SelectListItem {Text="Japan", Value="Japan"},
                    new SelectListItem {Text="United States", Value="United States"},
                    new SelectListItem {Text="United Kingdom", Value="United Kingdom"},
                    new SelectListItem {Text="China", Value="China"}
                };

                ViewBag.Department = list1;
                ViewBag.Occupation = list2;
                ViewBag.Nationality = list3;

                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
           
        }

        [HttpPost]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                string EmpNo = collection["EmpNo"];
                string Name = collection["Name"];
                string ICNo = collection["ICNo"];
                string DateOfBirth = collection["DateOfBirth"];
                string HiredDate = collection["HiredDate"];
                string Department = collection["Department"];
                string Occupation = collection["Occupation"];
                string Nationality = collection["Nationality"];
                string gender = collection["gender"];

                DateTime datebirth = Convert.ToDateTime(DateOfBirth);
                DateTime datehired = Convert.ToDateTime(HiredDate);

                var item = _Db.Employee.Where(x => x.EmpNo.Contains(EmpNo) || x.ICNo.Contains(ICNo)).FirstOrDefault();

                if(item != null) 
                {
                    TempData["Error"] = "EmpNo Or IC no already exist";
                }
                else
                {
                    string update_sql = "INSERT INTO [EMPLOYEE] VALUES(  '" + Name + "','" + EmpNo + "','" + gender + "','" + Nationality + "','" + ICNo + "','" + Department
                    + "','" + Occupation + "','" + DateOfBirth + "','" + HiredDate + "')";
                    _Db.Database.ExecuteSqlRaw(update_sql);

                    TempData["Success"] = "Employee create success";
                }

                

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult Edit(long id)
        {
            try
            {
                var list1 = new List<SelectListItem>
                {
                    new SelectListItem {Text="Information Technology", Value="Information Technology"},
                    new SelectListItem {Text="Human Resource", Value="Human Resource"},
                    new SelectListItem {Text="Finance", Value="Finance"},
                    new SelectListItem {Text="Sales", Value="Sales"}
                };

                var list2 = new List<SelectListItem>
                {
                new SelectListItem {Text="Manager of Information Technology", Value="Manager of Information Technology"},
                    new SelectListItem {Text="System Analysis", Value="System Analysis"},
                    new SelectListItem {Text="Software Engineer", Value="Software Engineer"},
                    new SelectListItem {Text="Manager of Sales", Value="Manager of Sales"},
                    new SelectListItem {Text="Sales Executive", Value="Sales Executive"},
                    new SelectListItem {Text="Purchasing", Value="Purchasing"},
                    new SelectListItem {Text="Manager of Human Resource", Value="Manager of Human Resource"},
                    new SelectListItem {Text="Human Resource Employee", Value="Human Resource Employee"},
                    new SelectListItem {Text="Manager of Finance", Value="Manager of Finance"},
                    new SelectListItem {Text="Finance Consultant", Value="Finance Consultant"}
                };

                var list3 = new List<SelectListItem>
                {
                new SelectListItem {Text="Malaysian", Value="Malaysian"},
                    new SelectListItem {Text="Singapore", Value="Singapore"},
                    new SelectListItem {Text="Japan", Value="Japan"},
                    new SelectListItem {Text="United States", Value="United States"},
                    new SelectListItem {Text="United Kingdom", Value="United Kingdom"},
                    new SelectListItem {Text="China", Value="China"}
                };

                ViewBag.Department = list1;
                ViewBag.Occupation = list2;
                ViewBag.Nationality = list3;

                var item = _Db.Employee.Where(x => x.Id.Equals(id)).FirstOrDefault();
                ViewBag.DateOfBirth = String.Format("{0:yyyy-MM-dd}", item.DateOfBirth);
                ViewBag.HiredDate = String.Format("{0:yyyy-MM-dd}", item.HiredDate);
                ViewBag.Id = id;

                return View(item);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
            
        }

        [HttpPost]
        public ActionResult Edit(IFormCollection collection)
        {
            try
            {
                string EmpId = collection["__EmployeeId"];

                string EmpNo = collection["EmpNo"];
                string Name = collection["Name"];
                string ICNo = collection["ICNo"];
                string DateOfBirth = collection["DateOfBirth"];
                string HiredDate = collection["HiredDate"];
                string Department = collection["Department"];
                string Occupation = collection["Occupation"];
                string Nationality = collection["Nationality"];
                string gender = collection["gender"];

                DateTime datebirth = Convert.ToDateTime(DateOfBirth);
                DateTime datehired = Convert.ToDateTime(HiredDate);
                long employeeid = Convert.ToInt32(EmpId);

                var item = _Db.Employee.Where(x => (x.EmpNo.Contains(EmpNo) || x.ICNo.Contains(ICNo)) && x.Id != employeeid).FirstOrDefault();

                if (item != null)
                {
                    TempData["Error"] = "Update Failure, EmpNo Or IC no already exist";
                }
                else
                {

                    string update_sql = "UPDATE [EMPLOYEE] SET Name = '" + Name + "',EmpNo ='" + EmpNo + "',Gender ='" + gender + "',Nationality ='"
                    + Nationality + "',ICNo ='" + ICNo + "',Department ='" + Department + "',Occupation ='" + Occupation
                    + "',DateOfBirth ='" + DateOfBirth + "',HiredDate ='" + HiredDate + "'WHERE EMPLOYEEID = " + employeeid;
                    _Db.Database.ExecuteSqlRaw(update_sql);

                    TempData["Success"] = "Employee update success";
                }



                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult Delete(long id)
        {
            try
            {
                if(id > 0)
                {
                    var item = _Db.Employee.Where(x => x.Id.Equals(id)).FirstOrDefault();
                    if (item != null)
                    {
                        string delete_sql = "DELETE [EMPLOYEE]  WHERE EMPLOYEEID = " + id;
                        _Db.Database.ExecuteSqlRaw(delete_sql);

                        TempData["Success"] = "Successfully delete enployee ";
                    }
                    else
                    {
                        TempData["Error"] = "Employee not found ";
                    }

                }


                return RedirectToAction("Index");
            }

            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
            
        }

        [HttpPost]
        public ActionResult Delete(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
