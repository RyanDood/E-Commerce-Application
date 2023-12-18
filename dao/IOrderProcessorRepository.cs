using Ecommerce_Application.Entities;

namespace Ecommerce_Application.Dao
{
    public interface IOrderProcessorRepository
    {
        public bool createProduct(Products product);
        public bool createCustomer(Customers customer);
        public bool deleteProduct(int id);
        public bool deleteCustomer(int id);
        public bool addToCart(Customers customer,Products product,int quantity);
        public bool removeFromCart(Customers customer, Products product);
        public List<object> getAllFromCart(Customers customer);
        public bool placeOrder(Customers customer, Dictionary<Products,int> productsWithQuantity,string address);
        public List<object> getOrdersByCustomer(int customerID);
        public bool cancelOrder(int orderID);
        public List<Customers> getAllCustomers();
        public List<Products> getAllProducts();
        public List<Cart> getAllCartDetails();
        public List<Orders> getAllOrders();
        public List<Order_Items> getAllOrderItemsDetails();
    }
}
