// -----------------------------------------------------------------------
// <copyright file="ValidationsMiddleware.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Validations
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;
    using Kitpymes.Core.Logger.Abstractions;
    using Kitpymes.Core.Shared;
    using Kitpymes.Core.Validations.Abstractions;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using UTIL = Kitpymes.Core.Shared.Util;

    /*
        Clase del middlware ValidationsMiddleware
        Contiene el mensaje de devolución de los errores de validaciones
    */

    /// <summary>
    /// Clase del middlware <c>ValidationsMiddleware</c>.
    /// Contiene el mensaje de devolución de los errores de validaciones.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las propiedades que se requieran para las validaciones.</para>
    /// </remarks>
    public class ValidationsMiddleware
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ValidationsMiddleware"/>.
        /// </summary>
        /// <param name="requestDelegate">Una función que puede procesar una solicitud HTTP.</param>
        public ValidationsMiddleware(RequestDelegate requestDelegate)
        {
            RequestDelegate = requestDelegate;
        }

        private RequestDelegate RequestDelegate { get; }

        private ILogger? Logger { get; set; }

        private string? RequestBody { get; set; }

        /// <summary>
        /// Devuelve el mensaje configurado al cliente si ocurre una excepción.
        /// </summary>
        /// <param name="httpContext">Encapsula toda la información específica de una solicitud HTTP.</param>
        /// <returns>Task.</returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                if (httpContext != null)
                {
                    Logger = httpContext.RequestServices.GetService<ILoggerService>()?.CreateLogger<ValidationsMiddleware>();

                    await ReadRequestBodyAsync(httpContext).ConfigureAwait(false);

                    await RequestDelegate(httpContext).ConfigureAwait(false);
                }
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(httpContext, exception).ConfigureAwait(false);
            }
        }

        private async Task ReadRequestBodyAsync(HttpContext httpContext)
        {
            httpContext.Request.EnableBuffering();

            RequestBody = await new StreamReader(httpContext.Request.Body).ReadToEndAsync();

            RequestBody = RequestBody.ToRemove("\r", " ").ToRemove("\n", " ");

            httpContext.Request.Body.Position = 0;
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var environment = httpContext.RequestServices.ToEnvironment();

            var optionalData = new Dictionary<string, IEnumerable<string>>();

            if (!string.IsNullOrWhiteSpace(RequestBody))
            {
                optionalData.AddOrUpdate(nameof(RequestBody), RequestBody);
            }

            var details = httpContext.ToDetails(optionalData);

            UTIL.IResult? result = null;

            var exceptionTypeName = exception.GetType().Name;

            HttpStatusCode statusCode = default;

            switch (exception)
            {
                case ValidationsException validationException when exception is ValidationsException:

                    statusCode = HttpStatusCode.BadRequest;

                    result = UTIL.Result.Error(options =>
                    {
                        options.WithTitle(Resources.MsgErrorsTitle)
                            .WithStatusCode(statusCode)
                            .WithMessages(validationException.Message)
                            .WithErrors(validationException.Errors);

                        if (environment.IsDevelopment())
                        {
                            options
                                .WithExceptionType(exceptionTypeName)
                                .WithDetails(details);
                        }
                    });

                    break;

                case UnauthorizedAccessException unauthorizedAccessException when exception is UnauthorizedAccessException:

                    statusCode = HttpStatusCode.Unauthorized;

                    result = UTIL.Result.Error(options =>
                    {
                        options
                            .WithTitle(Resources.MsgErrorsTitle)
                            .WithStatusCode(statusCode);

                        if (environment.IsDevelopment())
                        {
                            options
                                .WithMessages(unauthorizedAccessException.ToFullMessage())
                                .WithExceptionType(exceptionTypeName)
                                .WithDetails(details);
                        }
                        else
                        {
                            options.WithMessages(Resources.MsgUnauthorizedAccess);
                        }
                    });

                    break;

                default:

                    statusCode = HttpStatusCode.InternalServerError;

                    result = UTIL.Result.Error(options =>
                    {
                        options
                            .WithTitle(Resources.MsgErrorsTitle)
                            .WithStatusCode(statusCode);

                        if (environment.IsDevelopment())
                        {
                            options
                                .WithMessages(exception.ToFullMessage())
                                .WithExceptionType(exceptionTypeName)
                                .WithDetails(details);
                        }
                        else
                        {
                            Logger?.Error(details, exception);

                            options.WithMessages(Resources.MsgFriendlyUnexpectedError);
                        }
                    });

                    break;
            }

            await httpContext.Response.ToResultAsync(
                status: statusCode,
                message: Newtonsoft.Json.JsonConvert.SerializeObject(result),
                headers: new Dictionary<string, IEnumerable<string>>
                {
                    { nameof(Exception), new List<string> { exceptionTypeName } },
                }).ConfigureAwait(false);
        }
    }
}
