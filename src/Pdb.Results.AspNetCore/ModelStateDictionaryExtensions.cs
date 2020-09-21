namespace Microsoft.AspNetCore.Mvc.ModelBinding
{
    using System.Collections.Generic;

    internal static class ModelStateDictionaryExtensions
    {
        public static void AddModelErrors(this ModelStateDictionary modelStateDictionary, IDictionary<string, string[]> errors, string? prefix = null)
        {
            if (errors == null)
            {
                return;
            }

            foreach (var error in errors)
            {
                string? key = !string.IsNullOrEmpty(error.Key)
                    ? prefix != null ? prefix + "." + error.Key : error.Key
                    : string.Empty;

                var errorsMessages = error.Value;

                foreach (string errorMessage in errorsMessages)
                {
                    modelStateDictionary.AddModelError(key, errorMessage);
                }
            }
        }
    }
}
