namespace Microsoft.AspNetCore.Mvc
{
    using System;
    using System.Threading.Tasks;
    using Pdb.Results;

    public static class ControllerBaseResultOfTExtensions
    {
        public static Task<ActionResult> Result<T>(
            this ControllerBase controller,
            Func<Task<Result<T>>> request) =>
            ResultCore(
                controller,
                request,
                r => r.ToSuccessActionResult(controller),
                r => r.ToErrorActionResult(controller));

        public static Task<ActionResult> Result<T>(
            this ControllerBase controller,
            Func<Task<Result<T>>> request,
            Func<Result<T>, ActionResult> ok) =>
            ResultCore(
                controller,
                request,
                ok,
                result => result.ToErrorActionResult(controller));

        public static Task<ActionResult> Result<T>(
            this ControllerBase controller,
            Func<Task<Result<T>>> request,
            Func<Result<T>, ActionResult> ok,
            Func<Result<T>, ActionResult> error) =>
            ResultCore(
                controller,
                request,
                ok,
                error);

        private static async Task<ActionResult> ResultCore<T>(
            this ControllerBase controller,
            Func<Task<Result<T>>> request,
            Func<Result<T>, ActionResult> ok,
            Func<Result<T>, ActionResult> error)
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