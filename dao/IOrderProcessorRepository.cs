using Ecommerce_Application.Entities;

namespace Ecommerce_Application.Dao
{
    internal interface IOrderProcessorRepository
    {
        public abstract bool createProduct(Products product);
        public abstract bool createCustomer(Customers customer);
        public abstract bool deleteProduct(int id);
        public abstract bool deleteCustomer(int id);
        public abstract bool addToCart(Customers customer,Products product,int quantity);
        public abstract bool removeFromCart(Customers customer, Products product);
        public abstract List<object> getAllFromCart(Customers customer);
        public abstract bool placeOrder(Customers customer, Dictionary<Products,int> productsWithQuantity,string address);
        public List<object> getOrdersByCustomer(int customerID);
        public abstract bool cancelOrder(int orderID);
        public abstract List<Customers> getAllCustomers();
        public abstract List<Products> getAllProducts();
        public abstract List<Cart> getAllCartDetails();
        public abstract List<Orders> getAllOrders();
        public abstract List<Order_Items> getAllOrderItemsDetails();
    }
}
