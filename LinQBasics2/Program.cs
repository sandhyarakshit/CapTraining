﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinQBasics2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Customer c1 = new Customer { CustomerId = 1001, Email = "sreehariis@gmail.com", Name = "Sree" };
            Customer c2 = new Customer { CustomerId = 1002, Email = "kate@gmail.com", Name = "Kate" };
            Customer c3 = new Customer { CustomerId = 1003, Email = "mark@gmail.com", Name = "Mark" };

            Product p1 = new Product { ProductId = 1, Name = "Coffee", UnitPrice = 100.5F };
            Product p2 = new Product { ProductId = 2, Name = "Tea", UnitPrice = 120 };
            Product p3 = new Product { ProductId = 3, Name = "Shampoo", UnitPrice = 200 };
            Product p4 = new Product { ProductId = 4, Name = "Brushes", UnitPrice = 100 };

            Order o1 = new Order { CustomerId = c1.CustomerId, ProductId = p1.ProductId, OrderDate = DateTime.Now, OrderId = 1, OrderStatus = OrderStatus.DELIVERED };
            Order o2 = new Order { CustomerId = c2.CustomerId, ProductId = p1.ProductId, OrderDate = DateTime.Now, OrderId = 2, OrderStatus = OrderStatus.ONPROGRESS };
            Order o3 = new Order { CustomerId = c3.CustomerId, ProductId = p2.ProductId, OrderDate = DateTime.Now, OrderId = 3, OrderStatus = OrderStatus.OURFORDELIEVERY };
            Order o4 = new Order { CustomerId = c1.CustomerId, ProductId = p3.ProductId, OrderDate = DateTime.Now, OrderId = 4, OrderStatus = OrderStatus.DELIVERED };
            Order o5 = new Order { CustomerId = c1.CustomerId, ProductId = p4.ProductId, OrderDate = DateTime.Now, OrderId = 5, OrderStatus = OrderStatus.DELIVERED };


            List<Customer> customers = new List<Customer>();
            customers.Add(c1);
            customers.Add(c2);
            customers.Add(c3);

            List<Product> products = new List<Product>();
            products.Add(p1);
            products.Add(p2);
            products.Add(p3);
            products.Add(p4);

            List<Order> orders = new List<Order>();
            orders.Add(o1);
            orders.Add(o2);
            orders.Add(o3);
            orders.Add(o4);
            orders.Add(o5);

            var orderList = from order in orders
                         join customer in customers on order.CustomerId equals customer.CustomerId
                         join product in products on order.ProductId equals product.ProductId
                         //where order.OrderStatus == OrderStatus.OURFORDELIEVERY
                         select new { OrderId = order.OrderId, Name = customer.Name, Product = product.Name, order.OrderStatus };

            var orderList2 = orders.Join(customers
                                    , customer => customer.CustomerId
                                    , order => order.CustomerId
                                    , (order, customer) => new { OrderId = order.OrderId, Name = customer.Name, order.OrderStatus });

            //foreach (var order in orderList)
            //    Console.WriteLine($"{order.OrderId} {order.Product} {order.Name} {order.OrderStatus}");

            //var firstItem = orders.First(d => d.OrderStatus == OrderStatus.OURFORDELIEVERY);
            //Console.WriteLine(firstItem.OrderId);

            //var fistItem2 = orders.FirstOrDefault(d => d.OrderStatus == OrderStatus.PENDING);
            //if (fistItem2 != null)
            //    Console.WriteLine(firstItem.OrderId);
            //else
            //    Console.WriteLine("There are no items that is pending");

            //var lastItem = orders.Last(d => d.OrderStatus == OrderStatus.DELIVERED);
            //Console.WriteLine(lastItem.OrderId);

            //var lastItem2 = orders.LastOrDefault(d => d.OrderStatus == OrderStatus.PENDING);
            //if (lastItem2 != null)
            //    Console.WriteLine(lastItem2.OrderId);
            //else
            //    Console.WriteLine("There are no items that is pending");


            //if (orders.Any(d => d.CustomerId == 1001 && d.OrderStatus == OrderStatus.PENDING))
            //    Console.WriteLine("There are pending items for customer 10001");
            //else
            //    Console.WriteLine("All items are delivered for customer 10001");

            //if (orders.Where(d => d.CustomerId == 10002).All(d => d.OrderStatus == OrderStatus.DELIVERED))
            //{
            //    Console.WriteLine("All items are delivered for customer 10001");
            //}
            //else
            //    Console.WriteLine("There are still items to be delivered");

            for(int i = 1004; i< 1100;i++)
            {
                customers.Add(new Customer { CustomerId = i, Name = "Customer " + i, Email = "customer@gmail.com" });
            }

            //var top20Customers = customers.Take(20);

            //foreach(var c in top20Customers)
            //{
            //   Console.WriteLine(c.ToString());
            //}

            //var top20Customers = customers.Skip(20).Take(20);

            //foreach (var c in top20Customers)
            //{
            //    Console.WriteLine(c.ToString());
            //}

            var groupByProduct = orders.GroupBy(d => d.ProductId);

            //GROUP KEY,COUNT
            //      - ITEM 1
            //      - ITEM 2

            foreach(var group in groupByProduct)
            {
                Console.WriteLine($"PRODUCT : {group.Key}, TOTAL ORDERS : {group.Count()}");
                foreach (var order in group)
                    Console.WriteLine($"\t Customer Id : {order.CustomerId} {order.OrderStatus}");
            }

        }
    }
}
