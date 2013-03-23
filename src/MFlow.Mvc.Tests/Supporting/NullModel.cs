using System;

namespace MFlow.Mvc.Tests.Supporting
{
    public class NullModel : ValidatedModel<NullModel>
    {
        public NullModel()
        {
            GetValidator(null);
        }
    }
}
