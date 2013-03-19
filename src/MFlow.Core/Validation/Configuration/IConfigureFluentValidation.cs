using MFlow.Core.Validation.Configuration.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Validation.Configuration
{
    public interface IConfigureFluentValidation
    {
        CustomImplementationMode CustomImplementationMode { get; }

        IConfigureFluentValidation WithCustomImplementationMode(CustomImplementationMode mode);
    }
}
