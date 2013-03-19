using MFlow.Core.Validation.Configuration.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Validation.Configuration
{
    public class FluentValidationConfiguration : IConfigureFluentValidation
    {
        internal FluentValidationConfiguration()
        {
            CustomImplementationMode = Enums.CustomImplementationMode.Ignore;
        }

        public CustomImplementationMode CustomImplementationMode { get; private set; }

        public IConfigureFluentValidation WithCustomImplementationMode(CustomImplementationMode mode)
        {
            CustomImplementationMode = mode;
            return this;
        }
    }
}
