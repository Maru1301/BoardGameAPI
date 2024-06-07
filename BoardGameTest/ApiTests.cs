using Microsoft.AspNetCore.Mvc.Testing;
using BoardGame;
using RestSharp;
using Shouldly;
using BoardGame.Models.DTOs;
using Microsoft.Extensions.DependencyInjection;
using BoardGame.Infrastractures;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

//namespace BoardGameTest
//{
//    public class ApiTests(WebApplicationFactory<FakeStartup> factory) : IClassFixture<WebApplicationFactory<FakeStartup>>
//    {
//        [Theory, TestPriority(100)]
//        [InlineData("/api/Member/GetMemberInfo")]
//        public async Task Test01_GetMemberInfoTest(string url)
//        {
//            using var client = new RestClient(factory.CreateClient());
//            var request = new RestRequest(url);
//            var id = "66262710924804b2f0c40e2a";

//            using var scope = factory.Services.CreateScope();
//            var jwtHelper = scope.ServiceProvider.GetRequiredService<JWTHelper>();
//            var jwt = jwtHelper.GenerateToken(new MongoDB.Bson.ObjectId(id), "", Role.Member);
//            request.AddHeader("Authorization", $"Bearer {jwt}");

//            var response = await client.GetAsync(request);

//            response.ShouldNotBeNull();
//        }

//        [Theory, TestPriority(90)]
//        [InlineData("/api/Member/ListMembers")]
//        public async Task Test02_ListMembersTest(string url)
//        {
//            using var client = new RestClient(factory.CreateClient());
//            var request = new RestRequest(url);
//            var id = "66262710924804b2f0c40e2a";

//            using var scope = factory.Services.CreateScope();
//            var jwtHelper = scope.ServiceProvider.GetRequiredService<JWTHelper>();
//            var jwt = jwtHelper.GenerateToken(new MongoDB.Bson.ObjectId(id), "", Role.Admin);
//            request.AddHeader("Authorization", $"Bearer {jwt}");

//            var response = await client.GetAsync(request);

//            response.ShouldNotBeNull();
//        }

//        [Theory, TestPriority(80)]
//        [InlineData("/api/Member/Register")]
//        public async Task Test03_RegisterTest(string url)
//        {
//            using var client = new RestClient(factory.CreateClient());
//            var request = new RestRequest(url);
//            request.AddBody(new RegisterRequestDTO
//            {
//                Name = "unit test",
//                Account = "test",
//                Password = "test",
//                ConfirmPassword = "test",
//                Email = "93220allen@gmail.com",
//            });

//            var response = await client.PostAsync(request);

//            response.ShouldNotBeNull();
//        }

//        [Theory, TestPriority(70)]
//        [InlineData("/api/Member/Login")]
//        public async Task Test04_LoginTest(string url)
//        {
//            using var client = new RestClient(factory.CreateClient());
//            var request = new RestRequest(url);
//            request.AddBody(new MemberLoginRequestDTO
//            {
//                Account = "93220allen",
//                Password = "allen93220",
//            });

//            var response = await client.PostAsync(request);

//            response.ShouldNotBeNull();
//        }

        
//        [Theory, TestPriority(60)]
//        [InlineData("/api/Member/ResendConfirmationCode")]
//        public async Task Test05_ResendConfirmationCodeTest(string url)
//        {
//            using var client = new RestClient(factory.CreateClient());
//            var request = new RestRequest(url);
//            var id = "663893baa37c3d3fab56755f";

//            using var scope = factory.Services.CreateScope();
//            var jwtHelper = scope.ServiceProvider.GetRequiredService<JWTHelper>();
//            var jwt = jwtHelper.GenerateToken(new MongoDB.Bson.ObjectId(id), "", Role.Member);
//            request.AddHeader("Authorization", $"Bearer {jwt}");

//            var response = await client.GetAsync(request);

//            response.ShouldNotBeNull();
//        }

        
//        [Theory, TestPriority(50)]
//        [InlineData("/api/Member/EditMemberInfo")]
//        public async Task Test06_EditMemberInfoTest(string url)
//        {
//            using var client = new RestClient(factory.CreateClient());
//            var request = new RestRequest(url);
//            request.AddBody(new EditRequestDTO
//            {
//                Name = "test01",
//                Email = "test@example.com"
//            });
//            var id = "663893baa37c3d3fab56755f";

//            using var scope = factory.Services.CreateScope();
//            var jwtHelper = scope.ServiceProvider.GetRequiredService<JWTHelper>();
//            var jwt = jwtHelper.GenerateToken(new MongoDB.Bson.ObjectId(id), "", Role.Member);
//            request.AddHeader("Authorization", $"Bearer {jwt}");

//            var response = await client.PostAsync(request);

//            response.ShouldNotBeNull();
//        }

        
//        [Theory, TestPriority(40)]
//        [InlineData("/api/Member/ResetPassword")]
//        public async Task Test07_ResetPasswordTest(string url)
//        {
//            using var client = new RestClient(factory.CreateClient());
//            var request = new RestRequest(url);
//            request.AddBody(new ResetPasswordRequestDTO
//            {
//                OldPassword = "123456",
//                NewPassword = "123456"
//            });
//            var id = "663893baa37c3d3fab56755f";

//            using var scope = factory.Services.CreateScope();
//            var jwtHelper = scope.ServiceProvider.GetRequiredService<JWTHelper>();
//            var jwt = jwtHelper.GenerateToken(new MongoDB.Bson.ObjectId(id), "", Role.Member);
//            request.AddHeader("Authorization", $"Bearer {jwt}");

//            var response = await client.PostAsync(request);

//            response.ShouldNotBeNull();
//        }

        
//        [Theory, TestPriority(30)]
//        [InlineData("/api/Member/ValidateEmail")]
//        public async Task Test08_ValidateEmailTest(string url)
//        {
//            using var client = new RestClient(factory.CreateClient());
//            var request = new RestRequest(url);
//            request.AddParameter("memberId", "66388e053d17012a98f9222e");
//            request.AddParameter("confirmationCode", "906e72acaf18450ea0b7249494b7c114");

//            var response = await client.GetAsync(request);

//            response.ShouldNotBeNull();
//        }

//        [Theory, TestPriority(20)]
//        [InlineData("/api/Member/Delete")]
//        public async Task Test09_DeleteMemberTest(string url)
//        {
//            using var client = new RestClient(factory.CreateClient());
//            var request = new RestRequest(url);
//            var account = "test";
//            request.AddQueryParameter("account", account);

//            using var scope = factory.Services.CreateScope();
//            var jwtHelper = scope.ServiceProvider.GetRequiredService<JWTHelper>();
//            var jwt = jwtHelper.GenerateToken(new MongoDB.Bson.ObjectId(), account, Role.Admin);
//            request.AddHeader("Authorization", $"Bearer {jwt}");

//            var response = await client.DeleteAsync(request);

//            response.ShouldNotBeNull();
//        }
//    }
//}
