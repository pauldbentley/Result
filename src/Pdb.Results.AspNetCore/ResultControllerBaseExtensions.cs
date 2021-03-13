namespace Microsoft.AspNetCore.Mvc
{
    using System.Linq;
    using Pdb.Results;

    public static class ResultControllerBaseExtensions
    {
        public static IActionResult GetActionResult(this Result result, ControllerBase controller)
        {
            if (result.IsSuccessful)
            {
                return result.GetSuccessActionResult(controller);
            }

            return result.GetErrorActionResult(controller);
        }

        public static IActionResult GetSuccessActionResult(this Result result, ControllerBase controller)
        {
            if (result.IsSuccessful)
            {
                return controller.Ok();
            }

            return controller.BadRequest();
        }

        public static IActionResult GetSuccessActionResult<T>(this Result<T> result, ControllerBase controller)
        {
            if (result.IsSuccessful)
            {
                return controller.Ok(result.Value);
            }

            return controller.BadRequest();
        }

        public static IActionResult GetErrorActionResult(this Result result, ControllerBase controller)
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
                    .GroupBy(e => e.Identifier)
                    .Select(e => new
                    {
                        e.Key,
                        Value = e.Select(e => e.ErrorMessage)
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
                return controller.BadRequest(result.Problem ?? result.Errors);
            }

            return controller.BadRequest();
        }
    }
}
