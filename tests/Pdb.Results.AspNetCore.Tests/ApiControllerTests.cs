namespace Pdb.Results.AspNetCore.Tests
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http.Json;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Pdb.Results.TestWebApp;
    using Shouldly;
    using Xunit;

    public class ApiControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public ApiControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Ok()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/Test/Ok");
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            var content = await response.Content.ReadAsStringAsync();
            content.ShouldBeEmpty();
        }

        [Fact]
        public async Task NotFound()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/Test/NotFound");
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
            var content = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            content.Status.ShouldBe(404);
        }

        [Fact]
        public async Task Error()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/Test/Error");
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            var content = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            content.Status.ShouldBe(400);
        }

        [Fact]
        public async Task Error_with_problem()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/Test/ErrorWithProblem");
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            var content = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            var notify = (JsonElement)content.Extensions["notify"];
            notify.GetString().ShouldBe("Houston");
        }

        [Fact]
        public async Task Error_with_list_of_errors()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/Test/ErrorWithListOfErrors");
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            var content = await response.Content.ReadFromJsonAsync<List<string>>();
            var errors = new[]
            {
                "The first error",
                "The second error",
                "The third error",
            };
            content.ShouldBe(errors);
        }

        [Fact]
        public async Task Invalid()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/Test/Invalid");
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            var content = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            content.Status.ShouldBe(400);
        }

        [Fact]
        public async Task Invalid_with_an_error()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/Test/InvalidWithError");
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            var content = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            content.Status.ShouldBe(400);
            content.Errors.Count.ShouldBe(1);
            content.Errors["Field1"].ShouldBe(new[] { "There was an error" });
        }

        [Fact]
        public async Task Invalid_with_a_list_of_errors()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/Test/InvalidWithListOfErrors");
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            var content = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            content.Errors.Count.ShouldBe(2);
            content.Errors["Field1"].ShouldBe(new[] { "Field 1 first error", "Field 1 second error" });
            content.Errors["Field2"].ShouldBe(new[] { "Field 2 first error" });
        }

        [Fact]
        public async Task Accepted()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/Test/Accepted");
            response.StatusCode.ShouldBe(HttpStatusCode.Accepted);
        }
    }
}
