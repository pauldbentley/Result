namespace Microsoft.AspNetCore.Mvc.RazorPages
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Pdb.Results;

    public static class PageModelResultExtensions
    {
        public static Task<IActionResult> Result<TResult>(this PageModel page, Func<Task<IResult<TResult>>> request) =>
            Result(page, request, null!, null!);

        public static Task<IActionResult> Result<TResult>(this PageModel page, Func<Task<IResult<TResult>>> request, Action<TResult> ok) =>
            Result(page, request, ok, null!);

        public static Task<IActionResult> Result<TResult>(this PageModel page, Func<Task<IResult<TResult>>> request, string modelPrefix) =>
            Result(page, request, null!, modelPrefix);

        public static Task<IActionResult> Result<TResult>(this PageModel page, Func<Task<IResult<TResult>>> request, Action<TResult> ok, string modelPrefix) =>
            Result(
                page,
                request,
                value =>
                {
                    ok?.Invoke(value);
                    return page.Page();
                },
                modelPrefix);

        public static Task<IActionResult> Result<TResult>(this PageModel page, Func<Task<IResult<TResult>>> request, Func<TResult, IActionResult> ok) =>
            Result(page, request, ok, null!);

        public static async Task<IActionResult> Result<TResult>(this PageModel page, Func<Task<IResult<TResult>>> request, Func<TResult, IActionResult> ok, string modelPrefix)
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
                page.ModelState.AddModelErrors(result.ValidationErrors, modelPrefix);

                return page.Page();
            }

            if (result.Status == ResultStatus.Error)
            {
                page.ViewData["Error"] = result.Problem;
                page.ViewData["Errors"] = result.Errors;

                return page.Page();
            }

            return ok(result.Value);
        }

        public static Task<IActionResult> Result(this PageModel page, Func<Task<IResult>> request) =>
            Result(page, request, null!, null!);

        public static Task<IActionResult> Result(this PageModel page, Func<Task<IResult>> request, Func<IActionResult> ok) =>
            Result(page, request, ok, null!);

        public static Task<IActionResult> Result(this PageModel page, Func<Task<IResult>> request, string modelPrefix) =>
            Result(page, request, null!, modelPrefix);

        public static async Task<IActionResult> Result(this PageModel page, Func<Task<IResult>> request, Func<IActionResult> ok, string modelPrefix)
        {
            if (page is null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (!page.ModelState.IsValid)
            {
                return page.Page();
            }

            var result = await request();

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
                page.ModelState.AddModelErrors(result.ValidationErrors, modelPrefix);

                return page.Page();
            }

            if (result.Status == ResultStatus.Error)
            {
                page.ViewData["Error"] = result.Problem;
                page.ViewData["Errors"] = result.Errors;

                return page.Page();
            }

            var actionResult = ok?.Invoke() ?? page.Page();
            return actionResult;
        }
    }
}
