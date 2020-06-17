// -----------------------------------------------------------------------
// <copyright file="NullOrEmpty.cs" company="Kitpymes">
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
        public static (bool HasErrors, int Count) IsNullOrEmpty(params object?[] values)
        {
            var errorsIsNullOrEmpty = values.Where(value =>
            {
                switch (value)
                {
                    case null:
                    case string s when string.IsNullOrWhiteSpace(s):
                        return true;
                    case bool b when value is bool:
                        return false;
                    default:
                        return Equals(value, value.ToDefaultValue());
                }
            });

            return (errorsIsNullOrEmpty.Any(), errorsIsNullOrEmpty.Count());
        }
    }
}
