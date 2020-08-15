// -----------------------------------------------------------------------
// <copyright file="FluentValidationServiceCollectionExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Validations.FluentValidation
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using global::FluentValidation.AspNetCore;
    using Kitpymes.Core.Shared;
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
            if (settings != null && settings.Enabled.HasValue && settings.Enabled.Value && settings.Assemblies != null && settings.Assemblies.Any())
            {
                var assemblies = settings.Assemblies.ToAssembly();

                services.LoadFluentValidation(assemblies.ToArray());
            }

            return services;
        }

        private static IServiceCollection LoadFluentValidation(this IServiceCollection services, params Assembly[] assemblies)
        {
            var mvcBuilder = services.BuildServiceProvider().GetService<IMvcBuilder>() ?? services.AddMvc();

            mvcBuilder?.AddFluentValidation(x => x.RegisterValidatorsFromAssemblies(assemblies));

            return services;
        }
    }
}
