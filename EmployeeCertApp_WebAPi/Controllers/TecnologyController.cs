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
    public class TecnologyController : ApiController
    {
        private trackerEntities db = new trackerEntities();

        // GET api/Tecnology
        [Route("api/certificate/getCertificateCategories/")]
        [HttpGet]
        public IQueryable<CertificationCategory> GetCertificationCategories()
        {
            return db.CertificationCategories;
        }

       
        // PUT api/Tecnology/5
        public IHttpActionResult PutCertificationCategory(int id, CertificationCategory certificationcategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != certificationcategory.CertificationCategoryId)
            {
                return BadRequest();
            }

            db.Entry(certificationcategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CertificationCategoryExists(id))
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

        // POST api/Tecnology
        [ResponseType(typeof(CertificationCategory))]
        public IHttpActionResult PostCertificationCategory(CertificationCategory certificationcategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CertificationCategories.Add(certificationcategory);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CertificationCategoryExists(certificationcategory.CertificationCategoryId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = certificationcategory.CertificationCategoryId }, certificationcategory);
        }

        // DELETE api/Tecnology/5
        [ResponseType(typeof(CertificationCategory))]
        public IHttpActionResult DeleteCertificationCategory(int id)
        {
            CertificationCategory certificationcategory = db.CertificationCategories.Find(id);
            if (certificationcategory == null)
            {
                return NotFound();
            }

            db.CertificationCategories.Remove(certificationcategory);
            db.SaveChanges();

            return Ok(certificationcategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CertificationCategoryExists(int id)
        {
            return db.CertificationCategories.Count(e => e.CertificationCategoryId == id) > 0;
        }
    }
}