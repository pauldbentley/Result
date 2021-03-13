namespace Pdb.Results.Tests
{
    using Shouldly;

    public static class ShouldBeResultOfTExtensions
    {
        public static void ShouldBeOkResult<T>(this Result<T> actual)
        {
            actual.Errors.ShouldBeEmpty();
            actual.IsSuccessful.ShouldBeTrue();
            actual.Problem.ShouldBeNull();
            actual.Status.ShouldBe(ResultStatus.Ok);
            actual.ValidationErrors.ShouldBeEmpty();
        }
    }
}
