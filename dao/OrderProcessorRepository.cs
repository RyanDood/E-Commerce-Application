using E_Commerce_Application.exception;
using E_Commerce_Application.Util;
using Ecommerce_Application.Entities;
using System.Data.SqlClient;

namespace Ecommerce_Application.Dao
{
    public class OrderProcessorRepository : IOrderProcessorRepository
    {

        string connectionString = DBConnUtil.getConnectionString();

        public bool createCustomer(Customers customer)
        {
            bool status = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = "insert into Customers values(@name,@email,@password)";
                    sqlCommand.Parameters.AddWithValue("@name", customer.CustomerName);
                    sqlCommand.Parameters.AddWithValue("@email", customer.CustomerEmail);
                    sqlCommand.Parameters.AddWithValue("@password", customer.CustomerPassword);
                    sqlCommand.Connection = sqlConnection;
                    sqlConnection.Open();
                    int createCustomerStatus = sqlCommand.ExecuteNonQuery();
                    if (createCustomerStatus > 0)
                    {
                        status = true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                status = false;
            }
            return status;
        }

        public bool deleteCustomer(int id)
        {
            bool status = false;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "delete from Customers where customer_id = @customerID";
                sqlCommand.Parameters.AddWithValue("@customerID", id);
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();
                int deleteCustomerStatus = sqlCommand.ExecuteNonQuery();
                if (deleteCustomerStatus == 0)
                {
                    throw new CustomerNotFoundException($"\nCustomer ID {id} does not exist");
                }
                else if (deleteCustomerStatus > 0)
                {
                    status = true;
                }
            }

            return status;
        }

        public bool createProduct(Products product)
        {
            bool status = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = "insert into Products values(@name,@amount,@description,@quantity)";
                    sqlCommand.Parameters.AddWithValue("@name", product.ProductName);
                    sqlCommand.Parameters.AddWithValue("@amount", product.Price);
                    sqlCommand.Parameters.AddWithValue("@description", product.Description);
                    sqlCommand.Parameters.AddWithValue("@quantity", product.StockQuantity);
                    sqlCommand.Connection = sqlConnection;
                    sqlConnection.Open();
                    int createProductStatus = sqlCommand.ExecuteNonQuery();
                    if (createProductStatus > 0)
                    {
                        status = true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                status = false;
            }
            return status;
        }

