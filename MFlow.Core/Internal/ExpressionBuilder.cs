
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
		static IDictionary<string, object> _expressions= new Dictionary<string, object>();
		static IDictionary<InvokeCacheKey, object> _compilations = new Dictionary<InvokeCacheKey, object>();
		
		/// <summary>
		///     Compiles the expression
		/// </summary>
		public Func<T, C> Compile<C>(Expression<Func<T, C>> expression)
		{
			var key = string.Format("{0}_{1}", typeof(T).FullName, expression.Body.ToString());
			if(_expressions.ContainsKey(key))
				return (Func<T, C>)_expressions[key];
			
			var compiled = expression.Compile();
			_expressions.Add(key, compiled);
			return compiled;
		}
		
		/// <summary>
		///     Invokes the expression
		/// </summary>
		public C Invoke<C>(Func<T, C> compiled, T target)
		{
			var key = new InvokeCacheKey() { Target = target, Func = compiled };
			if(_compilations.ContainsKey(key))
				return (C)_compilations[key];
			
			var invoked = compiled.Invoke(target);
			_compilations.Add(key, invoked);
			return invoked;
		}
	}
	
	class InvokeCacheKey
	{
		public object Target{get;set;}
		public object Func{get;set;}
	}
}
