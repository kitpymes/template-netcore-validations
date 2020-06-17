# <img src="https://github.com/kitpymes/template-netcore-validations/raw/master/docs/images/logo.png" height="30px"> Validations

**Validaciones para multiples proveedores**

[![Build Status](https://github.com/kitpymes/template-netcore-validations/workflows/Validations/badge.svg)](https://github.com/kitpymes/template-netcore-validations/actions)
[![NuGet Status](https://img.shields.io/nuget/v/Kitpymes.Core.Validations)](https://www.nuget.org/packages/Kitpymes.Core.Validations/)
[![NuGet Download](https://img.shields.io/nuget/dt/Kitpymes.Core.Validations)](https://www.nuget.org/stats/packages/Kitpymes.Core.Validations?groupby=Version)
[![License](https://img.shields.io/github/license/kitpymes/template-netcore-validations)](https://github.com/kitpymes/template-netcore-validations/blob/master/docs/LICENSE.txt)
[![Size Repo](https://img.shields.io/github/repo-size/kitpymes/template-netcore-validations)](https://github.com/kitpymes/template-netcore-validations/)
[![Last Commit](https://img.shields.io/github/last-commit/kitpymes/template-netcore-validations)](https://github.com/kitpymes/template-netcore-validations/)

## 📋 Requerimientos 

* Visual Studio >= 2019 (v16.3)

* NET TargetFramework >= netcoreapp3.0

* Net Core SDK >= 3.0.100

* C# >= 8.0

* Conocimientos sobre Inyección de Dependencias

## 🔧 Instalación 

_Se puede instalar usando el administrador de paquetes Nuget o CLI dotnet._

_Nuget_

```
Install-Package Kitpymes.Core.Validations
```

_CLI dotnet_

```
dotnet add package Kitpymes.Core.Validations
```

## ⌨️ Código

```cs
public static class Check
{
    public static (bool HasErrors, int Count) IsAny(params IEnumerable?[] values) {}

    public static (bool HasErrors, int Count) IsCustom(params Func<bool>[] values) {}

    public static (bool HasErrors, int Count) IsEqual(object? value, params object?[] valuesCompare) {}

    public static (bool HasErrors, int Count) IsMax(long max, params object?[] values) {}

    public static (bool HasErrors, int Count) IsMin(long min, params object?[] values) {}

    public static (bool HasErrors, int Count) IsNullOrEmpty(params object?[] values) {}

    public static (bool HasErrors, int Count) IsRange(long min, long max, params object?[] values) {}

    public static (bool HasErrors, int Count) IsRegex(string regex, params string?[] values) {}

    public static (bool HasErrors, int Count) IsDirectory(params string?[] values) {}

    public static (bool HasErrors, int Count) IsEmail(params string?[] values) {}

    public static (bool HasErrors, int Count) IsExtension(params string?[] values) {}

    public static (bool HasErrors, int Count) IsFile(params string?[] values) {}

    public static (bool HasErrors, int Count) IsName(params string?[] values) {}

    public static (bool HasErrors, int Count) IsPassword(long min, params string?[] values) {}

    public static (bool HasErrors, int Count) IsSubdomain(params string?[] values) {}
}
```

```cs
public static class Regexp
{
    public const string ForDate = @"^((((0?[1-9]|[12]\d|3[01])[\.\-\/](0?[13578]|1[02])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|[12]\d|30)[\.\-\/](0?[13456789]|1[012])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|1\d|2[0-8])[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|(29[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00)))|(((0[1-9]|[12]\d|3[01])(0[13578]|1[02])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|[12]\d|30)(0[13456789]|1[012])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|1\d|2[0-8])02((1[6-9]|[2-9]\d)?\d{2}))|(2902((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00)))) ?((20|21|22|23|[01]\d|\d)(([:.][0-5]\d){1,2}))?$";

    public const string ForDecimal = @"^((-?[1-9]+)|[0-9]+)(\.?|\,?)([0-9]*)$";

    public const string ForEmail = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

    public const string ForHex = "^#?([a-f0-9]{6}|[a-f0-9]{3})$";

    public const string ForInteger = "^((-?[1-9]+)|[0-9]+)$";

    public const string ForLogin = "^[a-z0-9_-]{10,50}$";

    public const string ForPassword = @"^.*(?=.{10,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&+=]).*$";

    public const string ForTag = @"^<([a-z1-6]+)([^<]+)*(?:>(.*)<\/\1>| *\/>)$";

    public const string ForTime = @"^([01]?[0-9]|2[0-3]):[0-5][0-9]$";

    public const string ForUrl = @"^((https?|ftp|file):\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$";

    public const string ForHostname = @"^(([a-zA-Z0-9]|[a-zA-Z0-9][a-zA-Z0-9\-]*[a-zA-Z0-9])\.)*([A-Za-z0-9]|[A-Za-z0-9][A-Za-z0-9\-]*[A-Za-z0-9])$";

    public const string ForName = @"^[a-zA-Z ]*$";

    public const string ForSubdomain = @"^[a-zA-Z0-9]*$";
}
```

```cs
public class ValidationsException : Exception
{
    public ValidationsException(params string[] messages) {}

    public ValidationsException(IDictionary<string, string> messages) {}

    public int? Count { get; }

    public bool Contains(string message) {}
}
```

### Validator

```cs
public static class Validator
{
    public static ValidatorRule AddRule(object? value, Action<ValidatorRuleOptions> options) {}

    public static ValidatorRule AddRule(Func<bool> condition, string message) {}
}
```

```cs
public class ValidatorRule
{
    public bool IsValid { get; }

    public static void Add(Func<string> rule)  {}

    public ValidatorRule StopFirstError(bool stopFirstError = true) {}

    public ValidatorRule AddRule(object? value, Action<ValidatorRuleOptions> options) {}

    public ValidatorRule AddRule(Func<bool> condition, string message) {}

    public void Throw() {}

    public async Task ThrowAsync() {}
}
```

```cs
public class ValidatorRuleOptions
{
    public ValidatorRuleOptions IsAny(string? overrideRureFieldName = null) {}

    public ValidatorRuleOptions IsAnyWithMessage(string message) {}

    public ValidatorRuleOptions IsEqual(IEnumerable? valueCompare, (string fieldName, string fieldNameCompare)? fieldsName = null) {}

    public ValidatorRuleOptions IsEqualWithMessage(IEnumerable? valueCompare, string message) {}

    public ValidatorRuleOptions IsMax(long max, string? overrideRureFieldName = null) {}

    public ValidatorRuleOptions IsMaxWithMessage(long max, string message) {}

    public ValidatorRuleOptions IsMin(long min, string? overrideRureFieldName = null) {}

    public ValidatorRuleOptions IsMinWithMessage(long min, string message) {}

    public ValidatorRuleOptions IsNullOrEmpty(string? overrideRureFieldName = null) {}

    public ValidatorRuleOptions IsNullOrEmptyWithMessage(string message) {}

    public ValidatorRuleOptions IsRange(long min, long max, string? overrideRureFieldName = null) {}

    public ValidatorRuleOptions IsRangeWithMessage(long min, long max, string message) {}

    public ValidatorRuleOptions IsRegex(string regex, string? overrideRureFieldName = null) {}

    public ValidatorRuleOptions IsRegexWithMessage(string regex, string message) {}

    public ValidatorRuleOptions IsDirectory(string? overrideRureFieldName = null) {}

    public ValidatorRuleOptions IsDirectoryWithMessage(string message) {}

    public ValidatorRuleOptions IsEmail(string? overrideRureFieldName = null) {}

    public ValidatorRuleOptions IsEmailWithMessage(string message) {}

    public ValidatorRuleOptions IsExtension(string? overrideRureFieldName = null) {}

    public ValidatorRuleOptions IsExtensionWithMessage(string message) {}

    public ValidatorRuleOptions IsFile(string? overrideRureFieldName = null) {}

    public ValidatorRuleOptions IsFileWithMessage(string message) {}

    public ValidatorRuleOptions IsName(string? overrideRureFieldName = null) {}

    public ValidatorRuleOptions IsNameWithMessage(string message) {}

    public ValidatorRuleOptions IsPassword(long min, string? overrideRureFieldName = null) {}

    public ValidatorRuleOptions IsPasswordWithMessage(long min, string message) {}

    public ValidatorRuleOptions IsSubdomain(string? overrideRureFieldName = null) {}

    public ValidatorRuleOptions IsSubdomainWithMessage(string message) {}
}
```

**Ejemplo**

```cs
using Kitpymes.Core.Validations;
using System;

public class Person
{
    public Person(int age, string name, string email)
    {
        Validator
            .AddRule(age, x => x.IsMin(17).IsMax(51).WithRuleFieldName("Edad"))
            .AddRule(name, x => x.IsName("Nombre"))
            .AddRule(email, x => x.IsEmailWithMessage("El correo eléctronico tiene un formato incorrecto."))
            .Throw();

        Id = Guid.NewGuid();
        Age = age;
        Name = name;
        Email = email;
    }

    public Person ChangeName(string name)
    {
        Validator.AddRule(name, x => x.IsName("Nombre")).Throw();

        Name = name;

        return this;
    }

    public Guid Id { get; private set; }
    public int Age { get; private set; }
    public string? Name { get; private set; }
    public string? Email { get; private set; }
}
```

### FluentValidation

**Agregamos el middlware en la clase Startup**

```cs
app.LoadValidations();
```

**Agregamos el ConfigureApiBehaviorOptions en la clase Startup**

```cs
services.AddControllers()
    .ConfigureApiBehaviorOptions(x =>
    {
        x.InvalidModelStateResponseFactory = context =>
        {
            var messages = context.ModelState
                .Where(e => e.Value.Errors.Any())
                .ToDictionary
                (
                    key => key.Key,

                    value => string.Join(", ", value.Value.Errors.Select(e => e.ErrorMessage))
                );

            throw new ValidationsException(messages);
        };
    });
```

**Opción 1: configuración desde el appsetings**

```js
{
    "ValidationsSettings": {
        "FluentValidationSettings": {
            "Enabled": true, // Default: false
            "Assemblies": [ "Api.Models" ] // Default: null
        }
    }
}
```

```cs
services.LoadValidations(Configuration);
```

**Opción 2: configuración manual, agregamos los assemblies en formato string**

```cs
services.LoadValidations(validator => validator.UseFluentValidator("Api.Models"));
```

**Ejemplo**

```cs
public class PersonAddDto 
{
    public int? Age { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
}
```

```cs
using FluentValidation;
using Kitpymes.Core.Validations.FluentValidation;

public class PersonAddDtoValidator : AbstractValidator<PersonAddDto>
{
    public PersonAddDtoValidator()
    {
        RuleFor(_ => _.Age).IsRange(17, 51, "Edad");
        RuleFor(_ => _.Name).IsName("Nombre").IsMin(100);
        RuleFor(_ => _.Email).IsEmailWithMessage("El correo eléctronico tiene un formato incorrecto.");
    }
}
```

## 🔩 Resultados ( Solo en modo Development muestra el Details )

**Resultado utilizando FluentValidation**

![Resultado utilizando FluentValidation](images/screenshot/resultados_addperson.png)

**Resultado utilizando Validator**

![Resultado utlizando Validator](images/screenshot/resultados_changename.png)


## ⚙️ Pruebas Unitarias

_Cada proyecto tiene su test que se ejecutan desde el "Explorador de pruebas"_

![Resultado pruebas](images/screenshot/resultados_testing.png)


## 🛠️ Construido con 

* [NET Core](https://dotnet.microsoft.com/download) - Framework de trabajo
* [C#](https://docs.microsoft.com/es-es/dotnet/csharp/) - Lenguaje de programación
* [Inserción de dependencias](https://docs.microsoft.com/es-es/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.0) - Patrón de diseño de software
* [MSTest](https://docs.microsoft.com/es-es/dotnet/core/testing/unit-testing-with-mstest) - Pruebas unitarias
* [Nuget](https://www.nuget.org/) - Manejador de dependencias
* [Visual Studio](https://visualstudio.microsoft.com/) - Entorno de programacion
* [FluentValidation](https://fluentvalidation.net/) - Proveedor de validaciones


## ✒️ Autores 

* **Sebastian Ferrari** - *Trabajo Inicial* - [kitpymes](https://kitpymes.com)


## 📄 Licencia 

* Este proyecto está bajo la Licencia [LICENSE](LICENSE.txt)


## 🎁 Gratitud 

* Este proyecto fue diseñado para compartir, creemos que es la mejor forma de ayudar 📢
* Cada persona que contribuya sera invitada a tomar una 🍺 
* Gracias a todos! 🤓

---
[Kitpymes](https://github.com/kitpymes) 😊