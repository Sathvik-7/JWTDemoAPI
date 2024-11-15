using DemoAPI.Models;
using DemoAPI.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRegister authRegister;
        
        public AuthController(IAuthRegister authRegister)
        {
            this.authRegister = authRegister;   
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] LoginUser loginUser)
        {
            var result = await authRegister.Register(loginUser);
            
            if(result)
                return Ok("Successfully Registered");

            return BadRequest("Something went wrong! Try again.");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginUser loginUser)
        {
            if (ModelState.IsValid)
            {
                var result = await authRegister.Login(loginUser);

                if (result.IsLoggedIn)
                {
                    return Ok(result);
                }
                return BadRequest("Please enter valid credentials");
            }
            return BadRequest("Something went wrong!!");
        }
    }
}
