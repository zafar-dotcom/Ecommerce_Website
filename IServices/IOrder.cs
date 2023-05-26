using OnLineShop.Models;

namespace OnLineShop.IServices
{
    public interface IOrder
    {
        void InsertOrder(Order order);
        List<Order> GetOrders();
        int GetOrderCount();
    }
}
