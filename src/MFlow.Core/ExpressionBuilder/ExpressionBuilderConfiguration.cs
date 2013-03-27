using System;

namespace MFlow.Core.ExpressionBuilder
{
    /// <summary>
    ///     An implementation of expression builder configuration 
    /// </summary>
    public class ExpressionBuilderConfiguration : IConfigureExpressionBuilder
    {
        readonly IBuildExpressions _builder;

        /// <summary>
        ///     Constructor
        /// </summary>
        public ExpressionBuilderConfiguration(IBuildExpressions builder)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            _builder = builder;
        }

        /// <summary>
        ///     The builder
        /// </summary>
        public IBuildExpressions Builder
        {
            get { return _builder; }
        }
    }
}
