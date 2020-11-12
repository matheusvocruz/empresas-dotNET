using System;

namespace IMDb.Application.Responses.Usuario
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
