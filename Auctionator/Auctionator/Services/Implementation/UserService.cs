using System;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Auctionator.Data;
using Auctionator.Models;
using Auctionator.Models.Dtos;
using Auctionator.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Auctionator.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;

        public UserService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<User> AddUserAsync(UserDto userDto)
        {
            var user = new User {Email = userDto.Email, Password = userDto.Password, Name = userDto.Name};
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUser(string email)
        {
            User user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
            var a = user;
            return user;
        }

        public async Task<User> GetUser(string email, string password)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
    }
}