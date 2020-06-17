using Kitpymes.Core.Validations.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Kitpymes.Core.Validations.FluentValidation.Tests
{
    [TestClass]
    public class FluentValidatorSharedTests
    {
        [TestMethod]
        public void IsAny_PassingInvalidArgumentsReturnErrors()
        {
            var mock = new FakeObject
            {
                Roles = FakeList.NotNullCountZero,
                Permissions = FakeList.Null
            };

            var validator = new FakeObjectValidator();

            validator.RuleFor(_ => _.Roles).IsAny().IsAny(nameof(FakeObject.Roles) + FakeObject.FIELD_NAME);
            validator.RuleFor(_ => _.Permissions).IsAny().IsAny(nameof(FakeObject.Permissions) + FakeObject.FIELD_NAME);

            // Validate
            var result = validator.Validate(mock);
            var messages = result.ToString();

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 4);
            Assert.IsFalse(string.IsNullOrWhiteSpace(messages));
            Assert.IsTrue(messages.Contains(Messages.Any(nameof(FakeObject.Roles))));
            Assert.IsTrue(messages.Contains(Messages.Any(nameof(FakeObject.Roles) + FakeObject.FIELD_NAME)));
            Assert.IsTrue(messages.Contains(Messages.Any(nameof(FakeObject.Permissions))));
            Assert.IsTrue(messages.Contains(Messages.Any(nameof(FakeObject.Permissions) + FakeObject.FIELD_NAME)));
        }

        [TestMethod]
        public void IsEqual_PassingInvalidArgumentsReturnErrors()
        {
            var mock = new FakeObject
            {
                Roles = FakeList.NotNullCountZero
            };

            var validator = new FakeObjectValidator();

            validator.RuleFor(_ => _.Roles).IsEqual(FakeList.NotNull());
            validator.RuleFor(_ => _.Roles).IsEqual(FakeList.NotNull(), FakeObject.FIELD_NAME);
            validator.RuleFor(_ => _.Roles).IsEqual(FakeList.NotNull(), nameof(FakeObject.Roles) + FakeObject.FIELD_NAME, FakeObject.COMPARE_FIELD_NAME);

            // Validate
            var result = validator.Validate(mock);
            var messages = result.ToString();

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 3);
            Assert.IsFalse(string.IsNullOrWhiteSpace(messages));
            Assert.IsTrue(messages.Contains(Messages.Equal(nameof(FakeObject.Roles))));
            Assert.IsTrue(messages.Contains(Messages.Equal(FakeObject.FIELD_NAME)));
            Assert.IsTrue(messages.Contains(Messages.EqualWithFieldsName(nameof(FakeObject.Roles) + FakeObject.FIELD_NAME, FakeObject.COMPARE_FIELD_NAME)));
        }

        [TestMethod]
        public void IsMax_PassingInvalidArgumentsReturnErrors()
        {
            var mock = new FakeObject
            {
                Id = Guid.NewGuid(),
                Point = int.MaxValue,
                Name = Guid.NewGuid().ToString(),
                Roles = FakeList.NotNull(5)
            };

            var validator = new FakeObjectValidator();

            validator.RuleFor(_ => _.Id).IsMax(FakeObject.MIN).IsMax(FakeObject.MIN, nameof(FakeObject.Id) + FakeObject.FIELD_NAME);
            validator.RuleFor(_ => _.Point).IsMax(FakeObject.MIN).IsMax(FakeObject.MIN, nameof(FakeObject.Point) + FakeObject.FIELD_NAME);
            validator.RuleFor(_ => _.Name).IsMax(FakeObject.MIN).IsMax(FakeObject.MIN, nameof(FakeObject.Name) + FakeObject.FIELD_NAME);
            validator.RuleFor(_ => _.Roles).IsMax(FakeObject.MIN).IsMax(FakeObject.MIN, nameof(FakeObject.Roles) + FakeObject.FIELD_NAME);

            // Validate
            var result = validator.Validate(mock);
            var messages = result.ToString();

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 8);
            Assert.IsFalse(string.IsNullOrWhiteSpace(messages));
            Assert.IsTrue(messages.Contains(Messages.Max(FakeObject.MIN, nameof(FakeObject.Id))));
            Assert.IsTrue(messages.Contains(Messages.Max(FakeObject.MIN, nameof(FakeObject.Id) + FakeObject.FIELD_NAME)));
            Assert.IsTrue(messages.Contains(Messages.Max(FakeObject.MIN, nameof(FakeObject.Point))));
            Assert.IsTrue(messages.Contains(Messages.Max(FakeObject.MIN, nameof(FakeObject.Point) + FakeObject.FIELD_NAME)));
            Assert.IsTrue(messages.Contains(Messages.Max(FakeObject.MIN, nameof(FakeObject.Name))));
            Assert.IsTrue(messages.Contains(Messages.Max(FakeObject.MIN, nameof(FakeObject.Name) + FakeObject.FIELD_NAME)));
            Assert.IsTrue(messages.Contains(Messages.Max(FakeObject.MIN, nameof(FakeObject.Roles))));
            Assert.IsTrue(messages.Contains(Messages.Max(FakeObject.MIN, nameof(FakeObject.Roles) + FakeObject.FIELD_NAME)));

        }

        [TestMethod]
        public void IsMin_PassingInvalidArgumentsReturnErrors()
        {
            var mock = new FakeObject
            {
                Id = Guid.NewGuid(),
                Point = int.MaxValue,
                Name = Guid.NewGuid().ToString(),
                Roles = FakeList.NotNull(5)
            };

            var validator = new FakeObjectValidator();

            validator.RuleFor(_ => _.Id).IsMin(FakeObject.MAX).IsMin(FakeObject.MAX, nameof(FakeObject.Id) + FakeObject.FIELD_NAME);
            validator.RuleFor(_ => _.Point).IsMin(FakeObject.MAX).IsMin(FakeObject.MAX, nameof(FakeObject.Point) + FakeObject.FIELD_NAME);
            validator.RuleFor(_ => _.Name).IsMin(FakeObject.MAX).IsMin(FakeObject.MAX, nameof(FakeObject.Name) + FakeObject.FIELD_NAME);
            validator.RuleFor(_ => _.Roles).IsMin(FakeObject.MAX).IsMin(FakeObject.MAX, nameof(FakeObject.Roles) + FakeObject.FIELD_NAME);

            // Validate
            var result = validator.Validate(mock);
            var messages = result.ToString();

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 8);
            Assert.IsFalse(string.IsNullOrWhiteSpace(messages));
            Assert.IsTrue(messages.Contains(Messages.Min(FakeObject.MAX, nameof(FakeObject.Id))));
            Assert.IsTrue(messages.Contains(Messages.Min(FakeObject.MAX, nameof(FakeObject.Id) + FakeObject.FIELD_NAME)));
            Assert.IsTrue(messages.Contains(Messages.Min(FakeObject.MAX, nameof(FakeObject.Point))));
            Assert.IsTrue(messages.Contains(Messages.Min(FakeObject.MAX, nameof(FakeObject.Point) + FakeObject.FIELD_NAME)));
            Assert.IsTrue(messages.Contains(Messages.Min(FakeObject.MAX, nameof(FakeObject.Name))));
            Assert.IsTrue(messages.Contains(Messages.Min(FakeObject.MAX, nameof(FakeObject.Name) + FakeObject.FIELD_NAME)));
            Assert.IsTrue(messages.Contains(Messages.Min(FakeObject.MAX, nameof(FakeObject.Roles))));
            Assert.IsTrue(messages.Contains(Messages.Min(FakeObject.MAX, nameof(FakeObject.Roles) + FakeObject.FIELD_NAME)));
        }

        [TestMethod]
        public void IsNullOrEmpty_PassingInvalidArgumentsReturnErrors()
        {
            var mock = new FakeObject
            {
                Id = Guid.Empty,
                Point = null,
                Name = string.Empty,
                Roles = FakeList.Null
            };

            var validator = new FakeObjectValidator();

            validator.RuleFor(_ => _.Id).IsNullOrEmpty().IsNullOrEmpty(nameof(FakeObject.Id) + FakeObject.FIELD_NAME);
            validator.RuleFor(_ => _.Point).IsNullOrEmpty().IsNullOrEmpty(nameof(FakeObject.Point) + FakeObject.FIELD_NAME);
            validator.RuleFor(_ => _.Name).IsNullOrEmpty().IsNullOrEmpty(nameof(FakeObject.Name) + FakeObject.FIELD_NAME);
            validator.RuleFor(_ => _.Roles).IsNullOrEmpty().IsNullOrEmpty(nameof(FakeObject.Roles) + FakeObject.FIELD_NAME);

            // Validate
            var result = validator.Validate(mock);
            var messages = result.ToString();

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 8);
            Assert.IsFalse(string.IsNullOrWhiteSpace(messages));
            Assert.IsTrue(messages.Contains(Messages.NullOrEmpty(nameof(FakeObject.Id))));
            Assert.IsTrue(messages.Contains(Messages.NullOrEmpty(nameof(FakeObject.Id) + FakeObject.FIELD_NAME)));
            Assert.IsTrue(messages.Contains(Messages.NullOrEmpty(nameof(FakeObject.Point))));
            Assert.IsTrue(messages.Contains(Messages.NullOrEmpty(nameof(FakeObject.Point) + FakeObject.FIELD_NAME)));
            Assert.IsTrue(messages.Contains(Messages.NullOrEmpty(nameof(FakeObject.Name))));
            Assert.IsTrue(messages.Contains(Messages.NullOrEmpty(nameof(FakeObject.Name) + FakeObject.FIELD_NAME)));
            Assert.IsTrue(messages.Contains(Messages.NullOrEmpty(nameof(FakeObject.Roles))));
            Assert.IsTrue(messages.Contains(Messages.NullOrEmpty(nameof(FakeObject.Roles) + FakeObject.FIELD_NAME)));
        }

        [TestMethod]
        public void IsRange_PassingInvalidArgumentsReturnErrors()
        {
            var mock = new FakeObject
            {
                Id = Guid.NewGuid(),
                Point = int.MaxValue,
                Name = Guid.NewGuid().ToString(),
                Roles = FakeList.NotNull(10)
            };

            var validator = new FakeObjectValidator();

            validator.RuleFor(_ => _.Id).IsRange(FakeObject.MAX, FakeObject.MAX).IsRange(FakeObject.MAX, FakeObject.MAX, nameof(FakeObject.Id) + FakeObject.FIELD_NAME);
            validator.RuleFor(_ => _.Point).IsRange(FakeObject.MAX, FakeObject.MAX).IsRange(FakeObject.MAX, FakeObject.MAX, nameof(FakeObject.Point) + FakeObject.FIELD_NAME);
            validator.RuleFor(_ => _.Name).IsRange(FakeObject.MAX, FakeObject.MAX).IsRange(FakeObject.MAX, FakeObject.MAX, nameof(FakeObject.Name) + FakeObject.FIELD_NAME);
            validator.RuleFor(_ => _.Roles).IsRange(FakeObject.MAX, FakeObject.MAX).IsRange(FakeObject.MAX, FakeObject.MAX, nameof(FakeObject.Roles) + FakeObject.FIELD_NAME);

            // Validate
            var result = validator.Validate(mock);
            var messages = result.ToString();

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 8);
            Assert.IsFalse(string.IsNullOrWhiteSpace(messages));
            Assert.IsTrue(messages.Contains(Messages.Range(FakeObject.MAX, FakeObject.MAX, nameof(FakeObject.Id))));
            Assert.IsTrue(messages.Contains(Messages.Range(FakeObject.MAX, FakeObject.MAX, nameof(FakeObject.Id) + FakeObject.FIELD_NAME)));
            Assert.IsTrue(messages.Contains(Messages.Range(FakeObject.MAX, FakeObject.MAX, nameof(FakeObject.Point))));
            Assert.IsTrue(messages.Contains(Messages.Range(FakeObject.MAX, FakeObject.MAX, nameof(FakeObject.Point) + FakeObject.FIELD_NAME)));
            Assert.IsTrue(messages.Contains(Messages.Range(FakeObject.MAX, FakeObject.MAX, nameof(FakeObject.Name))));
            Assert.IsTrue(messages.Contains(Messages.Range(FakeObject.MAX, FakeObject.MAX, nameof(FakeObject.Name) + FakeObject.FIELD_NAME)));
            Assert.IsTrue(messages.Contains(Messages.Range(FakeObject.MAX, FakeObject.MAX, nameof(FakeObject.Roles))));
            Assert.IsTrue(messages.Contains(Messages.Range(FakeObject.MAX, FakeObject.MAX, nameof(FakeObject.Roles) + FakeObject.FIELD_NAME)));
        }

        [TestMethod]
        public void IsRegex_PassingInvalidArgumentsReturnErrors()
        {
            var mock = new FakeObject
            {
                Name = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString(),
                Subdomain = Guid.NewGuid().ToString(),
            };

            var validator = new FakeObjectValidator();

            validator.RuleFor(_ => _.Name).IsRegex(Regexp.ForName).IsRegex(Regexp.ForName, nameof(FakeObject.Name) + FakeObject.FIELD_NAME);
            validator.RuleFor(_ => _.Email).IsRegex(Regexp.ForEmail).IsRegex(Regexp.ForEmail, nameof(FakeObject.Email) + FakeObject.FIELD_NAME);
            validator.RuleFor(_ => _.Password).IsRegex(Regexp.ForPassword).IsRegex(Regexp.ForPassword, nameof(FakeObject.Password) + FakeObject.FIELD_NAME);
            validator.RuleFor(_ => _.Subdomain).IsRegex(Regexp.ForSubdomain).IsRegex(Regexp.ForSubdomain, nameof(FakeObject.Subdomain) + FakeObject.FIELD_NAME);

            // Validate
            var result = validator.Validate(mock);
            var messages = result.ToString();

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 8);
            Assert.IsFalse(string.IsNullOrWhiteSpace(messages));
            Assert.IsTrue(messages.Contains(Messages.Regex(nameof(FakeObject.Name))));
            Assert.IsTrue(messages.Contains(Messages.Regex(nameof(FakeObject.Name) + FakeObject.FIELD_NAME)));
            Assert.IsTrue(messages.Contains(Messages.Regex(nameof(FakeObject.Email))));
            Assert.IsTrue(messages.Contains(Messages.Regex(nameof(FakeObject.Email) + FakeObject.FIELD_NAME)));
            Assert.IsTrue(messages.Contains(Messages.Regex(nameof(FakeObject.Password))));
            Assert.IsTrue(messages.Contains(Messages.Regex(nameof(FakeObject.Password) + FakeObject.FIELD_NAME)));
            Assert.IsTrue(messages.Contains(Messages.Regex(nameof(FakeObject.Subdomain))));
            Assert.IsTrue(messages.Contains(Messages.Regex(nameof(FakeObject.Subdomain) + FakeObject.FIELD_NAME)));
        }
    }
}