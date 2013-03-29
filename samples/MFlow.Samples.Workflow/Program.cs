using MFlow.Core;
using MFlow.Core.ExpressionBuilder;
using System;

namespace MFlow.Samples.Workflow
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MFlowConfiguration.Current.WithExpressionBuilder(new ExpressionBuilderConfiguration(new CachingExpressionBuilder()));
            MFlowConfiguration.Current.WithoutStatistics();

            var orderWorkflow = new OrderWorkflow(new OrderStatus() { OrderId = 100, status = Status.Received });
            orderWorkflow.Configure();

            Console.ReadLine();
        }
    }
}
