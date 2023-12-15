namespace E_Commerce_Application.Exception
{
    internal class ProductNotFoundException:ApplicationException
    {
        public ProductNotFoundException(string msg): base(msg)
        {

        }
    }
}
