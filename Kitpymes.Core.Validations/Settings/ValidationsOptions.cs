// -----------------------------------------------------------------------
// <copyright file="ValidationsOptions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Validations
{
    using System.Linq;

    /*
        Configuración de las validaciones ValidationsOptions
        Contiene las opciones de las validaciones
    */

    /// <summary>
    /// Configuración de las validaciones <c>ValidationsOptions</c>.
    /// Contiene las opciones de las validaciones.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las opciones de las validaciones.</para>
    /// </remarks>
    public class ValidationsOptions
    {
        /// <summary>
        /// Obtiene la configuración de FluentValidation.
        /// </summary>
        public ValidationsSettings ValidationsSettings { get; private set; } = new ValidationsSettings();

        /// <summary>
        /// Habilita el proveedor de validaciones FluentValidation.
        /// </summary>
        /// <param name="assembliesNames">Nombre de los assemblies.</param>
        /// <returns>La clase ValidationsOptions.</returns>
        public ValidationsOptions UseFluentValidator(params string[] assembliesNames)
        {
            if (assembliesNames != null && assembliesNames.Any())
            {
                ValidationsSettings.FluentValidationSettings.Enabled = true;

                ValidationsSettings.FluentValidationSettings?.Assemblies?.AddRange(assembliesNames);
            }

            return this;
        }
    }
}
