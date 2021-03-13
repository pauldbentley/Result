namespace Pdb.Results
{
    using System.Collections.Generic;

    public static class ValidationErrorCollectionExtensions
    {
        public static void Add(this ICollection<ValidationError> collection, string identifier, string errorMessage)
        {
            collection.Add(new ValidationError(identifier, errorMessage));
        }

        public static void Add(this ICollection<ValidationError> collection, string identifier, params string[] errorMessages)
        {
            foreach (var errorMessage in errorMessages)
            {
                collection.Add(identifier, errorMessage);
            }
        }
    }
}
