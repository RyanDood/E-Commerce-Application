using E_Commerce_Application.exception;
using E_Commerce_Application.Exception;
using Ecommerce_Application.Dao;
using Ecommerce_Application.Entities;

namespace E_Commerce_Application.Dao
{
    internal class MenuServiceProvider:IMenuServiceProvider
    {
        IOrderProcessorRepository orderProcessorRepository = new OrderProcessorRepository();

        public void createProduct()
        {
            try
            {
                Console.WriteLine("\nEnter Product Name:");
                string inputProductName = Console.ReadLine();
                Console.WriteLine("\nEnter Product Amount:");
                int inputProductAmount = int.Parse(Console.ReadLine());
                Console.WriteLine("\nEnter Product Desciption:");
                string inputProductDescription = Console.ReadLine();
                Console.WriteLine("\nEnter Product Quantity:");
                int inputProductQuantity = int.Parse(Console.ReadLine());
                if(inputProductAmount > 0 && inputProductQuantity > 0)
                {
                    Products product = new Products(0, inputProductName, inputProductAmount, inputProductDescription, inputProductQuantity);
                    if (orderProcessorRepository.createProduct(product))
                    {
                        Console.WriteLine($"\nProduct {inputProductName} was added successfully\n");
                    }
                    else
                    {
                        Console.WriteLine($"Unable to Add the Product\n");
                    }
                }
                else
                {
                    throw new InvalidValueException($"Invalid Value");
                }
            }
            catch(System.Exception e)
            {
                Console.WriteLine($"\n{ e.Message}\n");
            }
        }

        public void createCustomer()
        {
            try
            {
                Console.WriteLine("\nEnter Customer Name:");
                string inputCustomerName = Console.ReadLine();
                Console.WriteLine("\nEnter Customer Email:");
                string inputCustomerEmail = Console.ReadLine();
                Console.WriteLine("\nEnter Customer Password:");
                string inputCustomerPassword = Console.ReadLine();
                Customers newCustomer = new Customers(0, inputCustomerName, inputCustomerEmail, inputCustomerPassword);
                if (orderProcessorRepository.createCustomer(newCustomer))
                {
                    Console.WriteLine($"\nHooray! {inputCustomerName}, you have completed the registration\n");
                }
                else
                {
                    Console.WriteLine($"Registration was not successful\n");
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"\n{e.Message}\n");
            }
        }

        public void deleteProduct()
        {
            try
            {
                Console.WriteLine("\nEnter Product ID:");
                int inputProductID = int.Parse(Console.ReadLine());
                if (orderProcessorRepository.deleteProduct(inputProductID))
                {
                    Console.WriteLine("\nProduct Deleted Successfully\n");
                }
                else
                {
                    Console.WriteLine("Unable to Delete Product\n");
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"\n{e.Message}\n");
            }
        }

        public void deleteCustomer()
        {
            try
            {
                Console.WriteLine("\nEnter Customer ID:");
                int inputCustomerID = int.Parse(Console.ReadLine());
                if (orderProcessorRepository.deleteCustomer(inputCustomerID))
                {
                    Console.WriteLine("\nCustomer Deleted Successfully\n");
                }
                else
                {
                    Console.WriteLine("Unable to Delete Customer\n");
                }     
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"\n{e.Message}\n");
            }
        }

        public void addToCart()
        {
            Console.WriteLine("\nAdd Products in your Cart from the given Product List\n");
            viewProducts();

            try
            {
                Console.WriteLine("\nEnter Customer ID:");
                int inputCustomerID = int.Parse(Console.ReadLine());
                List<Customers> customers = orderProcessorRepository.getAllCustomers();
                Customers searchedCustomer = customers.Find(x => x.CustomerID == inputCustomerID);
                if (searchedCustomer == null)
                {
                    throw new CustomerNotFoundException($"Customer ID {inputCustomerID} does not exist");
                }

                Console.WriteLine("\nEnter Product ID:");
                int inputProductID = int.Parse(Console.ReadLine());
                List<Products> products = orderProcessorRepository.getAllProducts();
                Products searchedProduct = products.Find(x => x.ProductID == inputProductID);
                if (searchedProduct == null)
                {
                    throw new ProductNotFoundException($"Product ID {inputProductID} does not exist");
                }

                Console.WriteLine("\nEnter the Quantity to add in Cart:");
                int inputProductQuantity = int.Parse(Console.ReadLine());
                if(inputProductQuantity < searchedProduct.StockQuantity && inputProductQuantity > 0)
                {
                    if (orderProcessorRepository.addToCart(searchedCustomer, searchedProduct, inputProductQuantity))
                    {
                        Console.WriteLine("\nProduct added to cart successfully\n");
                    }
                    else
                    {
                        Console.WriteLine($"Unable to Add the Product to Cart\n");
                    }
                }
                else
                {
                    throw new InvalidValueException($"Invalid Quantity Value");
                }
            }
                
            catch (System.Exception e)
            {
                Console.WriteLine($"\n{ e.Message}\n");
            }
        }

