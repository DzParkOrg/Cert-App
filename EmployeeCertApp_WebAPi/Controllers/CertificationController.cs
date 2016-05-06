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
    public class CertificationController : ApiController
    {
        private trackerEntities db = new trackerEntities();

        // GET api/Certification
        [Route("api/certificate/getCertificationList/")]
        [HttpGet]
        public IQueryable<Certification> GetCertifications()
        {
            return db.Certifications;
        }

        // GET api/Certification/5
        [ResponseType(typeof(Certification))]
        [Route("api/certificate/getCertification/{employeeId}")]
        [HttpGet]
        public IHttpActionResult GetCertification(int employeeId)
        {
            //get certification category id from employee table 
            Employee employee = db.Employees.FirstOrDefault(m=> m.EmployeeID == employeeId);
            //get all certifications from that skill id. 

            var certificationList = db.Certifications.Where(m=> m.CertificationCategoryId == employee.CertificationCategoryId).ToList();
            if (certificationList == null)
                {
                    return Ok("no certificates");
                }

            return Ok(certificationList);
        }

        [ResponseType(typeof(Certification))]
        [Route("api/certificate/getCertificationPerCategoryId/{id}")]
        [HttpGet]
        public IHttpActionResult GetCertificationCategory(int id)
        {
            var certification = db.Certifications.Where(m => m.CertificationCategoryId == id).ToList();
            if (certification == null)
            {
                return NotFound();
            }

            return Ok(certification);
        }


        // PUT api/Certification/5
        [Route("api/certificate/updateCertificate/")]
        [HttpPut]
        public IHttpActionResult PutCertification(Certification certification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (id != certification.CertificationId)
            //{
            //    return BadRequest();
            //}

            certification.CreatedByUserId = 1;
            certification.CreationTS = DateTime.Now;
            certification.LastModdifiedbyUserId = 1;
            certification.LastModdifiedTS = DateTime.Now;

            db.Entry(certification).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CertificationExists(certification.CertificationId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //return StatusCode(HttpStatusCode.OK);
            return Ok("success");
        }

        // POST api/Certification
        [ResponseType(typeof(Certification))]
        [Route("api/certification/postCertification/")]
        [HttpPost]
        public IHttpActionResult PostCertification(Certification certification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            certification.CreatedByUserId = 1;
            certification.CreationTS = DateTime.Now;
            certification.LastModdifiedbyUserId = 1;
            certification.LastModdifiedTS = DateTime.Now;

            db.Certifications.Add(certification);
            db.SaveChanges();

            return Ok("success");
        }

        // DELETE api/Certification/5
        [ResponseType(typeof(Certification))]
        public IHttpActionResult DeleteCertification(int id)
        {
            Certification certification = db.Certifications.Find(id);
            if (certification == null)
            {
                return NotFound();
            }

            db.Certifications.Remove(certification);
            db.SaveChanges();

            return Ok(certification);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CertificationExists(int id)
        {
            return db.Certifications.Count(e => e.CertificationId == id) > 0;
        }
    }
}