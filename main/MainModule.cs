using E_Commerce_Application.Dao;

namespace E_Commerce_Application.Main
{
    internal class MainModule
    {
        public void menu()
        {
            IMenu menu = new Menu();

            Console.WriteLine("Welcome to .NET Ecommerce Platform\n");
            restart:
            Console.WriteLine("Please Select from below\n");
            Console.WriteLine("1. Register Customer\n2. Create Product.\n3. Delete Product.\n4. Add to cart.\n5. Remove from Cart\n6. View cart.\n7. Place order\n8. View Customer Order\n9. Cancel Order\n10. Delete Customer\n11. View All Customers\n12. View All Products\n13. View All Carts\n14. View All Orders\n15. View All Order Item Details\n");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    menu.createCustomer();
                    break;
                case 2:
                    menu.createProduct();
                    break;
                case 3:
                    menu.deleteProduct();
                    break;
                case 4:
                    menu.addToCart();
                    break;
                case 5:
                    menu.removeFromCart();
                    break;
                case 6:
                    menu.getAllFromCart();
                    break;
                case 7:
                    menu.placeOrder();
                    break;
                case 8:
                    menu.getOrdersByCustomer();
                    break;
                case 9:
                    menu.cancelOrder();
                    break;
                case 10:
                    menu.deleteCustomer();
                    break;
                case 11:
                    menu.viewCustomers();
                    break;
                case 12:
                    menu.viewProducts();
                    break;
                case 13:
                    menu.viewCart();
                    break;
                case 14:
                    menu.viewOrders();
                    break;
                case 15:
                    menu.viewOrderDetails();
                    break;
                default:
                    Console.WriteLine("\nTry Again\n");
                    break;
            }
            goto restart;
        }
    }
}
