
namespace MFlow.Core.ExpressionBuilder
{
    /// <summary>
    ///     A contract for configuring an expression builder
    /// </summary>
    public interface IConfigureExpressionBuilder
    {
        /// <summary>
        ///     The expression builder
        /// </summary>
        IBuildExpressions Builder { get; }
    }
}
