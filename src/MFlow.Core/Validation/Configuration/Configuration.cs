using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Validation.Configuration
{
    public class Configuration
    {
        public static readonly IConfigureFluentValidation Current = new FluentValidationConfiguration();
    }
}
