using Auctionator.Models;
using System.Threading.Tasks;
using Auctionator.Models.Dtos;

namespace Auctionator.Services.Interface
{
    public interface IUserService
    {
        Task<User> AddUserAsync(UserDto userDto);
        Task<User> GetUser(string email);
        Task<User> GetUser(string email, string password);
    }
}