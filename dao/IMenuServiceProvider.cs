namespace E_Commerce_Application.Dao
{
    internal interface IMenuServiceProvider
    {
        public void createProduct();
        public void createCustomer();
        public void deleteProduct();
        public void deleteCustomer();
        public void addToCart();
        public void removeFromCart();
        public void getAllFromCart();
        public void placeOrder();
        public void getOrdersByCustomer();
        public void cancelOrder();
        public void viewCustomers();
        public void viewProducts();
        public void viewCart();
        public void viewOrders();
        public void viewOrderDetails();
    }
}
