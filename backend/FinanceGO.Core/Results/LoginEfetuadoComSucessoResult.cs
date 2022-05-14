using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceGO.Core.Results
{
    public class LoginEfetuadoComSucessoResult : Result
    {
        public LoginEfetuadoComSucessoResult(object value) : base(true, value)
        {
        }
    }
}