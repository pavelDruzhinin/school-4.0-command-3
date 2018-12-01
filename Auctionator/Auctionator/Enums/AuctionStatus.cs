namespace Auctionator.Enums
{
    public enum AuctionStatus : byte
    {
        Wait = 1,
        Active,
        OnPayment,
        Completed,
        Failed
    }
}
