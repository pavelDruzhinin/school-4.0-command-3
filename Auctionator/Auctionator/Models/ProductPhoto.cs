namespace Auctionator.Models
{
    public class ProductPhoto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public byte[] Photo { get; set; }
        public string Description { get; set; }
    }
}