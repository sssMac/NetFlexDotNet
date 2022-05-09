using Xunit;
using System;

using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using GiraffeAPI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TestProject;

public class UnitTest1
{
    
    public class HostBuilder : WebApplicationFactory<App.Startup>
    {
        protected override IHostBuilder CreateHostBuilder() => Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(x => x.UseStartup<App.Startup>().UseTestServer());
    }
    
    public class IntegrationTests : IClassFixture<HostBuilder>
    {
        private readonly HttpClient _client;
        public IntegrationTests(HostBuilder server)
        {
            _client = server.CreateClient();
        }
        
        // Create User TESTS
        
        [Theory]
        [InlineData("kek","lol","kek")]
        [InlineData("lol","kek","lol")]
        public async Task CreateUserOK(string email,string password,string expected)
        {
            var user = new CreateUserRequest(email, password);
            string stringjson = JsonConvert.SerializeObject(user);
            var response = await _client.PostAsync("https://localhost:5001/API/user", new StringContent(stringjson));
            var actual = await response.Content.ReadAsStringAsync();
            dynamic jsonObject = JObject.Parse(actual);
            Assert.Equal(expected, (string)jsonObject.userName);
        }
        
        [Theory]
        [InlineData("","lol","Email is required")]
        [InlineData("lol","","Password is required")]
        public async Task CreateUserWrongEmailFormat(string email,string password,string expected)
        {
            var user = new CreateUserRequest(email, password);
            string stringjson = JsonConvert.SerializeObject(user);
            var response = await _client.PostAsync("https://localhost:5001/API/user", new StringContent(stringjson));
            var actual = await response.Content.ReadAsStringAsync();
            actual = actual.Trim('"');
            Assert.Equal(expected, actual);
        }

        // Get User TESTS
        
        
        [Theory]
        [InlineData("b72c2670-959b-4b48-b82f-b7eb7fc30178","lol")]
        public async Task GetUserByIdOK(Guid val1, string expected)
        {
            var response = await _client.GetAsync($"https://localhost:5001/API/user/{val1}");
            var actual = await response.Content.ReadAsStringAsync();
            dynamic jsonObject = JObject.Parse(actual);
            Assert.Equal(expected, (string)jsonObject.userName);
        }
        
        [Theory]
        [InlineData("30012610-f20f-482f-869a-8de0d4547670","User not found")]
        public async Task UserNotFind(Guid val1, string expected)
        {
            var response = await _client.GetAsync($"https://localhost:5001/API/user/{val1}");
            var actual = await response.Content.ReadAsStringAsync();
            actual = actual.Trim('"');
            Assert.Equal(expected, actual);
        }

        
        // Ban User TESTS
        
        
        [Theory]
        [InlineData("b72c2670-959b-4b48-b82f-b7eb7fc30178", true)]
        public async Task BanUserOK(Guid val1, bool expected)
        {
            var response = await _client.GetAsync($"https://localhost:5001/API/user/ban/{val1}");
            var actual = await response.Content.ReadAsStringAsync();
            dynamic jsonObject = JObject.Parse(actual);
            Assert.Equal(expected, (bool)jsonObject.isBanned);
        }
        
        
        
        // UnBan User TESTS
        
        [Theory]
        [InlineData("b72c2670-959b-4b48-b82f-b7eb7fc30178", false)]
        public async Task UnBanUserOK(Guid val1, bool expected)
        {
            var response = await _client.GetAsync($"https://localhost:5001/API/user/unban/{val1}");
            var actual = await response.Content.ReadAsStringAsync();
            dynamic jsonObject = JObject.Parse(actual);
            Assert.Equal(expected, (bool)jsonObject.isBanned);
        }
        
        [Theory]
        [InlineData("b72c2670-959b-4b48-b82f-b7eb7fc30178", "User not found or already unbanned")]
        public async Task UnBanUserAlreadyBanned(Guid val1, string expected)
        {
            var response = await _client.GetAsync($"https://localhost:5001/API/user/unban/{val1}");
            var actual = await response.Content.ReadAsStringAsync();
            actual = actual.Trim('"');
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData("b72c2670-959b-4b48-b82f-b7eb7fc30178", "User not found or already banned")]
        public async Task BanUserAlreadyBanned(Guid val1, string expected)
        {
            var response = await _client.GetAsync($"https://localhost:5001/API/user/ban/{val1}");
            var actual = await response.Content.ReadAsStringAsync();
            actual = actual.Trim('"');
            Assert.Equal(expected, actual);
        }
        
        
    }
}