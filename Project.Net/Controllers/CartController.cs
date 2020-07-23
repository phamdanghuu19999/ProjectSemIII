using Project.Net.Models.DataModel;
using Project.Net.Models.Respositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Net.Controllers
{
    public class CartController : Controller
    {
		Respository<Product> _products;
		Respository<Project.Net.Models.DataModel.Attributes> _attributes;
		Respository<Order> _orders;
		Respository<ProductAttr> _proAttr;
		public CartController()
		{
			_products = new Respository<Product>();
            _attributes = new Respository<Project.Net.Models.DataModel.Attributes>();
			_orders = new Respository<Order>();
			_proAttr = new Respository<ProductAttr>();
        }
		// GET: Cart
		public ActionResult Index()
        {
			ViewBag.Attrs = _attributes.GetAll().AsQueryable().Include(x => x.AttributeTypes);
			return View();
        }
		[HttpPost]
		public void AddToCart(Item itemCart)
		{
		
			var attrs = itemCart.Attributes.Split(',');
			var colorId = attrs[0];			
			var _product = _products.Get(itemCart.ProductId);
            //Thêm vào giỏ hàng
            var itemCarts = new ItemCart();
			itemCarts.Product = _product;
			var p = _proAttr.GetBy(x => x.ProductId == itemCart.ProductId && x.AttrId == Convert.ToInt32(colorId));
			foreach (var item in p)
			{
				itemCart.price = item.PriceByColor;
			}
			itemCarts.Quantity = itemCart.Quantity;
            itemCarts.Attributes = itemCart.Attributes;
			itemCarts.price = itemCart.price;
            List<ItemCart> lst = new List<ItemCart>();
			if (Session["cart"]!=null)
			{
				lst = Session["cart"] as List<ItemCart>;
				var check = false;
				foreach (var item in lst)
				{
					if (item.Product.ProductId== itemCart.ProductId && item.Attributes == itemCart.Attributes )
					{
						item.Quantity += item.Quantity;
						check = true;
					}
				}
				if (!check)
				{
                    lst.Add(itemCarts);
                }

			}
			else
			{
                lst.Add(itemCarts);
            }
			Session["cart"] = lst;
		}
		public ActionResult Payment()
		{
			if (Session["cart"] == null)
			{
				return RedirectToAction("Index");
			}
			List<ItemCart> lst = new List<ItemCart>();
			lst = Session["cart"] as List<ItemCart>;
			if (lst.Count==0)
			{
				return RedirectToAction("Index");
			}
			return View();
		}
		[HttpPost]
		public ActionResult Payment(Order order)
		{
            var price = 0;
           
			order.Created = DateTime.Now;
			order.Status = 1;
			List<ItemCart> lst = new List<ItemCart>();
			lst = Session["cart"] as List<ItemCart>;
			List<OrderDetails> orderDetails = new List<OrderDetails>();
			foreach (var item in lst)
			{
                //if (item.Product.SalePrice != null)
                //{
                //    price = (int)item.Product.SalePrice;
                //}
                //else
                //{
                //    price = (int)item.Product.Price;
                //}
                OrderDetails od = new OrderDetails()
                {
                    ProductId = item.Product.ProductId,
                    Attributes = item.Attributes,
                    Quantity = item.Quantity,
                    Price = item.price,
				
				};
				orderDetails.Add(od);
			}
			order.OrderDetails = orderDetails;
			if (!_orders.Add(order))
			{
				return View();
			}
            string _header = System.IO.File.ReadAllText(Server.MapPath(@"~/App_Data/header_order.txt"));
            string _footer = System.IO.File.ReadAllText(Server.MapPath(@"~/App_Data/footer_order.txt"));
            #region Thông tin người nhận
            string _main = String.Format(@"<h2 class='title'>ĐƠN HÀNG ONETECH{5}</h2>
				<p>
					<b>Họ tên người nhận</b>
					<span>{0}</span>
				</p>
				<p>
					<b>Email</b>
					<span>{1}</span>
				</p>
				<p>
					<b>SĐT</b>
					<span>{2}</span>
				</p>
				<p>
					<b>Địa chỉ</b>
					<span>{3}</span>
				</p>
				<p>
					<b>Ngày mua</b>
					<span>{4:dd/MM/yyyy | HH:mm:ss}</span>
				</p>", order.FullName, order.Email, order.Phone, order.Address, order.Created, order.OrderId);
            #endregion
            #region Sản phẩm mua
            double total = 0;
            _main += @"<table class='table text-center'>
					<thead>
						<tr>
							<th>Sản phẩm</th>
							<th>Đơn giá</th>
							<th>Số lượng</th>
							<th>Thành tiền</th>
						</tr>
					</thead>
					<tbody>";
            foreach (var item in lst)
            {
                _main += @"<tr>";
                _main += String.Format(@"<td>{0}</td>", item.Product.ProductName);
                _main += String.Format(@"<td>${0:#,##}</td>", price);
                _main += String.Format(@"<td>{0}</td>", item.Quantity);
                _main += String.Format(@"<td>${0:#,##}</td>", item.price * item.Quantity);
                _main += @"</tr>";
                total += item.price * item.Quantity;
            }
            _main += String.Format(@"<tr>
							<th colspan='3' style='text-align: right;margin-right:60px;'>Tổng tiền</th>
							<th>${0:#,##}</th>
						</tr>
					</tbody>
				</table>", total);
            #endregion
            Helpers.SendEmail(order.Email, "quantrivienwebsite.bkap@gmail.com", "quantriwebbkap", "[BKAPSHOP]_THÔNG TIN ĐƠN HÀNG", _header + _main + _footer);
            Session.Remove("cart");
           
            return RedirectToAction("Index","Home");

        }
        [HttpPost]
        public ActionResult UpdateCart(int id,int qty,string attr)
        {
            var lstCart = Session["cart"] as List<ItemCart>;
            foreach (var item in lstCart)
            {
                if (item.Product.ProductId == id && item.Attributes == attr)
                {
                    item.Quantity = qty;
                  
                }
            }
            Session["cart"] = lstCart;
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        public ActionResult RemoveItemCart(int id,string attr)
        {
            var lstCart = Session["cart"] as List<ItemCart>;
			foreach (var item in lstCart.ToList())
			{
				if (item.Product.ProductId == id && item.Attributes == attr)
				{
					lstCart.Remove(item);

				}
			}
			
            
            return RedirectToAction("Index");
        }
    }
}