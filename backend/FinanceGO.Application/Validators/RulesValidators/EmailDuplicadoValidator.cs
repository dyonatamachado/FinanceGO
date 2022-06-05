using FinanceGO.Application.Validators.IRulesValidators;
using FinanceGO.Core.Repositories.UsuarioRepositories;
using System.Threading.Tasks;

namespace FinanceGO.Application.Validators.RulesValidators
{
    public class EmailDuplicadoValidator : IEmailDuplicadoValidator
    {
        private readonly IUsuarioQueryRepository _repository;

        public EmailDuplicadoValidator(IUsuarioQueryRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> EmailIsDuplicado(string email)
        {
            var possivelEmailDuplicado = await _repository.GetUsuarioByEmailAsync(email);

            if (possivelEmailDuplicado == null) 
                return false;

            return true;
        }
    }
}
