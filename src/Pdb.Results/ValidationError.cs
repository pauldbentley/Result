namespace Pdb.Results
{
    public class ValidationError
    {
        public ValidationError(string? identifier, string errorMessage)
        {
            Identifier = identifier;
            ErrorMessage = errorMessage;
        }

        public string? Identifier { get; }

        public string ErrorMessage { get; }
    }
}
