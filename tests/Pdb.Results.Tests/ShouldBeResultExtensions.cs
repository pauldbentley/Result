namespace Pdb.Results.Tests
{
    using Shouldly;

    public static class ShouldBeResultExtensions
    {
        public static void ShouldBeOkResult(this Result actual)
        {
            actual.Errors.ShouldBeEmpty();
            actual.IsSuccessful.ShouldBeTrue();
            actual.Problem.ShouldBeNull();
            actual.Status.ShouldBe(ResultStatus.Ok);
            actual.ValidationErrors.ShouldBeEmpty();
        }

        public static void ShouldBeErrorResult(this Result actual)
        {
            actual.IsSuccessful.ShouldBeFalse();
            actual.Status.ShouldBe(ResultStatus.Error);
            actual.ValidationErrors.ShouldBeEmpty();
        }

        public static void ShouldBeForbiddenResult(this Result actual)
        {
            actual.Errors.ShouldBeEmpty();
            actual.IsSuccessful.ShouldBeFalse();
            actual.Problem.ShouldBeNull();
            actual.Status.ShouldBe(ResultStatus.Forbidden);
            actual.ValidationErrors.ShouldBeEmpty();
        }

        public static void ShouldBeNotFoundResult(this Result actual)
        {
            actual.Errors.ShouldBeEmpty();
            actual.IsSuccessful.ShouldBeFalse();
            actual.Problem.ShouldBeNull();
            actual.Status.ShouldBe(ResultStatus.NotFound);
            actual.ValidationErrors.ShouldBeEmpty();
        }

        public static void ShouldBeInvalidResult(this Result actual)
        {
            actual.Errors.ShouldBeEmpty();
            actual.IsSuccessful.ShouldBeFalse();
            actual.Problem.ShouldBeNull();
            actual.Status.ShouldBe(ResultStatus.Invalid);
        }
    }
}
