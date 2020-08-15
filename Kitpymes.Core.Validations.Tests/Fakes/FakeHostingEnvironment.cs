using Microsoft.AspNetCore.Hosting;
using Moq;

namespace Kitpymes.Core.Validations.Tests
{
    public class FakeHostingEnvironment
    {
        public static IWebHostEnvironment Get(string? enviromentName)
        {
            var environment = new Mock<IWebHostEnvironment>();

            if (!string.IsNullOrWhiteSpace(enviromentName))
            {
                environment
                    .Setup(e => e.EnvironmentName)
                    .Returns(enviromentName);
            }

            return environment.Object;
        }
    }
}
