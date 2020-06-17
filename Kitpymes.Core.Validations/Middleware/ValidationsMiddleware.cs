// -----------------------------------------------------------------------
// <copyright file="ValidationsMiddleware.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Validations
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Mime;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Kitpymes.Core.Validations.Abstractions;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

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
        /// <param name="request">Una función que puede procesar una solicitud HTTP.</param>
        /// <param name="loggerFactory">Representa un tipo utilizado para configurar el registro de errores.</param>
        public ValidationsMiddleware(RequestDelegate request, ILoggerFactory loggerFactory)
        {
            Request = request;

            Logger = loggerFactory.CreateLogger<ValidationsMiddleware>();
        }

        private RequestDelegate Request { get; }

        private ILogger<ValidationsMiddleware> Logger { get; }

        private string? RequestData { get; set; }

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
                    httpContext.Request.EnableBuffering();

                    using var reader = new StreamReader(
                        httpContext.Request.Body,
                        encoding: Encoding.UTF8,
                        detectEncodingFromByteOrderMarks: false,
                        leaveOpen: true);

                    RequestData = await reader.ReadToEndAsync().ConfigureAwait(false);

                    RequestData = RequestData.ToEmptyReplace("\r\n", " ");

                    httpContext.Request.Body.Position = 0;
                }

                await Request(httpContext).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                var environment = (IWebHostEnvironment)httpContext.RequestServices.GetService(typeof(IWebHostEnvironment));

                var details = httpContext.ToDetails(RequestData);

                dynamic result = new System.Dynamic.ExpandoObject();

                result.Success = false;

                result.ErrorId = Guid.NewGuid().ToString();

                HttpStatusCode statusCode = default;

                switch (exception)
                {
                    case ValidationsException validationException when exception is ValidationsException:

                        result.Message = validationException.Message;

                        if (environment.IsDevelopment())
                        {
                            result.Details = details;
                        }

                        statusCode = HttpStatusCode.BadRequest;

                        break;

                    case UnauthorizedAccessException unauthorizedAccessException when exception is UnauthorizedAccessException:

                        if (environment.IsDevelopment())
                        {
                            result.Message = unauthorizedAccessException.ToFullMessage();

                            result.Details = details;
                        }
                        else
                        {
                            result.Message = Resources.MsgUnauthorizedAccess;
                        }

                        statusCode = HttpStatusCode.Unauthorized;

                        break;

                    default:

                        if (environment.IsDevelopment())
                        {
                            result.Message = exception.ToFullMessage();

                            result.Details = details;
                        }
                        else
                        {
                            Logger.LogError(exception, details);

                            result.Message = Resources.MsgFriendlyUnexpectedError;
                        }

                        statusCode = HttpStatusCode.InternalServerError;

                        break;
                }

                result.Status = (int)statusCode;

                httpContext.Response.Clear();

                httpContext.Response.StatusCode = result.Status;

                httpContext.Response.ContentType = MediaTypeNames.Application.Json;

                httpContext.Response.Headers.Add(nameof(Exception), exception.GetType().Name);

                var json = JsonConvert.SerializeObject(result as object);

                await httpContext.Response.WriteAsync(json).ConfigureAwait(false);
            }
        }
    }
}
