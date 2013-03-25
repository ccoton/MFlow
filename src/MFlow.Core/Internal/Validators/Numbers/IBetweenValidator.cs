using System;
using MFlow.Core.Internal.Validators;

namespace MFlow.Core.Internal.Validators.Numbers
{
    public interface IBetweenValidator : IComparisonValidator<int, Between<int>>
    {
    }
}

