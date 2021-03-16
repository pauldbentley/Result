namespace Microsoft.AspNetCore.Mvc
{
    using System;
    using System.Linq;
    using Pdb.Results;

    public static class ResultControllerBaseExtensions
    {
        public static IActionResult ToActionResult(this Result result, ControllerBase controller) =>
            result.ToActionResult(
                controller,
                result.ToSuccessActionResult,
                result.ToErrorActionResult);

        public static IActionResult ToActionResult<T>(this Result<T> result, ControllerBase controller) =>
            result.ToActionResult(
                controller,
                result.ToSuccessActionResult,
                result.ToErrorActionResult);

        public static IActionResult ToActionResult(
            this Result result,
            ControllerBase controller,
            Func<ControllerBase, IActionResult> ok)
        {
            if (result.IsSuccessful)
            {
                return ok(controller);
            }

            return result.ToErrorActionResult(controller);
        }

        public static IActionResult ToActionResult<T>(
            this Result<T> result,
            ControllerBase controller,
            Func<ControllerBase, IActionResult> ok)
        {
            if (result.IsSuccessful)
            {
                return ok(controller);
            }

            return result.ToErrorActionResult(controller);
        }

        public static IActionResult ToActionResult(
            this Result result,
            ControllerBase controller,
            Func<ControllerBase, IActionResult> ok,
            Func<ControllerBase, IActionResult> error)
        {
            if (result.IsSuccessful)
            {
                return ok(controller);
            }

            return error(controller);
        }

        public static IActionResult ToActionResult<T>(
            this Result<T> result,
            ControllerBase controller,
            Func<ControllerBase, IActionResult> ok,
            Func<ControllerBase, IActionResult> error)
        {
            if (result.IsSuccessful)
            {
                return ok(controller);
            }

            return error(controller);
        }

        public static IActionResult ToSuccessActionResult(this Result result, ControllerBase controller)
        {
            return controller.Ok();
        }

        public static IActionResult ToSuccessActionResult<T>(this Result<T> result, ControllerBase controller)
        {
            return controller.Ok(result.Value);
        }

        public static IActionResult ToErrorActionResult(this Result result, ControllerBase controller)
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
                var validationErrors = result
                    .ValidationErrors
                    .GroupBy(e => e.Identifier ?? string.Empty)
                    .Select(e => new
                    {
                        e.Key,
                        Value = e.Select(e => e.Message ?? string.Empty)
                    });

                foreach (var error in validationErrors)
                {
                    foreach (var errorMessage in error.Value)
                    {
                        controller.ModelState.AddModelError(error.Key, errorMessage);
                    }
                }

                return controller.ValidationProblem();
            }

            if (result.Status == ResultStatus.Error)
            {
                if (result.Problem != null)
                {
                    return controller.BadRequest(result.Problem);
                }

                if (result.Errors.Any())
                {
                    return controller.BadRequest(result.Errors);
                }

                controller.BadRequest();
            }

            return controller.BadRequest();
        }
    }
}
