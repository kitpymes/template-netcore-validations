// -----------------------------------------------------------------------
// <copyright file="Min.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Validations
{
    using System;
    using Kitpymes.Core.Validations.Abstractions;

    /*
        Configuración de los validadores ValidatorRuleOptions
        Contiene las opciones de los validadores
    */

    /// <summary>
    /// Configuración de los validadores <c>ValidatorRuleOptions</c>.
    /// Contiene las opciones de los validadores.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las opciones que necesitamos para el validador.</para>
    /// </remarks>
    public partial class ValidatorRuleOptions
    {
        /// <summary>
        /// Comprueba si el valor ingresado es valido.
        /// </summary>
        /// <param name="min">Valor mínimo que puede tener.</param>
        /// <param name="overrideRureFieldName">Nombre del campo.</param>
        /// <returns>ValidatorRuleOptions.</returns>
        public ValidatorRuleOptions IsMin(long min, string? overrideRureFieldName = null)
        {
            if (IsMinHasErrors(min))
            {
                ValidatorRule.Add(() => Messages.Min(min, GetRuleFieldName(overrideRureFieldName)));
            }

            return this;
        }

        /// <summary>
        /// Comprueba si el valor ingresado es valido.
        /// </summary>
        /// <param name="min">Valor mínimo que puede tener.</param>
        /// <param name="message">Mensaje de error.</param>
        /// <returns>ValidatorRuleOptions.</returns>
        public ValidatorRuleOptions IsMinWithMessage(long min, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException(Messages.Required(nameof(message)));
            }

            if (IsMinHasErrors(min))
            {
                ValidatorRule.Add(() => message);
            }

            return this;
        }

        private bool IsMinHasErrors(long min) => Check.IsMin(min, _value).HasErrors;
    }
}
