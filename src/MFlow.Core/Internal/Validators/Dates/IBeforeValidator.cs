using System;
using MFlow.Core.Internal.Validators;

namespace MFlow.Core.Internal.Validators.Dates
{
    public interface IBeforeValidator : IComparisonValidator<DateTime, DateTime>
    {
    }
}

