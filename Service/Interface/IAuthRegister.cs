using DemoAPI.Models;

namespace DemoAPI.Service.Interface
{
    public interface IAuthRegister
    {
        Task<bool> Register(LoginUser loginUser);
       
        Task<LoginResponse> Login(LoginUser loginUser);

        string GenerateToken(string UserName);
    }
}
