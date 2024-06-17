using Microsoft.AspNetCore.Mvc.Testing;
using BoardGame;
using RestSharp;
using Shouldly;
using BoardGame.Models.DTOs;
using Microsoft.Extensions.DependencyInjection;
using BoardGame.Infrastractures;
using RestSharp.Authenticators;
using MongoDB.Bson;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace BoardGameTest
{
    public class ApiTests(WebApplicationFactory<FakeStartup> factory) : IClassFixture<WebApplicationFactory<FakeStartup>>
    {
        private RestRequest CreateRequestWithJwt(string url, string id, string account, string role)
        {
            var request = new RestRequest(url);

            using var scope = factory.Services.CreateScope();
            var jwtHelper = scope.ServiceProvider.GetRequiredService<JWTHelper>();
            var jwt = jwtHelper.GenerateToken(string.IsNullOrEmpty(id) ? new ObjectId() : new ObjectId(id), account, role);
            request.Authenticator = new JwtAuthenticator(jwt);

            return request;
        }

        [Theory, TestPriority(100)]
        [InlineData("/api/Member/GetMemberInfo", "66262710924804b2f0c40e2a")]
        public async Task Test01_GetMemberInfoTest(string url, string id)
        {
            using var client = new RestClient(factory.CreateClient());
            var request = CreateRequestWithJwt(url, id, string.Empty, Role.Member);

            var response = await client.GetAsync(request);

            response.ShouldNotBeNull();
        }

        [Theory, TestPriority(90)]
        [InlineData("/api/Member/ListMembers", "66262710924804b2f0c40e2a")]
        public async Task Test02_ListMembersTest(string url, string id)
        {
            using var client = new RestClient(factory.CreateClient());
            var request = CreateRequestWithJwt(url, id, string.Empty, Role.Admin);

            var response = await client.GetAsync(request);

            response.ShouldNotBeNull();
        }

        [Theory, TestPriority(80)]
        [InlineData("/api/Member/Register", "unit test", "test", "test", "test", "93220allen@gmail.com")]
        public async Task Test03_RegisterTest(string url, string name, string account, string password, string confirmPassword, string email)
        {
            using var client = new RestClient(factory.CreateClient());
            var request = new RestRequest(url);
            request.AddBody(new RegisterRequestDTO
            {
                Name = name,
                Account = account,
                Password = password,
                ConfirmPassword = confirmPassword,
                Email = email,
            });

            var response = await client.PostAsync(request);

            response.ShouldNotBeNull();
        }

        [Theory, TestPriority(70)]
        [InlineData("/api/Member/Login", "93220allen", "allen93220")]
        public async Task Test04_LoginTest(string url, string account, string password)
        {
            using var client = new RestClient(factory.CreateClient());
            var request = new RestRequest(url);
            request.AddBody(new MemberLoginRequestDTO
            {
                Account = account,
                Password = password
            });

            var response = await client.PostAsync(request);

            response.ShouldNotBeNull();
        }

        
        [Theory, TestPriority(60)]
        [InlineData("/api/Member/ResendConfirmationCode", "663893baa37c3d3fab56755f")]
        public async Task Test05_ResendConfirmationCodeTest(string url, string id)
        {
            using var client = new RestClient(factory.CreateClient());
            var request = CreateRequestWithJwt(url, id, string.Empty, Role.Member);

            var response = await client.GetAsync(request);

            response.ShouldNotBeNull();
        }

        
        [Theory, TestPriority(50)]
        [InlineData("/api/Member/EditMemberInfo", "test01", "test@example.com", "663893baa37c3d3fab56755f")]
        public async Task Test06_EditMemberInfoTest(string url, string name, string email, string id)
        {
            using var client = new RestClient(factory.CreateClient());
            var request = CreateRequestWithJwt(url, id, string.Empty, Role.Member);

            request.AddBody(new EditRequestDTO
            {
                Name = name,
                Email = email
            });

            var response = await client.PostAsync(request);

            response.ShouldNotBeNull();
        }

        
        [Theory, TestPriority(40)]
        [InlineData("/api/Member/ResetPassword", "123456", "123456", "663893baa37c3d3fab56755f")]
        public async Task Test07_ResetPasswordTest(string url, string oldPassword, string newPassword, string id)
        {
            using var client = new RestClient(factory.CreateClient());
            var request = CreateRequestWithJwt(url, id, string.Empty, Role.Member);

            request.AddBody(new ResetPasswordRequestDTO
            {
                OldPassword = oldPassword,
                NewPassword = newPassword
            });

            var response = await client.PostAsync(request);

            response.ShouldNotBeNull();
        }

        
        [Theory, TestPriority(30)]
        [InlineData("/api/Member/ValidateEmail", "66388e053d17012a98f9222e", "906e72acaf18450ea0b7249494b7c114")]
        public async Task Test08_ValidateEmailTest(string url, string memberId, string confirmationCode)
        {
            using var client = new RestClient(factory.CreateClient());
            var request = new RestRequest(url);
            request.AddParameter("memberId", memberId);
            request.AddParameter("confirmationCode", confirmationCode);

            var response = await client.GetAsync(request);

            response.ShouldNotBeNull();
        }

        [Theory, TestPriority(20)]
        [InlineData("/api/Member/Delete", "test")]
        public async Task Test09_DeleteMemberTest(string url, string account)
        {
            using var client = new RestClient(factory.CreateClient());
            var request = CreateRequestWithJwt(url, string.Empty, account, Role.Admin);
            request.AddQueryParameter("account", account);

            var response = await client.DeleteAsync(request);

            response.ShouldNotBeNull();
        }
    }
}
