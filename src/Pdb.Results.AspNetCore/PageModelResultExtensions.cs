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
            Action<TResult> okAction) =>
            ResultCoreAsync(
                page,
                request,
                value =>
                {
                    okAction(value);
                    return page.Page();
                },
                _ => Task.CompletedTask,
                null);

        public static Task<IActionResult> Result<TResult>(
            this PageModel page,
            Func<Task<Result<TResult>>> request,
            Action<TResult> okAction,
            Action<Result> errorAction) =>
            ResultCoreAsync(
                page,
                request,
                value =>
                {
                    okAction(value);
                    return page.Page();
                },
                error =>
                {
                    errorAction(error);
                    return Task.CompletedTask;
                },
                null);

        public static Task<IActionResult> Result<TResult>(
            this PageModel page,
            Func<Task<Result<TResult>>> request,
            Action<TResult> okAction,
            Func<Result, Task> errorTask) =>
            ResultCoreAsync(
                page,
                request,
                value =>
                {
                    okAction(value);
                    return page.Page();
                },
                error => errorTask(error),
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
            Action<TResult> okAction,
            string? modelStatePrefix = null) =>
            ResultCoreAsync(
                page,
                request,
                value =>
                {
                    okAction(value);
                    return page.Page();
                },
                _ => Task.CompletedTask,
                modelStatePrefix);

        public static Task<IActionResult> Result<TResult>(
            this PageModel page,
            Func<Task<Result<TResult>>> request,
            Action<TResult> okAction,
            Action<Result> errorAction,
            string? modelStatePrefix = null) =>
            ResultCoreAsync(
                page,
                request,
                result =>
                {
                    okAction(result);
                    return page.Page();
                },
                error =>
                {
                    errorAction(error);
                    return Task.CompletedTask;
                },
                modelStatePrefix);

        public static Task<IActionResult> Result<TResult>(
            this PageModel page,
            Func<Task<Result<TResult>>> request,
            Action<TResult> okAction,
            Func<Result, Task> errorTask,
            string? modelStatePrefix = null) =>
            ResultCoreAsync(
                page,
                request,
                value =>
                {
                    okAction(value);
                    return page.Page();
                },
                errorTask,
                modelStatePrefix);

        public static Task<IActionResult> Result<TResult>(
            this PageModel page,
            Func<Task<Result<TResult>>> request,
            Func<TResult, IActionResult> okActionResult) =>
            ResultCoreAsync(
                page,
                request,
                okActionResult,
                _ => Task.CompletedTask,
                null);

        public static Task<IActionResult> Result<TResult>(
            this PageModel page,
            Func<Task<Result<TResult>>> request,
            Func<TResult, IActionResult> okActionResult,
            Action<Result> errorAction) =>
            ResultCoreAsync(
                page,
                request,
                okActionResult,
                error =>
                {
                    errorAction(error);
                    return Task.CompletedTask;
                },
                null);

        public static Task<IActionResult> Result<TResult>(
            this PageModel page,
            Func<Task<Result<TResult>>> request,
            Func<TResult, IActionResult> okActionResult,
            Func<Result, Task> errorAction) =>
            ResultCoreAsync(
                page,
                request,
                okActionResult,
                errorAction,
                null);

        public static Task<IActionResult> Result<TResult>(
            this PageModel page,
            Func<Task<Result<TResult>>> request,
            Func<TResult, IActionResult> okActionResult,
            string? modelStatePrefix = null) =>
            ResultCoreAsync(
                page,
                request,
                okActionResult,
                _ => Task.CompletedTask,
                modelStatePrefix);

        public static Task<IActionResult> Result<TResult>(this PageModel page,
            Func<Task<Result<TResult>>> request,
            Func<TResult, IActionResult> okActionResult,
            Action<Result> errorAction,
            string? modelStatePrefix = null) =>
            ResultCoreAsync(
                page,
                request,
                okActionResult,
                error =>
                {
                    errorAction(error);
                    return Task.CompletedTask;
                },
                modelStatePrefix);

        public static Task<IActionResult> Result<TResult>(this PageModel page,
            Func<Task<Result<TResult>>> request,
            Func<TResult, IActionResult> okActionResult,
            Func<Result, Task> errorTask,
            string? modelStatePrefix = null) =>
            ResultCoreAsync(
                page,
                request,
                okActionResult,
                errorTask,
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
            Func<IActionResult> okActionResult) =>
            ResultCoreAsync(
                page,
                request,
                okActionResult,
                _ => Task.CompletedTask,
                null);

        public static Task<IActionResult> Result(
            this PageModel page,
            Func<Task<Result>> request,
            Func<IActionResult> okActionResult,
            Action<Result> errorAction) =>
            ResultCoreAsync(
                page,
                request,
                okActionResult,
                error =>
                {
                    errorAction(error);
                    return Task.CompletedTask;
                },
                null);

        public static Task<IActionResult> Result(
            this PageModel page,
            Func<Task<Result>> request,
            Func<IActionResult> okActionResult,
            Func<Result, Task> errorTask) =>
            ResultCoreAsync(
                page,
                request,
                okActionResult,
                errorTask,
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
            Func<IActionResult> okActionResult,
            string? modelStatePrefix = null) =>
            ResultCoreAsync(
                page,
                request,
                okActionResult,
                _ => Task.CompletedTask,
                modelStatePrefix);

        public static Task<IActionResult> Result(this PageModel page,
            Func<Task<Result>> request,
            Func<IActionResult> okActionResult,
            Action<Result> errorAction,
            string? modelStatePrefix = null) =>
            ResultCoreAsync(
                page,
                request,
                okActionResult,
                error =>
                {
                    errorAction(error);
                    return Task.CompletedTask;
                },
                modelStatePrefix);

        public static Task<IActionResult> Result(this PageModel page,
            Func<Task<Result>> request,
            Func<IActionResult> okActionResult,
            Func<Result, Task> errorTask,
            string? modelStatePrefix = null) =>
            ResultCoreAsync(
                page,
                request,
                okActionResult,
                errorTask,
                modelStatePrefix);

        private static async Task<IActionResult> ResultCoreAsync<TResult>(this PageModel page,
            Func<Task<Result<TResult>>> request,
            Func<TResult, IActionResult> okActionResult,
            Func<Result, Task> errorTask,
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

            if (okActionResult is null)
            {
                throw new ArgumentNullException(nameof(okActionResult));
            }

            if (!page.ModelState.IsValid)
            {
                await errorTask(Pdb.Results.Result.Invalid());
                return page.Page();
            }

            var result = await request();
            var errorActionResult = GetErrorActionResult(page, result, modelPrefix);
            
            if (errorActionResult != null)
            {
                await errorTask(result);
                return errorActionResult;
            }
            
            return okActionResult(result.Value);
        }

        private static async Task<IActionResult> ResultCoreAsync(this PageModel page,
            Func<Task<Result>> request,
            Func<IActionResult> ok,
            Func<Result, Task> errorTask,
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
                await errorTask(Pdb.Results.Result.Invalid());
                return page.Page();
            }

            var result = await request();
            var errorActionResult = GetErrorActionResult(page, result, modelPrefix);

            if (errorActionResult != null)
            {
                await errorTask(result);
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
    }
}
