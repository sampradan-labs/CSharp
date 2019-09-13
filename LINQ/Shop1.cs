using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ
{
    class Shop1
    {
        public string Name { get; set; }
        public Product ShopProduct { get; set; }

        public List<Product> GetShop1ProductList()
        {
            List<Product> products = new List<Product>();
            products.Add(new Product() { Category = "Laptops", Id = 2, Name = "Lenovo Inspiron", Rate = 160000 });
            products.Add(new Product() { Category = "Mobiles", Id = 3, Name = "Nokia Lumia 710", Rate = 15000 });
            products.Add(new Product() { Category = "Mobile OS", Id = 20, Rate = 10000, Name = "Android" });
            return products;

        }

    }
}
