using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FinanceGO.Application.ViewModels;
using FinanceGO.Core.Repositories.UsuarioRepositories;
using MediatR;

namespace FinanceGO.Application.Queries.UsuarioQueries.GetUsuarioById
{
    public class GetUsuarioByIdQuery : IRequest<UsuarioViewModel>
    {
        public GetUsuarioByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}