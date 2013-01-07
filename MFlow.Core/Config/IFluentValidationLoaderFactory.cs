using System;
using MFlow.Core.Validation;

namespace MFlow.Core
{
	/// <summary>
	///     A fluent validation loader interface
	/// </summary>
	public interface IFluentValidationLoaderFactory
	{
		IFluentValidationLoader GetLoader();
	}
}

