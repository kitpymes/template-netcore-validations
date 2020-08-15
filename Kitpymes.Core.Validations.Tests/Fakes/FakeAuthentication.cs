using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Security.Claims;

namespace Kitpymes.Core.Validations.Tests
{
    public static class FakeAuthentication
    {
        public static DefaultHttpContext GetHttpContextAuthenticated
        (
            this IServiceCollection services,

            string authenticationType,

            params Claim[] claims
        )
        {
            var context = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(claims, authenticationType))
            };

            services.AddAuthentication(authenticationType);

            var application = new ApplicationBuilder(services.BuildServiceProvider());

            application.Use(async (context, next) =>
            {
                await context.AuthenticateAsync(authenticationType).ConfigureAwait(false);

                await next().ConfigureAwait(false);
            });

            return context;
        }

        public static DefaultHttpContext GetHttpContextAuthenticated
        (
            string authenticationType,

            List<Claim> claims
        )
        {
            var context = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(claims, authenticationType))
            };

            var services = new ServiceCollection();

            services.AddAuthentication(authenticationType);

            var application = new ApplicationBuilder(services.BuildServiceProvider());

            application.Use(async (context, next) =>
            {
                await context.AuthenticateAsync(authenticationType).ConfigureAwait(false);

                await next().ConfigureAwait(false);
            });

            return context;
        }
    }
}
