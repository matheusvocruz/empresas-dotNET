using FluentValidation.Results;
using IMDb.Application.Requests.Voto;
using IMDb.Core;
using IMDb.Data.Interfaces.Repositorios;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace IMDb.Application.Commands.Voto
{
    public class CreateVotoCommandHandler : 
        CommandHandler, IRequestHandler<CreateVotoRequest, ValidationResult>
    {
        private readonly IVotoRepositorio _votoRepositorio;
        private readonly IFilmeRepositorio _filmeRepositorio;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateVotoCommandHandler(
            IVotoRepositorio votoRepositorio,
            IFilmeRepositorio filmeRepositorio,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _votoRepositorio = votoRepositorio;
            _filmeRepositorio = filmeRepositorio;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ValidationResult> Handle(CreateVotoRequest request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var filme = await _filmeRepositorio.RetornarFilmePeloId(request.FilmeId);

            if(filme == null)
            {
                AddError("Não existe nenhum filme com esse id");
                return ValidationResult;
            }

            var claim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var userId = Convert.ToInt64(claim.Value);
            var voto = await _votoRepositorio.VerificaDuplicidade(userId, request.FilmeId);
            var somatorio = await _votoRepositorio.RetornarSomatoriaVotos(request.FilmeId);

            if (voto != null)
            {
                filme.AtualizarMedia((request.Valor + (somatorio - voto.Valor)));
                voto.Update(request.Valor);
                _votoRepositorio.Update(voto);
            } else
            {
                var novoVoto = new Data.Entidades.Voto(userId, request.FilmeId, request.Valor);
                _votoRepositorio.Create(novoVoto);
                filme.AdicionarVoto();
                filme.AtualizarMedia((request.Valor + somatorio));
            }

            _filmeRepositorio.Update(filme);

            return await PersistData(_votoRepositorio.UnitOfWork);
        }
    }
}
