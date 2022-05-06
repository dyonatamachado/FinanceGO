using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceGO.Core.Results
{
    public class CriadoComSucessoResult : Result
    {
        public CriadoComSucessoResult(object value) : base(true, value)
        {
        }
    }
}