using System;
using System.Collections.Generic;
using System.Data;
//using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ
{
    class Linq2Objects
    {
        string[] names = { "Jon", "Bon", "Shyam", "Tina", "Meena", "Eena" };
        static List<Product> products = new List<Product>();


        private static void GetAllProducts()
        {
            if (products.Count == 0)
            {
                products.Add(new Product() { Category = "Laptops", Id = 1, Name = "Lenovo Ideapad", Rate = 70000 });
                products.Add(new Product() { Category = "Laptops", Id = 2, Name = "Nokia Inspiron", Rate = 160000 });
                products.Add(new Product() { Category = "Mobiles", Id = 3, Name = "Zumia Lenovo Lumia 710", Rate = 15000 });
            }
        }

        public static Array GetProductSubset()
        {
            GetAllProducts();
            Array objArray = (from product in products
                              select new { product.Category, product.Name }).ToArray();
            return objArray;
        }

        public static List<Product> GetProducts()
        {
            GetAllProducts();
            List<Product> result = (from product in products
                                    select product).ToList();
            return result;
        }

        public static IEnumerable<IGrouping<int,int>> GroupBy()
        {
            List<int> numbers = new List<int>() { 35, 44, 200, 84, 3987, 4, 199,
                                                329, 446, 208 };
            var query = from number in numbers
                        group number by number % 2;

            var q = (from product in products
                                group product by product.Name into temp
                     select temp.Key).ToList();

            
            foreach (var group in query)
            {
                Console.WriteLine(group.Key == 0 ? "\nEven numbers:" : "\nOdd numbers:");
                foreach (int i in group)
                    Console.WriteLine(i);
            }
            return query;

        }

        public void GetProduct(double rate, string category)
        {
            GetAllProducts();
            List<Product> result = (from product in products
                                    where product.Rate <= rate
                                    && product.Category == category
                                    select product
                              ).ToList();

            foreach (Product item in result)
            {
                Console.WriteLine("Name: {0}", item.Name);
                Console.WriteLine("Category: {0}", item.Category);
                Console.WriteLine("Rate: {0}", item.Rate);
            }

        }
        public static Product GetProduct(string productName)
        {
            GetAllProducts();
            Product result = (from product in products
                              where product.Name.Contains(productName)
                              select product
                              ).ToList().FirstOrDefault();
            return result;
        }

        public static List<Product> OrderByProducts()
        {
            GetAllProducts();
            List<Product> results = products.OrderByDescending(x => x.Rate).OrderByDescending(x => x.Name).ToList();

            List<Product> linqResult = (from product in products
                                        orderby product.Rate, product.Name descending
                                        select product).ToList();
            return linqResult;

        }

        public static void Aggregate()
        {
            double[] doubles = { 1.7, 2.3, 1.9, 4.1, 2.9 };

            double product = doubles.Aggregate((runningProduct, nextFactor) => runningProduct * nextFactor);

            Console.WriteLine("Total product of all numbers: {0}", product);
        }

        public static DataTable GroupProductsByCategory()
        {
            GetAllProducts();

            DataTable result = new DataTable();
            result.Columns.Add("Grouping");
            result.Columns.Add("Name");
            result.Columns.Add("Rate");


            //1 level group by
            products.GroupBy((Product y) => y.Category)
                .ToList()
                .ForEach(x =>   //List<IGroup<>, Product>
                {
                    foreach (var item in x)
                    {
                        result.Rows.Add(new object[] { x.Key, item.Name, item.Rate });
                        Console.WriteLine("Item: {0} Category:{1}", item.Name, x.Key);
                    }

                });
            //2 level group by
            products.GroupBy((Product y) => y.Category)
                .ToList()
                .GroupBy(m => "Rate")
                .ToList()
                .ForEach(x =>
            {
                foreach (var group in x)
                {
                    foreach (Product item in group)
                    {
                        result.Rows.Add(new object[] { x.Key, item.Name, item.Rate });
                        Console.WriteLine("Item: {0} Category:{1}", item.Name, x.Key);
                    }
                }

            });
            return result;


        }

        public void TakeParticularValue()
        {
            if (products.Count() == 0)
            {
                GetAllProducts();
            }

            IEnumerable<Product> SingleProduct = products.Where(p => p.Category == "Laptops").Take(1);
            Console.WriteLine("Taking a single value from the list");
            foreach (Product item in SingleProduct)
            {
                Console.WriteLine(item.Name);
            }

        }

        public static List<Product> SkipWhile()
        {
            if (products.Count() == 0)
            {
                GetAllProducts();
            }
            IEnumerable<Product> skippedItems = products.OrderBy(x => x.Name)
                                                .SkipWhile(x => (x.Name.Contains("Lenovo")));


            foreach (Product item in skippedItems)
            {
                Console.WriteLine(item.Name);
            }

            return skippedItems.ToList();
        }



        public void SumUsingLinq()
        {
            GetAllProducts();
            var concatResult = products.Concat(products);  //for concatenation of 2 lists
            var distinctResult = products.Select(p => p.Name).Distinct();    //Get distinct Values of a particular column
            double result = products.Sum((Product p) => p.Rate);
            Console.WriteLine("Sum of rates of all products: {0}", result);

        }

        public void SimpleJoin()
        {
            GetAllProducts();
            var result = from product in products
                         join shopProduct in new Shop1().GetShop1ProductList()
                         on product.Id equals shopProduct.Id into temp_lst
                         from temp_item in temp_lst.DefaultIfEmpty()
                         select new
                         {
                             ProductClassProduct = product.Name,
                             ShopProductName = temp_item.Name,
                             ProductClassId = product.Id,
                             ShopProductId = temp_item.Id


                         };

            foreach (var item in result)
            {
                Console.WriteLine("Product Detail -------");
                Console.WriteLine("Product Class Name: {0}", item.ProductClassProduct);
                Console.WriteLine("Shop Product Class Name: {0}", item.ShopProductName);
                Console.WriteLine("Product Class Id: {0}", item.ProductClassId);
                Console.WriteLine("Shop Product Class Id: {0}", item.ShopProductId);
            }
        }

        public static IEnumerable<object> Joins()
        {
            GetAllProducts();

            //Using Lambda Expressions
            Customer.GetCustomers().ForEach(customer =>
            {
                var joinResult = customer.Purchases.Join(products, //Class to be joined
                                          (pr => pr.Id),                //Property of above class used for the join
                                           (purchase => purchase.Id),   //Property of the other class used for the join
                                           ((cust, product) => new { Products = product, Purchases = cust }))   //select data
                                           .Distinct()                  //Get Distinct Data
                                           .ToList();                   //Convert to List
                joinResult.ForEach(item => Console.WriteLine("Product Detail: {0} | {1}", item.Products.Name, item.Purchases.Rate));

            }
                );

            (from customer in Customer.GetCustomers()   //Customer.GetCustomers().join
                    join product in products                    //List to be joined (viz. first param in lambda join)
                    on customer.CustomerId equals product.Id    //Join condition (is the 2nd & 3rd param of lamda join)
                    select new { customer.CustomerName, customer.CustomerId, product.Category, product.Rate }   //select required items (last param of lambda join)
                    ).ToList().ForEach(item => { Console.WriteLine(item.CustomerName); });
            
            //Using Linq
            return (from customer in Customer.GetCustomers()   //list 1
                    from purchase in customer.Purchases    //sub-list = list 2
                    join product in products       //list 3
                    on purchase.Id equals product.Id   //join list2 to list 3
                    select new
                    {
                        customer.CustomerName,
                        total = customer.Purchases.Sum(p=> { return p.Rate; })
                    }).Distinct();

            
            //foreach (var item in result.Distinct())
            //{
            //    Console.WriteLine("Details for a customer----------");
            //    Console.WriteLine(item.CustomerName);
            //    Console.WriteLine(item.total);
            //}

        }

        public static void GroupJoins()
        {
            GetProducts();
            var result = products.GroupJoin(new Shop1().GetShop1ProductList(), p => p.Id, sh => sh.Id, (sh, pr) => new { key = sh.Name, Products = pr });
            result.ToList().ForEach(item =>
            {
                Console.WriteLine("Key: {0}", item.key);
                foreach (var subItem in item.Products)
                {
                    Console.WriteLine("Sub-Item details: {0} | {1}", subItem.Category, subItem.Rate);
                }
            });
        }

        public void Sum()
        {
            if (products.Count() == 0)
            {
                GetAllProducts();
            }
            Console.WriteLine(products.Sum(x => x.Rate));

        }



    }
}
