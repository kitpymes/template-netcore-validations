// -----------------------------------------------------------------------
// <copyright file="Max.cs" company="Kitpymes">
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
        /// <param name="max">Valor máximo permitido.</param>
        /// <param name="values">Valores a validar.</param>
        /// <returns>(bool HasErrors, int Count).</returns>
        public static (bool HasErrors, int Count) IsMax(long max, params object?[] values)
        {
            var errorsIsMax = values.Where(value =>
            {
                if (IsNullOrEmpty(value).HasErrors)
                {
                    return true;
                }

                switch (value)
                {
                    case Guid g when g.ToString().Length > max:
                    case string s when s.Length > max:
                    case sbyte sb when sb > max:
                    case short sh when sh > max:
                    case int inte when inte > max:
                    case long lo when lo > max:
                    case byte by when by > max:
                    case ushort us when us > max:
                    case uint ui when ui > max:
                    case ulong ul when Convert.ToInt64(ul) > max:
                    case char ch when ch > max:
                    case float fl when fl > max:
                    case double d when d > max:
                    case decimal de when de > max:
                    case Array a when a.Length > max:
                    case ICollection c when c.Count > max:
                    case IEnumerable e when e.Cast<object>().Count() > max:
                        return true;
                    default:
                        return false;
                }
            });

            return (errorsIsMax.Any(), errorsIsMax.Count());
        }
    }
}
