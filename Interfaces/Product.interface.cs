namespace web_app_rest.Interfaces
{
    public class ProductInterface
    {
        public class ProductRequestPayload
        {
            public string name { get; set; }
            public string description { get; set; }
            public double price { get; set; }
        }

        public class ProductBrandQuestPayload 
        {
            public int productId { get; set; }
            public int brandId { get; set; }
            public int stock { get; set; }
            public double price{ get; set; }
        }
    }
}
