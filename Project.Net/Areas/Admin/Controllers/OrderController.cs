using Project.Net.Models.DataModel;
using Project.Net.Models.Respositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Net.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        Respository<Order> _odr;
        Respository<OrderDetails> _orderDetails;
       
        Respository<Product> _products;
        Respository<Attributes> _attributes;
        public OrderController()
        {
            _odr = new Respository<Order>();
            _products = new Respository<Product>();
            _orderDetails = new Respository<OrderDetails>();
            _attributes = new Respository<Attributes>();
        }
        // GET: Admin/Order
        public ActionResult Index(string table_search)
        {
            var data = _odr.GetAll().AsQueryable().Include(x=>x.Users);
            if (!String.IsNullOrEmpty(table_search))
            {
                data = data.AsQueryable().Where(x => x.Users.FullName.Contains(table_search.ToLower()));

                //data = data.Where(x => x.ProductName.Contains(table_search.ToLower())).ToList();

                // Dữ lại tham số tìm kiếm
                ViewBag.key = table_search;
            }
            return View(data);
        }
        public ActionResult Details(int id)
        {
            // Thông tin Order (thông tin mua hàng) và thông tin sản phẩm đã mua
            var order = _odr.GetAll().AsQueryable()
                //.Include(x => x.OrderDetails) // Chi tiết đơn hàng
                .Include(x => x.Users)
                .FirstOrDefault(x => x.OrderId == id);
            // Lấy chi tiết đơn hàng
            var orderDetails = _orderDetails
                .GetBy(x => x.OrderId == id);
            List<OrderDetailsViewModel> listODVM = new List<OrderDetailsViewModel>();
            foreach (var item in orderDetails)
            {
                OrderDetailsViewModel odv = new OrderDetailsViewModel();
                odv.Product = _products.Get(item.ProductId);
                odv.Quantity = item.Quantity;
                odv.Price = item.Price;
                odv.Money = item.Quantity * item.Price;
                // tách chuỗi mã attributes
                var _attrs = item.Attributes.Split(',');
                // lấy list các attributes
                List<Attributes> _lstAttrs = new List<Attributes>();
                foreach (var attrId in _attrs)
                {
                    _lstAttrs.Add(_attributes
                        .GetBy(x => x.Id == Convert.ToInt32(attrId)).AsQueryable()
                        .Include(x => x.AttributeTypes).First());
                }
                odv.Attributes = _lstAttrs;
                listODVM.Add(odv);
            }
           
          
            // Đưa và View
            ViewBag.orderDetails = listODVM;
            return View(order);
        }
        public ActionResult Update(int id, int status)
        {
            var order = _odr.Get(id);
            // Trường hợp ko hợp lệ
            if (order.Status > status && order.Status != 1)
            {
                TempData["msg"] = new ResponseMessage()
                {
                    Type = "callout-danger",
                    Message = "Cập nhật không hợp lệ!"
                };
                return RedirectToAction("Details", new { id = id });
            }
            order.Status = Convert.ToByte(status);
            _odr.Save();
            return RedirectToAction("Index");
        }
    }
}