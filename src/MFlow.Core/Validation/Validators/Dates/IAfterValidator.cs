using System;
using MFlow.Core.Validation.Validators;

namespace MFlow.Core.Validation.Validators.Dates
{
    public interface IAfterValidator : IComparisonValidator<DateTime, DateTime>
    {
    }
}

