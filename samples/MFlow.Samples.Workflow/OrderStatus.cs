using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFlow.Samples.Workflow
{
    class OrderStatus
    {
        public int OrderId { get; set; }
        public Status status { get; set; }
    }

    enum Status
    {
        Received,
        Processed, 
        Shipped,
        Completed
    }
}
