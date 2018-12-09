using Auctionator.Models;
using Auctionator.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auctionator.Services.Interface
{
    public interface IAuctionService
    {
        Task<List<Auction>> GetAll(Enums.AuctionStatus status);
        Task<List<Auction>> GetAuctionsByUser(string userId);
        Task<Auction> GetAuctionById(int id);
        Task<List<Auction>> GetAuctionsForMainPage(int count);
        Task<Auction> Create(AuctionDto auctionDto);
        Task<Auction> Update(AuctionDto auctionDto, int id);
        Task<Auction> Delete(int id);
        Task<Bet> AddBet(BetDto betDto);
        Task<List<Bet>> GetAllBets(int auctionId);
        Task Activate(IList<int> auctionId);
        Task Complete(IList<int> auctionId);
        Task EndPayTime(IList<int> auctionId);
        Task<Auction> Pay(int auctionId);
        Task<List<Auction>> NotPayed(string userId);
    }
}
