using System;

namespace IMDb.Data.Responses.Usuario
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
