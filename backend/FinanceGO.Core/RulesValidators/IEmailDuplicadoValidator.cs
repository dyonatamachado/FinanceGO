using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceGO.Core.RulesValidators
{
    public interface IEmailDuplicadoValidator
    {
        Task<bool> EmailIsDuplicado(string email);
    }
}
