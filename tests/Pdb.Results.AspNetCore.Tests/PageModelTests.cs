namespace Pdb.Results.AspNetCore.Tests
{
    using System;
    using System.Net;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Pdb.Results.TestWebApp;
    using Shouldly;
    using Xunit;

    public class PageModelTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public PageModelTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Ok()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("?Handler=Ok");
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            var content = await response.Content.ReadAsStringAsync();
            content.ShouldContain("OK");
        }

        [Fact]
        public async Task OkWithValue()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/Test/OkWithValue");
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            var content = await response.Content.ReadFromJsonAsync<DateTime>();
            content.ShouldBe(new DateTime(2021, 3, 16));
        }

        [Fact]
        public async Task NotFound()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("?Handler=NotFound");
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Forbidden()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("?Handler=Forbidden");
            response.StatusCode.ShouldBe(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Error()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("?Handler=Error");
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            var content = await response.Content.ReadAsStringAsync();
            content.ShouldContain("The first error.");
            content.ShouldContain("The second error.");
            content.ShouldContain("The third error.");
        }

        [Fact]
        public async Task Error_with_problem()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("?Handler=ErrorWithProblem");
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            var content = await response.Content.ReadAsStringAsync();
            content.ShouldContain("Houston");
            content.ShouldContain("There is a problem.");
        }

        [Fact]
        public async Task Invalid()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("?Handler=Invalid");
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            var content = await response.Content.ReadAsStringAsync();
            content.ShouldContain("Field 1 first error.");
            content.ShouldContain("Field 1 second error.");
            content.ShouldContain("Field 2 first error.");
        }
    }
}
