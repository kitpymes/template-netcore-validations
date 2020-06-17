﻿// -----------------------------------------------------------------------
// <copyright file="NullOrEmpty.cs" company="Kitpymes">
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
        /// <param name="overrideRureFieldName">Nombre del campo.</param>
        /// <returns>ValidatorRuleOptions.</returns>
        public ValidatorRuleOptions IsNullOrEmpty(string? overrideRureFieldName = null)
        {
            if (IsNullOrEmptyHasErrors())
            {
                ValidatorRule.Add(() => Messages.NullOrEmpty(GetRuleFieldName(overrideRureFieldName)));
            }

            return this;
        }

        /// <summary>
        /// Comprueba si el valor ingresado es valido.
        /// </summary>
        /// <param name="message">Mensaje de error.</param>
        /// <returns>ValidatorRuleOptions.</returns>
        public ValidatorRuleOptions IsNullOrEmptyWithMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException(Messages.Required(nameof(message)));
            }

            if (IsNullOrEmptyHasErrors())
            {
                ValidatorRule.Add(() => message);
            }

            return this;
        }

        private bool IsNullOrEmptyHasErrors() => Check.IsNullOrEmpty(_value).HasErrors;
    }
}
