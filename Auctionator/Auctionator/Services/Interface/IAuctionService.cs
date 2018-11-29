using Auctionator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auctionator.Services.Interface
{
    public interface IAuctionService
    {
        Task<List<Auction>> GetAll();
        Task<Auction> GetAuctionById(int id);
    }
}
