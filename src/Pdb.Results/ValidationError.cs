namespace Pdb.Results
{
    public class ValidationError
    {
        public ValidationError(string identifier, string message)
        {
            Identifier = identifier;
            Message = message;
        }

        public string Identifier { get; }

        public string Message { get; }
    }
}
