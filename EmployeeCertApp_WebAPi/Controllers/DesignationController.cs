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
    public class DesignationController : ApiController
    {
        private trackerEntities db = new trackerEntities();

        // GET api/Designation
        [Route("api/designation/getAllDesignation/")]
        [HttpGet]
        public IQueryable<Designation> GetDesignations()
        {
            return db.Designations;
        }

        // GET api/Designation/5
        [ResponseType(typeof(Designation))]
        public IHttpActionResult GetDesignation(int id)
        {
            Designation designation = db.Designations.Find(id);
            if (designation == null)
            {
                return NotFound();
            }

            return Ok(designation);
        }

        // PUT api/Designation/5
        public IHttpActionResult PutDesignation(int id, Designation designation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != designation.DesignationID)
            {
                return BadRequest();
            }

            db.Entry(designation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DesignationExists(id))
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

        // POST api/Designation
        [ResponseType(typeof(Designation))]
        public IHttpActionResult PostDesignation(Designation designation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Designations.Add(designation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = designation.DesignationID }, designation);
        }

        // DELETE api/Designation/5
        [ResponseType(typeof(Designation))]
        public IHttpActionResult DeleteDesignation(int id)
        {
            Designation designation = db.Designations.Find(id);
            if (designation == null)
            {
                return NotFound();
            }

            db.Designations.Remove(designation);
            db.SaveChanges();

            return Ok(designation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DesignationExists(int id)
        {
            return db.Designations.Count(e => e.DesignationID == id) > 0;
        }
    }
}