namespace Pdb.Results.TestWebApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("Ok")]
        public IActionResult OkResult() =>
            Result
                .Ok()
                .ToActionResult(this);

        [HttpGet("OkWithValue")]
        public IActionResult OkWithValueResult() =>
            Result
                .Ok(new DateTime(2021, 3, 16))
                .ToActionResult(this);

        [HttpGet("NotFound")]
        public IActionResult NotFoundResult() =>
            Result
                .NotFound()
                .ToActionResult(this);

        [HttpGet("Error")]
        public IActionResult ErrorResult() =>
            Result
                .Error(new[]
                {
                    "The first error",
                    "The second error",
                    "The third error",
                })
                .ToActionResult(this);

        [HttpGet("ErrorWithProblem")]
        public IActionResult ErrorResultWithProblem() =>
            Result
                .Error(new
                {
                    Notify = "Houston",
                })
                .ToActionResult(this);

        [HttpGet("Forbidden")]
        public IActionResult ForbiddenResult() =>
            Result
                .Forbidden()
                .ToActionResult(this);

        [HttpGet("Invalid")]
        public IActionResult InvalidResult() =>
            Result
                .Invalid(new List<ValidationError>
                {
                    { "Field1", "Field 1 first error" },
                    { "Field1", "Field 1 second error" },
                    { "Field2", "Field 2 first error" },
                })
                .ToActionResult(this);

        [HttpGet("Accepted")]
        public IActionResult AcceptedResult() =>
            Result
                .Ok()
                .ToActionResult(
                    this,
                    c => c.Accepted());
    }
}
