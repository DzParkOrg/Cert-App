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
    public class LoginController : ApiController
    {
        private trackerEntities db = new trackerEntities();

        // GET api/Login
        public IQueryable<UserDetail> GetUserDetails()
        {
            return db.UserDetails;
        }

        // GET api/Login/5
        [ResponseType(typeof(UserDetail))]
        [Route("api/login/getLoginDetails/{loginName}/{password}")]
        [HttpGet]
        public IHttpActionResult GetUserDetail(string loginName, string password)
        {
            UserDetail userdetail = db.UserDetails.SingleOrDefault(m => m.LoginName == loginName && m.Password == password);
            if (userdetail == null)
            {
                return Ok("error");
            }

            return Ok(userdetail);
        }

        // PUT api/Login/5
        public IHttpActionResult PutUserDetail(int id, UserDetail userdetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userdetail.UserId)
            {
                return BadRequest();
            }

            db.Entry(userdetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Login
        [ResponseType(typeof(UserDetail))]
        [Route("api/login/registerUserDetails/")]
        [HttpPost]
        public IHttpActionResult PostUserDetail(UserDetail userdetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                    UserDetail dbUser = db.UserDetails.SingleOrDefault(m => m.EmployeeId == userdetail.EmployeeId);
                    if (dbUser != null)
                    {
                        return Ok("already exists");
                    }
                    else
                    {
                        userdetail.CreationTS = DateTime.Now;
                        userdetail.LastModdifiedTS = DateTime.Now;
                        db.UserDetails.Add(userdetail);
                        db.SaveChanges();
                    }
             
            }
            catch (DbUpdateException)
            {
                if (UserDetailExists(userdetail.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok("success");
        }


        // DELETE api/Login/5
        [ResponseType(typeof(UserDetail))]
        public IHttpActionResult DeleteUserDetail(int id)
        {
            UserDetail userdetail = db.UserDetails.Find(id);
            if (userdetail == null)
            {
                return NotFound();
            }

            db.UserDetails.Remove(userdetail);
            db.SaveChanges();

            return Ok(userdetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserDetailExists(int id)
        {
            return db.UserDetails.Count(e => e.UserId == id) > 0;
        }
    }
}