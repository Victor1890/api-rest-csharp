namespace web_app_rest.Interfaces
{
    public class SuperMarketInterface
    {
        public class SuperMarketRequestPayload
        {
            public string name {  get; set; }
        }

        public class SuperMarketBrandRequestPayload
        {
            public int superMarketId { get; set; }
            public int brandId { get; set; }
        }
    }
}
