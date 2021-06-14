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
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using Kitpymes.Core.Shared;
    using Kitpymes.Core.Validations.Abstractions;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
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
        /// <param name="loggerFactory">Representa un tipo utilizado para configurar el registro de errores.</param>
        public ValidationsMiddleware(RequestDelegate requestDelegate, ILoggerFactory loggerFactory)
        {
            RequestDelegate = requestDelegate;

            Logger = loggerFactory.CreateLogger<ValidationsMiddleware>();
        }

        private RequestDelegate RequestDelegate { get; }

        private ILogger<ValidationsMiddleware> Logger { get; }

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
                    await ReadRequestBodyAsync(httpContext).ConfigureAwait(false);
                }

                await RequestDelegate(httpContext).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(httpContext, exception).ConfigureAwait(false);
            }
        }

        private async Task ReadRequestBodyAsync(HttpContext httpContext)
        {
            var request = httpContext.Request;

            request.EnableBuffering();

            var buffer = new byte[Convert.ToInt32(request.ContentLength, CultureInfo.CurrentCulture)];

            await request.Body.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);

            RequestBody = Encoding.UTF8.GetString(buffer).ToRemove("\r\n", " ");

            request.Body.Position = 0;
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
                            Logger.LogError(exception, details);

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
