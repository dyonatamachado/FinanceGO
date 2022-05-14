using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceGO.Core.Results
{
    public class RegistroEncontradoResult : Result
    {
        public RegistroEncontradoResult(object value) : base(true, value)
        {
        }
    }
}