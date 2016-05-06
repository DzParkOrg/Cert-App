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
    public class CertificationRequestController : ApiController
    {
        private trackerEntities db = new trackerEntities();

        // GET api/CertificationRequest
        [Route("api/certification/getApprovalList/")]
        [HttpGet]
        public IHttpActionResult GetCertificationRequests()
        {
            var approvalList = (from req in db.CertificationRequests
                                join cert in db.Certifications on req.RequestForCertificationId equals cert.CertificationId
                                join certCat in db.CertificationCategories on req.CertificationCategory equals certCat.CertificationCategoryId
                                join user in db.UserDetails on req.RequestByUserId equals user.UserId 
                                join emp in db.Employees on user.EmployeeId equals emp.EmployeeID
                                select new
                                {
                                    certificationDetails = cert.CertificationCode + "-" + cert.CertificationTitle,
                                    req.RequestDate,
                                    req.amount,
                                    req.Remarks,
                                    req.RequestId,
                                    req.IRA,
                                    req.Status,
                                    req.RequestByUserId,
                                    req.RequestForCertificationId,
                                    req.quarter,
                                    req.CertificationCategory,
                                    req.ApproverRemarks,
                                    certCat.CertificationCategoryTitle ,
                                    name = emp.FirstName + " " + emp.LastName
                                }).ToList();
            return Ok(approvalList);
        }

        // GET api/CertificationRequest/5
        [ResponseType(typeof(CertificationRequest))]
        public IHttpActionResult GetCertificationRequest(int id)
        {
            CertificationRequest certificationrequest = db.CertificationRequests.Find(id);
            if (certificationrequest == null)
            {
                return NotFound();
            }

            return Ok(certificationrequest);
        }

        // PUT api/CertificationRequest/5
        [Route("api/certification/updateCertificationStatus/{id}")]
        [HttpPut]
        public IHttpActionResult PutCertificationRequest(int id, CertificationRequest certificationrequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != certificationrequest.RequestId)
            {
                return Ok("error");
            }

            db.Entry(certificationrequest).State = EntityState.Modified;

            try
            {
                certificationrequest.CreationTS = DateTime.Now;
                certificationrequest.LastModdifiedTS = DateTime.Now;
                certificationrequest.LastModdifiedbyUserId = certificationrequest.RequestByUserId;
                certificationrequest.CreatedByUserId = certificationrequest.RequestByUserId;
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CertificationRequestExists(id))
                {
                    return Ok("error");
                }
                else
                {
                    throw;
                }
            }

            return Ok("success");
        }

        // POST api/CertificationRequest
        [ResponseType(typeof(CertificationRequest))]
        [Route("api/certificate/nomination/")]
        [HttpPost]
        public IHttpActionResult PostCertificationRequest(CertificationRequest certificationrequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            certificationrequest.RequestDate = DateTime.Now;
            certificationrequest.CreationTS = DateTime.Now;
            certificationrequest.LastModdifiedTS = DateTime.Now;
            certificationrequest.LastModdifiedbyUserId = certificationrequest.RequestByUserId;
            certificationrequest.CreatedByUserId = certificationrequest.RequestByUserId;
            certificationrequest.Status = "Pending";
            db.CertificationRequests.Add(certificationrequest);
            db.SaveChanges();

            return Ok("success");
        }

        // DELETE api/CertificationRequest/5
        [ResponseType(typeof(CertificationRequest))]
        public IHttpActionResult DeleteCertificationRequest(int id)
        {
            CertificationRequest certificationrequest = db.CertificationRequests.Find(id);
            if (certificationrequest == null)
            {
                return NotFound();
            }

            db.CertificationRequests.Remove(certificationrequest);
            db.SaveChanges();

            return Ok(certificationrequest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CertificationRequestExists(int id)
        {
            return db.CertificationRequests.Count(e => e.RequestId == id) > 0;
        }
    }
}