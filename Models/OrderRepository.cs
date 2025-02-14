using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Query.Internal;
using GentsOutfits_Authentication_.Models;
using System.Data;
using static Dapper.SqlMapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GentsOutfits_Authentication_.Models
{
    public class OrderRepository:IOrder,IRepository<OrderDetail>
    {
        static string connect = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=GentsOutfits;Integrated Security=True";
        IRepository<OrderDetail> _repository=new GenericRepository<OrderDetail>("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=GentsOutfits;Integrated Security=True");
        public void Update(OrderDetail o)
        {
            _repository.Update(o);
        }


        public void Add(OrderDetail o)
        {

            using (var connection = new SqlConnection(connect))
            {
                connection.Open();


                string query = "INSERT INTO OrderDetail (Id,PlacedAt,Name,Address,City,Country,PostalCode) VALUES (@Id,@PlacedAt,@Name,@Address,@City,@Country,@PostalCode);";
                connection.Execute(query, o);
            }
        }
        public IEnumerable<OrderDetail> GetAll()
        {
            return _repository.GetAll();
        }
        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public OrderDetail GetById(int id)
        {
           return _repository.GetById(id);
        }
        public List<CartItems> getOrderProducts(string id)
        { 
         List<CartItems> cartItems = new List<CartItems>();

            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                string query = "SELECT * FROM CartItems WHERE UserId = @UserId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CartItems cartItem = new CartItems
                            {
                                UserId = reader["UserId"].ToString(),
                                ProductId = Convert.ToInt32(reader["ProductId"]),
                                Name = reader["Name"].ToString(),
                                Price = Convert.ToDecimal(reader["Price"]),
                                Discprice = Convert.ToDecimal(reader["Discprice"]),
                                Image = reader["Image"].ToString(),
                                Category = reader["Category"].ToString(),
                                Quantity = Convert.ToInt32(reader["Quantity"])
                            };
                    cartItems.Add(cartItem);
                        }
                    }
                }
            }
            return cartItems;
        }
        public void AddOrderProduct(List<OrderProduct> product)
        {
            using (var connection = new SqlConnection(connect))
            {
                connection.Open();
                var query = $"INSERT INTO OrderProduct (OrderId,ProductId) VALUES (@OrderId,@ProductId);";
                connection.Execute(query, product);
            }
        }
        public void deleteCartItems(string userId)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                string query = "delete FROM CartItems WHERE UserId = @UserId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
