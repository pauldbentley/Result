namespace Pdb.Results
{
    public enum ResultStatus
    {
        /// <summary>
        /// The result was successful.
        /// </summary>
        Ok,

        /// <summary>
        /// There was an error.
        /// </summary>
        Error,

        /// <summary>
        /// The user is not authorized to perform the action.
        /// </summary>
        Forbidden,

        /// <summary>
        /// Validation of the input has failed.
        /// </summary>
        Invalid,

        /// <summary>
        /// The resource was not found.
        /// </summary>
        NotFound,
    }
}