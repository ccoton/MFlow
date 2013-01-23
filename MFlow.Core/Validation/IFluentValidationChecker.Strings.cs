namespace MFlow.Core.Validation
{
    /// <summary>
    ///     A fluent validation interface
    /// </summary>
    public partial interface IFluentValidationChecker<T>
    {
        /// <summary>
        ///     Checks if the expressions evaluates to a string that is empty
        /// </summary>
        IFluentValidation<T> IsNotEmpty ();

        /// <summary>
        ///     Checks if the expression evaluates to a string that matches the regEx 
        /// </summary>
        IFluentValidation<T> Mathes (string regEx);

        /// <summary>
        ///     Checks if the expression evaluates to a string that is an email address 
        /// </summary>
        IFluentValidation<T> IsEmail ();

        /// <summary>
        ///     Checks if the expression evaluates to a string that contains value
        /// </summary>
        IFluentValidation<T> Contains (string value);

        /// <summary>
        ///     Checks if the expression evaluates to a string that is of length
        /// </summary>
        IFluentValidation<T> IsLength (int length);
        
        /// <summary>
        ///     Checks if the expression evaluates to a string longer than length
        /// </summary>
        IFluentValidation<T> IsLongerThan (int length);
        
        /// <summary>
        ///     Checks if the expression evaluates to a string shorter than length
        /// </summary>
        IFluentValidation<T> IsShorterThan (int length);

        /// <summary>
        ///     Check if the expression evaluates to a string matching a credit card pattern
        /// </summary>
        IFluentValidation<T> IsCreditCard ();

        /// <summary>
        ///     Check if the expression evaluates to a string that is a post code
        /// </summary>
        IFluentValidation<T> IsPostCode ();

        /// <summary>
        ///     Check if the expression evaluates to a strin that is a zip code
        /// </summary>
        IFluentValidation<T> IsZipCode ();

        /// <summary>
        ///    Check if the expression evalates to a string that is numeric
        /// </summary>
        IFluentValidation<T> IsNumeric ();

        /// <summary>
        ///    Check if the expression evalates to a string that is alpha only
        /// </summary>
        IFluentValidation<T> IsAlpha ();

    }
}
