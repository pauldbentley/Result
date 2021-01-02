namespace Microsoft.AspNetCore.Mvc.RazorPages
{
    using System;
    using System.Threading.Tasks;
    using Pdb.Results;

    public static class PageModelResultExtensions
    {
        public static Task<IActionResult> Result<TResult>(
            this PageModel page,
            Func<Task<Result<TResult>>> request) =>
            Result(page, request, value => { }, null);

        public static Task<IActionResult> Result<TResult>(
            this PageModel page,
            Func<Task<Result<TResult>>> request,
            Action<TResult> ok) =>
            Result(page, request, ok, null);

        public static Task<IActionResult> Result<TResult>(
            this PageModel page,
            Func<Task<Result<TResult>>> request,
            string? modelPrefix = null) =>
            Result(page, request, value => { }, modelPrefix);

        public static Task<IActionResult> Result<TResult>(this PageModel page,
            Func<Task<Result<TResult>>> request,
            Action<TResult> ok,
            string? modelPrefix = null) =>
            Result(
                page,
                request,
                value =>
                {
                    ok.Invoke(value);
                    return page.Page();
                },
                modelPrefix);

        public static Task<IActionResult> Result<TResult>(this PageModel page,
            Func<Task<Result<TResult>>> request,
            Func<TResult, IActionResult> ok) =>
            Result(page, request, ok, null);

        public static async Task<IActionResult> Result<TResult>(this PageModel page,
            Func<Task<Result<TResult>>> request,
            Func<TResult, IActionResult> ok,
            string? modelPrefix = null)
        {
            if (page is null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (ok is null)
            {
                throw new ArgumentNullException(nameof(ok));
            }

            if (!page.ModelState.IsValid)
            {
                return page.Page();
            }

            var result = await request();
            return GetResultAction(page, result, modelPrefix) ?? ok(result.Value);
        }

        public static Task<IActionResult> Result(this PageModel page, Func<Task<Result>> request) =>
            Result(page, request, () => page.Page(), null);

        public static Task<IActionResult> Result(
            this PageModel page,
            Func<Task<Result>> request,
            Func<IActionResult> ok) =>
            Result(page, request, ok, null);

        public static Task<IActionResult> Result(
            this PageModel page,
            Func<Task<Result>> request,
            string? modelPrefix = null) =>
            Result(page, request, () => page.Page(), modelPrefix);

        public static async Task<IActionResult> Result(this PageModel page,
            Func<Task<Result>> request,
            Func<IActionResult> ok,
            string? modelPrefix = null)
        {
            if (page is null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (ok is null)
            {
                throw new ArgumentNullException(nameof(ok));
            }

            if (!page.ModelState.IsValid)
            {
                return page.Page();
            }

            var result = await request();
            return GetResultAction(page, result, modelPrefix) ?? ok();
        }

        private static IActionResult? GetResultAction(
            PageModel page,
            Result result,
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
                foreach (var error in result.ValidationErrors)
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
                page.ViewData["Error"] = result.Problem;
                page.ViewData["Errors"] = result.Errors;

                foreach (var error in result.Errors)
                {
                    page.ModelState.AddModelError(string.Empty, error);
                }

                return page.Page();
            }

            return null;
        }
    }
}
