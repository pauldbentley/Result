namespace Pdb.Results.TestWebApp.Pages
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System;
    using System.Collections.Generic;

    public class IndexModel : PageModel
    {
        public IActionResult OnGetOk() =>
            Result
                .Ok()
                .ToActionResult(this);

        public IActionResult OnGetNotFound() =>
            Result
                .NotFound()
                .ToActionResult(this);

        public IActionResult OnGetError() =>
            Result
                .Error(
                    "The first error.",
                    "The second error.",
                    "The third error.")
                .ToActionResult(this);

        public IActionResult OnGetErrorWithProblem() =>
            Result
                .Error(Tuple.Create("Houston", "There is a problem."))
                .ToActionResult(this);

        public IActionResult OnGetInvalid() =>
            Result
                .Invalid(new List<ValidationError>
                {
                    { "Field1", "Field 1 first error." },
                    { "Field1", "Field 1 second error." },
                    { "Field2", "Field 2 first error." },
                })
                .ToActionResult(this);

        public IActionResult OnGetForbidden() =>
            Result
                .Forbidden()
                .ToActionResult(this);
    }
}
