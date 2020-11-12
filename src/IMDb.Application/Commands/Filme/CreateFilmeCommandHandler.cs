using FluentValidation.Results;
using IMDb.Application.Requests.Filme;
using IMDb.Core;
using IMDb.Data.Interfaces.Repositorios;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IMDb.Application.Commands.Filme
{
    public class CreateFilmeCommandHandler :
        CommandHandler, IRequestHandler<CreateFilmeRequest, ValidationResult>
    {

        private readonly IFilmeRepositorio _filmeRepositorio;

        public CreateFilmeCommandHandler(IFilmeRepositorio filmeRepositorio)
        {
            _filmeRepositorio = filmeRepositorio;
        }

        public async Task<ValidationResult> Handle(CreateFilmeRequest request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var verificaDuplicidade = await _filmeRepositorio.RetornarFilmePeloNome(request.Nome);

            if (verificaDuplicidade != null)
            {
                AddError("Já existe um filme com esse nome");
                return ValidationResult;
            }

            var filme = new Data.Entidades.Filme(request.Nome, request.Descricao, request.Diretor, request.Genero);

            _filmeRepositorio.Create(filme);

            return await PersistData(_filmeRepositorio.UnitOfWork);
        }
    }
}
