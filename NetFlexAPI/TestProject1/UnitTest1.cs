using Xunit;
using System;

using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using GiraffeAPI;
using GiraffeAPI.Models;
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
            _client.DefaultRequestHeaders.Add("Authorization","Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJBZG1pbiIsImp0aSI6ImIxN2RhZDZkLTA0OWYtNGMwYy05YjFjLTFmMGRiMDJiNTNkYyIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwibmJmIjoxNjUyODAwNzQ3LCJleHAiOjE2NTI4MDQzNDcsImlzcyI6Imp3dHdlYmFwcC5uZXQiLCJhdWQiOiJqd3R3ZWJhcHAubmV0In0.a6RSXl4uVB04pyzaFBBKr82yRXu-wS7sHs6CvdliA7E");
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
        [InlineData("f92ac032-4341-4845-99c7-6dc2ed8a9624","lkl")]
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
        [InlineData("f92ac032-4341-4845-99c7-6dc2ed8a9624", "User not found or already banned")]
        public async Task AAAABanUserAlreadyBanned(Guid val1, string expected)
        {
            var response = await _client.GetAsync($"https://localhost:5001/API/user/ban/{val1}");
            var actual = await response.Content.ReadAsStringAsync();
            actual = actual.Trim('"');
            Assert.Equal(expected, actual);
        }
        
        
        [Theory]
        [InlineData("f92ac032-4341-4845-99c7-6dc2ed8a9624", true)]
        public async Task AAABanUserOK(Guid val1, bool expected)
        {
            var response = await _client.GetAsync($"https://localhost:5001/API/user/ban/{val1}");
            var actual = await response.Content.ReadAsStringAsync();
            dynamic jsonObject = JObject.Parse(actual);
            Assert.Equal(expected, (bool)jsonObject.isBanned);
        }
        
        
        
        // UnBan User TESTS
        
        [Theory]
        [InlineData("f92ac032-4341-4845-99c7-6dc2ed8a9624", "User not found or already unbanned")]
        public async Task AAAAUnBanUserAlreadyBanned(Guid val1, string expected)
        {
            var response = await _client.GetAsync($"https://localhost:5001/API/user/unban/{val1}");
            var actual = await response.Content.ReadAsStringAsync();
            actual = actual.Trim('"');
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData("f92ac032-4341-4845-99c7-6dc2ed8a9624", false)]
        public async Task ZZZUnBanUserOK(Guid val1, bool expected)
        {
            var response = await _client.GetAsync($"https://localhost:5001/API/user/unban/{val1}");
            var actual = await response.Content.ReadAsStringAsync();
            dynamic jsonObject = JObject.Parse(actual);
            Assert.Equal(expected, (bool)jsonObject.isBanned);
        }
        
        
        [Theory]
        [InlineData("37050332-97c2-4fb9-a9cd-97b5c86b35d6","User")]
        public async Task GetRoleById(Guid val1, string expected)
        {
            var response = await _client.GetAsync($"https://localhost:5001/API/role/{val1}");
            var actual = await response.Content.ReadAsStringAsync();
            dynamic jsonObject = JObject.Parse(actual);
            Assert.Equal(expected, (string)jsonObject.roleName);
        }
        
        [Theory]
        [InlineData("30012610-f20f-482f-869a-8de0d4547679","Role not found")]
        public async Task RoleNotFound(Guid val1, string expected)
        {
            var response = await _client.GetAsync($"https://localhost:5001/API/role/{val1}");
            var actual = await response.Content.ReadAsStringAsync();
            actual = actual.Trim('"');
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData("8a23aa6c-1a78-4628-9dfa-e7c69e3f02c2","POP",null)]
        [InlineData("8a23aa6c-1a78-4628-9dfa-e7c69e3f02c2","Redactor",null)]
        public async Task UpdateRole(Guid RoleId,string RoleName,string expected)
        {
            var role = new Role(RoleId, RoleName);
            string stringjson = JsonConvert.SerializeObject(role);
            var response = await _client.PostAsync("https://localhost:5001/API/role/update", new StringContent(stringjson));
            var actual = await response.Content.ReadAsStringAsync();
            dynamic jsonObject = JObject.Parse(actual);
            Assert.Equal(expected, (string)jsonObject.hasErrors);
        }
        
        [Theory]
        [InlineData("kek","qq","Auth is faild")]
        [InlineData("lol","qq","Auth is faild")]
        public async Task WrongAuth(string email,string password,string expected)
        {
            var user = new UserAuth(email, password);
            string stringjson = JsonConvert.SerializeObject(user);
            var response = await _client.PostAsync("https://localhost:5001/API/user/auth", new StringContent(stringjson));
            var actual = await response.Content.ReadAsStringAsync();
            actual = actual.Trim('"');
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData("kek","lol","Auth is faild")]
        public async Task Auth(string email,string password,string expected)
        {
            var user = new UserAuth(email, password);
            string stringjson = JsonConvert.SerializeObject(user);
            var response = await _client.PostAsync("https://localhost:5001/API/user/auth", new StringContent(stringjson));
            var actual = await response.Content.ReadAsStringAsync();
            actual = actual.Trim('"');
            Assert.NotEqual(expected, actual);
        }
        
        [Theory]
        [InlineData("role",null)]
        public async Task CreateRole(string roleName,string expected)
        {
            var role = new CreateRole(roleName);
            string stringjson = JsonConvert.SerializeObject(role);
            var response = await _client.PostAsync("https://localhost:5001/API/role", new StringContent(stringjson));
            var actual = await response.Content.ReadAsStringAsync();
            dynamic jsonObject = JObject.Parse(actual);
            Assert.Equal(expected, (string)jsonObject.hasErrors);
        }
        
        [Theory]
        [InlineData("ff520269-78f3-4c6a-898f-c778968dce41","lol","POP","POP","POP","POP",false,"POP",false,null)]
        [InlineData("ff520269-78f3-4c6a-898f-c778968dce41","POP","POP","POP","POP","POP",false,"POP",false,null)]
        public async Task UpdateUser(Guid Id,string avatar,string userName,string normalizedUserName,string email, string normalizedEmail,bool emailConfirmed,
            string passwordHash,bool isBanned,string expected)
        {
            var user = new User(Id,avatar,userName,normalizedUserName,email,normalizedEmail,emailConfirmed,passwordHash,isBanned);
            string stringjson = JsonConvert.SerializeObject(user);
            var response = await _client.PostAsync("https://localhost:5001/API/user/update", new StringContent(stringjson));
            var actual = await response.Content.ReadAsStringAsync();
            dynamic jsonObject = JObject.Parse(actual);
            Assert.Equal(expected, (string)jsonObject.hasErrors);
        }
        
        [Theory]
        [InlineData("ff520269-78f3-4c6a-898f-c778968dce41","POP","POP","POP","POP","POP",false,"POP",false,"User not updated")]
        // [InlineData("ff520269-78f3-4c6a-898f-c778968dce41","POP","POP","POP","POP","POP",false,"POP",false,null)]
        public async Task UpdateUserWrong(Guid Id,string avatar,string userName,string normalizedUserName,string email, string normalizedEmail,bool emailConfirmed,
            string passwordHash,bool isBanned,string expected)
        {
            var user = new User(Id,avatar,userName,normalizedUserName,email,normalizedEmail,emailConfirmed,passwordHash,isBanned);
            string stringjson = JsonConvert.SerializeObject(user);
            var response = await _client.PostAsync("https://localhost:5001/API/user/update", new StringContent(stringjson));
            var actual = await response.Content.ReadAsStringAsync();
            actual = actual.Trim('"');
            Assert.Equal(expected, actual);
        }
        
    }
}