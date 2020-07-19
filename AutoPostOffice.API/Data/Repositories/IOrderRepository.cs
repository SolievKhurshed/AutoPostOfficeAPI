using AutoPostOffice.API.Data.Entities;
using System.Collections.Generic;

namespace AutoPostOffice.API.Data.Repositories
{
    public interface IOrderRepository
    {
        Order GetOrder(int orderNumber);
        bool AddOrder(Order order);
        int GetLastOrderId();
        bool CancelOrder(int orderNumber, string description);
        bool AlterOrder(int orderNumber, Order order);
        IEnumerable<OrderTracking> GetOrderHistory(int orderNumber);
    }
}
