using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_ValidationLoader.given
{
    public class a_validation_loader
    {
        protected static ValidationLoader validation_loader;

        Establish context = () =>
        {
            validation_loader = new ValidationLoader();
        };
    }
}
