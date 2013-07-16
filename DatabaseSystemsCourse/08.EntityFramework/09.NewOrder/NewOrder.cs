﻿using System;
using EntityFramework.Library;

//you can see order details with this query
//SELECT TOP 1000 [OrderID],[ProductID],[Quantity]
//  FROM [northwind].[dbo].[Order Details]
//  WHERE Quantity > 100

namespace _09.NewOrder
{
    class NewOrder
    {
        static northwindEntities db = new northwindEntities();

        static void Main(string[] args)
        {
            //<productID, quantity>
            Tuple<int, int> firstProduct = new Tuple<int, int>(1, 999);
            Tuple<int, int> secondProduct = new Tuple<int, int>(3, 999);
            InsertOrder("CHOPS", DateTime.Now, "Lukovit", firstProduct, secondProduct);
        }

        static void InsertOrder(string customerID, DateTime orderDate, string address, params Tuple<int, int>[] products)
        {
            Order newOrder = new Order()
            {
                CustomerID = customerID,
                OrderDate = orderDate,
                ShipAddress = address
            };

            var insertedOrder = db.Orders.Add(newOrder);

            foreach (var prod in products)
            {
                Order_Detail newDetail = new Order_Detail()
                {
                    OrderID = insertedOrder.OrderID,
                    ProductID = prod.Item1,
                    Quantity = (short)prod.Item2
                };

                db.Order_Details.Add(newDetail);
            }

            db.SaveChanges();
        }
    }
}

