using AutoPostOffice.API.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AutoPostOffice.API.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private static IList<Order> Orders { get; } = new List<Order>()
        {
            new Order
                {
                    OrderNumber = 1,
                    Status = OrderStatus.InStock,                    
                    Products = new List<Product>()
                                    {
                                        new Product { Name = "Book", Amount = 1},
                                        new Product { Name = "CD", Amount = 1}
                                    },
                    Cost = 250.89m,
                    PostNumber = "POST-11111",
                    RecipientPhoneNumber = "+7903-587-12-55",
                    RecipientFirstName = "Хуршед",
                    RecipientLastName = "Солиев",
                    RecipientMiddleName = "Юнусович"
                },
                new Order
                {
                    OrderNumber = 2,
                    Status = OrderStatus.IssuedToCourier,                   
                    Products = new List<Product>() { new Product { Name = "Boots", Amount = 2} },
                    Cost = 366.10m,
                    PostNumber = "POST-22222",
                    RecipientPhoneNumber = "+7903-968-47-23",
                    RecipientFirstName = "Джек",
                    RecipientLastName = "Блек"
                }
        };

        private static IList<OrderTracking> OrdersTracking { get; } = new List<OrderTracking>()
        {
            new OrderTracking
            {
                ChangesCount = 0,
                Modified = DateTime.Now,
                ChangeType = ChangeType.NoChanges,
                OrderNumber = 1,
                Status = OrderStatus.InStock,
                Products = new List<Product>()
                                {
                                    new Product { Name = "Book", Amount = 1},
                                    new Product { Name = "CD", Amount = 1}
                                },
                Cost = 250.89m,
                PostNumber = "POST-11111",
                RecipientPhoneNumber = "+7903-587-12-55",
                RecipientFirstName = "Хуршед",
                RecipientLastName = "Солиев",
                RecipientMiddleName = "Юнусович"
            },
            new OrderTracking
            {
                ChangesCount = 0,
                Modified = DateTime.Now,
                ChangeType = ChangeType.NoChanges,
                OrderNumber = 2,
                Status = OrderStatus.IssuedToCourier,
                Products = new List<Product>() { new Product { Name = "Boots", Amount = 2} },
                Cost = 366.10m,
                PostNumber = "POST-22222",
                RecipientPhoneNumber = "+7903-968-47-23",
                RecipientFirstName = "Джек",
                RecipientLastName = "Блек"
            }
        };

        public Order GetOrder(int orderNumber)
        {
            return Orders.Where(c => c.OrderNumber == orderNumber).FirstOrDefault();
        }

        public bool AddOrder(Order order)
        {
            if (order != null)
            {
                int orderId = GetLastOrderId();
                order.OrderNumber = ++orderId;
                order.Status = OrderStatus.Registered;

                Orders.Add(order);

                //сохраняем историю
                var newOrderTracking = new OrderTracking()
                {
                    ChangesCount = 0,
                    Modified = DateTime.Now,
                    ChangeType = ChangeType.NoChanges,
                    OrderNumber = order.OrderNumber,
                    Status = order.Status,
                    Products = order.Products,
                    Cost = order.Cost,
                    PostNumber = order.PostNumber,
                    RecipientPhoneNumber = order.RecipientPhoneNumber,
                    RecipientFirstName = order.RecipientFirstName,
                    RecipientLastName = order.RecipientLastName,
                    RecipientMiddleName = order.RecipientMiddleName
                };
                OrdersTracking.Add(newOrderTracking);

                return true;
            }
            return false;
        }

        public int GetLastOrderId()
        {
            return Orders.OrderBy(item => item.OrderNumber).Last().OrderNumber;
        }

        public bool CancelOrder(int orderNumber, string description)
        {
            var order = GetOrder(orderNumber);
            if (order != null)
            {
                order.Status = OrderStatus.Cancelled;
                order.Description = description;
                return true;
            }
            return false;
        }

        public bool AlterOrder(int orderNumber, Order alteredOrder)
        {
            var order = Orders.Where(c => c.OrderNumber == orderNumber).FirstOrDefault();

            if (order != null)
            {
                order.Cost = alteredOrder.Cost ?? order.Cost;
                order.RecipientPhoneNumber = alteredOrder.RecipientPhoneNumber ?? order.RecipientPhoneNumber;
                order.RecipientFirstName = alteredOrder.RecipientFirstName ?? order.RecipientFirstName;
                order.RecipientLastName = alteredOrder.RecipientLastName ?? order.RecipientLastName;
                order.RecipientMiddleName = alteredOrder.RecipientMiddleName ?? order.RecipientMiddleName;

                if (alteredOrder.Products.Count() == 0 || alteredOrder.Products == null)
                {
                    order.Products = order.Products;
                }
                else
                {
                    order.Products = alteredOrder.Products;
                }


                //сохраняем изменения
                var orderTracking = OrdersTracking.Where(c => c.OrderNumber == orderNumber).ToList();
                if (orderTracking.Count > 0)
                {
                    int trackingCount = orderTracking.Count()-1;
                    var newOrderTracking = new OrderTracking()
                    {
                        ChangesCount = ++trackingCount,
                        Modified = DateTime.Now,
                        ChangeType = ChangeType.OrderContent,
                        OrderNumber = order.OrderNumber,
                        Status = order.Status,
                        Products = order.Products,
                        Cost = order.Cost,
                        PostNumber = order.PostNumber,
                        RecipientPhoneNumber = order.RecipientPhoneNumber,
                        RecipientFirstName = order.RecipientFirstName,
                        RecipientLastName = order.RecipientLastName,
                        RecipientMiddleName = order.RecipientMiddleName
                    };
                    OrdersTracking.Add(newOrderTracking);
                }
                return true;
            }
            return false;
        }

        public IEnumerable<OrderTracking> GetOrderHistory(int orderNumber)
        {
            return OrdersTracking.Where(c => c.OrderNumber == orderNumber).ToList();
        }
    }
}
