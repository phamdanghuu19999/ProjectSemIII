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
    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: api/Users
        [Route("")]
        [HttpGet]
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserId)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [Route("")]
        [ResponseType(typeof(User))]
        [HttpPost]
        public IHttpActionResult PostUser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();

            return Ok(user);
        }

        [Route("Login")]
        [HttpPost]
        public IHttpActionResult Login(UserLogin user)
        {
            var cus = db.Users.SingleOrDefault(x => x.UserName==user.UserName&&x.Password==user.Password);
            if (cus != null)
            {
                return Ok(cus);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("getUsername")]
        [HttpGet]
        public IHttpActionResult getUsername(string username)
        {
            var cus = db.Users.SingleOrDefault(x => x.UserName == username);
            if (cus != null)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserId == id) > 0;
        }
    }
}