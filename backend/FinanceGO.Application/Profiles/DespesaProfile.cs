using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FinanceGO.Application.Commands.DespesaCommands.CreateDespesa;
using FinanceGO.Application.Commands.DespesaCommands.UpdateDespesa;
using FinanceGO.Application.ViewModels;
using FinanceGO.Core.Entities;

namespace FinanceGO.Application.Profiles
{
    public class DespesaProfile : Profile
    {
        public DespesaProfile()
        {
            CreateMap<CreateDespesaCommand, Despesa>();
            CreateMap<Despesa, DespesaViewModel>();
            CreateMap<UpdateDespesaCommand, Despesa>();            
        }
    }
}