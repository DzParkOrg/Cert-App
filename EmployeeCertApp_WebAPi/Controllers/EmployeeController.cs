using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using EmployeeCertApp_WebAPi.Models;
using System.Web.Http.Cors;

namespace EmployeeCertApp_WebAPi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmployeeController : ApiController
    {
        private trackerEntities db = new trackerEntities();

        // GET api/Employee
      
        public IQueryable<Employee> GetEmployees()
        {
            var employees = db.Employees;
            return employees;
        }

        // GET api/Employee/5
        [ResponseType(typeof(Employee))]
        [Route("api/register/getEmployeeDetails/{empCode}")]
        [HttpGet]
        public IHttpActionResult GetEmployee(string empCode)
        {
            Employee employee = db.Employees.SingleOrDefault(m => m.EmployeeCode == empCode);
            if (employee == null)
            {
                return Ok("not exists");
            }

            return Ok(employee);
        }

        [ResponseType(typeof(Employee))]
        [Route("api/employee/GetEmployeeDetailsPerEmpId/{empId}")]
        [HttpGet]
        public IHttpActionResult GetEmployeeDetailsPerEmpId(int empId)
        {
            Employee employee = db.Employees.SingleOrDefault(m => m.EmployeeID == empId);
            if (employee == null)
            {
                return Ok("not exists");
            }

            return Ok(employee);
        }


        [ResponseType(typeof(Employee))]
        [Route("api/register/updateEmployeeDetails/{id}")]
        [HttpPut]
        public IHttpActionResult PutEmployee(int id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return Ok("data entered is not of correct format");
            }

            if (id != employee.EmployeeID)
            {
                return Ok("error");
            }

            db.Entry(employee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return Ok("employee not found");
                }
                else
                {
                    throw;
                }
            }

            return Ok("success");
        }

        // POST api/Employee
        [ResponseType(typeof(Employee))]
        [Route("api/register/postEmployeeDetails/")]
        [HttpPost]
        public IHttpActionResult PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Employee dbEmployee = db.Employees.SingleOrDefault(m => m.EmployeeCode == employee.EmployeeCode);
            if (dbEmployee != null)
            {
                return Ok("already exists");
            }
            else
            {
                employee.CreationTimeStamp = DateTime.Now;
                employee.LastModifiedTimeStamp = DateTime.Now;
                db.Employees.Add(employee);
                db.SaveChanges();
            }
            return Ok("success");
        }

        // DELETE api/Employee/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult DeleteEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employee);
            db.SaveChanges();

            return Ok(employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return db.Employees.Count(e => e.EmployeeID == id) > 0;
        }
    }
}