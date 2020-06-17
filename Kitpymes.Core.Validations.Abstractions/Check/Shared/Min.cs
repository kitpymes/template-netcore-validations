// -----------------------------------------------------------------------
// <copyright file="Min.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Validations.Abstractions
{
    using System;
    using System.Collections;
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
        /// <param name="min">Valor mínimo permitido.</param>
        /// <param name="values">Valores a validar.</param>
        /// <returns>(bool HasErrors, int Count).</returns>
        public static (bool HasErrors, int Count) IsMin(long min, params object?[] values)
        {
            var errorsIsMin = values.Where(value =>
            {
                if (IsNullOrEmpty(value).HasErrors)
                {
                    return true;
                }

                switch (value)
                {
                    case Guid g when g.ToString().Length < min:
                    case string s when s.Length < min:
                    case sbyte sb when sb < min:
                    case short sh when sh < min:
                    case int inte when inte < min:
                    case long lo when lo < min:
                    case byte by when by < min:
                    case ushort us when us < min:
                    case uint ui when ui < min:
                    case ulong ul when Convert.ToInt64(ul) < min:
                    case char ch when ch < min:
                    case float fl when fl < min:
                    case double d when d < min:
                    case decimal de when de < min:
                    case Array a when a.Length < min:
                    case ICollection c when c.Count < min:
                    case IEnumerable e when e.Cast<object>().Count() < min:
                        return true;
                    default:
                        return false;
                }
            });

            return (errorsIsMin.Any(), errorsIsMin.Count());
        }
    }
}
