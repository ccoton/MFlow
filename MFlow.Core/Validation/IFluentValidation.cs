using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFlow.Core.Conditions;

namespace MFlow.Core.Validation
{
    public interface IFluentValidation
    {
        IFluentValidation If(bool condition);
        IFluentValidation And(bool condition);
        IFluentValidation Or(bool condition);
        bool Is(bool condition);
        void Then(Action execute);
        void Throw<T>(T exception) where T : Exception;
    }
}
