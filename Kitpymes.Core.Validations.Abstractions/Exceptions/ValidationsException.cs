// -----------------------------------------------------------------------
// <copyright file="ValidationsException.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Validations.Abstractions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    /*
        Clase de excepci�n ValidationsException
        Contiene las propiedades de la excepci�n
    */

    /// <summary>
    /// Clase de excepci�n <c>ValidationsException</c>.
    /// Contiene la excepci�n que es lanzada cuando se produce un error.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las propiedades que queremos que devuelva la excepci�n.</para>
    /// </remarks>
    /// <inheritdoc/>
    [Serializable]
    public class ValidationsException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ValidationsException"/>.
        /// </summary>
        /// <param name="messages">Mensajes de errores.</param>
        public ValidationsException(params string[] messages)
            : this(string.Join(", ", messages)) { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ValidationsException"/>.
        /// </summary>
        /// <param name="errors">Lista de errores.</param>
        public ValidationsException(IDictionary<string, IEnumerable<string>> errors)
        => Errors = errors;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ValidationsException"/>.
        /// </summary>
        /// <param name="info">Almacena todos los datos necesarios para serializar o deserializar un objeto.</param>
        /// <param name="context">Describe el origen y el destino de una secuencia serializada dada y proporciona un contexto adicional definido por el llamador.</param>
        protected ValidationsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ValidationsException"/>.
        /// </summary>
        /// <param name="message">Mensajes de errores.</param>
        /// <param name="innerException">Excepci�n interna.</param>
        protected ValidationsException(string message, Exception innerException)
            : base(message, innerException) { }

        private ValidationsException() { }

        private ValidationsException(string message)
            : base(message) { }

        /// <summary>
        /// Obtiene los errores.
        /// </summary>
        public IDictionary<string, IEnumerable<string>>? Errors { get; }

        /// <summary>
        /// Obtiene un valor que indica si existen errores.
        /// </summary>
        public bool HasErrors => Errors?.Count > 0 || !string.IsNullOrWhiteSpace(Message);

        /// <summary>
        /// Verifica si el mensaje <paramref name="message"/> esta contenido en la excepci�n/>.
        /// </summary>
        /// <param name="message">El mensaje que se quiere verificar si ya esta contenido en la excepci�n.</param>
        /// <returns>
        /// Si encontro o no el mensaje.
        /// </returns>
        public bool Contains(string message)
        => Message != null && Message.Contains(message);

        /// <summary>
        /// Verifica si el mensaje <paramref name="message"/> esta contenido en la excepci�n/>.
        /// </summary>
        /// <param name="fieldName">Nombre del campo.</param>
        /// <param name="message">Mensaje de error.</param>
        /// <returns>bool.</returns>
        public bool Contains(string fieldName, string message)
        => Errors != null && Errors[fieldName] != null && Errors[fieldName].ToList().Contains(message);
    }
}
