using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Internal.Validators
{
    public struct Between<T>
    {
        public T Upper { get; set; }
        public T Lower { get; set; }
    }
}
