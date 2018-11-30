using Auctionator.Models;
using Auctionator.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auctionator.Services.Interface
{
    public interface IAuctionService
    {
        Task<List<Auction>> GetAll(Enums.AuctionStatus status);
        Task<Auction> GetAuctionById(int id);
        Task<List<Auction>> GetAuctionsForMainPage(int count);
        Task<Auction> Create(AuctionDto auctionDto);
        Task<Auction> Update(AuctionDto auctionDto, int id);
        Task<Bet> AddBet(BetDto betDto);
        Task<List<Bet>> GetAllBets(int auctionId);
    }
}
