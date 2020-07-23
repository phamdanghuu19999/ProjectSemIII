using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using Project.Net.Models.DataModel;
using Project.Net.Models.DbContext;

namespace Project.Net.Api
{
    [RoutePrefix("api/Products")]
    public class ProductsController : ApiController
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: api/Products
        [Route("")]
        public IQueryable<Product> GetProducts()
        {
            return db.Products.Include(p => p.Categories).Include(x => x.ProductNotes);
        }
        [Route("ProductInCate/{id:int}")]
        [HttpGet]
        public IQueryable<Product> ProductInCate(int id)
        {
            return db.Products.Where(x => x.Status == true).Where(x => x.CategoryId == id);
        }

        [Route("ComponentProduct")]
        [HttpGet]
        public Dictionary<string, IQueryable> ComponentProduct()
        {
            var deal = db.Products.Include(p => p.Categories).Include(x => x.ProductNotes).Where(x => x.ProductNotes.Any(b => b.Notes.AttrName.Equals("deal of week"))).Take(3);
            var trend = db.Products.Include(p => p.Categories).Include(x => x.ProductNotes).Where(x => x.ProductNotes.Any(b => b.Notes.AttrName.Equals("trend"))).Take(3);
            var news = db.Products.Include(p => p.Categories).Include(x => x.ProductNotes).Where(x => x.ProductNotes.Any(b => b.Notes.AttrName.Equals("new"))).Take(16);
            var normal = db.Products.Include(p => p.Categories).Include(x => x.ProductNotes).Where(x => x.ProductNotes.Any(b => b.Notes.AttrName.Equals("feature"))).Take(16);
            var sale = db.Products.Include(p => p.Categories).Include(x => x.ProductNotes).Where(x => x.ProductNotes.Any(b => b.Notes.AttrName.Equals("sale"))).Take(16);
            var data = new Dictionary<string, IQueryable>
            {
                { "deal" ,deal },
                {"trend",trend },
                {"new",news },
                {"feature",normal },
                {"sale",sale }
            };
            return data;
        }

        [Route("SearchProduct")]
        [HttpGet]
        public IQueryable SearchProduct(string name)
        {
            return db.Products.Where(x => x.ProductName.Contains(name.ToLower()));
        }

        // GET: api/Products/5
        [Route("{id:int}")]
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
			Product product = db.Products.Include(x=>x.ProductAttrs).FirstOrDefault(x=>x.ProductId==id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

      
	}
}