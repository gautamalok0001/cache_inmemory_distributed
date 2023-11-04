namespace memory_cache.Data
{
    public static class TestDumpData
    {
        public static List<Customer> customers = new List<Customer> 
        {
            new Customer { CustomerId = 1, CustomerName = "Alok Gautam", CustomerEmail = "alok.gautam@gmail.com"},
            new Customer { CustomerId = 2, CustomerName = "Ayush Gautam", CustomerEmail = "ayush.gautam@gmail.com"},
            new Customer {CustomerId = 3, CustomerName = "Aditya Gautam", CustomerEmail = "aditya.gautam@gmail.com"},
            new Customer {CustomerId = 4, CustomerName = "Aman Gautam", CustomerEmail = "aman.gautam@gmail.com"},
            new Customer {CustomerId = 5, CustomerName = "Raj Gautam", CustomerEmail = "raj.gautam@gmail.com"}
        };
    }
}