        public bool deleteProduct(int id)
        {
            bool status = false;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "delete from Products where product_id = @productID";
                sqlCommand.Parameters.AddWithValue("@productID", id);
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();
                int deleteProductStatus = sqlCommand.ExecuteNonQuery();
                if (deleteProductStatus == 0)
                {
                    throw new ProductNotFoundException($"\nProduct ID {id} does not exist");
                }
                else if (deleteProductStatus > 0)
                {
                    status = true;
                }
            }
            return status;
        }

        public bool addToCart(Customers customer, Products product, int quantity)
        {
            bool status = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = "insert into Cart values(@customerID,@productID,@quantity)";
                    sqlCommand.Parameters.AddWithValue("@customerID", customer.CustomerID);
                    sqlCommand.Parameters.AddWithValue("@productID", product.ProductID);
                    sqlCommand.Parameters.AddWithValue("@quantity", quantity);
                    sqlCommand.Connection = sqlConnection;
                    sqlConnection.Open();
                    int createCartStatus = sqlCommand.ExecuteNonQuery();
                    if (createCartStatus > 0)
                    {
                        status = true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write($"\n{e.Message}");
                status = false;
            }
            return status;
        }

        public bool removeFromCart(Customers customer, Products product)
        {
            bool status = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = "delete from Cart where customer_id = @customerID and product_id = @productID";
                    sqlCommand.Parameters.AddWithValue("@customerID", customer.CustomerID);
                    sqlCommand.Parameters.AddWithValue("@productID", product.ProductID);
                    sqlCommand.Connection = sqlConnection;
                    sqlConnection.Open();
                    int deleteCartStatus = sqlCommand.ExecuteNonQuery();
                    if (deleteCartStatus == 0)
                    {
                        throw new CartNotFoundException($"\nCart containing both Customer ID {customer.CustomerID} and Product ID {product.ProductID} does not exist");
                    }
                    else if (deleteCartStatus > 0)
                    {
                        status = true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                status = false;
            }
            return status;
        }

        public List<object> getAllFromCart(Customers customer)
        {
            List<object> getUserCarts = new List<object>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = "select Products.product_id,name,price,description,total from Products inner join (select customer_id,product_id,sum(quantity) as total from Cart group by customer_id,product_id) C on Products.product_id = C.product_id where customer_id =  @customerID";
                    sqlCommand.Parameters.AddWithValue("@customerID", customer.CustomerID);
                    sqlCommand.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        var getUserCart = new {ProductID = (int)reader["product_id"],ProductName = (string)reader["name"],ProductPrice = (int)reader["price"],Description = (string)reader["description"], QuantityAdded = (int)reader["total"]};
                        getUserCarts.Add(getUserCart);
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            return getUserCarts;
        }

        public bool placeOrder(Customers customer, Dictionary<Products, int> productsWithQuantity, string address)
        {
            bool status = false;
            string date = DateTime.Now.ToString("yyyy/MM/dd");
            int price = 0;

            foreach (var productQuantity in productsWithQuantity)
            {
                price = price + productQuantity.Key.Price * productQuantity.Value;
            }

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = "insert into Orders(customer_id,order_date,total_price,shipping_address) output inserted.order_id values (@customerID,@orderDate,@price,@address)";
                    sqlCommand.Parameters.AddWithValue("@customerID", customer.CustomerID);
                    sqlCommand.Parameters.AddWithValue("@orderDate", date);
                    sqlCommand.Parameters.AddWithValue("@price", price);
                    sqlCommand.Parameters.AddWithValue("@address", address);
                    sqlCommand.Connection = sqlConnection;
                    sqlConnection.Open();
                    int orderID = (int)sqlCommand.ExecuteScalar();

                    if (orderID != null)
                    {
                        status = true;
                    }

                    foreach (var productQuantity in productsWithQuantity)
                    {
                        bool result = placeOrderItems(orderID, productQuantity.Key.ProductID, productQuantity.Value);
                        if (result == false)
                        {
                            status = false;
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.Write($"\n{e.Message}");
                status = false;
            }
            return status;
        }

        public bool placeOrderItems(int orderID,int productID, int productQuantity)
        {
            bool status = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = "insert into Order_Items(order_id,product_id,quantity) values (@orderID,@productID,@quantity)";
                    sqlCommand.Parameters.AddWithValue("@orderID", orderID);
                    sqlCommand.Parameters.AddWithValue("@productID", productID);
                    sqlCommand.Parameters.AddWithValue("@quantity", productQuantity);
                    sqlCommand.Connection = sqlConnection;
                    sqlConnection.Open();
                    int createCartStatus = sqlCommand.ExecuteNonQuery();
                    if (createCartStatus > 0)
                    {
                        status = true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write($"\n{e.Message}");
                status = false;
            }
            return status;
        }
 
        public List<object> getOrdersByCustomer(int customerID)
        {
            List<object> getUserOrders = new List<object>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = "select customer_id,O.order_id,Products.product_id,name,price,quantity from Products inner join (select customer_id,Orders.order_id,product_id,quantity from Orders inner join Order_Items on Orders.order_id = Order_Items.order_id) O on Products.product_id = O.product_id where customer_id = @customerID";
                    sqlCommand.Parameters.AddWithValue("@customerID", customerID);
                    sqlCommand.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        var getUserOrder = new { CustomerID = (int)reader["customer_id"], OrderID = (int)reader["order_id"], ProductID = (int)reader["product_id"], ProductName = (string)reader["name"], ProductPrice = (int)reader["price"], ProductQuantity = (int)reader["quantity"] };
                        getUserOrders.Add(getUserOrder);
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            return getUserOrders;
        }

        public bool cancelOrder(int orderID)
        {
            bool status = false;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "delete from Orders where order_id = @orderID";
                sqlCommand.Parameters.AddWithValue("@orderID", orderID);
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();
                int deleteOrderStatus = sqlCommand.ExecuteNonQuery();
                if (deleteOrderStatus == 0)
                {
                    throw new OrderNotFoundException($"\nOrder ID {orderID} does not exist");
                }
                else if (deleteOrderStatus > 0)
                {
                    status = true;
                }
            }
            return status;
        }

        public List<Customers> getAllCustomers()
        {
            List<Customers> getCustomers = new List<Customers>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = "select * from Customers";
                    sqlCommand.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        Customers customer = new Customers((int)reader["customer_id"], (string)reader["name"], (string)reader["email"], (string)reader["password"]);
                        getCustomers.Add(customer);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return getCustomers;
        }

        public List<Products> getAllProducts()
        {
            List<Products> getProducts = new List<Products>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = "select * from Products";
                    sqlCommand.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        Products product = new Products((int)reader["product_id"], (string)reader["name"], (int)reader["price"], (string)reader["description"], (int)reader["stockQuantity"]);
                        getProducts.Add(product);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return getProducts;
        }

        public List<Cart> getAllCartDetails()
        {
            List<Cart> getCarts = new List<Cart>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = "select * from Cart";
                    sqlCommand.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        Cart cart = new Cart((int)reader["cart_id"], (int)reader["customer_id"], (int)reader["product_id"], (int)reader["quantity"]);
                        getCarts.Add(cart);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return getCarts;
        }

        public List<Orders> getAllOrders()
        {
            List<Orders> getOrders = new List<Orders>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = "select * from Orders";
                    sqlCommand.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        DateTime date = (DateTime)reader["order_date"];
                        string convertedDate = date.ToString("yyyy/MM/dd");
                        Orders order = new Orders((int)reader["order_id"], (int)reader["customer_id"], convertedDate, (int)reader["total_price"], (string)reader["shipping_address"]);
                        getOrders.Add(order);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return getOrders;
        }

        public List<Order_Items> getAllOrderItemsDetails()
        {
            List<Order_Items> getOrderItems = new List<Order_Items>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = "select * from Order_Items";
                    sqlCommand.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        Order_Items order_item = new Order_Items((int)reader["order_item_id"], (int)reader["order_id"], (int)reader["product_id"], (int)reader["quantity"]);
                        getOrderItems.Add(order_item);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return getOrderItems;
        }

    }
}
