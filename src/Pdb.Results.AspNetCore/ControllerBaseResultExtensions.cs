namespace Microsoft.AspNetCore.Mvc
{
    using System;
    using System.Threading.Tasks;
    using Pdb.Results;

    public static class ControllerBaseResultExtensions
    {
        public static Task<IActionResult> Result(
            this ControllerBase controller,
            Func<Task<Result>> request,
            Func<IActionResult> ok) =>
            ResultCore(
                controller,
                request,
                ok);

        public static Task<IActionResult> Result(
            this ControllerBase controller,
            Func<Task<Result>> request) =>
            ResultCore(
                controller,
                request,
                () => controller.Ok());

        public static Task<IActionResult> Result<TValue>(
            this ControllerBase controller,
            Func<Task<Result<TValue>>> request,
            Func<TValue, IActionResult> ok) =>
            ResultCore(
                controller,
                request,
                ok);

        public static Task<IActionResult> Result<TValue>(
            this ControllerBase controller,
            Func<Task<Result<TValue>>> request) =>
            ResultCore(
                controller,
                request,
                value => controller.Ok(value));

        private static async Task<IActionResult> ResultCore(
            this ControllerBase controller,
            Func<Task<Result>> request,
            Func<IActionResult> ok)
        {
            Guard(controller, request, ok);

            if (!controller.ModelState.IsValid)
            {
                return controller.BadRequest();
            }

            var result = await request();
            return GetResultAction(controller, result) ?? ok();
        }

        private static async Task<IActionResult> ResultCore<TValue>(
            this ControllerBase controller,
            Func<Task<Result<TValue>>> request,
            Func<TValue, IActionResult> ok)
        {
            Guard(controller, request, ok);

            if (!controller.ModelState.IsValid)
            {
                return controller.BadRequest();
            }

            var result = await request();
            return GetResultAction(controller, result) ?? ok(result.Value);
        }

        private static IActionResult? GetResultAction(ControllerBase controller, Result result)
        {
            if (result.Status == ResultStatus.NotFound)
            {
                return controller.NotFound();
            }

            if (result.Status == ResultStatus.Forbidden)
            {
                return controller.Forbid();
            }

            if (result.Status == ResultStatus.Invalid)
            {
                foreach (var error in result.ValidationErrors)
                {
                    foreach (var errorMessage in error.Value)
                    {
                        controller.ModelState.AddModelError(error.Key, errorMessage);
                    }

                    return controller.BadRequest();
                }
            }

            if (result.Status == ResultStatus.Error)
            {
                return controller.BadRequest(result.Problem ?? result.Errors);
            }

            return null;
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