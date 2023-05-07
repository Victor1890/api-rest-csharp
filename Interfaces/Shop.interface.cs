namespace web_app_rest.Interfaces
{
    public class ShopInterface
    {
        public class ShopRequestPayload
        {
            public int productId { get; set;}
            public int superMarketId { get; set; }
            public int stock { get; set; }
        }
    }
}
