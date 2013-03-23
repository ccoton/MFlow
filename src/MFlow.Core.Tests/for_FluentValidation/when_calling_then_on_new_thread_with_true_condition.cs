using Machine.Specifications;
using MFlow.Core.Tests.Supporting;
using MFlow.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class when_calling_then_on_new_thread_with_true_condition : given.a_fluent_user_validator_with_true_condition
    {
        static int threadId;
        static int executedThreadId; 

        Because of = () =>
        {
            threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
            ((IFluentValidation<User>)validator).Then(() =>
                {
                    executedThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
                }, Conditions.Enums.ExecuteThread.New);
        };

        It should_execute_the_then_action_on_a_new_thread = () => { threadId.ShouldNotEqual(executedThreadId); };
    }
}
