namespace Markets.Models
{
    public class Store
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