        public void removeFromCart()
        {
            Console.WriteLine("\nRemove Products from your Cart from the given Cart List\n");
            viewCart();

            try
            {
                Console.WriteLine("\nEnter Customer ID:");
                int inputCustomerID = int.Parse(Console.ReadLine());
                List<Customers> customers = orderProcessorRepository.getAllCustomers();
                Customers searchedCustomer = customers.Find(x => x.CustomerID == inputCustomerID);
                if (searchedCustomer == null)
                {
                    throw new CustomerNotFoundException($"Customer ID {inputCustomerID} does not exist");
                }

                Console.WriteLine("\nEnter Product ID:");
                int inputProductID = int.Parse(Console.ReadLine());
                List<Products> products = orderProcessorRepository.getAllProducts();
                Products searchedProduct = products.Find(x => x.ProductID == inputProductID);
                if (searchedProduct == null)
                {
                    throw new ProductNotFoundException($"Product ID {inputProductID} does not exist");
                }

                if (orderProcessorRepository.removeFromCart(searchedCustomer, searchedProduct))
                {
                    Console.WriteLine("\nProduct Removed From Cart Successfully\n");
                }
                else
                {
                    Console.WriteLine("::Unable to Remove Product from Cart\n");
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"\n{e.Message}\n");
            }
        }

