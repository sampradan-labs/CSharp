using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LINQ
{
    class Program
    {

        public static List<Product> GetProducts() {
            List<Product> lstProducts = new List<Product>();
            lstProducts.Add(new Product() { Id = 1, Name = "Iphone", Category = "Mobiles", Rate = 50000 });
            lstProducts.Add(new Product() { Id = 2, Name = "Lenovo", Category = "Laptops", Rate = 70000 });
            lstProducts.Add(new Product() { Id = 3, Name = "Lavie clutches", Category = "Clutches", Rate = 2000 });
            lstProducts.Add(new Product() { Id = 4, Name = "Hats", Category = "Summer Wear", Rate = 200 });
            lstProducts.Add(new Product() { Id = 5, Name = "Rupa Thermals", Category = "Winter Wear", Rate = 1000 });

            return lstProducts;
        }

       
        static void Main(string[] args)
        {
            //Get all products with a rate > 10000
            List<Product> result = (from singleProduct in GetProducts()
                         where singleProduct.Rate > 10000
                         select singleProduct).ToList();
            Customer Customer = new Customer();


            //Using Lambda Expressions
            //Customer.GetCustomers().ForEach(customer => 
            //        {
            //            var joinResult = customer.Purchases.Join(GetProducts(), //Class to be joined
            //                                      (pr => pr.Id),                //Property of above class used for the join
            //                                       (purchase => purchase.Id),   //Property of the other class used for the join
            //                                       ((cust, product) => new { Products = product, Purchases = cust }))   //select data
            //                                       .Distinct()                  //Get Distinct Data
            //                                       .ToList();                   //Convert to List
            //            joinResult.ForEach(item => Console.WriteLine("Product Detail: {0} | {1}", item.Products.Name, item.Purchases.Rate));

            //        }
            //    );


            var joinResult = GetProducts().Join(Customer.GetCustomers(),
                                                p => { return p.Id; },
                                                c => { return c.CustomerId; },
                                                (pr, cust) =>
                                                {
                                                    return new
                                                    {
                                                        CustomerId = cust.CustomerId,
                                                        CustomerName = cust.CustomerName,
                                                        Purchases = cust.Purchases,
                                                        ShopCategory = pr.Category
                                                    };
                                                }
                                                ).ToList();

            joinResult.ForEach(item =>
            {
                Console.WriteLine("Customer Details for : {0} - {1}", item.CustomerId, item.CustomerName);
                item.Purchases.ForEach(singlePurchase =>
                {
                    Console.WriteLine("## Purchase Detail: {0} | {1} | {2}",
                                            singlePurchase.Name,
                                            singlePurchase.Category,
                                            singlePurchase.Rate);
                });

            });
               



            Console.ReadKey();
        }

        private static void Loop(Product result)
        {
            Console.WriteLine(result.Name);
        }
    }
}
