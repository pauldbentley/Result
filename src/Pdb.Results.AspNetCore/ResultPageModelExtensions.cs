namespace Microsoft.AspNetCore.Mvc.RazorPages
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Pdb.Results;

    public static class ResultPageModelExtensions
    {
        public static IActionResult GetActionResult(
            this Result result,
            PageModel page,
            string? modelPrefix = null)
        {
            if (result.Status == ResultStatus.NotFound)
            {
                return page.NotFound();
            }

            if (result.Status == ResultStatus.Forbidden)
            {
                return page.Forbid();
            }

            if (result.Status == ResultStatus.Invalid)
            {
                var validationErrors = result
                    .ValidationErrors
                    .GroupBy(e => e.Identifier)
                    .Select(e => new
                    {
                        e.Key,
                        Value = e.Select(e => e.ErrorMessage)
                    });

                foreach (var error in validationErrors)
                {
                    string? key = !string.IsNullOrEmpty(error.Key)
                        ? modelPrefix != null ? modelPrefix + "." + error.Key : error.Key
                        : string.Empty;

                    var errorsMessages = error.Value;

                    foreach (string errorMessage in errorsMessages)
                    {
                        page.ModelState.AddModelError(key, errorMessage);
                    }
                }

                return page.Page();
            }

            if (result.Status == ResultStatus.Error)
            {
                foreach (var error in result.Errors)
                {
                    page.ModelState.AddModelError(string.Empty, error);
                }

                return page.Page();
            }

            return page.BadRequest();
        }
    }
}
