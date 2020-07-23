using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Net.Models.DataModel;
using Project.Net.Models.DbContext;
using Project.Net.Models.Respositories;

namespace Project.Net.Controllers
{
    [RoutePrefix("Products")]
    public class ProductsController : Controller
    {
		Respository<ProductAttr> _prAttr;
		private ProjectDbContext db = new ProjectDbContext();
		// GET: Products
		public ProductsController()
		{
			_prAttr = new Respository<ProductAttr>();
		}
        public ActionResult Index()
        {
            return View();
        }
        [Route("ProductInCate/{id:int}")]
        [HttpGet]
        public ActionResult ProductInCate(int id)
        {
            ViewBag.id = id;
            return View("Index");
        }

        [Route("Search/{name}")]
        public ActionResult SearchProduct(string name)
        {
            ViewBag.name = name;
            return View("Index");
        }

        private bool ProductExists(int id)
        {
		
			return db.Products.AsQueryable().Include(x => x.ProductAttrs).Count(x => x.ProductId == id) > 0;
        }
        
        public ActionResult ProductDetails(int id)
        {
            if (ProductExists(id))
            {

            }
            return View();
        }
        [Route("Details/{id:int}")]
        [HttpGet]
        public ActionResult Details(int id)
        {
            if (ProductExists(id))
            {

                ViewBag.id = id;
                return View();
            }
            return RedirectToAction("Index");
        }
		public ActionResult GetPrice(int id, int colorId,int price)
		{
		
			var p = _prAttr.GetBy(x=>x.ProductId==id && x.AttrId==colorId).ToList();
			foreach (var item in p)
			{
				price = (int)item.PriceByColor;
			}
			
			return Json(new {
				Id = id, Color = colorId, Price = price
			}, JsonRequestBehavior.AllowGet);
		}
    }
}