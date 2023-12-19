namespace E_Commerce_Application.exception
{
    public class ProductNotFoundException:ApplicationException
    {
        public ProductNotFoundException(string msg): base(msg)
        {

        }
    }
}
