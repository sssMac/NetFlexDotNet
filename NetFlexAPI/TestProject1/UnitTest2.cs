﻿using Xunit;
using System;

using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using GiraffeAPI;
using GiraffeAPI.Data;
using GiraffeAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;

namespace TestProject;

public class UnitTest2
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup: class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<ApplicationContext.ApplicationContext>));

                services.Remove(descriptor);

                services.AddDbContext<ApplicationContext.ApplicationContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<ApplicationContext.ApplicationContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    db.Database.EnsureCreated();

                    try
                    {
                        Utilities.InitializeDbForTests(db);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                                            "database with test messages. Error: {Message}", ex.Message);
                    }
                }
            });
        }
    }

    public class IntegrationTests : IClassFixture<CustomWebApplicationFactory<App.Startup>>
    {
        private readonly CustomWebApplicationFactory<App.Startup> _factory;
        private readonly HttpClient _client;

        // public IntegrationTests(HostBuilder server)
        // {
        //     _client = server.CreateClient();
        //     string jwt = JwtCreate.generateToken("Admin");
        //     _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");
        // }
        
        public IntegrationTests(CustomWebApplicationFactory<App.Startup> factory)
        {
            _factory = factory; 
            _client = _factory.CreateClient();
            string jwt = JwtCreate.generateToken("Admin");
            _client.DefaultRequestHeaders.Add("Authorization",$"Bearer {jwt}");
        }

        // Create User TESTS

        [Theory]
        [InlineData("kek", "lol", "kek")]
        [InlineData("lol", "kek", "lol")]
        public async Task CreateUserOK(string email, string password, string expected)
        {
            var user = new CreateUserRequest(email, password);
            string stringjson = JsonConvert.SerializeObject(user);
            var response = await _client.PostAsync("/API/user", new StringContent(stringjson));
            var actual = await response.Content.ReadAsStringAsync();
            dynamic jsonObject = JObject.Parse(actual);
            Assert.Equal(expected, (string) jsonObject.userName);
        }

        [Theory]
        [InlineData("", "lol", "Email is required")]
        [InlineData("lol", "", "Password is required")]
        public async Task CreateUserWrongEmailFormat(string email, string password, string expected)
        {
            var user = new CreateUserRequest(email, password);
            string stringjson = JsonConvert.SerializeObject(user);
            var response = await _client.PostAsync("/API/user", new StringContent(stringjson));
            var actual = await response.Content.ReadAsStringAsync();
            actual = actual.Trim('"');
            Assert.Equal(expected, actual);
        }

        // Get User TESTS


        [Theory]
        [InlineData("f92ac032-4341-4845-99c7-6dc2ed8a9624", "Alexey@gmail.com")]
        public async Task GetUserByIdOK(Guid val1, string expected)
        {
            var response = await _client.GetAsync($"/API/user/{val1}");
            var actual = await response.Content.ReadAsStringAsync();
            dynamic jsonObject = JObject.Parse(actual);
            Assert.Equal(expected, (string) jsonObject.userName);
        }

        [Theory]
        [InlineData("30012610-f20f-482f-869a-8de0d4547670", "User not found")]
        public async Task UserNotFind(Guid val1, string expected)
        {
            var response = await _client.GetAsync($"/API/user/{val1}");
            var actual = await response.Content.ReadAsStringAsync();
            actual = actual.Trim('"');
            Assert.Equal(expected, actual);
        }


        // Ban User TESTS

        [Theory]
        [InlineData("f92ac032-4341-4845-99c7-6dc2ed8a9624", "User not found or already banned")]
        public async Task ABanUserAlreadyBanned(Guid val1, string expected)
        {
            var response = await _client.GetAsync($"/API/user/ban/{val1}");
            var actual = await response.Content.ReadAsStringAsync();
            actual = actual.Trim('"');
            Assert.Equal(expected, actual);
        }


        [Theory]
        [InlineData("f92ac032-4341-4845-99c7-6dc2ed8a9624", true)]
        public async Task ABanUserOK(Guid val1, bool expected)
        {
            var response = await _client.GetAsync($"/API/user/ban/{val1}");
            var actual = await response.Content.ReadAsStringAsync();
            dynamic jsonObject = JObject.Parse(actual);
            Assert.Equal(expected, (bool) jsonObject.isBanned);
        }



        // UnBan User TESTS

        [Theory]
        [InlineData("f92ac032-4341-4845-99c7-6dc2ed8a9624", "User not found or already unbanned")]
        public async Task AAUnBanUserAlreadyBanned(Guid val1, string expected)
        {
            var response = await _client.GetAsync($"/API/user/unban/{val1}");
            var actual = await response.Content.ReadAsStringAsync();
            actual = actual.Trim('"');
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("f92ac032-4341-4845-99c7-6dc2ed8a9624", false)]
        public async Task AAUnBanUserOK(Guid val1, bool expected)
        {
            var response = await _client.GetAsync($"/API/user/unban/{val1}");
            var actual = await response.Content.ReadAsStringAsync();
            dynamic jsonObject = JObject.Parse(actual);
            Assert.Equal(expected, (bool) jsonObject.isBanned);
        }


        [Theory]
        [InlineData("37050332-97c2-4fb9-a9cd-97b5c86b35d6", "User")]
        public async Task GetRoleById(Guid val1, string expected)
        {
            var response = await _client.GetAsync($"/API/role/{val1}");
            var actual = await response.Content.ReadAsStringAsync();
            dynamic jsonObject = JObject.Parse(actual);
            Assert.Equal(expected, (string) jsonObject.roleName);
        }

        [Theory]
        [InlineData("30012610-f20f-482f-869a-8de0d4547679", "Role not found")]
        public async Task RoleNotFound(Guid val1, string expected)
        {
            var response = await _client.GetAsync($"/API/role/{val1}");
            var actual = await response.Content.ReadAsStringAsync();
            actual = actual.Trim('"');
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("57050332-97c2-4fb9-a9cd-97b5c86b35d6", "Redactor1", null)]
        [InlineData("57050332-97c2-4fb9-a9cd-97b5c86b35d6", "Redactor", null)]
        public async Task UpdateRole(Guid RoleId, string RoleName, string expected)
        {
            var role = new Role(RoleId, RoleName);
            string stringjson = JsonConvert.SerializeObject(role);
            var response = await _client.PostAsync("/API/role/update", new StringContent(stringjson));
            var actual = await response.Content.ReadAsStringAsync();
            dynamic jsonObject = JObject.Parse(actual);
            Assert.Equal(expected, (string) jsonObject.hasErrors);
        }

        [Theory]
        [InlineData("kek", "qq", "Auth is faild")]
        [InlineData("lol", "qq", "Auth is faild")]
        public async Task WrongAuth(string email, string password, string expected)
        {
            var user = new UserAuth(email, password);
            string stringjson = JsonConvert.SerializeObject(user);
            var response = await _client.PostAsync("/API/user/auth", new StringContent(stringjson));
            var actual = await response.Content.ReadAsStringAsync();
            actual = actual.Trim('"');
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Alexey@gmail.com", "123321", "Auth is faild")]
        public async Task Auth(string email, string password, string expected)
        {
            var user = new UserAuth(email, password);
            string stringjson = JsonConvert.SerializeObject(user);
            var response = await _client.PostAsync("/API/user/auth", new StringContent(stringjson));
            var actual = await response.Content.ReadAsStringAsync();
            actual = actual.Trim('"');
            Assert.NotEqual(expected, actual);
        }

        [Theory]
        [InlineData("role", null)]
        public async Task CreateRole(string roleName, string expected)
        {
            var role = new CreateRole(roleName);
            string stringjson = JsonConvert.SerializeObject(role);
            var response = await _client.PostAsync("/API/role", new StringContent(stringjson));
            var actual = await response.Content.ReadAsStringAsync();
            dynamic jsonObject = JObject.Parse(actual);
            Assert.Equal(expected, (string) jsonObject.hasErrors);
        }

        [Theory]
        [InlineData("93d3cb87-1446-4c48-9ce2-e70ef629ec43","lol","POP","POP","POP","POP",false,"POP",false,null)]
        [InlineData("93d3cb87-1446-4c48-9ce2-e70ef629ec43","POP","POP","POP","POP","POP",false,"POP",false,null)]
        public async Task UpdateUser(Guid Id,string avatar,string userName,string normalizedUserName,string email, string normalizedEmail,bool emailConfirmed,
            string passwordHash,bool isBanned,string expected)
        {
            var user = new User(Id,avatar,userName,normalizedUserName,email,normalizedEmail,emailConfirmed,passwordHash,isBanned);
            string stringjson = JsonConvert.SerializeObject(user);
            var response = await _client.PostAsync("/API/user/update", new StringContent(stringjson));
            var actual = await response.Content.ReadAsStringAsync();
            dynamic jsonObject = JObject.Parse(actual);
            Assert.Equal(expected, (string)jsonObject.hasErrors);
        }
        
        [Theory]
        [InlineData("93d3cb87-1446-4c48-9ce2-e70ef629ec43","POP","POP","POP","POP","POP",false,"POP",false,"User not updated")]
        public async Task UpdateUserWrong(Guid Id,string avatar,string userName,string normalizedUserName,string email, string normalizedEmail,bool emailConfirmed,
            string passwordHash,bool isBanned,string expected)
        {
            var user = new User(Id,avatar,userName,normalizedUserName,email,normalizedEmail,emailConfirmed,passwordHash,isBanned);
            string stringjson = JsonConvert.SerializeObject(user);
            var response = await _client.PostAsync("/API/user/update", new StringContent(stringjson));
            var actual = await response.Content.ReadAsStringAsync();
            actual = actual.Trim('"');
            Assert.Equal(expected, actual);
        }
        
    }
}