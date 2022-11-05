using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using CRUD_Core_MVC.Models;
using System.Data.Common;

namespace CRUD_Core_MVC.Controllers
{
    public class EmployeeController : Controller
    {
        public IConfiguration _configuration { get; }
        SqlConnection con;
        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
            con = new SqlConnection(_configuration.GetConnectionString("DBCon"));

        }
        // GET: EmployeeController
        public ActionResult Index()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM tbl_Emp";
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    employees.Add(new Employee
                    {
                        Id = Convert.ToInt32(sdr["Id"]),
                        Name = Convert.ToString(sdr["Name"]),
                        Age = Convert.ToInt32(sdr["Age"])
                    });
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return View(employees);
        }

        // GET: EmployeeController/Details/5
        public Employee Det(int id)
        {
            Employee employee = new Employee();
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM tbl_Emp Where(ID=" + id + ");";
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                //while (sdr.Read())
                //{
                sdr.Read();
                employee = (new Employee

                {
                    Id = Convert.ToInt32(sdr.GetValue(0)),
                    Name = Convert.ToString(sdr.GetValue(1)),
                    Age = Convert.ToInt32(sdr.GetValue(2))
                });
                sdr.Close();
            }
            finally
            {
                con.Close();
            }
            return employee;
        }
        public ActionResult Details(int id)
        {
            //Employee employee = new Employee();
            try
            {
                //SqlCommand cmd = con.CreateCommand();
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = "SELECT * FROM tbl_Emp Where(ID=" + id + ");";
                //con.Open();
                //SqlDataReader sdr = cmd.ExecuteReader();
                ////while (sdr.Read())
                ////{
                //sdr.Read();
                //employee = (new Employee

                //{
                //    Id = Convert.ToInt32(sdr.GetValue(0)),
                //    Name = Convert.ToString(sdr.GetValue(1)),
                //    Age = Convert.ToInt32(sdr.GetValue(2))
                //});
                //sdr.Close();
                return View(Det(id));
            }

            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
            finally
            {
                con.Close();
            }
            //return View(employee);
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into tbl_Emp(Name, Age) values('" + employee.Name + "'," + employee.Age + ")";
                con.Open();
                cmd.ExecuteNonQuery();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
            finally
            {
                con.Close();
            }
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            //var employee = Details(id);
            return View(Det(id));
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Employee employee)
        {
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Update tbl_Emp Set Name='" + employee.Name + "', Age=" + employee.Age + "Where(ID=" + id + ");";
                con.Open();
                cmd.ExecuteNonQuery();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
            finally
            {
                con.Close();
            }
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(Det(id));
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Employee employee)
        {
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Delete from tbl_Emp Where(ID=" + id + ");";
                con.Open();
                cmd.ExecuteNonQuery();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
            finally
            {
                con.Close();
            }
        }
    }
}
