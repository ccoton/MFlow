
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
    }
}
