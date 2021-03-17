namespace Microsoft.AspNetCore.Mvc.RazorPages
{
    using System;
    using System.Threading.Tasks;
    using Pdb.Results;
    using static Pdb.Results.Result;

    public static class PageModelResultOfTExtensions
    {
        public static Task<IActionResult> Result<T>(
            this PageModel page,
            Func<Task<Result<T>>> request) =>
            ResultCoreAsync(
                page,
                request,
                result => result.ToSuccessActionResult(page),
                result => result.ToErrorActionResult(page));

        public static Task<IActionResult> Result<T>(
            this PageModel page,
            Func<Task<Result<T>>> request,
            Func<Result<T>, IActionResult> ok) =>
            ResultCoreAsync(
                page,
                request,
                ok,
                result => result.ToErrorActionResult(page));

        public static Task<IActionResult> Result<T>(
            this PageModel page,
            Func<Task<Result<T>>> request,
            Func<Result<T>, IActionResult> ok,
            string modelPrefix) =>
            ResultCoreAsync(
                page,
                request,
                ok,
                result => result.ToErrorActionResult(page, modelPrefix));

        public static Task<IActionResult> Result<T>(
            this PageModel page,
            Func<Task<Result<T>>> request,
            Action<Result<T>> ok) =>
            ResultCoreAsync(
                page,
                request,
                result =>
                {
                    ok(result);
                    return result.ToSuccessActionResult(page);
                },
                result => result.ToErrorActionResult(page));

        public static Task<IActionResult> Result<T>(
            this PageModel page,
            Func<Task<Result<T>>> request,
            Func<Result<T>, IActionResult> ok,
            Func<Result<T>, Task> error,
            string modelPrefix) =>
            ResultCoreAsync(
                page,
                request,
                ok,
                async result =>
                {
                    await error(result);
                    return result.ToErrorActionResult(page, modelPrefix);
                });

        public static Task<IActionResult> Result<T>(
            this PageModel page,
            Func<Task<Result<T>>> request,
            Func<Result<T>, IActionResult> ok,
            Action<Result<T>> error,
            string modelPrefix) =>
            ResultCoreAsync(
                page,
                request,
                ok,
                result =>
                {
                    error(result);
                    return result.ToErrorActionResult(page, modelPrefix);
                });

        private static async Task<IActionResult> ResultCoreAsync<T>(this PageModel page,
            Func<Task<Result<T>>> request,
            Func<Result<T>, IActionResult> ok,
            Func<Result<T>, IActionResult> error)
        {
            Guard(page, request, ok, error);

            if (!page.ModelState.IsValid)
            {
                return error(Invalid<T>());
            }

            var result = await request();
            if (result.IsSuccessful)
            {
                return ok(result);
            }

            return error(result);
        }

        private static async Task<IActionResult> ResultCoreAsync<T>(this PageModel page,
            Func<Task<Result<T>>> request,
            Func<Result<T>, IActionResult> ok,
            Func<Result<T>, Task<IActionResult>> error)
        {
            Guard(page, request, ok, error);

            if (!page.ModelState.IsValid)
            {
                return await error(Invalid<T>());
            }

            var result = await request();
            if (result.IsSuccessful)
            {
                return ok(result);
            }

            return await error(result);
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
