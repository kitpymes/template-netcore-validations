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
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /*
        Clase de excepción ValidationsException
        Contiene la excepción que es lanzada cuando se produce un error
    */

    /// <summary>
    /// Clase de excepción <c>ValidationsException</c>.
    /// Contiene la excepción que es lanzada cuando se produce un error.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las propiedades que queremos que devuelva la excepción al frontend.</para>
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
            : this(string.Join(", ", messages))
        {
            Count = messages.Length;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ValidationsException"/>.
        /// </summary>
        /// <param name="messages">Mensajes de errores.</param>
        public ValidationsException(IDictionary<string, string> messages)
        : this(messages.ToSerialize()) { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ValidationsException"/>.
        /// </summary>
        /// <param name="info">Almacena todos los datos necesarios para serializar o deserializar un objeto.</param>
        /// <param name="context">Describe el origen y el destino de una secuencia serializada dada y proporciona un contexto adicional definido por el llamador.</param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected ValidationsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Count = info.GetInt32("Count");
        }

        private ValidationsException() { }

        private ValidationsException(string message)
            : base(message) { }

        private ValidationsException(string message, Exception innerException)
            : base(message, innerException) { }

        /// <summary>
        /// Obtiene la cantidad de mensajes contenidos en la excepción.
        /// </summary>
        public int? Count { get; }

        /// <summary>
        /// Verifica si el mensaje <paramref name="message"/> esta contenido en la excepción/>.
        /// </summary>
        /// <param name="message">El mensaje que se quiere verificar si ya esta contenido en la excepción.</param>
        /// <returns>
        /// Si encontro o no el mensaje.
        /// </returns>
        public bool Contains(string message) => Message.Contains(message, StringComparison.CurrentCulture);
    }
}
