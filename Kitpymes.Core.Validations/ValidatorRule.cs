// -----------------------------------------------------------------------
// <copyright file="ValidatorRule.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Validations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Kitpymes.Core.Validations.Abstractions;

    /*
        Configuración de las reglas de validación ValidatorRule
        Contiene las reglas de validación
    */

    /// <summary>
    /// Configuración de las reglas de validación <c>ValidatorRule</c>.
    /// Contiene las reglas de validación .
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las reglas de validaciones necesarias.</para>
    /// </remarks>
    public class ValidatorRule
    {
        private static readonly List<Func<string>> _rules = new ();

        private bool _stopFirstError = false;

        /// <summary>
        /// Obtiene un valor que indica si ya hubo algun error.
        /// </summary>
        /// <returns>Un booleano.</returns>
        public bool IsValid => !_rules.Any();

        /// <summary>
        /// Agrega una nueva regla.
        /// </summary>
        /// <param name="rule">Regla de validación.</param>
        public static void Add(Func<string> rule) => _rules.Add(rule);

        /// <summary>
        /// Si quiere que pare de validar cuando encuentre el primer error.
        /// </summary>
        /// <param name="stopFirstError">Párametro de seteo.</param>
        /// <returns>ValidatorRule.</returns>
        public ValidatorRule StopFirstError(bool stopFirstError = true)
        {
            _stopFirstError = stopFirstError;

            return this;
        }

        /// <summary>
        /// Para agregar una nueva regla de validación.
        /// </summary>
        /// <param name="value">Valor a validar.</param>
        /// <param name="options">Validadores disponibles.</param>
        /// <returns>ValidatorRule.</returns>
        public ValidatorRule AddRule(object? value, Action<ValidatorRuleOptions> options)
        => Validator.AddRule(value, options);

        /// <summary>
        /// Para agregar una nueva regla de validación.
        /// </summary>
        /// <param name="condition">Condición a validar.</param>
        /// <param name="message">Mensaje de error a mostrar.</param>
        /// <returns>ValidatorRule.</returns>
        public ValidatorRule AddRule(Func<bool> condition, string message)
        => Validator.AddRule(condition, message);

        /// <summary>
        /// Lanza una excepción del tipo ValidationsException si es que hubo algún error.
        /// </summary>
        public void Throw()
        {
            if (!IsValid)
            {
                var errors = _stopFirstError
                     ? new string[] { _rules.First().Invoke() }
                     : _rules.Select(rule => rule.Invoke()).ToArray();

                _rules.Clear();

                throw new ValidationsException(errors);
            }
        }

        /// <summary>
        /// Lanza una excepción asincrona del tipo ValidationsException si es que hubo algún error.
        /// </summary>
        /// <returns>Task.</returns>
        public async Task ThrowAsync()
        => await Task.Run(() => Throw()).ConfigureAwait(false);

        /// <summary>
        /// Lanza una excepción del tipo ApplicationException si es que hubo algún error.
        /// </summary>
        public void ThrowApplicationException()
        {
            if (!IsValid)
            {
                var errors = _stopFirstError
                     ? new string[] { _rules.First().Invoke() }
                     : _rules.Select(rule => rule.Invoke()).ToArray();

                _rules.Clear();

                throw new ApplicationException(string.Join(",", errors));
            }
        }

        /// <summary>
        /// Lanza una excepción asincrona del tipo ApplicationException si es que hubo algún error.
        /// </summary>
        /// <returns>Task.</returns>
        public async Task ThrowApplicationExceptionAsync()
        => await Task.Run(() => ThrowApplicationException()).ConfigureAwait(false);
    }
}