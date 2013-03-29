using MFlow.Core.Validation.Builder;
using MFlow.Core.Validation.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFlow.Samples.Workflow
{
    class OrderWorkflow
    {
        readonly IFluentValidationBuilder<OrderStatus> _workflow;

        public OrderWorkflow(OrderStatus orderStatus)
        {
            _workflow = new FluentValidationFactory().CreateFor(orderStatus);
        }

        public void Configure()
        {
            _workflow
                .Check(o => o.status)
                .IsEqualTo(Status.Received)
                .Then(() => { EmailService.Send(new { email = "Received order" }); })
                .Check(o => o.status)
                .IsEqualTo(Status.Processed)
                .Then(() => { EmailService.Send(new { email = "Order processed" }); })
                .Check(o => o.status)
                .IsEqualTo(Status.Shipped)
                .Then(() => { EmailService.Send(new { email = "Order shipped" }); });
        }
    }
}
