using Kitpymes.Core.Validations.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kitpymes.Core.Validations.FluentValidation.Tests
{
    [TestClass]
    public class FluentValidatorCompositeTests
    {
        [TestMethod]
        public void IsDirectory_PassingInvalidArgumentsReturnErrors()
        {
            var mock = new FakeObject
            {
                DirectoryPath = FakeTypes.ReferenceTypes.ClassTypes.String_New(),
            };

            var validator = new FakeObjectValidator();

            validator.RuleFor(_ => _.DirectoryPath)
                .IsDirectory()
                .IsDirectory(nameof(FakeObject.DirectoryPath) + FakeObject.FIELD_NAME);

            // Validate
            var result = validator.Validate(mock);
            var messages = result.ToString();

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 2);
            Assert.IsFalse(string.IsNullOrWhiteSpace(messages));
            Assert.IsTrue(messages.Contains(Messages.Directory(mock.DirectoryPath, "Directory Path")));
            Assert.IsTrue(messages.Contains(Messages.Directory(mock.DirectoryPath, nameof(FakeObject.DirectoryPath) + FakeObject.FIELD_NAME)));
        }

        [TestMethod]
        public void IsEmail_PassingInvalidArgumentsReturnErrors()
        {
            var mock = new FakeObject
            {
                Email = FakeTypes.ReferenceTypes.ClassTypes.String_New(),
            };

            var validator = new FakeObjectValidator();

            validator.RuleFor(_ => _.Email)
                .IsEmail()
                .IsEmail(nameof(FakeObject.Email) + FakeObject.FIELD_NAME);

            // Validate
            var result = validator.Validate(mock);
            var messages = result.ToString();

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 2);
            Assert.IsFalse(string.IsNullOrWhiteSpace(messages));
            Assert.IsTrue(messages.Contains(Messages.Email(mock.Email, nameof(FakeObject.Email))));
            Assert.IsTrue(messages.Contains(Messages.Email(mock.Email, nameof(FakeObject.Email) + FakeObject.FIELD_NAME)));
        }

        [TestMethod]
        public void IsExtension_PassingInvalidArgumentsReturnErrors()
        {
            var mock = new FakeObject
            {
                FilePathExtension = FakeTypes.ReferenceTypes.ClassTypes.String_New(),
            };

            var validator = new FakeObjectValidator();

            validator.RuleFor(_ => _.FilePathExtension)
                .IsExtension()
                .IsExtension(nameof(FakeObject.FilePathExtension) + FakeObject.FIELD_NAME);

            // Validate
            var result = validator.Validate(mock);
            var messages = result.ToString();

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 2);
            Assert.IsFalse(string.IsNullOrWhiteSpace(messages));
            Assert.IsTrue(messages.Contains(Messages.Extension(mock.FilePathExtension, "File Path Extension")));
            Assert.IsTrue(messages.Contains(Messages.Extension(mock.FilePathExtension, nameof(FakeObject.FilePathExtension) + FakeObject.FIELD_NAME)));
        }

        [TestMethod]
        public void IsFile_PassingInvalidArgumentsReturnErrors()
        {
            var mock = new FakeObject
            {
                FilePath = FakeTypes.ReferenceTypes.ClassTypes.String_New(),
            };

            var validator = new FakeObjectValidator();

            validator.RuleFor(_ => _.FilePath)
                .IsFile()
                .IsFile(nameof(FakeObject.FilePath) + FakeObject.FIELD_NAME);

            // Validate
            var result = validator.Validate(mock);
            var messages = result.ToString();

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 2);
            Assert.IsFalse(string.IsNullOrWhiteSpace(messages));
            Assert.IsTrue(messages.Contains(Messages.File(mock.FilePath, "File Path")));
            Assert.IsTrue(messages.Contains(Messages.File(mock.FilePath, nameof(FakeObject.FilePath) + FakeObject.FIELD_NAME)));
        }

        [TestMethod]
        public void IsName_PassingInvalidArgumentsReturnErrors()
        {
            var mock = new FakeObject
            {
                Name = FakeTypes.ReferenceTypes.ClassTypes.String_New(),
            };

            var validator = new FakeObjectValidator();

            validator.RuleFor(_ => _.Name)
                .IsName()
                .IsName(nameof(FakeObject.Name) + FakeObject.FIELD_NAME);

            // Validate
            var result = validator.Validate(mock);
            var messages = result.ToString();

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 2);
            Assert.IsFalse(string.IsNullOrWhiteSpace(messages));
            Assert.IsTrue(messages.Contains(Messages.Name(mock.Name, nameof(FakeObject.Name))));
            Assert.IsTrue(messages.Contains(Messages.Name(mock.Name, nameof(FakeObject.Name) + FakeObject.FIELD_NAME)));
        }

        [TestMethod]
        public void IsPassword_PassingInvalidArgumentsReturnErrors()
        {
            var mock = new FakeObject
            {
                Password = FakeTypes.ReferenceTypes.ClassTypes.String_New(),
            };

            var validator = new FakeObjectValidator();

            validator.RuleFor(_ => _.Password)
                .IsPassword(FakeObject.MAX)
                .IsPassword(FakeObject.MAX, nameof(FakeObject.Password) + FakeObject.FIELD_NAME);

            // Validate
            var result = validator.Validate(mock);
            var messages = result.ToString();

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 2);
            Assert.IsFalse(string.IsNullOrWhiteSpace(messages));
            Assert.IsTrue(messages.Contains(Messages.Password(FakeObject.MAX, nameof(FakeObject.Password))));
            Assert.IsTrue(messages.Contains(Messages.Password(FakeObject.MAX, nameof(FakeObject.Password) + FakeObject.FIELD_NAME)));
        }

        [TestMethod]
        public void IsSubdomain_PassingInvalidArgumentsReturnErrors()
        {
            var mock = new FakeObject
            {
                Subdomain = FakeTypes.ReferenceTypes.ClassTypes.String_New(),
            };

            var validator = new FakeObjectValidator();

            validator.RuleFor(_ => _.Subdomain)
                .IsSubdomain()
                .IsSubdomain(nameof(FakeObject.Subdomain) + FakeObject.FIELD_NAME);

            // Validate
            var result = validator.Validate(mock);
            var messages = result.ToString();

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 2);
            Assert.IsFalse(string.IsNullOrWhiteSpace(messages));
            Assert.IsTrue(messages.Contains(Messages.Subdomain(mock.Subdomain, nameof(FakeObject.Subdomain))));
            Assert.IsTrue(messages.Contains(Messages.Subdomain(mock.Subdomain, nameof(FakeObject.Subdomain) + FakeObject.FIELD_NAME)));
        }
    }
}