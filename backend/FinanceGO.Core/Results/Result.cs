using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceGO.Core.Results
{
    public abstract class Result
    {
        public Result(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public Result(bool isSuccess, object value)
        {
            IsSuccess = isSuccess;
            Value = value;
        }

        public bool IsSuccess { get; private set; }
        public object Value { get; private set; }

    }
}