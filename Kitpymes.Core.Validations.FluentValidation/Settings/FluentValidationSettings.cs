// -----------------------------------------------------------------------
// <copyright file="FluentValidationSettings.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Validations.FluentValidation
{
    using System.Collections.Generic;

    /*
        Configuración de las validaciones FluentValidationSettings
        Contiene las opciones de FluentValidation
    */

    /// <summary>
    /// Configuración de las validaciones <c>FluentValidationSettings</c>.
    /// Contiene la configuración de FluentValidation.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las opciones de FluentValidation.</para>
    /// </remarks>
    public class FluentValidationSettings
    {
        /// <summary>
        /// Valor por defecto que indica si esta habilitado el servicio.
        /// </summary>
        public const bool DefaultEnabled = false;

        private bool _enabled = DefaultEnabled;
        private List<string> assemblies = new List<string>();

        /// <summary>
        /// Obtiene o establece un valor que indica el servicio esta habilitado.
        /// <para><strong>Default:</strong> <see cref="DefaultEnabled"/> = false.</para>
        /// </summary>
        public bool? Enabled
        {
            get => _enabled;
            set
            {
                if (value.HasValue)
                {
                    _enabled = value.Value;
                }
            }
        }

        /// <summary>
        /// Obtiene los nombres de los assemblies.
        /// </summary>
        public List<string>? Assemblies
        {
            get => this.assemblies;
            internal set
            {
                if (value?.Count > 0)
                {
                    this.assemblies = value;
                }
            }
        }
    }
}
