namespace Auctionator.Enums
{
    public enum ProductStatus : byte
    {
        WaitAuction = 1,
        OnAuction,
        OnPayment,
        Paid,
        Deleted
    }
}