namespace Pdb.Results.Tests
{
    using System;
    using System.Collections.Generic;
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
            var actual = Result.Error();
            
            actual.ShouldBeErrorResult();
            actual.Errors.ShouldBeEmpty();
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


            object? nullProblem = null;
            Should.Throw<ArgumentNullException>(() => Result.Error(nullProblem!));
        }

        [Theory]
        [InlineData("1")]
        [InlineData(null!)]
        public void Should_create_error_result_with_single_error(string error)
        {
            var actual = Result.Error(error);

            actual.ShouldBeErrorResult();
            actual.Errors.Count.ShouldBe(1);
            actual.Errors[0].ShouldBe(error);
            actual.Problem.ShouldBeNull();
        }

        [Fact]
        public void Should_create_error_result_with_array_of_errors()
        {
            var actual = Result.Error("1", "2", "3");

            actual.ShouldBeErrorResult();
            actual.Errors.Count.ShouldBe(3);
            actual.Errors[0].ShouldBe("1");
            actual.Errors[1].ShouldBe("2");
            actual.Errors[2].ShouldBe("3");
            actual.Problem.ShouldBeNull();

            string[]? nullErrors = null;
            Should.Throw<ArgumentNullException>(() => Result.Error(nullErrors!));
        }

        [Fact]
        public void Should_create_error_result_with_list_of_errors()
        {
            var errors = new List<string>
            {
                "1",
                "2",
                "3"
            };

            var actual = Result.Error(errors);

            actual.ShouldBeErrorResult();
            actual.Errors.GetHashCode().ShouldNotBe(errors.GetHashCode());
            actual.Errors.Count.ShouldBe(3);
            actual.Errors[0].ShouldBe("1");
            actual.Errors[1].ShouldBe("2");
            actual.Errors[2].ShouldBe("3");
            actual.Problem.ShouldBeNull();


            List<string>? nullErrors = null;
            Should.Throw<ArgumentNullException>(() => Result.Error(nullErrors!));
        }

        [Fact]
        public void Should_create_forbidden_result()
        {
            var actual = Result.Forbidden();

            actual.ShouldBeForbiddenResult();
        }

        [Fact]
        public void Should_create_invalid_result()
        {
            var actual = Result.Invalid();

            actual.ShouldBeInvalidResult();
            actual.ValidationErrors.ShouldBeEmpty();
        }

        [Fact]
        public void Should_create_invalid_result_with_single_error_message()
        {
            var actual = Result.Invalid("error");

            actual.ShouldBeInvalidResult();
            actual.ValidationErrors.Count.ShouldBe(1);

            var error = actual.ValidationErrors[0];
            error.Identifier.ShouldBeNull();
            error.ErrorMessage.ShouldBe("error");
        }

        [Fact]
        public void Should_create_invalid_result_with_single_identifier_and_error_message()
        {
            var actual = Result.Invalid("key", "error");

            actual.ShouldBeInvalidResult();
            actual.ValidationErrors.Count.ShouldBe(1);

            var error = actual.ValidationErrors[0];
            error.Identifier.ShouldBe("key");
            error.ErrorMessage.ShouldBe("error");
        }

        [Fact]
        public void Should_create_invalid_result_with_single_key_and_list_of_errors()
        {
            var validationErrors = new List<ValidationError>
            {
                { "key", "1", "2", "3" },
            };

            var actual = Result.Invalid(validationErrors);

            actual.ShouldBeInvalidResult();
            actual.ValidationErrors.Count.ShouldBe(3);
            actual.ValidationErrors.ShouldAllBe(e => e.Identifier == "key");

            actual.ValidationErrors[0].ErrorMessage.ShouldBe("1");
            actual.ValidationErrors[1].ErrorMessage.ShouldBe("2");
            actual.ValidationErrors[2].ErrorMessage.ShouldBe("3");
        }

        [Fact]
        public void Should_create_not_found_result()
        {
            var actual = Result.NotFound();

            actual.ShouldBeNotFoundResult();
        }
    }
}
