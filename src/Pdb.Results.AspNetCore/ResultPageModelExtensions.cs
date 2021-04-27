namespace Microsoft.AspNetCore.Mvc.RazorPages
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Pdb.Results;
    using Pdb.Results.AspNetCore;

    public static class ResultPageModelExtensions
    {
        public static ActionResult ToSuccessActionResult(
            this Result result,
            PageModel page)
        {
            var context = ResultContext.Create(result);
            return ToSuccessActionResult(page, context);
        }

        public static ActionResult ToSuccessActionResult<T>(
            this Result<T> result,
            PageModel page)
        {
            var context = ResultContext.Create(result);
            return ToSuccessActionResult(page, context);
        }

        public static ActionResult ToErrorActionResult(
            this Result result,
            PageModel page,
            string? modelPrefix = default)
        {
            var context = ResultContext.Create(result);
            return ToErrorActionResult(page, context, modelPrefix!);
        }

        public static ActionResult ToErrorActionResult<T>(
            this Result<T> result,
            PageModel page,
            string? modelPrefix = default)
        {
            var context = ResultContext.Create(result);
            return ToErrorActionResult(page, context, modelPrefix!);
        }

        public static ActionResult ToActionResult(
            this Result result,
            PageModel page,
            string? modelPrefix = default)
        {
            var context = ResultContext.Create(result);
            return ToActionResult(
                page,
                context,
                page => ToSuccessActionResult(page, context),
                page => ToErrorActionResult(page, context, modelPrefix!));
        }

        public static ActionResult ToActionResult<T>(
            this Result<T> result,
            PageModel page,
            string? modelPrefix = default)
        {
            var context = ResultContext.Create(result);
            return ToActionResult(
                page,
                context,
                page => ToSuccessActionResult(page, context),
                page => ToErrorActionResult(page, context, modelPrefix!));
        }

        public static ActionResult ToSuccessActionResult(
            PageModel page,
            ResultContext context)
        {
            return page.Page();
        }

        public static ActionResult ToErrorActionResult(
            PageModel page,
            ResultContext context,
            string modelPrefix)
        {
            var result = context.Result;

            if (result.Status == ResultStatus.NotFound)
            {
                return page.NotFound();
            }

            if (result.Status == ResultStatus.Forbidden)
            {
                return page.Forbid();
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
                    string? key = !string.IsNullOrEmpty(error.Key)
                        ? modelPrefix != null ? modelPrefix + "." + error.Key : error.Key
                        : string.Empty;

                    var errorsMessages = error.Value;

                    foreach (string errorMessage in errorsMessages)
                    {
                        page.ModelState.AddModelError(key, errorMessage);
                    }
                }

                return page.Page();
            }

            if (result.Status == ResultStatus.Error)
            {
                if (result.Problem != null)
                {
                    page.ViewData["Problem"] = result.Problem;
                    return page.Page();
                }

                foreach (var error in result.Errors)
                {
                    page.ModelState.AddModelError(string.Empty, error ?? string.Empty);
                }

                return page.Page();
            }

            return page.Page();
        }

        private static ActionResult ToActionResult(
            PageModel page,
            ResultContext context,
            Func<PageModel, ActionResult> ok,
            Func<PageModel, ActionResult> error)
        {
            var result = context.Result;

            if (result.IsSuccessful)
            {
                return ok(page);
            }

            return error(page);
        }
    }
}
