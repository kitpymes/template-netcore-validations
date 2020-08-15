using Tests.Api.Models;
using Kitpymes.Core.Shared;
using Kitpymes.Core.Validations.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Api.EndpointsTests
{
    [TestClass]
    public class ValidatorTests
    {
        [TestMethod]
        public async Task Validate_Endpoint_AddPerson()
        {
            // Arrange
            var host = await new HostBuilder()
                .ConfigureWebHost(webHost => webHost
                .ConfigureAppConfiguration(configHost =>
                {
                    configHost.SetBasePath(Directory.GetCurrentDirectory());
                    configHost.AddJsonFile("appsettings.json", optional: true);
                    configHost.AddEnvironmentVariables(prefix: "PREFIX_");
                })
                .UseTestServer()
                .UseStartup<Startup>())
                .StartAsync();

            var client = host.GetTestClient();

            var uri = "/Validator/AddPerson";

            var person = new
            {
                Age = 15,
                Email = "ddd@@_.45r",
                Name = ""
            };

            var stringContent = new StringContent(person.ToSerialize(), Encoding.UTF8, "application/json");

            // Act
            var httpResponse = await client.PostAsync(uri, stringContent);
            var httpResponseContent = await httpResponse.Content.ReadAsStringAsync();

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(httpResponseContent));

            Log(httpResponseContent);
        }

        [TestMethod]
        public void Validate_Domain_AddPerson()
        {
            // Arrange
            var age = 15;
            var name = "";
            var email = "ddd@@_.45r";

            // Act
            var exception = Assert.ThrowsException<ValidationsException>(() => new Person(age, name, email));

            // Assert
            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.HasErrors);

            Log(exception.Errors.ToSerialize());
        }

        [TestMethod]
        public void Validate_Domain_ChangeName()
        {
            // Arrange
            var invalidName = "435_///3sdff·543";
            var person = new Person(20, "Pedro", "pedro@gmail.com");

            // Act
            var exception = Assert.ThrowsException<ValidationsException>(() => person.ChangeName(invalidName));

            // Assert
            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.HasErrors);

            Log(exception.Errors.ToSerialize());
        }

        private void Log(string message) => Console.WriteLine(message);
    }
}
