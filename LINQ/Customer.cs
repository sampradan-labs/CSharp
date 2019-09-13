using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ
{
    class Customer
    {
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }
        public List<Product> Purchases { get; set; }


        public static List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();
            customers.Add(new Customer()
            {
                CustomerId = 1,
                CustomerName = "Rekha",
                Purchases = new List<Product>(){
                                                 new Product(){ Id=1, Name="Lenovo Ideapad", Category="Laptops", Rate=70000},
                                                 new Product(){ Id=3, Name="Nokia Lumia 710", Category="Mobiles", Rate=15000}
                                                                                                            }
            });
            customers.Add(new Customer()
            {
                CustomerId = 2,
                CustomerName = "Nishant",
                Purchases = new List<Product>(){
                                                 new Product(){ Id=3, Name="Nokia Lumia 710", Category="Mobiles", Rate=15000}
                                                                                                            }
            });
            return customers;
        }
    }
}
