using PagedList;
using Project.Net.Models.DataModel;
using Project.Net.Models.Respositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Net.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        Respository<Product> _product;
        Respository<Category> _cat;
        Respository<Supplier> _sup;
        Respository<AttributeType> _attr;
        Respository<ProductAttr> _prAttr;
        Respository<Note> _note;
        Respository<ProductNote> _proNote;
		Respository<OrderDetails> _od;
        public ProductsController()
        {
            _product = new Respository<Product>();
            _cat = new Respository<Category>();
            _sup = new Respository<Supplier>();
            _attr = new Respository<AttributeType>();
            _prAttr = new Respository<ProductAttr>();
            _note = new Respository<Note>();
            _proNote = new Respository<ProductNote>();
			_od = new Respository<OrderDetails>();

        }
        // GET: Admin/Products
        public ActionResult Index(int page =1, int pageSize = 6,string table_search=null)
        {
            var data = _product.GetAll().AsQueryable().Include(x => x.Categories).Include(x => x.Suppliers);
            if (!String.IsNullOrEmpty(table_search))
            {
                data = data.AsQueryable().Where(x=>x.ProductName.Contains(table_search.ToLower()));
                
                 //data = data.Where(x => x.ProductName.Contains(table_search.ToLower())).ToList();
                
                    // Dữ lại tham số tìm kiếm
                    ViewBag.key = table_search;
            }

            return View(data.OrderBy(x => x.ProductId).ToPagedList(page, pageSize));
        }
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_cat.GetAll(), "CategoryId", "CategoryName");
            ViewBag.SupplierId = new SelectList(_sup.GetAll(), "SupplierId", "SupplierName");
            ViewBag.AttrType = _attr.GetAll().AsQueryable().Include(x=>x.Attributes).AsEnumerable();
            ViewBag.proNote = _note.GetBy(x => x.Type == 2).AsEnumerable();
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Product p,IEnumerable<HttpPostedFileBase> file)
        {
            ViewBag.CategoryId = new SelectList(_cat.GetAll(), "CategoryId", "CategoryName");
            ViewBag.SupplierId = new SelectList(_sup.GetAll(), "SupplierId", "SupplierName");
            ViewBag.AttrType = _attr.GetAll().AsQueryable().Include(x => x.Attributes).AsEnumerable();
            ViewBag.proNote = _note.GetBy(x=>x.Type==2).AsEnumerable();
            if (p.ProductAttrs != null)
            {
                p.ProductAttrs.ToList().ForEach(x => x.ProductId = p.ProductId);
            }
            if (p.ProductNotes != null)
            {
                p.ProductNotes.ToList().ForEach(x => x.ProductId = p.ProductId);
            }
            if (ModelState.IsValid)
            {
                if (_product.Add(p))
                {
                    return RedirectToAction("Index");
                }
                return View();
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            
            ViewBag.AttrType = _attr.GetAll().AsQueryable().Include(x => x.Attributes).AsEnumerable();
            ViewBag.proNote = _note.GetBy(x => x.Type == 2).AsEnumerable();

            var data = _product.GetAll().AsQueryable().Include(x => x.ProductAttrs).Include(x=>x.ProductNotes).FirstOrDefault(x => x.ProductId == id);
            ViewBag.CategoryId = new SelectList(_cat.GetAll(), "CategoryId", "CategoryName",data.CategoryId);
            ViewBag.SupplierId = new SelectList(_sup.GetAll(), "SupplierId", "SupplierName",data.SupplierId);
           
            return View(data);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(Product p)
        {
            ViewBag.CategoryId = new SelectList(_cat.GetAll(), "CategoryId", "CategoryName",p.CategoryId);
            ViewBag.SupplierId = new SelectList(_sup.GetAll(), "SupplierId", "SupplierName",p.SupplierId);
            ViewBag.proNote = _note.GetBy(x => x.Type == 2).AsEnumerable();
            ViewBag.AttrType = _attr.GetAll().AsQueryable().Include(x => x.Attributes).AsEnumerable();
            if (p.ProductAttrs != null)
            {
                _prAttr.RemoveRange(_prAttr.GetBy(x => x.ProductId == p.ProductId));
                 p.ProductAttrs.ToList().ForEach(x => x.ProductId = p.ProductId);
                _prAttr.AddRange(p.ProductAttrs);
            }
            if (p.ProductNotes != null)
            {
                _proNote.RemoveRange(_proNote.GetBy(x => x.ProductId == p.ProductId));
                p.ProductNotes.ToList().ForEach(x => x.ProductId = p.ProductId);
                _proNote.AddRange(p.ProductNotes);
            }
            if (ModelState.IsValid)
            {
                if (_product.Edit(p))
                {
                    return RedirectToAction("Index");
                }
                return View(p);
            }
            return View(p);
        }
        public ActionResult Delete(int id)
        {
            
            try
            {
				if (_od.GetAll().Any(x=>x.ProductId == id))
				{
					TempData["msg"] = new ResponseMessage()
					{
						Type = "alert-danger",
						Message = "Failure! Products are in the order"
					};
					return RedirectToAction("Index");
				}
                //Xóa thuộc tính sp
                _prAttr.RemoveRange(_prAttr.GetBy(x => x.ProductId == id));
                _proNote.RemoveRange(_proNote.GetBy(x => x.ProductId == id));
                _product.Remove(id);
                TempData["msg"] = new ResponseMessage()
                {
                    Type = "alert-success",
                    Message = "Delete Successfully!"
                };
            }
            catch (Exception ex)
            {

                TempData["msg"] = new ResponseMessage()
                {
                    Type="alert-danger",
                    Message=ex.Message
                };
            }
            return RedirectToAction("Index");
        }
    }
   
}