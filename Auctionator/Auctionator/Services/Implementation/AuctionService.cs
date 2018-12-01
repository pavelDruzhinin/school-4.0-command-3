using Auctionator.Data;
using Auctionator.Models;
using Auctionator.Models.Dtos;
using Auctionator.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auctionator.Services.Implementation
{
    public class AuctionService : IAuctionService
    {
        private readonly ApplicationDbContext _db;

        public AuctionService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<Auction>> GetAll(Enums.AuctionStatus status)
        {
            return await _db.Auctions.Where(x => x.Status == status).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<List<Auction>> GetAuctionsByUser(string userId)
        {
            return await _db.Auctions.Where(x => x.WinnerId == userId).Include(x => x.Winner).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<Auction> GetAuctionById(int id)
        {
            return await _db.Auctions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Auction>> GetAuctionsForMainPage(int count)
        {
            var dbCount = await _db.Auctions.CountAsync();
            count = dbCount < count ? dbCount : count;
            return await _db.Auctions.Where(x => x.EndDateTime < System.DateTime.Now.AddMinutes(-1) && x.Status == Enums.AuctionStatus.Active).Take(count).OrderBy(x => x.EndDateTime).ToListAsync();
        }

        public async Task<Auction> Create(AuctionDto auctionDto)
        {
            var newAuction = new Auction() {
                Status = auctionDto.StartDateTime > System.DateTime.Now ? Enums.AuctionStatus.Wait : Enums.AuctionStatus.Active,
                StartDateTime = auctionDto.StartDateTime,
                EndDateTime = auctionDto.StartDateTime.AddDays(3), // 3 дня на аукцион
                StartPrice = auctionDto.StartPrice,
                ProductId = auctionDto.ProductId
            };

            await _db.Auctions.AddAsync(newAuction);
            await _db.SaveChangesAsync();

            return newAuction;
        }

        public async Task<Auction> Update(AuctionDto auctionDto, int id)
        {
            var auc = await GetAuctionById(id);

            _db.Auctions.Attach(auc);

            auc.StartDateTime = auctionDto.StartDateTime;
            auc.EndDateTime = auctionDto.StartDateTime.AddDays(3);
            auc.Status = auctionDto.Status == Enums.AuctionStatus.Failed ? Enums.AuctionStatus.Failed : auctionDto.StartDateTime > System.DateTime.Now ? Enums.AuctionStatus.Wait : Enums.AuctionStatus.Active;
            auc.StartPrice = auctionDto.StartPrice;

            await _db.SaveChangesAsync();

            return auc;
        }

        public async Task<List<Bet>> GetAllBets(int auctionId)
        {
            return await _db.Bets.Where(x => x.AuctionId == auctionId).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<Bet> AddBet(BetDto betDto)
        {
            var auction = _db.Auctions.FirstOrDefaultAsync(x => x.Id == betDto.AuctionId);

            var newBet = new Bet()
            {
                CurrentBet = betDto.CurrentBet,
                UserId = betDto.UserId,
                AuctionId = betDto.AuctionId
            };

            await _db.Bets.AddAsync(newBet);
            await _db.SaveChangesAsync();

            return newBet;
        }
    }
}
