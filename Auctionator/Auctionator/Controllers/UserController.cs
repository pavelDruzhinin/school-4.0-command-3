using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Auctionator.Models;
using Auctionator.Models.Dtos;
using Auctionator.Services.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Auctionator.Controllers
{
    [Route("")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        private async Task Authenticate(string userId, string userEmail, string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userId),
                new Claim(ClaimTypes.Email, userEmail),
                new Claim(ClaimTypes.Name, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }


        [HttpPost]
        [Route("register")]
        public async Task<JsonResult> Register([FromBody]UserDto userDto)
        {
            try
            {
                //UserDto userDto = JsonConvert.DeserializeObject<UserDto>(userData);
                if (await _userService.GetUser(userDto.Email) != null) // Проверка, существует ли пользователь с таким Email-ом
                    throw new Exception("Пользователь с таким E-mail адресом уже существует!");

                var user = await _userService.AddUserAsync(userDto); // добавление пользователя в БД
                await Authenticate(user.Id, user.Email, user.Name); // аутентификация пользователя, создание Cookie для него
                return Json(new { success = true, result = new { name = user.Name } });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, result = ex.Message });
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<JsonResult> Login([FromBody]UserDto userDto)
        {
            try
            {
                var user = await _userService.GetUser(userDto.Email, userDto.Password);
                if (user == null)
                    throw new Exception("Неправильный E-mail или пароль!");

                await Authenticate(user.Id, user.Email, user.Name); // аутентификация пользователя, создание Cookie для него
                return Json(new { success = true, result = new {name = user.Name} });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, result = ex.Message });
            }
        }

        [HttpGet]
        [Route("logout")]
        public async Task<JsonResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, result = ex.Message });
            }
        }
    }
}