// -----------------------------------------------------------------------
// <copyright file="FluentValidationServiceCollectionExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Validations.FluentValidation
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using global::FluentValidation.AspNetCore;
    using Kitpymes.Core.Shared;
    using Kitpymes.Core.Validations.Abstractions;
    using Microsoft.Extensions.DependencyInjection;

    /*
        Clase de extensión FluentValidationServiceCollectionExtensions
        Contiene las extensiones de los servicios de las validaciones
    */

    /// <summary>
    /// Clase de extensión <c>FluentValidationServiceCollectionExtensions</c>.
    /// Contiene las extensiones de los servicios de las validaciones.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las extensiones de los servicios para las validaciones.</para>
    /// </remarks>
    public static class FluentValidationServiceCollectionExtensions
    {
        /// <summary>
        /// Carga el servicio de validaciones.
        /// </summary>
        /// <param name="services">Colección de servicios.</param>
        /// <param name="settings">Configuración de FluentValidation.</param>
        /// <returns>La interface IServiceCollection.</returns>
        public static IServiceCollection LoadFluentValidation(this IServiceCollection services, FluentValidationSettings settings)
        {
            if (settings?.Enabled == true && settings.Assemblies != null && settings.Assemblies.Any())
            {
                services.LoadFluentValidation(settings.Assemblies.ToArray());
            }

            return services;
        }

        /// <summary>
        /// Carga el servicio de validaciones.
        /// </summary>
        /// <param name="services">Colección de servicios.</param>
        /// <param name="assemblies">Ensamblados donde se aplica FluentValidation.</param>
        /// <returns>La interface IServiceCollection.</returns>
        private static IServiceCollection LoadFluentValidation(this IServiceCollection services, params Assembly[] assemblies)
        {
            var mvcBuilder = services.BuildServiceProvider().GetService<IMvcBuilder>() ?? services.AddMvc();

            mvcBuilder.ConfigureApiBehaviorOptions(x =>
            {
                x.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value.Errors.Any())
                        .ToDictionary(
                            key => key.Key,
                            value => value.Value.Errors.Select(e => e.ErrorMessage));

                    throw new ValidationsException(errors);
                };
            }).AddFluentValidation(x => x.RegisterValidatorsFromAssemblies(assemblies));

            return services;
        }
    }
}
