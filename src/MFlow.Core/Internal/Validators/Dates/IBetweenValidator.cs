using System;
using MFlow.Core.Internal.Validators;

namespace MFlow.Core.Internal.Validators.Dates
{
    public interface IBetweenValidator : IComparisonValidator<DateTime, Between<DateTime>>
    {
    }
}

