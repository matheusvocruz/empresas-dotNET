using System.Collections.Generic;
using System.Threading.Tasks;
using IMDb.Api.Extensions;
using IMDb.Application.Interfaces.Queries;
using IMDb.Application.Requests.Filme;
using IMDb.Application.Responses.Filme;
using IMDb.Core;
using IMDb.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMDb.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    public class FilmeController : MainController
    {
        private readonly IFilmeQueries _iFilmeQueries;
        private readonly IMediatorHandler _mediatorHandler;

        public FilmeController(
            IFilmeQueries iFilmeQueries,
            IMediatorHandler mediatorHandler
        )
        {
            _iFilmeQueries = iFilmeQueries;
            _mediatorHandler = mediatorHandler;
        }

        /// <summary>
        /// Listagem dos Filmes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<FilmeResponse>))]
        public async Task<IActionResult> IndexAsync([FromQuery] ListFilmeRequest request)
            => CustomResponse(await _iFilmeQueries.RetornarFilmes(request));

        /// <summary>
        /// Retornar Filme
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(FilmeResponse))]
        public async Task<IActionResult> IndexAsync([FromRoute] long id)
            => CustomResponse(await _iFilmeQueries.RetornarFilmePeloId(id));

        /// <summary>
        /// Criação do Filme
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeToClaimValue("TipoUsuario", "Administrador")]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> CreateAsync([FromBody] CreateFilmeRequest request)
            => CustomResponse(await _mediatorHandler.SendCommand(request));
    }
}
