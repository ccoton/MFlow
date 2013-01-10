
using System;
using System.Linq.Expressions;

namespace MFlow.Core.Internal
{
	/// <summary>
	///     An expression builder interface
	/// </summary>
	interface IExpressionBuilder<T>
	{
		Func<T, C> Compile<C>(Expression<Func<T, C>> expression);
	}
}