        public void getAllFromCart()
        {
            try
            {
                Console.WriteLine("\nEnter Customer ID:");
                int inputCustomerID = int.Parse(Console.ReadLine());
                List<Customers> customers = orderProcessorRepository.getAllCustomers();
                Customers searchedCustomer = customers.Find(x => x.CustomerID == inputCustomerID);
                if (searchedCustomer == null)
                {
                    throw new CustomerNotFoundException($"Customer ID {inputCustomerID} does not exist");
                }

                List<object> getUserCarts = orderProcessorRepository.getAllFromCart(searchedCustomer);
                if(getUserCarts.Count == 0)
                {
                    throw new CartNotFoundException($"No Cart History for Customer ID {inputCustomerID}");
                }

                Console.WriteLine($"\nCart Details of Customer ID:{inputCustomerID}");
                foreach (var getUserCart in getUserCarts)
                {
                    Console.WriteLine($"\n{getUserCart}");
                }
                Console.WriteLine("");
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"\n{e.Message}\n");
            }
        }

        public void placeOrder()
        {
            Dictionary<Products, int> productQuantity = new Dictionary<Products, int>();

            try
            {
                Console.WriteLine("\nEnter Customer ID:");
                int inputCustomerID = int.Parse(Console.ReadLine());
                List<Customers> customers = orderProcessorRepository.getAllCustomers();
                Customers searchedCustomer = customers.Find(x => x.CustomerID == inputCustomerID);
                if (searchedCustomer == null)
                {
                    throw new CustomerNotFoundException($"Customer ID {inputCustomerID} does not exist");
                }

                Console.WriteLine("\nEnter Customer Address:");
                string inputCustomerAddress = Console.ReadLine();
                Console.WriteLine("\nChoose what to order from the given Products:\n");
                viewProducts();

                restart:
                Console.WriteLine("\nEnter Product ID:");
                int inputProductID = int.Parse(Console.ReadLine());
                List<Products> products = orderProcessorRepository.getAllProducts();
                Products searchedProduct = products.Find(x => x.ProductID == inputProductID);
                if (searchedProduct == null)
                {
                    throw new ProductNotFoundException($"Product ID {inputProductID} does not exist");
                }

                Console.WriteLine("\nEnter Product Quantity:");
                int inputQuantity = int.Parse(Console.ReadLine());
                if(inputQuantity < searchedProduct.StockQuantity && inputQuantity > 0)
                {
                    Console.WriteLine("\nAdd more Products?:\n 1.Yes \t 2.No");
                    int userChoice = int.Parse(Console.ReadLine());
                    if (userChoice == 1)
                    {
                        productQuantity.Add(searchedProduct, inputQuantity);
                        goto restart;
                    }
                    else if (userChoice == 2)
                    {
                        productQuantity.Add(searchedProduct, inputQuantity);
                        if (orderProcessorRepository.placeOrder(searchedCustomer, productQuantity, inputCustomerAddress))
                        {
                            Console.WriteLine("\nYour Order was placed successfully\n");
                        }
                        else
                        {
                            Console.WriteLine($"Unable to place your order\n");
                        }
                    }
                    else
                    {
                        throw new InvalidValueException($"Incorrect Choice");
                    }
                }
                else
                {
                    throw new InvalidValueException($"Invalid Quantity Value");
                }
                
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"\n{e.Message}\n");
            }
        }

        public void getOrdersByCustomer()
        {
            try
            {
                Console.WriteLine("\nEnter Customer ID:");
                int inputCustomerID = int.Parse(Console.ReadLine());
                List<Customers> customers = orderProcessorRepository.getAllCustomers();
                Customers searchedCustomer = customers.Find(x => x.CustomerID == inputCustomerID);
                if (searchedCustomer == null)
                {
                    throw new CustomerNotFoundException($"Customer ID {inputCustomerID} does not exist");
                }

                List<object> getUserOrders = orderProcessorRepository.getOrdersByCustomer(inputCustomerID);
                if (getUserOrders.Count == 0)
                {
                    throw new OrderNotFoundException($"No Order History for Customer ID {inputCustomerID}");
                }
                else
                {
                    Console.WriteLine($"\nOrder Details of Customer ID:{inputCustomerID}");
                    foreach (var getUserOrder in getUserOrders)
                    {
                        Console.WriteLine($"\n{getUserOrder}");
                    }
                    Console.WriteLine("");
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"\n{e.Message}\n");
            }

        }

        public void cancelOrder()
        {
            try
            {
                Console.WriteLine("\nEnter Order ID:");
                int inputOrderID = int.Parse(Console.ReadLine());
                if (orderProcessorRepository.cancelOrder(inputOrderID))
                {
                    Console.WriteLine("\nOrder Cancelled Successfully\n");
                }
                else
                {
                    Console.WriteLine("Unable to cancel your Order\n");
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"\n{e.Message}\n");
            }
        }

        public void viewCustomers()
        {
            Console.WriteLine("\nAll Customers");
            List<Customers> allCustomers = orderProcessorRepository.getAllCustomers();
            foreach (var customer in allCustomers)
            {
                Console.WriteLine(customer);
            }
            Console.WriteLine("");
        }

        public void viewProducts()
        {
            Console.WriteLine("\nAll Products");
            List<Products> allProducts = orderProcessorRepository.getAllProducts();
            foreach (var product in allProducts)
            {
                Console.WriteLine(product);
            }
            Console.WriteLine("");
        }

        public void viewCart()
        {
            Console.WriteLine("\nAll Cart Details");
            List<Cart> allCarts = orderProcessorRepository.getAllCartDetails();
            foreach (var cart in allCarts)
            {
                Console.WriteLine(cart);
            }
            Console.WriteLine("");
        }

        public void viewOrders()
        {
            Console.WriteLine("\nAll Orders Details");
            List<Orders> allOrders = orderProcessorRepository.getAllOrders();
            foreach (var order in allOrders)
            {
                Console.WriteLine(order);
            }
            Console.WriteLine("");
        }

        public void viewOrderDetails()
        {
            Console.WriteLine("\nAll Order Item Details");
            List<Order_Items> allOrderItems = orderProcessorRepository.getAllOrderItemsDetails();
            foreach (var orderItem in allOrderItems)
            {
                Console.WriteLine(orderItem.ToString());
            }
            Console.WriteLine("");
        }

    }
}
