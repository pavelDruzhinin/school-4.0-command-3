using Auctionator.Data;
using Auctionator.Models;
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

        public async Task<List<Auction>> GetAll()
        {
            return await _db.Auctions.ToListAsync();
        }

        public async Task<Auction> GetAuctionById(int id)
        {
            return await _db.Auctions.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
