
namespace MFlow.Core.Statistics
{
    /// <summary>
    ///     A contract for configuring validation statistics
    /// </summary>
    public interface IConfigureValidationStatistics
    {
        /// <summary>
        ///     The recorded used to record statistics
        /// </summary>
        IRecordValidationStatistics Recorder { get; }

        /// <summary>
        ///     Is statistics enabled
        /// </summary>
        bool Enabled { get; }

        /// <summary>
        ///    Enables
        /// </summary>
        IConfigureValidationStatistics Enable();

        /// <summary>
        ///    Disables
        /// </summary>
        IConfigureValidationStatistics Disable();
    }
}
