// -----------------------------------------------------------------------
// <copyright file="ValidationResultExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Validations.FluentValidation
{
    using global::FluentValidation.Results;

    /*
        Clase de extensión ValidationResultExtensions
        Contiene las extensiones de la excepción de valdiaciones
    */

    /// <summary>
    /// Clase de extensión <c>ValidationResultExtensions</c>.
    /// Contiene las extensiones de la excepción de validaciones.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las extensiones de la excepción de validaciones.</para>
    /// </remarks>
    public static class ValidationResultExtensions
    {
        /// <summary>
        /// Para buscar un mensaje de error en la lista de errores de la excepción.
        /// </summary>
        /// <param name="validationResult">El resultado de ejecutar un validador.</param>
        /// <param name="message">El mensaje que queremos buscar en la lista de errores.</param>
        /// <returns>Un verdadero si contiene el mensaje buscado, si no falso.</returns>
        public static bool Contains(this ValidationResult validationResult, string message)
        => validationResult != null && validationResult.ToString().Contains(message, System.StringComparison.CurrentCulture);
    }
}
