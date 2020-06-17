// -----------------------------------------------------------------------
// <copyright file="RuleBuilderExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Validations.FluentValidation
{
    using System.Collections;
    using global::FluentValidation;
    using Kitpymes.Core.Validations.Abstractions;

    /*
        Clase de extensión RuleBuilderExtensions
        Contiene las extensiones de las validaciones custom
    */

    /// <summary>
    /// Clase de extensión <c>RuleBuilderExtensions</c>.
    /// Contiene las extensiones de las validaciones custom.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las validaciones custom.</para>
    /// </remarks>
    public static class RuleBuilderExtensions
    {
        /// <summary>
        /// Comprueba si el valor ingresado es valido.
        /// </summary>
        /// <typeparam name="T">Entidad a validar.</typeparam>
        /// <typeparam name="TProperty">Propiedad de la entidad.</typeparam>
        /// <param name="ruleBuilder">Regla de validación.</param>
        /// <param name="overrideFieldName">Nombre del campo.</param>
        /// <returns>IRuleBuilder.</returns>
        public static IRuleBuilder<T, TProperty> IsAny<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, string? overrideFieldName = null)
            where TProperty : IEnumerable?
        => ruleBuilder.Custom((value, context) =>
        {
            if (!(value is IEnumerable) || Check.IsAny(value).HasErrors)
            {
                context.AddFailure(Messages.Any(overrideFieldName ?? context.DisplayName));
            }
        });

        /// <summary>
        /// Comprueba si el valor ingresado es valido.
        /// </summary>
        /// <typeparam name="T">Entidad a validar.</typeparam>
        /// <typeparam name="TProperty">Propiedad de la entidad.</typeparam>
        /// <param name="ruleBuilder">Regla de validación.</param>
        /// <param name="message">Mensaje a mostrar.</param>
        /// <returns>IRuleBuilder.</returns>
        public static IRuleBuilder<T, TProperty> IsAnyWithMessage<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, string message)
           where TProperty : IEnumerable?
           => ruleBuilder.Custom((value, context) =>
           {
               if (!(value is IEnumerable) || Check.IsAny(value).HasErrors)
               {
                   context.AddFailure(message);
               }
           });

        /// <summary>
        /// Comprueba si el valor ingresado es valido.
        /// </summary>
        /// <typeparam name="T">Entidad a validar.</typeparam>
        /// <typeparam name="TProperty">Propiedad de la entidad.</typeparam>
        /// <param name="ruleBuilder">Regla de validación.</param>
        /// <param name="valueCompare">Valor a comparar.</param>
        /// <param name="overrideFieldName">Nombre del campo.</param>
        /// <param name="fieldNameCompare">Nombre del campo a comparar.</param>
        /// <returns>IRuleBuilder.</returns>
        public static IRuleBuilder<T, TProperty> IsEqual<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, TProperty valueCompare, string? overrideFieldName = null, string? fieldNameCompare = null)
         => ruleBuilder.Custom((value, context) =>
         {
             if (Check.IsEqual(value, valueCompare).HasErrors)
             {
                 var properrtyName = overrideFieldName ?? context.DisplayName;

                 context.AddFailure(string.IsNullOrWhiteSpace(fieldNameCompare)
                     ? Messages.Equal(properrtyName)
                     : Messages.EqualWithFieldsName(properrtyName, fieldNameCompare));
             }
         });

        /// <summary>
        /// Comprueba si el valor ingresado es valido.
        /// </summary>
        /// <typeparam name="T">Entidad a validar.</typeparam>
        /// <typeparam name="TProperty">Propiedad de la entidad.</typeparam>
        /// <param name="ruleBuilder">Regla de validación.</param>
        /// <param name="valueCompare">Valor a comparar.</param>
        /// <param name="message">Mensaje a mostrar.</param>
        /// <returns>IRuleBuilder.</returns>
        public static IRuleBuilder<T, TProperty> IsEqualWithMessage<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, TProperty valueCompare, string message)
         => ruleBuilder.Custom((value, context) =>
         {
             if (Check.IsEqual(value, valueCompare).HasErrors)
             {
                 context.AddFailure(message);
             }
         });

        /// <summary>
        /// Comprueba si el valor ingresado es valido.
        /// </summary>
        /// <typeparam name="T">Entidad a validar.</typeparam>
        /// <typeparam name="TProperty">Propiedad de la entidad.</typeparam>
        /// <param name="ruleBuilder">Regla de validación.</param>
        /// <param name="max">Valor máximo que puede tener el campo.</param>
        /// <param name="overrideFieldName">Nombre del campo.</param>
        /// <returns>IRuleBuilder.</returns>
        public static IRuleBuilder<T, TProperty> IsMax<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, long max, string? overrideFieldName = null)
        => ruleBuilder.Custom((value, context) =>
        {
            if (Check.IsMax(max, value).HasErrors)
            {
                context.AddFailure(Messages.Max(max, overrideFieldName ?? context.DisplayName));
            }
        });

        /// <summary>
        /// Comprueba si el valor ingresado es valido.
        /// </summary>
        /// <typeparam name="T">Entidad a validar.</typeparam>
        /// <typeparam name="TProperty">Propiedad de la entidad.</typeparam>
        /// <param name="ruleBuilder">Regla de validación.</param>
        /// <param name="max">Valor máximo que puede tener el campo.</param>
        /// <param name="message">Mensaje a mostrar.</param>
        /// <returns>IRuleBuilder.</returns>
        public static IRuleBuilder<T, TProperty> IsMaxWithMessage<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, long max, string message)
        => ruleBuilder.Custom((value, context) =>
        {
            if (Check.IsMax(max, value).HasErrors)
            {
                context.AddFailure(message);
            }
        });

        /// <summary>
        /// Comprueba si el valor ingresado es valido.
        /// </summary>
        /// <typeparam name="T">Entidad a validar.</typeparam>
        /// <typeparam name="TProperty">Propiedad de la entidad.</typeparam>
        /// <param name="ruleBuilder">Regla de validación.</param>
        /// <param name="min">Valor mínimo que puede tener el campo.</param>
        /// <param name="overrideFieldName">Nombre del campo.</param>
        /// <returns>IRuleBuilder.</returns>
        public static IRuleBuilder<T, TProperty> IsMin<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, long min, string? overrideFieldName = null)
        => ruleBuilder.Custom((value, context) =>
        {
            if (Check.IsMin(min, value).HasErrors)
            {
                context.AddFailure(Messages.Min(min, overrideFieldName ?? context.DisplayName));
            }
        });

        /// <summary>
        /// Comprueba si el valor ingresado es valido.
        /// </summary>
        /// <typeparam name="T">Entidad a validar.</typeparam>
        /// <typeparam name="TProperty">Propiedad de la entidad.</typeparam>
        /// <param name="ruleBuilder">Regla de validación.</param>
        /// <param name="min">Valor mínimo que puede tener el campo.</param>
        /// <param name="message">Mensaje a mostrar.</param>
        /// <returns>IRuleBuilder.</returns>
        public static IRuleBuilder<T, TProperty> IsMinWithMessage<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, long min, string message)
        => ruleBuilder.Custom((value, context) =>
        {
            if (Check.IsMin(min, value).HasErrors)
            {
                context.AddFailure(message);
            }
        });

        /// <summary>
        /// Comprueba si el valor ingresado es valido.
        /// </summary>
        /// <typeparam name="T">Entidad a validar.</typeparam>
        /// <typeparam name="TProperty">Propiedad de la entidad.</typeparam>
        /// <param name="ruleBuilder">Regla de validación.</param>
        /// <param name="overrideFieldName">Nombre del campo.</param>
        /// <returns>IRuleBuilder.</returns>
        public static IRuleBuilder<T, TProperty> IsNullOrEmpty<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, string? overrideFieldName = null)
        => ruleBuilder.Custom((value, context) =>
        {
            if (Check.IsNullOrEmpty(value).HasErrors)
            {
                context.AddFailure(Messages.NullOrEmpty(overrideFieldName ?? context.DisplayName));
            }
        });

        /// <summary>
        /// Comprueba si el valor ingresado es valido.
        /// </summary>
        /// <typeparam name="T">Entidad a validar.</typeparam>
        /// <typeparam name="TProperty">Propiedad de la entidad.</typeparam>
        /// <param name="ruleBuilder">Regla de validación.</param>
        /// <param name="message">Mensaje a mostrar.</param>
        /// <returns>IRuleBuilder.</returns>
        public static IRuleBuilder<T, TProperty> IsNullOrEmptyWithMessage<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, string message)
        => ruleBuilder.Custom((value, context) =>
        {
            if (Check.IsNullOrEmpty(value).HasErrors)
            {
                context.AddFailure(message);
            }
        });

        /// <summary>
        /// Comprueba si el valor ingresado es valido.
        /// </summary>
        /// <typeparam name="T">Entidad a validar.</typeparam>
        /// <typeparam name="TProperty">Propiedad de la entidad.</typeparam>
        /// <param name="ruleBuilder">Regla de validación.</param>
        /// <param name="min">Valor mínimo que puede tener el campo.</param>
        /// <param name="max">Valor máximo que puede tener el campo.</param>
        /// <param name="overrideFieldName">Nombre del campo.</param>
        /// <returns>IRuleBuilder.</returns>
        public static IRuleBuilder<T, TProperty> IsRange<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, long min, long max, string? overrideFieldName = null)
         => ruleBuilder.Custom((value, context) =>
         {
             if (Check.IsRange(min, max, value).HasErrors)
             {
                 context.AddFailure(Messages.Range(min, max, overrideFieldName ?? context.DisplayName));
             }
         });

        /// <summary>
        /// Comprueba si el valor ingresado es valido.
        /// </summary>
        /// <typeparam name="T">Entidad a validar.</typeparam>
        /// <typeparam name="TProperty">Propiedad de la entidad.</typeparam>
        /// <param name="ruleBuilder">Regla de validación.</param>
        /// <param name="min">Valor mínimo que puede tener el campo.</param>
        /// <param name="max">Valor máximo que puede tener el campo.</param>
        /// <param name="message">Mensaje a mostrar.</param>
        /// <returns>IRuleBuilder.</returns>
        public static IRuleBuilder<T, TProperty> IsRangeWithMessage<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, long min, long max, string message)
         => ruleBuilder.Custom((value, context) =>
         {
             if (Check.IsRange(min, max, value).HasErrors)
             {
                 context.AddFailure(message);
             }
         });

        /// <summary>
        /// Comprueba si el valor ingresado es valido.
        /// </summary>
        /// <typeparam name="T">Entidad a validar.</typeparam>
        /// <param name="ruleBuilder">Regla de validación.</param>
        /// <param name="regex">Expresión regular que tiene que cumplir el campo.</param>
        /// <param name="overrideFieldName">Nombre del campo.</param>
        /// <returns>IRuleBuilder.</returns>
        public static IRuleBuilder<T, string?> IsRegex<T>(this IRuleBuilder<T, string?> ruleBuilder, string regex, string? overrideFieldName = null)
        => ruleBuilder.Custom((value, context) =>
        {
            if (Check.IsRegex(regex, value).HasErrors)
            {
                context.AddFailure(Messages.Regex(overrideFieldName ?? context.DisplayName));
            }
        });

        /// <summary>
        /// Comprueba si el valor ingresado es valido.
        /// </summary>
        /// <typeparam name="T">Entidad a validar.</typeparam>
        /// <param name="ruleBuilder">Regla de validación.</param>
        /// <param name="regex">Expresión regular que tiene que cumplir el campo.</param>
        /// <param name="message">Mensaje a mostrar.</param>
        /// <returns>IRuleBuilder.</returns>
        public static IRuleBuilder<T, string?> IsRegexWithMessage<T>(this IRuleBuilder<T, string?> ruleBuilder, string regex, string message)
        => ruleBuilder.Custom((value, context) =>
        {
            if (Check.IsRegex(regex, value).HasErrors)
            {
                context.AddFailure(message);
            }
        });

        /// <summary>
        /// Comprueba si el valor ingresado es valido.
        /// </summary>
        /// <typeparam name="T">Entidad a validar.</typeparam>
        /// <param name="ruleBuilder">Regla de validación.</param>
        /// <param name="overrideFieldName">Nombre del campo.</param>
        /// <returns>IRuleBuilder.</returns>
        public static IRuleBuilder<T, string?> IsDirectory<T>(this IRuleBuilder<T, string?> ruleBuilder, string? overrideFieldName = null)
        => ruleBuilder.Custom((value, context) =>
        {
            if (Check.IsDirectory(value).HasErrors)
            {
                context.AddFailure(Messages.Directory(value, overrideFieldName ?? context.DisplayName));
            }
        });

        /// <summary>
        /// Comprueba si el valor ingresado es valido.
        /// </summary>
        /// <typeparam name="T">Entidad a validar.</typeparam>
        /// <param name="ruleBuilder">Regla de validación.</param>
        /// <param name="message">Mensaje a mostrar.</param>
        /// <returns>IRuleBuilder.</returns>
        public static IRuleBuilder<T, string?> IsDirectoryWithMessage<T>(this IRuleBuilder<T, string?> ruleBuilder, string message)
        => ruleBuilder.Custom((value, context) =>
        {
            if (Check.IsDirectory(value).HasErrors)
            {
                context.AddFailure(message);
            }
        });

        /// <summary>
        /// Comprueba si el valor ingresado es valido.
        /// </summary>
        /// <typeparam name="T">Entidad a validar.</typeparam>
        /// <param name="ruleBuilder">Regla de validación.</param>
        /// <param name="overrideFieldName">Nombre del campo.</param>
        /// <returns>IRuleBuilder.</returns>
        public static IRuleBuilder<T, string?> IsEmail<T>(this IRuleBuilder<T, string?> ruleBuilder, string? overrideFieldName = null)
        => ruleBuilder.Custom((value, context) =>
        {
            if (Check.IsEmail(value).HasErrors)
            {
                context.AddFailure(Messages.Email(value, overrideFieldName ?? context.DisplayName));
            }
        });

        /// <summary>
        /// Comprueba si el valor ingresado es valido.
        /// </summary>
        /// <typeparam name="T">Entidad a validar.</typeparam>
        /// <param name="ruleBuilder">Regla de validación.</param>
        /// <param name="message">Mensaje a mostrar.</param>
        /// <returns>IRuleBuilder.</returns>
        public static IRuleBuilder<T, string?> IsEmailWithMessage<T>(this IRuleBuilder<T, string?> ruleBuilder, string message)
        => ruleBuilder.Custom((value, context) =>
        {
            if (Check.IsEmail(value).HasErrors)
            {
                context.AddFailure(message);
            }
        });

        /// <summary>
        /// Comprueba si el valor ingresado es valido.
        /// </summary>
        /// <typeparam name="T">Entidad a validar.</typeparam>
        /// <param name="ruleBuilder">Regla de validación.</param>
        /// <param name="overrideFieldName">Nombre del campo.</param>
        /// <returns>IRuleBuilder.</returns>
        public static IRuleBuilder<T, string?> IsExtension<T>(this IRuleBuilder<T, string?> ruleBuilder, string? overrideFieldName = null)
        => ruleBuilder.Custom((value, context) =>
        {
            if (Check.IsEmail(value).HasErrors)
            {
                context.AddFailure(Messages.Extension(value, overrideFieldName ?? context.DisplayName));
            }
        });

        /// <summary>
        /// Comprueba si el valor ingresado es valido.
        /// </summary>
        /// <typeparam name="T">Entidad a validar.</typeparam>
        /// <param name="ruleBuilder">Regla de validación.</param>
        /// <param name="message">Mensaje a mostrar.</param>
        /// <returns>IRuleBuilder.</returns>
        public static IRuleBuilder<T, string?> IsExtensionWithMessage<T>(this IRuleBuilder<T, string?> ruleBuilder, string message)
        => ruleBuilder.Custom((value, context) =>
        {
            if (Check.IsEmail(value).HasErrors)
            {
                context.AddFailure(message);
            }
        });

        /// <summary>
        /// Comprueba si el valor ingresado es valido.
        /// </summary>
        /// <typeparam name="T">Entidad a validar.</typeparam>
        /// <param name="ruleBuilder">Regla de validación.</param>
        /// <param name="overrideFieldName">Nombre del campo.</param>
        /// <returns>IRuleBuilder.</returns>
        public static IRuleBuilder<T, string?> IsFile<T>(this IRuleBuilder<T, string?> ruleBuilder, string? overrideFieldName = null)
        => ruleBuilder.Custom((value, context) =>
        {
            if (Check.IsEmail(value).HasErrors)
            {
                context.AddFailure(Messages.File(value, overrideFieldName ?? context.DisplayName));
            }
        });

        /// <summary>
        /// Comprueba si el valor ingresado es valido.
        /// </summary>
        /// <typeparam name="T">Entidad a validar.</typeparam>
        /// <param name="ruleBuilder">Regla de validación.</param>
        /// <param name="message">Mensaje a mostrar.</param>
        /// <returns>IRuleBuilder.</returns>
        public static IRuleBuilder<T, string?> IsFileWithMessage<T>(this IRuleBuilder<T, string?> ruleBuilder, string message)
        => ruleBuilder.Custom((value, context) =>
        {
            if (Check.IsEmail(value).HasErrors)
            {
                context.AddFailure(message);
            }
        });

        /// <summary>
        /// Comprueba si el valor ingresado es valido.
        /// </summary>
        /// <typeparam name="T">Entidad a validar.</typeparam>
        /// <param name="ruleBuilder">Regla de validación.</param>
        /// <param name="overrideFieldName">Nombre del campo.</param>
        /// <returns>IRuleBuilder.</returns>
        public static IRuleBuilder<T, string?> IsName<T>(this IRuleBuilder<T, string?> ruleBuilder, string? overrideFieldName = null)
        => ruleBuilder.Custom((value, context) =>
        {
            if (Check.IsEmail(value).HasErrors)
            {
                context.AddFailure(Messages.Name(value, overrideFieldName ?? context.DisplayName));
            }
        });

        /// <summary>
        /// Comprueba si el valor ingresado es valido.
        /// </summary>
        /// <typeparam name="T">Entidad a validar.</typeparam>
        /// <param name="ruleBuilder">Regla de validación.</param>
        /// <param name="message">Mensaje a mostrar.</param>
        /// <returns>IRuleBuilder.</returns>
        public static IRuleBuilder<T, string?> IsNameWithMessage<T>(this IRuleBuilder<T, string?> ruleBuilder, string message)
        => ruleBuilder.Custom((value, context) =>
        {
            if (Check.IsEmail(value).HasErrors)
            {
                context.AddFailure(message);
            }
        });

        /// <summary>
        /// Comprueba si el valor ingresado es valido.
        /// </summary>
        /// <typeparam name="T">Entidad a validar.</typeparam>
        /// <param name="ruleBuilder">Regla de validación.</param>
        /// <param name="min">La cantidad mínima de caracteres que puede contener.</param>
        /// <param name="overrideFieldName">Nombre del campo.</param>
        /// <returns>IRuleBuilder.</returns>
        public static IRuleBuilder<T, string?> IsPassword<T>(this IRuleBuilder<T, string?> ruleBuilder, long min, string? overrideFieldName = null)
        => ruleBuilder.Custom((value, context) =>
        {
            if (Check.IsPassword(min, value).HasErrors)
            {
                context.AddFailure(Messages.Password(min, overrideFieldName ?? context.DisplayName));
            }
        });

        /// <summary>
        /// Comprueba si el valor ingresado es valido.
        /// </summary>
        /// <typeparam name="T">Entidad a validar.</typeparam>
        /// <param name="ruleBuilder">Regla de validación.</param>
        /// <param name="min">La cantidad mínima de caracteres que puede contener.</param>
        /// <param name="message">Mensaje a mostrar.</param>
        /// <returns>IRuleBuilder.</returns>
        public static IRuleBuilder<T, string?> IsPasswordWithMessage<T>(this IRuleBuilder<T, string?> ruleBuilder, long min, string message)
        => ruleBuilder.Custom((value, context) =>
        {
            if (Check.IsPassword(min, value).HasErrors)
            {
                context.AddFailure(message);
            }
        });

        /// <summary>
        /// Comprueba si el valor ingresado es valido.
        /// </summary>
        /// <typeparam name="T">Entidad a validar.</typeparam>
        /// <param name="ruleBuilder">Regla de validación.</param>
        /// <param name="overrideFieldName">Nombre del campo.</param>
        /// <returns>IRuleBuilder.</returns>
        public static IRuleBuilder<T, string?> IsSubdomain<T>(this IRuleBuilder<T, string?> ruleBuilder, string? overrideFieldName = null)
        => ruleBuilder.Custom((value, context) =>
        {
            if (Check.IsEmail(value).HasErrors)
            {
                context.AddFailure(Messages.Subdomain(value, overrideFieldName ?? context.DisplayName));
            }
        });

        /// <summary>
        /// Comprueba si el valor ingresado es valido.
        /// </summary>
        /// <typeparam name="T">Entidad a validar.</typeparam>
        /// <param name="ruleBuilder">Regla de validación.</param>
        /// <param name="message">Mensaje a mostrar.</param>
        /// <returns>IRuleBuilder.</returns>
        public static IRuleBuilder<T, string?> IsSubdomainWithMessage<T>(this IRuleBuilder<T, string?> ruleBuilder, string message)
        => ruleBuilder.Custom((value, context) =>
        {
            if (Check.IsEmail(value).HasErrors)
            {
                context.AddFailure(message);
            }
        });
    }
}
