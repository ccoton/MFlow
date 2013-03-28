using System;
using MFlow.Core.Validation.Validators;
using MFlow.Core.Internal.Validators;

namespace MFlow.Core.Validation.Validators.Dates
{
    public interface IBetweenValidator : IComparisonValidator<DateTime, Between<DateTime>>
    {
    }
}

