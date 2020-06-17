// -----------------------------------------------------------------------
// <copyright file="Messages.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Validations.Abstractions
{
    /*
        Configuración de los mensajes de error MessagesSettings
        Contiene las opciones de los mensajes de error
    */

    /// <summary>
    /// Configuración de los mensajes de error <c>MessagesSettings</c>.
    /// Contiene las opciones de los mensajes de error.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todos los mensajes de error.</para>
    /// </remarks>
    public static class Messages
    {
        /// <summary>
        /// Mensaje por defecto para formatos inválidos.
        /// </summary>
        /// <param name="fieldName">Nombre del campo.</param>
        /// <returns>Mensaje de error.</returns>
        public static string InvalidFormat(string? fieldName = null)
        => string.IsNullOrWhiteSpace(fieldName)
            ? Resources.MsgInvalidFormat : Resources.MsgInvalidFormatFieldName.ToFormat(fieldName);

        /// <summary>
        /// Mensaje por defecto cuando el valor ingresado es nulo o vacío.
        /// </summary>
        /// <param name="fieldName">Nombre del campo.</param>
        /// <returns>Mensaje de error.</returns>
        public static string Required(string? fieldName = null)
        => string.IsNullOrWhiteSpace(fieldName)
            ? Resources.MsgRequired : Resources.MsgRequiredFieldName.ToFormat(fieldName);

        /// <summary>
        /// Mensaje por defecto para formatos inválidos.
        /// </summary>
        /// <param name="fieldName">Nombre del campo.</param>
        /// <returns>Mensaje de error.</returns>
        public static string Regex(string? fieldName = null)
        => InvalidFormat(fieldName);

        /// <summary>
        /// Mensaje por defecto cuando el valor ingresado no contiene caracteres o objetos.
        /// </summary>
        /// <param name="fieldName">Nombre del campo.</param>
        /// <returns>Mensaje de error.</returns>
        public static string Any(string? fieldName = null)
        => Required(fieldName);

        /// <summary>
        /// Mensaje por defecto cuando el valor ingresado es nulo o vacío.
        /// </summary>
        /// <param name="fieldName">Nombre del campo.</param>
        /// <returns>Mensaje de error.</returns>
        public static string NullOrEmpty(string? fieldName = null)
        => Required(fieldName);

        /// <summary>
        /// Mensaje por defecto cuando dos campos comparados no son iguales.
        /// </summary>
        /// <param name="fieldName">Nombre del campo.</param>
        /// <returns>Mensaje de error.</returns>
        public static string Equal(string? fieldName = null)
        => string.IsNullOrWhiteSpace(fieldName)
            ? Resources.MsgEqual : Resources.MsgEqualFieldName.ToFormat(fieldName);

        /// <summary>
        /// Mensaje por defecto cuando dos campos comparados no son iguales.
        /// </summary>
        /// <param name="fieldName">Nombre del campo.</param>
        /// <param name="fieldNameCompare">Nombre del campo a comparar.</param>
        /// <returns>Mensaje de error.</returns>
        public static string EqualWithFieldsName(string? fieldName, string? fieldNameCompare)
        => string.IsNullOrWhiteSpace(fieldName) || string.IsNullOrWhiteSpace(fieldNameCompare)
            ? Resources.MsgEqual : Resources.MsgEqualFieldsName.ToFormat(fieldName, fieldNameCompare);

        /// <summary>
        /// Mensaje por defecto para cuando el valor ingresado es mayor que el configurado.
        /// </summary>
        /// <param name="max">Valor máximo que puede tener.</param>
        /// <param name="fieldName">Nombre del campo.</param>
        /// <returns>Mensaje de error.</returns>
        public static string Max(long max, string? fieldName = null)
        => string.IsNullOrWhiteSpace(fieldName)
            ? Resources.MsgMax.ToFormat(max) : Resources.MsgMaxFieldName.ToFormat(fieldName, max);

        /// <summary>
        /// Mensaje por defecto para cuando el valor ingresado es mayor que el configurado.
        /// </summary>
        /// <param name="min">Valor mínimo que puede tener.</param>
        /// <param name="fieldName">Nombre del campo.</param>
        /// <returns>Mensaje de error.</returns>
        public static string Min(long min, string? fieldName = null)
        => string.IsNullOrWhiteSpace(fieldName) ? Resources.MsgMin.ToFormat(min) : Resources.MsgMinFieldName.ToFormat(fieldName, min);

        /// <summary>
        /// Mensaje por defecto cuando el valor ingresado no se encuentra entre los rangos configurados.
        /// </summary>
        /// <param name="min">Valor mínimo que puede tener.</param>
        /// <param name="max">Valor máximo que puede tener.</param>
        /// <param name="fieldName">Nombre del campo.</param>
        /// <returns>Mensaje de error.</returns>
        public static string Range(long min, long max, string? fieldName = null)
        => string.IsNullOrWhiteSpace(fieldName)
            ? Resources.MsgRange.ToFormat(min, max) : Resources.MsgRangeFieldName.ToFormat(fieldName, min, max);

        /// <summary>
        /// Mensaje por defecto cuando el path de un directorio no existe.
        /// </summary>
        /// <param name="value">Ruta del directorio.</param>
        /// <param name="fieldName">Nombre del campo.</param>
        /// <returns>Mensaje de error.</returns>
        public static string Directory(string? value = null, string? fieldName = null)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.IsNullOrWhiteSpace(fieldName)
                    ? Resources.MsgDirectory
                    : Resources.MsgDirectoryWithFieldName.ToFormat(fieldName);
            }
            else
            {
                return string.IsNullOrWhiteSpace(fieldName)
                    ? Resources.MsgDirectoryWithValue.ToFormat(value)
                    : Resources.MsgDirectoryWithValueAndFieldName.ToFormat(value, fieldName);
            }
        }

        /// <summary>
        /// Mensaje por defecto cuando el valor ingresado no tiene el formato correcto de un email.
        /// </summary>
        /// <param name="value">Ruta del directorio.</param>
        /// <param name="fieldName">Nombre del campo.</param>
        /// <returns>Mensaje de error.</returns>
        public static string Email(string? value = null, string? fieldName = null)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.IsNullOrWhiteSpace(fieldName)
                    ? Resources.MsgEmail
                    : Resources.MsgEmailWithFieldName.ToFormat(fieldName);
            }
            else
            {
                return string.IsNullOrWhiteSpace(fieldName)
                    ? Resources.MsgEmailWithValue.ToFormat(value)
                    : Resources.MsgEmailWithValueAndFieldName.ToFormat(value, fieldName);
            }
        }

        /// <summary>
        /// Mensaje por defecto cuando el path de un directorio no existe.
        /// </summary>
        /// <param name="value">Ruta del directorio.</param>
        /// <param name="fieldName">Nombre del campo.</param>
        /// <returns>Mensaje de error.</returns>
        public static string Extension(string? value = null, string? fieldName = null)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.IsNullOrWhiteSpace(fieldName)
                    ? Resources.MsgExtension
                    : Resources.MsgExtensionWithFieldName.ToFormat(fieldName);
            }
            else
            {
                return string.IsNullOrWhiteSpace(fieldName)
                    ? Resources.MsgExtensionWithValue.ToFormat(value)
                    : Resources.MsgExtensionWithValueAndFieldName.ToFormat(value, fieldName);
            }
        }

        /// <summary>
        /// Mensaje por defecto cuando el path de un directorio no existe.
        /// </summary>
        /// <param name="value">Ruta del directorio.</param>
        /// <param name="fieldName">Nombre del campo.</param>
        /// <returns>Mensaje de error.</returns>
        public static string File(string? value = null, string? fieldName = null)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.IsNullOrWhiteSpace(fieldName)
                    ? Resources.MsgFile
                    : Resources.MsgFileWithFieldName.ToFormat(fieldName);
            }
            else
            {
                return string.IsNullOrWhiteSpace(fieldName)
                    ? Resources.MsgFileWithValue.ToFormat(value)
                    : Resources.MsgFileWithValueAndFieldName.ToFormat(value, fieldName);
            }
        }

        /// <summary>
        /// Mensaje por defecto cuando el path de un directorio no existe.
        /// </summary>
        /// <param name="value">Ruta del directorio.</param>
        /// <param name="fieldName">Nombre del campo.</param>
        /// <returns>Mensaje de error.</returns>
        public static string Name(string? value = null, string? fieldName = null)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.IsNullOrWhiteSpace(fieldName)
                    ? Resources.MsgName
                    : Resources.MsgNameWithFieldName.ToFormat(fieldName);
            }
            else
            {
                return string.IsNullOrWhiteSpace(fieldName)
                    ? Resources.MsgNameWithValue.ToFormat(value)
                    : Resources.MsgNameWithValueAndFieldName.ToFormat(value, fieldName);
            }
        }

        /// <summary>
        /// Mensaje por defecto para cuando el valor ingresado es mayor que el configurado.
        /// </summary>
        /// <param name="min">Valor mínimo que puede tener.</param>
        /// <param name="fieldName">Nombre del campo.</param>
        /// <returns>Mensaje de error.</returns>
        public static string Password(long min, string? fieldName = null)
        => string.IsNullOrWhiteSpace(fieldName)
                ? Resources.MsgPassword.ToFormat(min)
                : Resources.MsgPasswordWithFieldName.ToFormat(min, fieldName);

        /// <summary>
        /// Mensaje por defecto cuando el path de un directorio no existe.
        /// </summary>
        /// <param name="value">Ruta del directorio.</param>
        /// <param name="fieldName">Nombre del campo.</param>
        /// <returns>Mensaje de error.</returns>
        public static string Subdomain(string? value = null, string? fieldName = null)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.IsNullOrWhiteSpace(fieldName)
                    ? Resources.MsgSubdomain
                    : Resources.MsgSubdomainWithFieldName.ToFormat(fieldName);
            }
            else
            {
                return string.IsNullOrWhiteSpace(fieldName)
                    ? Resources.MsgSubdomainWithValue.ToFormat(value)
                    : Resources.MsgSubdomainWithValueAndFieldName.ToFormat(value, fieldName);
            }
        }
    }
}
