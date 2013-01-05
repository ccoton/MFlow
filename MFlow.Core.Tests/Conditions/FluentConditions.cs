using System;
using System.Threading;
using MFlow.Core.Conditions;
using MFlow.Core.Conditions.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MFlow.Core.Tests.Conditions
{
    [TestClass]
    public class FluentConditions
    {
        [TestMethod]
        public void Test_Simple_Fluent_Condition_Returns_True()
        {
            IFluentConditions<object> fluentConditions = new MFlow.Core.Conditions.FluentConditions<object>(new Object());
            Assert.IsTrue(fluentConditions.If(1 == 1).Satisfied());
        }

        [TestMethod]
        public void Test_Simple_Fluent_Condition_Returns_False()
        {
            IFluentConditions<object> fluentConditions = new MFlow.Core.Conditions.FluentConditions<object>(new Object());
            Assert.IsFalse(fluentConditions.If(1 == 2).Satisfied());
        }

        [TestMethod]
        public void Test_Fluent_Chain_Of_True_Conditions()
        {
            IFluentConditions<object> fluentConditions = new MFlow.Core.Conditions.FluentConditions<object>(new Object());
            var chain = fluentConditions.If(1 == 1).And(true == true).And(false == false).Satisfied();
            Assert.IsTrue(chain);
        }

        [TestMethod]
        public void Test_Fluent_Chain_Of_False_Conditions()
        {
            IFluentConditions<object> fluentConditions = new MFlow.Core.Conditions.FluentConditions<object>(new Object());
            var chain = fluentConditions.If(1 == 2).And(true == false).And(false == true).Satisfied();
            Assert.IsFalse(chain);
        }

        [TestMethod]
        public void Test_Fluent_Chain_Containing_True_Or()
        {
            IFluentConditions<object> fluentConditions = new MFlow.Core.Conditions.FluentConditions<object>(new Object());
            var chain = fluentConditions.If(1 == 2).Or(2 == 2).Satisfied();
            Assert.IsTrue(chain);
        }

        [TestMethod]
        public void Test_Fluent_Chain_Containing_False_Or()
        {
            IFluentConditions<object> fluentConditions = new MFlow.Core.Conditions.FluentConditions<object>(new Object());
            var chain = fluentConditions.If(1 == 2).Or(2 == 4).Satisfied();
            Assert.IsFalse(chain);
        }

        [TestMethod]
        public void Test_Fluent_Chain_Of_Conditions_Executes()
        {
            var output = false;
            IFluentConditions<object> fluentConditions = new MFlow.Core.Conditions.FluentConditions<object>(new Object());
            fluentConditions.And(1 == 1).And(true == true).And(false == false).Then(
                    () =>
                    {
                        output = true;
                    }
                );
            Assert.IsTrue(output);
        }

        [TestMethod]
        public void Test_Fluent_Chain_Of_Conditions_Executes_On_Thread()
        {
            var thread = System.Threading.Thread.CurrentThread.ManagedThreadId;
            IFluentConditions<object> fluentConditions = new MFlow.Core.Conditions.FluentConditions<object>(new Object());

            fluentConditions.And(1 == 1).And(true == true).And(false == false)
                .Then(() =>
                    {
                        Assert.IsTrue(thread != System.Threading.Thread.CurrentThread.ManagedThreadId);
                    }, ExecuteThread.New
                );
        }

        [TestMethod]
        public void Test_Fluent_Chain_Of_Conditions_Doesnt_Execute()
        {
            var output = false;
            IFluentConditions<object> fluentConditions = new MFlow.Core.Conditions.FluentConditions<object>(new Object());
            fluentConditions.And(1 == 1).And(true == true).And(false == true).Then(
                    () =>
                    {
                        output = true;
                    }
                );
            Assert.IsFalse(output);
        }

        [TestMethod]
        public void Test_Fluent_Chain_Of_Conditions_Executes_Else()
        {
            var output = 1;
            IFluentConditions<object> fluentConditions = new MFlow.Core.Conditions.FluentConditions<object>(new Object());
            fluentConditions.And(1 == 1).And(true == true).And(true == false).Then(
                    () =>
                    {
                        output = 2;
                    })
                .Else(() =>
                    {
                        output = 3;
                    });
            Assert.IsTrue(output == 3);
        }

        [TestMethod]
        public void Test_Fluent_Chain_Of_Conditions_Executes_Else_On_Thread()
        {
            var thread = System.Threading.Thread.CurrentThread.ManagedThreadId;
            IFluentConditions<object> fluentConditions = new MFlow.Core.Conditions.FluentConditions<object>(new Object());

            fluentConditions.And(1 == 1).And(true == true).And(true == false).Then(
                    () =>
                    {
                        Assert.IsTrue(false);
                    }, ExecuteThread.New
                )
                .Else(() =>
                            {
                                Assert.IsTrue(thread != System.Threading.Thread.CurrentThread.ManagedThreadId);
                            }, ExecuteThread.New
                );
        }

        [TestMethod]
        public void Test_Fluent_Conditions_Clear()
        {
            IFluentConditions<object> fluentConditions = new MFlow.Core.Conditions.FluentConditions<object>(new Object());
            var output = fluentConditions.If(true).Satisfied();
            fluentConditions.Clear();
            output = fluentConditions.If(false).Satisfied();
            Assert.IsFalse(output);
        }

        [TestMethod]
        public void Test_Fluent_Conditions_Satisfied_Supresses_Warnings()
        {
            IFluentConditions<object> fluentConditions = new MFlow.Core.Conditions.FluentConditions<object>(new Object());
            var output = fluentConditions.If(true).And(false, output: ConditionOutput.Warning).Satisfied();
            Assert.IsTrue(output);
        }

        [TestMethod]
        public void Test_Fluent_Conditions_Satisfied_Doesnt_Supress_Warnings()
        {
            IFluentConditions<object> fluentConditions = new MFlow.Core.Conditions.FluentConditions<object>(new Object());
            var output = fluentConditions.If(true).And(false, output: ConditionOutput.Warning).Satisfied(suppressWarnings:false);
            Assert.IsFalse(output);
        }
    }
}
