using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MFlow.Core.Events
{
    public interface IEvents
    {
        void Raise<T>(T eventArgs);
        IDisposable Register<T>(Action<T> callback);
    }
}
