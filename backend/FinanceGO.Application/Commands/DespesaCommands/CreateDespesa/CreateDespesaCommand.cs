using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Application.ViewModels;
using FinanceGO.Core.Enums;
using FinanceGO.Core.Results;
using FinanceGO.Core.UserServices;
using MediatR;

namespace FinanceGO.Application.Commands.DespesaCommands.CreateDespesa
{
    public class CreateDespesaCommand : IRequest<Result>
    {
        public CreateDespesaCommand(string descricao, double valor, DateTime data, ILoggedUserService usuarioService, Categoria categoria = 0)
        {
            Descricao = descricao;
            Valor = valor;
            Data = data;
            Categoria = categoria;
            UsuarioId = usuarioService.GetUserId();
        }

        [Required]
        public string Descricao { get; set; }
        [Required]
        public double Valor { get; set; }
        [Required]
        public DateTime Data { get; set; }
        [Range(0, 7, ErrorMessage = "O dado de Categoria não é obrigatório, porém só aceita valores inteiros entre 0 e 7.")]
        public Categoria Categoria { get; set; }
        [Required]
        public int UsuarioId { get; private set; }
    }
}