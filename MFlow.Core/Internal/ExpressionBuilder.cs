
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MFlow.Core.Internal
{
	/// <summary>
	///     An expression builder
	/// </summary>
	class ExpressionBuilder<T> : IExpressionBuilder<T>
	{
		static IDictionary<string, object> _expressions;
		
		private static readonly ExpressionBuilder<T> instance= new ExpressionBuilder<T>();
		public static ExpressionBuilder<T> Instance { get { return instance; } }
		
		private ExpressionBuilder()
		{
			_expressions = new Dictionary<string, object>();
		}
		
		public Func<T, C> Compile<C>(Expression<Func<T, C>> expression)
		{
			var key = expression.Body.ToString();
			if(_expressions.ContainsKey(key))
				return (Func<T, C>)_expressions[key];
			
			var compiled = expression.Compile();
			_expressions.Add(key, compiled);
			return compiled;
		}
	}
}
