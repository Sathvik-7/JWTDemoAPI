using DemoAPI.Models;
using DemoAPI.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DemoAPI.Service.Implementation
{
    public class AuthRegisterUser : IAuthRegister
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthRegisterUser(UserManager<IdentityUser> userManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        
        public async Task<bool> Register(LoginUser loginUser)
        {
            var userInfo = new IdentityUser 
            {
                UserName = loginUser.UserName,
                Email = loginUser.UserName
            };

            var result = await _userManager.CreateAsync(userInfo, loginUser.Password);

            return result.Succeeded;
        }

        public async Task<LoginResponse> Login(LoginUser loginUser)
        {
            var response = new LoginResponse();

            var userInfo = await _userManager.FindByEmailAsync(loginUser.UserName);

            if (userInfo == null || (await _userManager.CheckPasswordAsync(userInfo,loginUser.Password)) == false)
                return response;

            response.IsLoggedIn = true;
            response.JwtToken = this.GenerateToken(userInfo.UserName);

            return response;
        }

        public string GenerateToken(string UserName)
        {
            string token = string.Empty;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:key").Value));

            var signCredentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256Signature);

            var securityToken = new JwtSecurityToken(
                issuer : _configuration.GetSection("Jwt:Issuer").Value,
                audience : _configuration.GetSection("Jwt:Audience").Value,
                expires : DateTime.Now.AddMinutes(10),
                signingCredentials : signCredentials,
                claims : new List<Claim>() 
                {
                    new Claim(ClaimTypes.Email,UserName),
                    new Claim(ClaimTypes.Role,"Admin")
                }
                );

            token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }
    }
}
