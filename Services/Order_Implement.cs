using MySql.Data.MySqlClient;
using OnLineShop.IServices;
using OnLineShop.Models;
using System.Security.Cryptography;

namespace OnLineShop.Services
{
    public class Order_Implement : IOrder
    {
        private readonly string str;
        public Order_Implement()
        {
            str = "server=localhost;port=3306;uid=root;pwd=sobiazafar@2023;database=mvc_crud";
        }



        public void InsertOrder(Order order)
        {
            using (MySqlConnection connection = new MySqlConnection(str))
            {
                string orderQuery = "INSERT INTO ordertable (orderno, name, phoneno, email, address, orderdate) " +
                                    "VALUES (@OrderNo, @Name, @PhoneNo, @Email, @Address, @OrderDate)";

                string orderDetailQuery = "INSERT INTO orderdetail (orderid, productid) VALUES (@OrderId, @ProductId)";

                using (MySqlCommand orderCommand = new MySqlCommand(orderQuery, connection))
                using (MySqlCommand orderDetailCommand = new MySqlCommand(orderDetailQuery, connection))
                {
                    connection.Open();

                    orderCommand.Parameters.AddWithValue("@OrderNo", order.OrderNo);
                    orderCommand.Parameters.AddWithValue("@Name", order.Name);
                    orderCommand.Parameters.AddWithValue("@PhoneNo", order.PhoneNo);
                    orderCommand.Parameters.AddWithValue("@Email", order.Email);
                    orderCommand.Parameters.AddWithValue("@Address", order.Address);
                    orderCommand.Parameters.AddWithValue("@OrderDate", order.OrderDate);

                    orderCommand.ExecuteNonQuery();

                    int orderId = (int)orderCommand.LastInsertedId;

                    foreach (var orderDetail in order.Order_detail)
                    {
                        orderDetailCommand.Parameters.Clear();
                        orderDetailCommand.Parameters.AddWithValue("@OrderId", orderId);
                        orderDetailCommand.Parameters.AddWithValue("@ProductId", orderDetail.ProductId);
                        orderDetailCommand.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }
        }
        public List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();

            using (MySqlConnection connection = new MySqlConnection(str))
            {
                string query = "SELECT id, orderno, name, phoneno, email, address, orderdate FROM ordertable";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Order order = new Order
                            {
                                Id = reader.GetInt32("id"),
                                OrderNo = reader.GetString("orderno"),
                                Name = reader.GetString("name"),
                                PhoneNo = reader.GetString("phoneno"),
                                Email = reader.GetString("email"),
                                Address = reader.GetString("address"),
                                OrderDate = reader.GetDateTime("orderdate")
                            };

                            orders.Add(order);
                        }
                    }
                }
            }

            return orders;
        }


        public int GetOrderCount()
        {
            int count = 0;

            using (MySqlConnection connection = new MySqlConnection(str))
            {
                string query = "SELECT COUNT(*) FROM ordertable";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    count = Convert.ToInt32(command.ExecuteScalar());
                }
            }

            return count;
        }
    }
}
