using System.Threading.Tasks;
using IMDb.Application.Requests.Voto;
using IMDb.Core;
using IMDb.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMDb.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    public class VotoController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public VotoController(
            IMediatorHandler mediatorHandler
        )
        {
            _mediatorHandler = mediatorHandler;
        }

        /// <summary>
        /// Criação do Voto
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> CreateAsync([FromBody] CreateVotoRequest request)
            => CustomResponse(await _mediatorHandler.SendCommand(request));
    }
}
