
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace MFlow.Core.Internal
{
	/// <summary>
	///     An expression builder
	/// </summary>
	class ExpressionBuilder<T> : IExpressionBuilder<T>
	{
		static IDictionary<object, object> _expressions= new Dictionary<object, object>();
		static IDictionary<InvokeCacheKey, object> _compilations = new Dictionary<InvokeCacheKey, object>();
		
		/// <summary>
		///     Compiles the expression
		/// </summary>
		public Func<T, C> Compile<C>(Expression<Func<T, C>> expression)
		{
			if(_expressions.ContainsKey(expression))
				return (Func<T, C>)_expressions[expression];
			
			var compiled = expression.Compile();
			_expressions.Add(expression, compiled);
			return compiled;
		}
		
		/// <summary>
		///     Invokes the expression
		/// </summary>
		public C Invoke<C>(Func<T, C> compiled, T target)
		{

		   	if(_compilations.Any(c=>c.Key.Target.Equals(target) && (Func<T, C>)c.Key.Func == compiled))
        		return (C)_compilations.Single(c=>c.Key.Target.Equals(target) && (Func<T, C>)c.Key.Func == compiled).Value;
      		
		   	var key = new InvokeCacheKey() { Target = target, Func = compiled };
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
