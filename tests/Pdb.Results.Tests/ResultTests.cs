namespace Pdb.Results.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Shouldly;
    using Xunit;

    public class ResultTests
    {
        [Fact]
        public void Should_create_ok_result()
        {
            var actual = Result.Ok();

            actual.ShouldBeOkResult();
        }

        [Fact]
        public void Should_create_error_result()
        {
            string error = "An error has occurred";
            var actual = Result.Error(error);

            actual.ShouldBeErrorResult();
            actual.Errors.ShouldBe(new[] { error });
            actual.Problem.ShouldBeNull();
        }

        [Fact]
        public void Should_create_error_result_with_problem()
        {
            var problem = new
            {
                Notify = "houston",
            };

            var actual = Result.Error(problem);

            actual.ShouldBeErrorResult();
            actual.Errors.ShouldBeEmpty();
            actual.Problem.ShouldBe(problem);
        }

        [Theory]
        [InlineData("1", "2", "3")]
        [InlineData("1", null, "3")]
        public void Should_create_error_result_with_errors(params string[] errors)
        {
            var actual = Result.Error(errors);

            actual.ShouldBeErrorResult();
            actual.Errors.ShouldBe(errors);
            actual.Problem.ShouldBeNull();
        }

        [Fact]
        public void Should_create_forbidden_result()
        {
            var actual = Result.Forbidden();

            actual.ShouldBeForbiddenResult();
        }

        [Theory]
        [InlineData("1", "2", "3")]
        [InlineData("1", null, "3")]
        public void Should_create_invalid_result_with_errors(params string[] errors)
        {
            var validationErrors = errors
                .Select(e => new ValidationError(string.Empty, e));

            var actual = Result.Invalid(validationErrors);

            actual.ShouldBeInvalidResult();
            actual.ValidationErrors.ShouldAllBe(e => e.Identifier == string.Empty);
            var actualErrors = actual.ValidationErrors.Select(e => e.Message);
            actualErrors.ShouldBe(errors);
        }

        [Fact]
        public void Should_create_invalid_result_with_single_identifier_and_list_of_validation_errors()
        {
            var validationErrors = new List<ValidationError>
            {
                { "a", "1", "2", "3" },
                { "b", "1" },
                { "a", "4" },
            };

            var actual = Result.Invalid(validationErrors);

            actual.ShouldBeInvalidResult();
            actual.ValidationErrors.Count().ShouldBe(5);

            var a = actual.ValidationErrors.Where(e => e.Identifier == "a").Select(e => e.Message);
            a.ShouldBe(new[] { "1", "2", "3", "4" });

            var b = actual.ValidationErrors.Where(e => e.Identifier == "b").Select(e => e.Message);
            b.ShouldBe(new[] { "1" });
        }

        [Fact]
        public void Should_create_not_found_result()
        {
            var actual = Result.NotFound();

            actual.ShouldBeNotFoundResult();
        }
    }
}
