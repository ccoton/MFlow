using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MFlow.Core.Tests
{
    [TestClass]
    public class FluentConditions
    {
        [TestMethod]
        public void Test_Simple_Fluent_Condition_Returns_True()
        {
            IFluentConditions fluentConditions = new MFlow.Core.FluentConditions();
            Assert.IsTrue(fluentConditions.And(1 == 1).Is(true));
        }

        [TestMethod]
        public void Test_Simple_Fluent_Condition_Returns_False()
        {
            IFluentConditions fluentConditions = new MFlow.Core.FluentConditions();
            Assert.IsTrue(fluentConditions.And(1 == 2).Is(false));
        }

        [TestMethod]
        public void Test_Fluent_Chain_Of_True_Conditions()
        {
            IFluentConditions fluentConditions = new MFlow.Core.FluentConditions();
            var chain = fluentConditions.And(1 == 1).And(true == true).And(false == false).Is(true);
            Assert.IsTrue(chain);
        }

        [TestMethod]
        public void Test_Fluent_Chain_Of_False_Conditions()
        {
            IFluentConditions fluentConditions = new MFlow.Core.FluentConditions();
            var chain = fluentConditions.And(1 == 2).And(true == false).And(false == true).Is(false);
            Assert.IsTrue(chain);
        }

        [TestMethod]
        public void Test_Fluent_Chain_Containing_True_Or()
        {
            IFluentConditions fluentConditions = new MFlow.Core.FluentConditions();
            var chain = fluentConditions.And(1 == 2).Or(2 == 2).Is(true);
            Assert.IsTrue(chain);
        }

        [TestMethod]
        public void Test_Fluent_Chain_Containing_False_Or()
        {
            IFluentConditions fluentConditions = new MFlow.Core.FluentConditions();
            var chain = fluentConditions.And(1 == 2).Or(2 == 4).Is(false);
            Assert.IsTrue(chain);
        }

        [TestMethod]
        public void Test_Fluent_Chain_Of_Conditions_Executes()
        {
            var output = false;
            IFluentConditions fluentConditions = new MFlow.Core.FluentConditions();
            fluentConditions.And(1 == 1).And(true == true).And(false == false).Then(
                    () =>
                    {
                        output = true;
                    }
                );
            Assert.IsTrue(output);
        }

        [TestMethod]
        public void Test_Fluent_Chain_Of_Conditions_Doesnt_Execute()
        {
            var output = false;
            IFluentConditions fluentConditions = new MFlow.Core.FluentConditions();
            fluentConditions.And(1 == 1).And(true == true).And(false == true).Then(
                    () =>
                    {
                        output = true;
                    }
                );
            Assert.IsFalse(output);
        }
    }
}
