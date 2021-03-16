namespace Microsoft.AspNetCore.Mvc
{
    using System;
    using System.Threading.Tasks;
    using Pdb.Results;

    public static class ControllerBaseResultExtensions
    {
        public static Task<IActionResult> Result(
            this ControllerBase controller,
            Func<Task<Result>> request) =>
            ResultCore(
                controller,
                request,
                r => r.ToSuccessActionResult(controller),
                r => r.ToErrorActionResult(controller));

        public static Task<IActionResult> Result(
            this ControllerBase controller,
            Func<Task<Result>> request,
            Func<Result, IActionResult>? ok = default,
            Func<Result, IActionResult>? error = default) =>
            ResultCore(
                controller,
                request,
                ok ?? (r => r.ToSuccessActionResult(controller)),
                error ?? (r => r.ToErrorActionResult(controller)));

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

        private static async Task<IActionResult> ResultCore(
            this ControllerBase controller,
            Func<Task<Result>> request,
            Func<Result, IActionResult> ok,
            Func<Result, IActionResult> error)
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

        private static async Task<IActionResult> ResultCore<TValue>(
            this ControllerBase controller,
            Func<Task<Result<TValue>>> request,
            Func<Result<TValue>, IActionResult> ok,
            Func<Result<TValue>, IActionResult> error)
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