using Ecommerce_Application.Dao;
using E_Commerce_Application.exception;
using Ecommerce_Application.Entities;


namespace E_Commerce_Unit_Testing
{
    public class Tests
    {
        IOrderProcessorRepository orderProcessorRepository = new OrderProcessorRepository();

        [Test]
        public void productCreatedSuccessfully()
        {
            bool status = orderProcessorRepository.createProduct(new Products(0, "Colgate", 20, "Salt Flavour", 80));
            Assert.True(status);
        }

        [Test]
        public void productAddedToCartSuccessfully()
        {
            bool status = orderProcessorRepository.addToCart(new Customers(3, "Arun", "arun@gmail.com", "koai8"), new Products(502, "Baby Powder", 140, "Increased Protection", 12), 5);
            Assert.True(status);
        }

        [Test]
        public void orderPlacedSuccessfully()
        {
            bool status = orderProcessorRepository.placeOrder(new Customers(3, "Arun", "arun@gmail.com", "koai8"), new Dictionary<Products, int> { { new Products(502, "Baby Powder", 140, "Increased Protection", 12), 5 }, { new Products(508, "Hamam Oil", 100, "New Sandal Flavour", 19), 3 } }, "Pune,Maharashtra");
            Assert.True(status);
        }
       
        [Test]
        public void productExceptionCheck()
        {
            Assert.Throws<ProductNotFoundException>(() => orderProcessorRepository.deleteProduct(-88454));
        }

        [Test]
        public void customerExceptionCheck()
        {
            Assert.Throws<CustomerNotFoundException>(() => orderProcessorRepository.deleteCustomer(-88454));
        }
    }
}