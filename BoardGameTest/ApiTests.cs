using Microsoft.AspNetCore.Mvc.Testing;
using BoardGame;
using RestSharp;
using Shouldly;
using BoardGame.Models.DTOs;
using Microsoft.Extensions.DependencyInjection;
using BoardGame.Infrastractures;

namespace BoardGameTest
{
    public class ApiTests(WebApplicationFactory<FakeStartup> factory) : IClassFixture<WebApplicationFactory<FakeStartup>>
    {
        [Theory]
        [InlineData("/api/Member/Register")]
        public async Task RegisterTest(string url)
        {
            using var client = new RestClient(factory.CreateClient());
            var request = new RestRequest(url);
            request.AddBody(new RegisterRequestDTO
            {
                Name = "unit test",
                Account = "test",
                Password = "test",
                ConfirmPassword = "test",
                Email = "test@test.com",
            });

            var response = await client.PostAsync(request);

            response.ShouldNotBeNull();
        }

        [Theory]
        [InlineData("/api/Member/Delete")]
        public async Task DeleteMemberTest(string url)
        {
            using var client = new RestClient(factory.CreateClient());
            var request = new RestRequest(url);
            var objectId = "66598af45c522ca57654047e";
            request.AddQueryParameter("objectId", objectId);

            using var scope = factory.Services.CreateScope();
            var jwtHelper = scope.ServiceProvider.GetRequiredService<JWTHelper>();
            var jwt = jwtHelper.GenerateToken(new MongoDB.Bson.ObjectId(objectId), "", Role.Admin);
            request.AddHeader("Authorization", $"Bearer {jwt}");

            var response = await client.DeleteAsync(request);

            response.ShouldNotBeNull();
        }
    }
}
