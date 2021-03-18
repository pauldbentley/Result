namespace Microsoft.AspNetCore.Mvc.RazorPages
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Pdb.Results;

    public static class ResultPageModelExtensions
    {
        public static IActionResult ToActionResult<T>(
            this Result<T> result,
            PageModel page) =>
            ToActionResult(
                result,
                page,
                default!);

        public static IActionResult ToActionResult<T>(
            this Result<T> result,
            PageModel page,
            string modelPrefix) =>
            ToActionResult(
                result,
                page,
                result.ToSuccessActionResult,
                page => result.ToErrorActionResult(page, modelPrefix));

        public static IActionResult ToActionResult<T>(
            this Result<T> result,
            PageModel page,
            Func<PageModel, IActionResult> ok,
            Func<PageModel, IActionResult> error)
        {
            if (result.IsSuccessful)
            {
                return ok(page);
            }

            return error(page);
        }

        public static IActionResult ToSuccessActionResult<T>(
            this Result<T> result,
            PageModel page)
        {
            return page.Page();
        }

        public static IActionResult ToErrorActionResult<T>(
            this Result<T> result,
            PageModel page) =>
            ToErrorActionResult(result, page, default!);

        public static IActionResult ToErrorActionResult<T>(
            this Result<T> result,
            PageModel page,
            string modelPrefix)
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
                    .GroupBy(e => e.Identifier ?? string.Empty)
                    .Select(e => new
                    {
                        e.Key,
                        Value = e.Select(e => e.Message ?? string.Empty)
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
                if (result.Problem != null)
                {
                    page.ViewData["Problem"] = result.Problem;
                    return page.Page();
                }

                foreach (var error in result.Errors)
                {
                    page.ModelState.AddModelError(string.Empty, error ?? string.Empty);
                }

                return page.Page();
            }

            return page.Page();
        }
    }
}
