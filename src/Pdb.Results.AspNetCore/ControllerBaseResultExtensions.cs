namespace Microsoft.AspNetCore.Mvc
{
    using System;
    using System.Threading.Tasks;
    using Pdb.Results;

    public static class ControllerBaseResultExtensions
    {
        public static async Task<IActionResult> Result(this ControllerBase controller, Func<Task<IResult>> request, Func<IActionResult> ok)
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

            if (!controller.ModelState.IsValid)
            {
                return controller.BadRequest();
            }

            var result = await request();
            return GetResultAction(controller, result) ?? ok();
        }

        public static Task<IActionResult> Result(this ControllerBase controller, Func<Task<IResult>> request) =>
            Result(controller, request, () => controller.Ok());

        public static async Task<IActionResult> Result<TValue>(this ControllerBase controller, Func<Task<IResult<TValue>>> request, Func<TValue, IActionResult> ok)
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

            if (!controller.ModelState.IsValid)
            {
                return controller.BadRequest();
            }

            var result = await request();
            return GetResultAction(controller, result) ?? ok(result.Value);
        }

        public static Task<IActionResult> Result<TValue>(this ControllerBase controller, Func<Task<IResult<TValue>>> request) =>
            Result(controller, request, value => controller.Ok(value));

        private static IActionResult? GetResultAction(ControllerBase controller, IResult result)
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
    }
}