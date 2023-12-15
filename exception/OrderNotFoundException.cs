namespace E_Commerce_Application.Exception
{
    internal class OrderNotFoundException:ApplicationException
    {
        public OrderNotFoundException(string msg): base(msg)
        {
            
        }
    }
}
