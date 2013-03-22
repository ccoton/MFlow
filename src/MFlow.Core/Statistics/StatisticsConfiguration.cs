using System;

namespace MFlow.Core.Statistics
{
    /// <summary>
    ///     An implementation of statistics configuration 
    /// </summary>
    public class StatisticsConfiguration : IConfigureValidationStatistics
    {
        readonly IRecordValidationStatistics _recorder;

        /// <summary>
        ///     Constructor
        /// </summary>
        public StatisticsConfiguration(IRecordValidationStatistics recorder)
        {
            if (recorder == null)
                throw new ArgumentNullException("recorder");

            _recorder = recorder;
        }

        /// <summary>
        ///     The recorder
        /// </summary>
        public IRecordValidationStatistics Recorder
        {
            get { return _recorder; }
        }
    }
}
