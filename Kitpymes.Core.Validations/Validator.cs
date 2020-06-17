// -----------------------------------------------------------------------
// <copyright file="Validator.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Validations
{
    using System;
    using Kitpymes.Core.Validations.Abstractions;

    /*
        Configuración de las opciones comunes de los validadores Validator
        Contiene las opciones comunes de los validadores
    */

    /// <summary>
    /// Configuración de las opciones comunes de los validadores <c>Validator</c>.
    /// Contiene las opciones comunes de los validadores.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las opciones comunes que necesitamos para los validadores.</para>
    /// </remarks>
    public static class Validator
    {
        /// <summary>
        /// Para agregar una nueva regla de validación.
        /// </summary>
        /// <param name="value">Valor a validar.</param>
        /// <param name="options">Validadores disponibles.</param>
        /// <returns>ValidatorRule.</returns>
        public static ValidatorRule AddRule(object? value, Action<ValidatorRuleOptions> options)
        {
            options.ToConfigureOrDefault(new ValidatorRuleOptions(value));

            return new ValidatorRule();
        }

        /// <summary>
        /// Para agregar una nueva regla de validación.
        /// </summary>
        /// <param name="condition">Condición a validar.</param>
        /// <param name="message">Mensaje de error a mostrar.</param>
        /// <returns>ValidatorRule.</returns>
        public static ValidatorRule AddRule(Func<bool> condition, string message)
        {
            if (Check.IsCustom(condition).HasErrors)
            {
                ValidatorRule.Add(() => message);
            }

            return new ValidatorRule();
        }
    }
}
