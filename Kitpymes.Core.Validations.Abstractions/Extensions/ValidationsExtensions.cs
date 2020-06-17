// -----------------------------------------------------------------------
// <copyright file="ValidationsExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Validations.Abstractions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Text.Json;
    using System.Text.RegularExpressions;
    using Microsoft.AspNetCore.Http;

    /*
        Clase de extensión ValidationsExtensions
        Contiene las extensiones de las validaciones
    */

    /// <summary>
    /// Clase de extensión <c>ValidationsExtensions</c>.
    /// Contiene las extensiones de las validaciones.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las extensiones para las validaciones.</para>
    /// </remarks>
    public static class ValidationsExtensions
    {
        /// <summary>
        /// Combina las opciones personalizadas <paramref name="customOptions"/> de una acción con las opciones por defecto <paramref name="defaultOptions"/>.
        /// </summary>
        /// <typeparam name="TOptions">Es de tipo class.</typeparam>
        /// <param name="customOptions">Opciones personalizadas.</param>
        /// <param name="defaultOptions">Opcionces por defecto.</param>
        /// <returns>
        /// Las opciones combinadas.
        /// </returns>
        [return: NotNull]
        public static TOptions ToConfigureOrDefault<TOptions>(this Action<TOptions>? customOptions, TOptions? defaultOptions = null)
            where TOptions : class, new()
        {
            defaultOptions ??= new TOptions();

            customOptions?.Invoke(defaultOptions);

            return defaultOptions;
        }

        /// <summary>
        /// Serializa un objeto.
        /// </summary>
        /// <typeparam name="T">Tipo del valor.</typeparam>
        /// <param name="value">El valor a serializar.</param>
        /// <param name="options">Los ajustes para la serialización.</param>
        /// <returns>El valor serializado.</returns>
        public static string ToSerialize<T>(this T value, JsonSerializerOptions? options = null)
        {
            options ??= new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };

            return value is null ? string.Empty : JsonSerializer.Serialize(value, options);
        }

        /// <summary>
        /// Formatea un valor.
        /// </summary>
        /// <param name="value">El valor que se le quiere dar formato.</param>
        /// <param name="args">Los argumentos para el formato.</param>
        /// <returns>El valor formateado.</returns>
        public static string ToFormat(this string value, params object[] args)
        => value.ToFormat(CultureInfo.CurrentCulture, args);

        /// <summary>
        /// Formatea un valor.
        /// </summary>
        /// <param name="value">El valor que se le quiere dar formato.</param>
        /// <param name="formatProvider">El proveedor para dar formato.</param>
        /// <param name="args">Los argumentos para el formato.</param>
        /// <returns>El valor formateado.</returns>
        public static string ToFormat(this string value, IFormatProvider formatProvider, params object[] args)
        => string.Format(formatProvider, value, args);

        /// <summary>
        /// Obtiene el mensaje corto de la excepción.
        /// </summary>
        /// <param name="exception">La excepción que se obtiene el mensaje.</param>
        /// <returns>El mensaje de la excepción.</returns>
        public static string ToMessage(this Exception exception)
        {
            if (exception is null)
            {
                return string.Empty;
            }

            return exception.InnerException is null
                 ? exception.Message
                 : exception.Message + "\n\n --> " + exception.InnerException;
        }

        /// <summary>
        /// Reemplaza valores por espacios en blanco.
        /// </summary>
        /// <param name="value">Valor a remplazar.</param>
        /// <param name="replace">Valores que se quieren eliminar reemplazaran.</param>
        /// <returns>El valor sin caracteres espaciales.</returns>
        public static string? ToEmptyReplace(this string value, params string[] replace)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            foreach (var item in replace)
            {
                value = value.Replace(item, string.Empty, StringComparison.CurrentCulture);
            }

            return value;
        }

        /// <summary>
        /// Obtiene el mensaje full de la excepción.
        /// </summary>
        /// <param name="exception">La excepción que se obtiene el mensaje.</param>
        /// <returns>El mensaje de la excepción.</returns>
        public static string ToFullMessage(this Exception exception)
        {
            if (exception is null)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();

            sb.Append($"| Error: {exception.ToMessage()} ");

            var stackFrame = new StackTrace(exception, true)?.GetFrame(0);

            var declaringType = stackFrame?.GetMethod()?.DeclaringType;

            if (declaringType != null)
            {
                sb.Append($"| File: {declaringType} ");
            }

            var fileLineNumber = stackFrame?.GetFileLineNumber();

            if (fileLineNumber.HasValue)
            {
                sb.Append($"| Line: {fileLineNumber.Value} ");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Obtiene el valor pode defecto.
        /// </summary>
        /// <typeparam name="T">Tipo del valor.</typeparam>
        /// <param name="value">El valor a obtener su valor por defecto.</param>
        /// <returns>El valor por defecto.</returns>
        public static object? ToDefaultValue<T>(this T value)
        => value?.GetType().ToDefaultValue();

        /// <summary>
        /// Obtiene el valor pode defecto.
        /// </summary>
        /// <param name="type">El tipo del valor.</param>
        /// <returns>El valor por defecto.</returns>
        public static object? ToDefaultValue([NotNull] this Type type)
        {
            if (type is null)
            {
                return default;
            }

            return type.IsValueType ? Activator.CreateInstance(type) : default;
        }

        /// <summary>
        /// Verifica si un objeto es numerico o no.
        /// </summary>
        /// <param name="value">El valor a evaluar.</param>
        /// <returns>Si es numerico o no.</returns>
        public static bool ToIsNumeric(this object value)
            => double.TryParse(value?.ToString(), out var v);

        /// <summary>
        /// Verifica si un objeto es numerico o no.
        /// </summary>
        /// <param name="value">El valor a evaluar.</param>
        /// <returns>El valor comvertido en double.</returns>
        public static double? ToNumericParse(this object value)
        {
            if (double.TryParse(value?.ToString(), out var v))
            {
                return v;
            }

            return null;
        }

        /// <summary>
        /// Devuelve los detalles del HttpContext.
        /// </summary>
        /// <param name="httpContext">HttpContext donde se obtienen los detalles.</param>
        /// <param name="data">Datos ingresados.</param>
        /// <returns>Los detalles.</returns>
        public static string? ToDetails(this HttpContext httpContext, string? data = null)
        {
            if (httpContext is null)
            {
                return null;
            }

            var sb = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(httpContext.Connection?.RemoteIpAddress?.MapToIPv6().ToString()))
            {
                sb.Append($"| IP: {httpContext.Connection.RemoteIpAddress.MapToIPv6().ToString()} ");
            }

            if (httpContext.Request.Headers["User-Agent"].Any())
            {
                sb.Append($"| UserAgent: {string.Join(", ", httpContext.Request.Headers["User-Agent"].Select(x => x))} ");
            }

            sb.Append($"| ContentType: {httpContext.Request.ContentType} ");

            var user = httpContext.User.Identity.IsAuthenticated ? httpContext.User.Identity.Name : "Anonymous";

            sb.Append($"| User: {user}");

            var path = $"{httpContext.Request.Scheme}://{httpContext.Request.Host.Value}{httpContext.Request.Path.Value}";

            sb.Append($"| {httpContext.Request.Method}: {path} ");

            if (!string.IsNullOrWhiteSpace(data))
            {
                sb.Append($"| Data: {data} ");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Carga los assemblies por el nombre.
        /// </summary>
        /// <param name="assemblies">Nombre de los assemblies a cargar.</param>
        /// <returns>Lista de assemblies cargados y lista de errores al cargar.</returns>
        public static IEnumerable<Assembly> ToAssemblyThrow([NotNull] this List<string> assemblies)
        => assemblies.Select(assembly => assembly.ToAssemblyThrow());

        /// <summary>
        /// Carga los assemblies por el nombre.
        /// </summary>
        /// <param name="assembly">Nombre del assembly a cargar.</param>
        /// <returns>El assembly cargado.</returns>
        public static Assembly ToAssemblyThrow([NotNull] this string assembly)
        {
            try
            {
                return Assembly.Load(assembly);
            }
            catch (System.IO.FileNotFoundException)
            {
                throw new ApplicationException($"The file '{assembly}' cannot be found.");
            }
            catch (BadImageFormatException)
            {
                throw new ApplicationException($"The file '{assembly}' is not an assembly.");
            }
            catch (System.IO.FileLoadException)
            {
                throw new ApplicationException($"The assembly '{assembly}' has already been loaded.");
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
    }
}
