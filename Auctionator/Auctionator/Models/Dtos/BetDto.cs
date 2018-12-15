namespace Auctionator.Models.Dtos
{
    public class BetDto
    {
        public double CurrentBet { get; set; }
        public string UserId { get; set; }
        public int AuctionId { get; set; }

        // ИЗМЕНИТЬ НА:
        //public double CurrentBet { get; set; }
        //public string UserName { get; set; }
        //public string BetDateTime { get; set; }
    }
}
