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
    public class CertificationCategoriesController : ApiController
    {
        private trackerEntities db = new trackerEntities();

        // GET api/CertificationCategories
        [Route("api/certification/getCertificationCategories/")]
        [HttpGet]
        public IQueryable<CertificationCategory> GetCertificationCategories()
        {
            return db.CertificationCategories;
        }

        // GET api/CertificationCategories/5
        [ResponseType(typeof(CertificationCategory))]
        public IHttpActionResult GetCertificationCategory(int id)
        {
            CertificationCategory certificationcategory = db.CertificationCategories.Find(id);
            if (certificationcategory == null)
            {
                return NotFound();
            }

            return Ok(certificationcategory);
        }

        // PUT api/CertificationCategories/5
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

        // POST api/CertificationCategories
        [ResponseType(typeof(CertificationCategory))]
        [Route("api/certification/postCertificationCategories/")]
        [HttpPost]
        public IHttpActionResult PostCertificationCategory(CertificationCategory certificationcategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CertificationCategories.Add(certificationcategory);
            db.SaveChanges();

            return Ok("success");
        }

        // DELETE api/CertificationCategories/5
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