using Machine.Specifications;
using MFlow.Core.Conditions.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_Conditions
{
    public class when_calling_then_on_new_thread_with_multiple_true_conditions : given.an_if_or_condition_evaluating_to_true
    {
        static int threadId;
        static int onThreadId;

        Because of = () =>
        {
            threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
            fluent_conditions.Then(
                    () =>
                    {
                        onThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
                    }, ExecuteThread.New
                );
        };

        It should_execute_the_then_action_on_a_new_thread = () => { threadId.ShouldNotEqual(onThreadId); };
    }
}
