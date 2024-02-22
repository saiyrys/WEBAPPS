namespace Markets.Contracts.Requests.Stores
{
    public class UpdateProductStoreRequest
    {
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public int NewCount { get; set; }
    }
}
