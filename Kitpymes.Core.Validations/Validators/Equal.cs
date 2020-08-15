// -----------------------------------------------------------------------
// <copyright file="Equal.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Validations
{
    using System;
    using System.Collections;
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
        /// <param name="valueCompare">Valor a comparar.</param>
        /// <param name="fieldsName">Nombre de los campos.</param>
        /// <returns>ValidatorRuleOptions.</returns>
        public ValidatorRuleOptions IsEqual(IEnumerable? valueCompare, (string fieldName, string fieldNameCompare)? fieldsName = null)
        {
            if (fieldsName.HasValue)
            {
                if (string.IsNullOrWhiteSpace(fieldsName.Value.fieldName))
                {
                    throw new ArgumentException(Messages.Required(nameof(fieldsName.Value.fieldName)));
                }

                if (string.IsNullOrWhiteSpace(fieldsName.Value.fieldNameCompare))
                {
                    throw new ArgumentException(Messages.Required(nameof(fieldsName.Value.fieldNameCompare)));
                }

                return IsEqualWithMessage(valueCompare, Messages.EqualWithFieldsName(fieldsName.Value.fieldName, fieldsName.Value.fieldNameCompare));
            }

            if (IsEqualHasErrors(valueCompare))
            {
                ValidatorRule.Add(() => Messages.Equal(GetRuleFieldName()));
            }

            return this;
        }

        /// <summary>
        /// Comprueba si el valor ingresado es valido.
        /// </summary>
        /// <param name="valueCompare">Valor a comparar.</param>
        /// <param name="message">Mensaje de error.</param>
        /// <returns>ValidatorRuleOptions.</returns>
        public ValidatorRuleOptions IsEqualWithMessage(IEnumerable? valueCompare, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException(Messages.Required(nameof(message)));
            }

            if (IsEqualHasErrors(valueCompare))
            {
                ValidatorRule.Add(() => message);
            }

            return this;
        }

        private bool IsEqualHasErrors(IEnumerable? valueCompare) => Shared.Util.Check.IsEqual(_value, valueCompare).HasErrors;
    }
}
