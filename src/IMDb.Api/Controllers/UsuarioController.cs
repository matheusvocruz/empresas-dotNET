using IMDb.Application.Interfaces.Queries;
using IMDb.Application.Requests.Usuario;
using IMDb.Application.Responses.Usuario;
using IMDb.Core;
using IMDb.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDb.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    public class UsuarioController : MainController
    {
        private readonly IUsuarioQueries _iUsuarioQueries;
        private readonly IMediatorHandler _mediatorHandler;

        public UsuarioController(
            IUsuarioQueries iUsuarioQueries,
            IMediatorHandler mediatorHandler
        )
        {
            _iUsuarioQueries = iUsuarioQueries;
            _mediatorHandler = mediatorHandler;
        }

        /// <summary>
        /// Listagem dos usuários
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<UsuarioResponse>))]
        public async Task<IActionResult> IndexAsync([FromQuery] PaginationSearch request)
            => CustomResponse(await _iUsuarioQueries.RetornarUsuariosNaoAdministrativos(request));

        /// <summary>
        /// Criação do Usuário
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUsuarioRequest request)
            => CustomResponse(await _mediatorHandler.SendCommand(request));

        /// <summary>
        /// Atualização do Usuário
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateUsuarioRequest request)
            => CustomResponse(await _mediatorHandler.SendCommand(request));

        /// <summary>
        /// Remoção do Usuário
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync([FromBody] DeleteUsuarioRequest request)
            => CustomResponse(await _mediatorHandler.SendCommand(request));
    }
}
