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
            ResultCoreAsync(
                page,
                request,
                _ => page.Page(),
                _ => Task.CompletedTask,
                null);

        public static Task<IActionResult> Result<TResult>(
            this PageModel page,
            Func<Task<Result<TResult>>> request,
            Action<TResult> ok) =>
            ResultCoreAsync(
                page,
                request,
                result =>
                {
                    ok(result);
                    return page.Page();
                },
                _ => Task.CompletedTask,
                null);

        public static Task<IActionResult> Result<TResult>(
            this PageModel page,
            Func<Task<Result<TResult>>> request,
            Action<TResult> ok,
            Action<Result> error) =>
            ResultCoreAsync(
                page,
                request,
                value =>
                {
                    ok(value);
                    return page.Page();
                },
                result =>
                {
                    error(result);
                    return Task.CompletedTask;
                },
                null);

        public static Task<IActionResult> Result<TResult>(
            this PageModel page,
            Func<Task<Result<TResult>>> request,
            Action<TResult> ok,
            Func<Result, Task> error) =>
            ResultCoreAsync(
                page,
                request,
                value =>
                {
                    ok(value);
                    return page.Page();
                },
                result => error(result),
                null);

        public static Task<IActionResult> Result<TResult>(
            this PageModel page,
            Func<Task<Result<TResult>>> request,
            string? modelStatePrefix = null) =>
            ResultCoreAsync(
                page,
                request,
                _ => page.Page(),
                _ => Task.CompletedTask,
                modelStatePrefix);

        public static Task<IActionResult> Result<TResult>(
            this PageModel page,
            Func<Task<Result<TResult>>> request,
            Action<TResult> ok,
            string? modelStatePrefix = null) =>
            ResultCoreAsync(
                page,
                request,
                result =>
                {
                    ok(result);
                    return page.Page();
                },
                _ => Task.CompletedTask,
                modelStatePrefix);

        public static Task<IActionResult> Result<TResult>(
            this PageModel page,
            Func<Task<Result<TResult>>> request,
            Action<TResult> ok,
            Action<Result> error,
            string? modelStatePrefix = null) =>
            ResultCoreAsync(
                page,
                request,
                result =>
                {
                    ok(result);
                    return page.Page();
                },
                result =>
                {
                    error(result);
                    return Task.CompletedTask;
                },
                modelStatePrefix);

        public static Task<IActionResult> Result<TResult>(
            this PageModel page,
            Func<Task<Result<TResult>>> request,
            Action<TResult> ok,
            Func<Result, Task> error,
            string? modelStatePrefix = null) =>
            ResultCoreAsync(
                page,
                request,
                result =>
                {
                    ok(result);
                    return page.Page();
                },
                error,
                modelStatePrefix);

        public static Task<IActionResult> Result<TResult>(
            this PageModel page,
            Func<Task<Result<TResult>>> request,
            Func<TResult, IActionResult> ok) =>
            ResultCoreAsync(
                page,
                request,
                ok,
                _ => Task.CompletedTask,
                null);

        public static Task<IActionResult> Result<TResult>(
            this PageModel page,
            Func<Task<Result<TResult>>> request,
            Func<TResult, IActionResult> ok,
            Action<Result> error) =>
            ResultCoreAsync(
                page,
                request,
                ok,
                result =>
                {
                    error(result);
                    return Task.CompletedTask;
                },
                null);

        public static Task<IActionResult> Result<TResult>(
            this PageModel page,
            Func<Task<Result<TResult>>> request,
            Func<TResult, IActionResult> ok,
            Func<Result, Task> error) =>
            ResultCoreAsync(
                page,
                request,
                ok,
                error,
                null);

        public static Task<IActionResult> Result<TResult>(
            this PageModel page,
            Func<Task<Result<TResult>>> request,
            Func<TResult, IActionResult> ok,
            string? modelStatePrefix = null) =>
            ResultCoreAsync(
                page,
                request,
                ok,
                _ => Task.CompletedTask,
                modelStatePrefix);

        public static Task<IActionResult> Result<TResult>(this PageModel page,
            Func<Task<Result<TResult>>> request,
            Func<TResult, IActionResult> ok,
            Action<Result> error,
            string? modelStatePrefix = null) =>
            ResultCoreAsync(
                page,
                request,
                ok,
                result =>
                {
                    error(result);
                    return Task.CompletedTask;
                },
                modelStatePrefix);

        public static Task<IActionResult> Result<TResult>(this PageModel page,
            Func<Task<Result<TResult>>> request,
            Func<TResult, IActionResult> ok,
            Func<Result, Task> error,
            string? modelStatePrefix = null) =>
            ResultCoreAsync(
                page,
                request,
                ok,
                error,
                modelStatePrefix);

        public static Task<IActionResult> Result(
            this PageModel page,
            Func<Task<Result>> request) =>
            ResultCoreAsync(
                page,
                request,
                () => page.Page(),
                _ => Task.CompletedTask,
                null);

        public static Task<IActionResult> Result(
            this PageModel page,
            Func<Task<Result>> request,
            Func<IActionResult> ok) =>
            ResultCoreAsync(
                page,
                request,
                ok,
                _ => Task.CompletedTask,
                null);

        public static Task<IActionResult> Result(
            this PageModel page,
            Func<Task<Result>> request,
            Func<IActionResult> ok,
            Action<Result> error) =>
            ResultCoreAsync(
                page,
                request,
                ok,
                result =>
                {
                    error(result);
                    return Task.CompletedTask;
                },
                null);

        public static Task<IActionResult> Result(
            this PageModel page,
            Func<Task<Result>> request,
            Func<IActionResult> ok,
            Func<Result, Task> error) =>
            ResultCoreAsync(
                page,
                request,
                ok,
                error,
                null);

        public static Task<IActionResult> Result(
            this PageModel page,
            Func<Task<Result>> request,
            string? modelStatePrefix = null) =>
            ResultCoreAsync(
                page,
                request,
                () => page.Page(),
                _ => Task.CompletedTask,
                modelStatePrefix);

        public static Task<IActionResult> Result(this PageModel page,
            Func<Task<Result>> request,
            Func<IActionResult> ok,
            string? modelStatePrefix = null) =>
            ResultCoreAsync(
                page,
                request,
                ok,
                _ => Task.CompletedTask,
                modelStatePrefix);

        public static Task<IActionResult> Result(this PageModel page,
            Func<Task<Result>> request,
            Func<IActionResult> ok,
            Action<Result> error,
            string? modelStatePrefix = null) =>
            ResultCoreAsync(
                page,
                request,
                ok,
                result =>
                {
                    error(result);
                    return Task.CompletedTask;
                },
                modelStatePrefix);

        public static Task<IActionResult> Result(this PageModel page,
            Func<Task<Result>> request,
            Func<IActionResult> ok,
            Func<Result, Task> error,
            string? modelStatePrefix = null) =>
            ResultCoreAsync(
                page,
                request,
                ok,
                error,
                modelStatePrefix);

        private static async Task<IActionResult> ResultCoreAsync<TResult>(this PageModel page,
            Func<Task<Result<TResult>>> request,
            Func<TResult, IActionResult> ok,
            Func<Result, Task> error,
            string? modelPrefix = null)
        {
            Guard(page, request, ok, error);

            if (!page.ModelState.IsValid)
            {
                await error(Pdb.Results.Result.Invalid());
                return page.Page();
            }

            var result = await request();
            var errorActionResult = GetErrorActionResult(page, result, modelPrefix);

            if (errorActionResult != null)
            {
                await error(result);
                return errorActionResult;
            }

            return ok(result.Value);
        }

        private static async Task<IActionResult> ResultCoreAsync(this PageModel page,
            Func<Task<Result>> request,
            Func<IActionResult> ok,
            Func<Result, Task> error,
            string? modelPrefix = null)
        {
            Guard(page, request, ok, error);

            if (!page.ModelState.IsValid)
            {
                await error(Pdb.Results.Result.Invalid());
                return page.Page();
            }

            var result = await request();
            var errorActionResult = GetErrorActionResult(page, result, modelPrefix);

            if (errorActionResult != null)
            {
                await error(result);
                return errorActionResult;
            }

            return ok();
        }

        private static IActionResult? GetErrorActionResult(
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

        private static void Guard(object page, object request, object ok, object error)
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

            if (error is null)
            {
                throw new ArgumentNullException(nameof(error));
            }
        }
    }
}
