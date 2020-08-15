// -----------------------------------------------------------------------
// <copyright file="ValidationsServiceCollectionExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Validations
{
    using System;
    using System.Linq;
    using Kitpymes.Core.Shared;
    using Kitpymes.Core.Validations.Abstractions;
    using Kitpymes.Core.Validations.FluentValidation;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /*
        Clase de extensión ValidationsServiceCollectionExtensions
        Contiene las extensiones de los servicios de las validaciones
    */

    /// <summary>
    /// Clase de extensión <c>ValidationsServiceCollectionExtensions</c>.
    /// Contiene las extensiones de los servicios de las validaciones.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las extensiones de los servicios para las validaciones.</para>
    /// </remarks>
    public static class ValidationsServiceCollectionExtensions
    {
        /// <summary>
        /// Carga el servicio de validaciones.
        /// </summary>
        /// <param name="services">Colección de servicios.</param>
        /// <param name="configuration">El appsettings con su configuración.</param>
        /// <returns>La interface IServiceCollection.</returns>
        public static IServiceCollection LoadValidations(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration?.GetSection(nameof(ValidationsSettings))?.Get<ValidationsSettings>();

            if (settings != null)
            {
                services.LoadValidations(settings);
            }

            return services;
        }

        /// <summary>
        /// Carga el servicio de validaciones.
        /// </summary>
        /// <param name="services">Colección de servicios.</param>
        /// <param name="options">Las opciones personalizadas.</param>
        /// <returns>La interface IServiceCollection.</returns>
        public static IServiceCollection LoadValidations(this IServiceCollection services, Action<ValidationsOptions> options)
         {
            var settings = options.ToConfigureOrDefault();

            services.LoadValidations(settings.ValidationsSettings);

            return services;
         }

        private static IServiceCollection LoadValidations(this IServiceCollection services, ValidationsSettings settings)
        {
            if (settings?.FluentValidationSettings != null)
            {
                var mvcBuilder = services.ToService<IMvcBuilder>() ?? services.AddControllers();

                mvcBuilder.ConfigureApiBehaviorOptions(x =>
                {
                    x.InvalidModelStateResponseFactory = context =>
                    {
                        var messages = context.ModelState
                            .Where(e => e.Value.Errors.Any())
                            .ToDictionary(
                                key => key.Key,
                                value => string.Join(", ", value.Value.Errors.Select(e => e.ErrorMessage)));

                        throw new ValidationsException(messages);
                    };
                });

                services.LoadFluentValidation(settings.FluentValidationSettings);
            }

            return services;
        }
    }
}
