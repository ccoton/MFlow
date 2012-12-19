using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFlow.Core.Conditions;

namespace MFlow.Core.Validation
{
    public class FluentValidation : FluentConditions, IFluentValidation
    {

        public void Throw<T>(T exception) where T : Exception
        {
            if (Is(true))
                throw exception;
        }

        public IFluentValidation If(bool condition)
        {
            base.And(condition);
            return this;
        }

        public new IFluentValidation And(bool condition)
        {
            base.And(condition);
            return this;
        }

        public new IFluentValidation Or(bool condition)
        {
            base.Or(condition);
            return this;
        }

        public new bool Is(bool condition)
        {
            return base.Is(condition);
        }

        public new IFluentValidation Then(Action execute)
        {
            base.Then(execute);
            return this;
        }
    }
}
