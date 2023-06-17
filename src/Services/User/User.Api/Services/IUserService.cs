using User.Api.Dtos;

namespace User.Api.Services
{
    public interface IUserService
    {
        Task<string> GetToken(LoginDto loginDto);
        Task<bool> Register(RegisterDto registerDto);
    }
}
