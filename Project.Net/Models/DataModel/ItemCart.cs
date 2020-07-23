using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Net.Models.DataModel
{
	public class ItemCart
	{
		public Product Product{ get; set; }
		public string  Attributes { get; set; }
		public int Quantity { get; set; }
		public float price { get; set; }
	}

    public class Item
    {
        public int ProductId { get; set; }
        public string Attributes { get; set; }
        public int Quantity { get; set; }
		public float price { get; set; }
	}
}