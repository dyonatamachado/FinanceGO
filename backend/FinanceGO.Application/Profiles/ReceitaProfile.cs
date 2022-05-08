using AutoMapper;
using FinanceGO.Application.Commands.ReceitaCommands.CreateReceita;
using FinanceGO.Application.Commands.ReceitaCommands.UpdateReceita;
using FinanceGO.Application.ViewModels;
using FinanceGO.Core.Entities;

namespace FinanceGO.Application.Profiles
{
    public class ReceitaProfile : Profile
    {
        public ReceitaProfile()
        {
            CreateMap<Receita, ReceitaViewModel>();
            CreateMap<CreateReceitaCommand, Receita>();     
            CreateMap<UpdateReceitaCommand, Receita>();     
        }
    }
}