using Kitpymes.Core.Validations.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Kitpymes.Core.Validations.Tests
{
    [TestClass]
    public class ValidationsMiddlewareTests
    {
        #region ValidationException

        [TestMethod]
        public async Task Passing_ValidationExceptionWithErrors_Returns_BadRequestStatusWithErrors()
        {
            string environmentName = "Development";

            var key = Guid.NewGuid().ToString();
            var message = Guid.NewGuid().ToString();

            var errors = new Dictionary<string, IEnumerable<string>>
            {
                { key, new[] { message } }
            };

            var exception = new ValidationsException(errors);

            (DefaultHttpContext httpContext, string result) = await InvokeMiddlewareAsync
            (
                exception, environmentName
            ).ConfigureAwait(false);

            Assert.IsTrue(result.Contains(message, StringComparison.CurrentCulture));

            Assert.IsNotNull(httpContext.Response);

            Assert.IsTrue(httpContext.Response.StatusCode == (int)HttpStatusCode.BadRequest);

            Assert.IsTrue(httpContext.Response.ContentType == MediaTypeNames.Application.Json);
        }

        [TestMethod]
        public async Task Passing_ValidationExceptionWithErrorsWithDetails_Returns_BadRequestStatusWithErrors()
        {
            string environmentName = "Development";

            var key = Guid.NewGuid().ToString();
            var message = Guid.NewGuid().ToString();

            var errors = new Dictionary<string, IEnumerable<string>>
            {
                { key, new[] { message } }
            };

            (string path, string host) details = ("/" + Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            var exception = new ValidationsException(errors);

            (DefaultHttpContext httpContext, string result) = await InvokeMiddlewareAsync
            (
                exception, environmentName, details
            ).ConfigureAwait(false);

            Assert.IsTrue(result.Contains(message, StringComparison.CurrentCulture));

            Assert.IsTrue(result.Contains(details.path, StringComparison.CurrentCulture));

            Assert.IsTrue(result.Contains(details.host, StringComparison.CurrentCulture));

            Assert.IsNotNull(httpContext.Response);

            Assert.IsTrue(httpContext.Response.StatusCode == (int)HttpStatusCode.BadRequest);

            Assert.IsTrue(httpContext.Response.ContentType == MediaTypeNames.Application.Json);
        }

        #endregion ValidationException

        #region UnauthorizedAccessException

        [DataTestMethod]
        [DataRow("Development")]
        [DataRow("Production")]
        public async Task Passing_UnauthorizedAccessException_Returns_UnauthorizedStatusWithMessage(string environmentName)
        {
            var exception = new UnauthorizedAccessException(Guid.NewGuid().ToString());

            var message = environmentName == "Development"
                           ? exception.Message
                           : Resources.MsgUnauthorizedAccess;

            (DefaultHttpContext httpContext, string result) = await InvokeMiddlewareAsync
            (
                exception,

                environmentName
            ).ConfigureAwait(false);


            Assert.IsTrue(result.Contains(message, StringComparison.CurrentCulture));

            Assert.IsNotNull(httpContext.Response);

            Assert.IsTrue(httpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized);

            Assert.IsTrue(httpContext.Response.ContentType == MediaTypeNames.Application.Json);
        }

        #endregion UnauthorizedAccessException

        #region DefaultException

        [DataTestMethod]
        [DataRow("Development")]
        [DataRow("Production")]
        public async Task Passing_Exception_Returns_InternalServerErrorStatusWithMessage(string environmentName)
        {
            var exception = new Exception(Guid.NewGuid().ToString());

            var message = environmentName == "Development"
                           ? exception.Message
                           : Resources.MsgFriendlyUnexpectedError;

            (DefaultHttpContext httpContext, string result) = await InvokeMiddlewareAsync
            (
                exception, environmentName
            ).ConfigureAwait(false);

            Assert.IsTrue(result.Contains(message, StringComparison.CurrentCulture));

            Assert.IsNotNull(httpContext.Response);

            Assert.IsTrue(httpContext.Response.StatusCode == (int)HttpStatusCode.InternalServerError);

            Assert.IsTrue(httpContext.Response.ContentType == MediaTypeNames.Application.Json);
        }

        #endregion DefaultException

        private async Task<(DefaultHttpContext httpContext, string result)> InvokeMiddlewareAsync
        (
          Exception exception,

          string? environmentName,

          (string path, string host)? details = null
        )
        {
            var services = new ServiceCollection();

            services.AddSingleton(FakeHostingEnvironment.Get(environmentName));

            var httpContext = new DefaultHttpContext
            {
                RequestServices = services.BuildServiceProvider()
            };

            httpContext.Response.Body = new MemoryStream();

            httpContext.Request.Path = new PathString(details?.path);

            httpContext.Request.Host = new HostString(details?.host!);

            var middleware = new ValidationsMiddleware((HttpContext context) => throw exception);

            await middleware.InvokeAsync(httpContext).ConfigureAwait(false);

            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);

            using var reader = new StreamReader(httpContext.Response.Body);

            string result = await reader.ReadToEndAsync().ConfigureAwait(false);

            return (httpContext, result);
        }
    }
}