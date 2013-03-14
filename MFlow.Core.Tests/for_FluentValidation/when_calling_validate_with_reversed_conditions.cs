using Machine.Specifications;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_validate_with_reversed_conditions : given.a_fluent_user_validator_with_multiple_false_reversed_conditions
    {

        static ICollection<IValidationResult<User>> results;

        Because of = () => { 
            
            results = validator.Validate().ToList(); 
        
        };

        It should_be_satisfied = () => { validator.Satisfied().ShouldBeTrue(); };
 
    }
}
