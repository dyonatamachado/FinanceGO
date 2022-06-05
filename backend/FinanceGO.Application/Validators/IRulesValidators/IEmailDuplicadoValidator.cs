using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceGO.Application.Validators.IRulesValidators
{
    public interface IEmailDuplicadoValidator
    {
        Task<bool> EmailIsDuplicado(string email);
    }
}
