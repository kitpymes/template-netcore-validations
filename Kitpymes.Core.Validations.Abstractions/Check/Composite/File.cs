// -----------------------------------------------------------------------
// <copyright file="File.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Validations.Abstractions
{
    using System.Linq;

    /*
        Configuración de los validadores Validator
        Contiene las opciones de los validadores
    */

    /// <summary>
    /// Configuración de los validadores <c>Validator</c>.
    /// Contiene las opciones de los validadores.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las opciones que necesitamos para el validador.</para>
    /// </remarks>
    public static partial class Check
    {
        /// <summary>
        /// Comprueba si los valores ingresados son validos.
        /// </summary>
        /// <param name="values">Valores a validar.</param>
        /// <returns>(bool HasErrors, int Count).</returns>
        public static (bool HasErrors, int Count) IsFile(params string?[] values)
        {
            var errorsIsFile = values.Where(value => IsNullOrEmpty(value).HasErrors || !System.IO.File.Exists(value));

            return (errorsIsFile.Any(), errorsIsFile.Count());
        }
    }
}
