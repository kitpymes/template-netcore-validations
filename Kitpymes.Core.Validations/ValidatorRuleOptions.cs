// -----------------------------------------------------------------------
// <copyright file="ValidatorRuleOptions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Validations
{
    /*
        Configuración de los validadores ValidatorRuleOptions
        Contiene las opciones comunes de los validadores
    */

    /// <summary>
    /// Configuración de los validadores <c>ValidatorRuleOptions</c>.
    /// Contiene las opciones comunes de los validadores.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las opciones comunes a todos los validadores.</para>
    /// </remarks>
    public partial class ValidatorRuleOptions
    {
        private readonly object? _value;

        private string? _fieldName = null;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ValidatorRuleOptions"/>.
        /// </summary>
        public ValidatorRuleOptions() { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ValidatorRuleOptions"/>.
        /// </summary>
        /// <param name="value">El valor a validar.</param>
        public ValidatorRuleOptions(object? value)
        {
            _value = value;
        }

        /// <summary>
        /// Agregamos el nombre del parámetro del valor ingresado.
        /// </summary>
        /// <param name="fieldName">Nombre del campo.</param>
        public void WithRuleFieldName(string fieldName)
        {
            if (!string.IsNullOrWhiteSpace(fieldName))
            {
                _fieldName = fieldName;
            }
        }

        private string? GetRuleFieldName(string? overrideParamNameIfNotExist = null)
        => !string.IsNullOrWhiteSpace(overrideParamNameIfNotExist) ? overrideParamNameIfNotExist : _fieldName;
    }
}
