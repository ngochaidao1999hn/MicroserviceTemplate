using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using User.Api.Dtos;
using User.Api.Models;

namespace User.Api.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        private readonly IConfiguration _configuration;
        public UserService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;

        }
        public async Task<string> GetToken(LoginDto loginDto)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                throw new Exception("Email/Password not correct");
            }
            var res = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!res.Succeeded) 
            {
                throw new Exception("Email/Password not correct");
            }
            var userClaimPrincipals = await _userClaimsPrincipalFactory.CreateAsync(user);
            var claims = userClaimPrincipals.Claims;
            var issuer = _configuration["JWT:Issuer"];
            var audience = _configuration["JWT:Audience"];
            var securityKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey,
            SecurityAlgorithms.HmacSha256);
            var expDate = DateTime.Now.AddDays(1);
            var token = new JwtSecurityToken(issuer: issuer,
                audience: audience,
                signingCredentials: credentials,
                claims: claims,
                expires: expDate);
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }

        public async Task<bool> Register(RegisterDto registerDto)
        {
            var user = new ApplicationUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            { 
                throw new Exception(result.Errors.First().Description);
            }
            return result.Succeeded;
        }
    }
}
