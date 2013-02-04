using MFlow.Core.Tests.Supporting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.Validation
{
    [TestFixture]
    class ValidationResult
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_Create_New_Validation_Result_When_Null_Argument()
        {
            var result = new MFlow.Core.Validation.ValidationResult<User>(null);
        }
    }
}
