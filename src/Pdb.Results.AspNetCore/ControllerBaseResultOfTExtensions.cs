namespace Microsoft.AspNetCore.Mvc
{
    using System;
    using System.Threading.Tasks;
    using Pdb.Results;

    public static class ControllerBaseResultOfTExtensions
    {
        public static Task<IActionResult> Result<T>(
            this ControllerBase controller,
            Func<Task<Result<T>>> request) =>
            ResultCore(
                controller,
                request,
                r => r.ToSuccessActionResult(controller),
                r => r.ToErrorActionResult(controller));

        public static Task<IActionResult> Result<T>(
            this ControllerBase controller,
            Func<Task<Result<T>>> request,
            Func<Result<T>, IActionResult>? ok = default,
            Func<Result<T>, IActionResult>? error = default) =>
            ResultCore(
                controller,
                request,
                ok ?? (r => r.ToSuccessActionResult(controller)),
                error ?? (r => r.ToErrorActionResult(controller)));

        private static async Task<IActionResult> ResultCore<T>(
            this ControllerBase controller,
            Func<Task<Result<T>>> request,
            Func<Result<T>, IActionResult> ok,
            Func<Result<T>, IActionResult> error)
        {
            Guard(controller, request, ok);

            if (!controller.ModelState.IsValid)
            {
                return controller.ValidationProblem();
            }

            var result = await request();

            if (result.IsSuccessful)
            {
                return ok(result);
            }

            return error(result);
        }

        private static void Guard(object controller, object request, object ok)
        {
            if (controller is null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (ok is null)
            {
                throw new ArgumentNullException(nameof(ok));
            }
        }
    }
}