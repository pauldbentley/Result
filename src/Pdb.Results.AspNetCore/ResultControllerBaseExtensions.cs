namespace Microsoft.AspNetCore.Mvc
{
    using System;
    using System.Linq;
    using Pdb.Results;
    using Pdb.Results.AspNetCore;

    public static class ResultControllerBaseExtensions
    {
        public static ActionResult ToSuccessActionResult(
            this Result result,
            ControllerBase controller)
        {
            var context = ResultContext.Create(result);
            return ToSuccessActionResult(controller, context);
        }

        public static ActionResult ToSuccessActionResult<T>(
            this Result<T> result,
            ControllerBase controller)
        {
            var context = ResultContext.Create(result);
            return ToSuccessActionResult(controller, context);
        }

        public static ActionResult ToErrorActionResult(
            this Result result,
            ControllerBase controller)
        {
            var context = ResultContext.Create(result);
            return ToErrorActionResult(controller, context);
        }

        public static ActionResult ToErrorActionResult<T>(
            this Result<T> result,
            ControllerBase controller)
        {
            var context = ResultContext.Create(result);
            return ToErrorActionResult(controller, context);
        }

        public static ActionResult ToActionResult(
            this Result result,
            ControllerBase controller)
        {
            var context = ResultContext.Create(result);
            return ToActionResult(
                controller,
                context,
                c => ToSuccessActionResult(c, context),
                c => ToErrorActionResult(c, context));
        }

        public static ActionResult ToActionResult(
            this Result result,
            ControllerBase controller,
            Func<ControllerBase, ActionResult> ok)
        {
            var context = ResultContext.Create(result);
            return ToActionResult(
                controller,
                context,
                ok,
                c => ToErrorActionResult(c, context));
        }

        public static ActionResult ToActionResult<T>(
            this Result<T> result,
            ControllerBase controller)
        {
            var context = ResultContext.Create(result);
            return ToActionResult(
                controller,
                context,
                c => ToSuccessActionResult(c, context),
                c => ToErrorActionResult(c, context));
        }

        private static ActionResult ToActionResult(
            ControllerBase controller,
            ResultContext context,
            Func<ControllerBase, ActionResult> ok,
            Func<ControllerBase, ActionResult> error)
        {
            var result = context.Result;

            if (result.IsSuccessful)
            {
                return ok(controller);
            }

            return error(controller);
        }

        private static ActionResult ToSuccessActionResult(
            ControllerBase controller,
            ResultContext context)
        {
            if (context.IsValueResult)
            {
                return controller.Ok(context.Value);
            }

            return controller.Ok();
        }

        private static ActionResult ToErrorActionResult(
            ControllerBase controller,
            ResultContext context)
        {
            var result = context.Result;

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
