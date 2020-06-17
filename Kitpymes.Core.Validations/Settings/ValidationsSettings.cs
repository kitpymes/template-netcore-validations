// -----------------------------------------------------------------------
// <copyright file="ValidationsSettings.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Validations
{
    using Kitpymes.Core.Validations.FluentValidation;

    /*
        Configuración de las validaciones ValidationsSettings
        Contiene las opciones de los tipos de validadores
    */

    /// <summary>
    /// Configuración de las validaciones <c>ValidationsSettings</c>.
    /// Contiene las opciones de los tipos de validadores.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las opciones de los proveedores de validaciones.</para>
    /// </remarks>
    public class ValidationsSettings
    {
        /// <summary>
        /// Obtiene o establece la configuración de FluentValidation.
        /// </summary>
        public FluentValidationSettings FluentValidationSettings { get; set; } = new FluentValidationSettings();
    }
}
