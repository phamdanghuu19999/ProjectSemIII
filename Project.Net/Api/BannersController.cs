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
using Project.Net.Models.DataModel;
using Project.Net.Models.DbContext;

namespace Project.Net.Api
{
    [RoutePrefix("api/Banners")]
    public class BannersController : ApiController
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: api/Banners
        [Route("")]
        public IQueryable<Banner> GetBanners()
        {
            return db.Banners.Where(x => x.Status == true).Include(x=>x.Notes);
        }

        // GET: api/Banners/5
        [ResponseType(typeof(Banner))]
        public IHttpActionResult GetBanner(int id)
        {
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return NotFound();
            }

            return Ok(banner);
        }

        // PUT: api/Banners/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBanner(int id, Banner banner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != banner.Id)
            {
                return BadRequest();
            }

            db.Entry(banner).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BannerExists(id))
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

        // POST: api/Banners
        [ResponseType(typeof(Banner))]
        public IHttpActionResult PostBanner(Banner banner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Banners.Add(banner);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = banner.Id }, banner);
        }

        // DELETE: api/Banners/5
        [ResponseType(typeof(Banner))]
        public IHttpActionResult DeleteBanner(int id)
        {
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return NotFound();
            }

            db.Banners.Remove(banner);
            db.SaveChanges();

            return Ok(banner);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BannerExists(int id)
        {
            return db.Banners.Count(e => e.Id == id) > 0;
        }
    }
}