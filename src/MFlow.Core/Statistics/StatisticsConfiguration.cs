using System;

namespace MFlow.Core.Statistics
{
    /// <summary>
    ///     An implementation of statistics configuration 
    /// </summary>
    public class StatisticsConfiguration : IConfigureValidationStatistics
    {
        readonly IRecordValidationStatistics _recorder;
        bool _enabled; 

        /// <summary>
        ///     Constructor
        /// </summary>
        public StatisticsConfiguration(IRecordValidationStatistics recorder)
        {
            if (recorder == null)
                throw new ArgumentNullException("recorder");
            _enabled = false;
            _recorder = recorder;
        }

        /// <summary>
        ///     The recorder
        /// </summary>
        public IRecordValidationStatistics Recorder
        {
            get { return _recorder; }
        }

        /// <summary>
        ///     Is statistics enabled
        /// </summary>
        public bool Enabled { get { return _enabled; } }

        /// <summary>
        ///     Enable statistics
        /// </summary>
        public IConfigureValidationStatistics Enable()
        {
            _enabled = true;
            return this;
        }

        /// <summary>
        ///     Disable statistics
        /// </summary>
        public IConfigureValidationStatistics Disable()
        {
            _enabled = false;
            return this;
        }
    }
}
