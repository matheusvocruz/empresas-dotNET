using IMDb.Application.Interfaces.Queries;
using IMDb.Application.Requests.Usuario;
using IMDb.Application.Responses.Usuario;
using IMDb.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IMDb.Api.Controllers
{
    [ApiVersion("1.0")]
    public class AutenticacaoController : MainController
    {
        private readonly IUsuarioQueries _iUsuarioQueries;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AutenticacaoController(
            IUsuarioQueries iUsuarioQueries,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _iUsuarioQueries = iUsuarioQueries;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Autenticar o usuário
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(TokenResponse))]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AutenticationUsuarioRequest request)
            => CustomResponse(await _iUsuarioQueries.Autenticar(request));

        /// <summary>
        /// Atualizar o token
        /// </summary>
        /// <returns></returns>
        [HttpPost("RefreshToken")]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(TokenResponse))]
        public async Task<IActionResult> RefreshTokenAsync()
        {
            var claim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var userId = Convert.ToInt64(claim.Value);

            return CustomResponse(await _iUsuarioQueries.RefreshToken(userId));
        }
    }
}