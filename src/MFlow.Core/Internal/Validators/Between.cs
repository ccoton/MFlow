using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Internal.Validators
{
    public class Between<T>
    {
        public T Upper { get; set; }
        public T Lower { get; set; }
    }
}
