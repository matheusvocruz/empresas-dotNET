using Microsoft.Extensions.DependencyInjection;
using MediatR;
using IMDb.Core.Interfaces;
using IMDb.Core;
using FluentValidation.Results;
using IMDb.Application.Requests.Voto;
using IMDb.Application.Commands.Voto;
using IMDb.Data.Interfaces.Repositorios;
using IMDb.Data.Repositorios;
using IMDb.Application.Interfaces.Queries;
using IMDb.Application.Queries;
using AutoMapper;
using IMDb.Application.Configurations;
using IMDb.Data.Contextos;
using IMDb.Application.Commands.Usuario;
using IMDb.Application.Requests.Usuario;
using IMDb.Application.Commands.Filme;
using IMDb.Application.Requests.Filme;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IMDb.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            //Mediator - Voto
            services.AddScoped<IRequestHandler<CreateVotoRequest, ValidationResult>, CreateVotoCommandHandler>();
            //Mediator - Usuário
            services.AddScoped<IRequestHandler<CreateUsuarioRequest, ValidationResult>, CreateUsuarioCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateUsuarioRequest, ValidationResult>, UpdateUsuarioCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteUsuarioRequest, ValidationResult>, DeleteUsuarioCommandHandler>();
            //Mediator - Filme
            services.AddScoped<IRequestHandler<CreateFilmeRequest, ValidationResult>, CreateFilmeCommandHandler>();


            //Queries
            services.AddScoped<IUsuarioQueries, UsuarioQueries>();
            services.AddScoped<IFilmeQueries, FilmeQueries>();


            //Repositórios
            services.AddScoped<IFilmeRepositorio, FilmeRepositorio>();
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<IVotoRepositorio, VotoRepositorio>();


            //Contextos
            services.AddScoped<IMDbContexto>();

            //
            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();


            //AutoMapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DomainToViewModelMappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
