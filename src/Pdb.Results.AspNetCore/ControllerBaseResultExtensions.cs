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
            Func<Result, IActionResult> ok) =>
            ResultCore(
                controller,
                request,
                ok,
                result => result.ToErrorActionResult(controller));

        public static Task<IActionResult> Result(
            this ControllerBase controller,
            Func<Task<Result>> request,
            Func<Result, IActionResult> ok,
            Func<Result, IActionResult> error) =>
            ResultCore(
                controller,
                request,
                ok,
                error);

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