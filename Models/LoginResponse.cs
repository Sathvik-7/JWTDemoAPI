﻿namespace DemoAPI.Models
{
    public class LoginResponse
    {
        public bool IsLoggedIn { get; set; } = false;

        public string JwtToken { get; set; } = string.Empty;
    }
}
