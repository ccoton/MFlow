using MFlow.Client.Models;
namespace MFlow.Client
{
    /// <summary>
    ///     A class used as a model to the ValidationApi controller
    /// </summary>
    public class ModelToValidate
    {
        /// <summary>
        ///     The object to try and validate
        /// </summary>
        public object Validate { get; set; }
        
        /// <summary>
        ///     The type of the object we are trying to validate
        /// </summary>
        public string Type { get; set; }
        
        /// <summary>
        ///     The type of model validation
        /// </summary>
        public ModelValidationType ValidationType {get;set;}
        
    }
}

