using Kitpymes.Core.Validations.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kitpymes.Core.Validations.FluentValidation.Tests
{
    [TestClass]
    public class FluentValidatorTests
    {
        [TestMethod]
        public void IsNullOrAny_PassingInvalidArgumentsReturnErrors()
        {
            var mock = new FakeObject
            {
                Roles = FakeTypes.ColecctionsTypes.List_CountZero,
                Permissions = FakeTypes.ColecctionsTypes.List_Null
            };

            var validator = new FakeObjectValidator();

            validator.RuleFor(_ => _.Roles).IsNullOrAny().IsNullOrAny(nameof(FakeObject.Roles) + FakeObject.FIELD_NAME);
            validator.RuleFor(_ => _.Permissions).IsNullOrAny().IsNullOrAny(nameof(FakeObject.Permissions) + FakeObject.FIELD_NAME);

            // Validate
            var result = validator.Validate(mock);
            var messages = result.ToString();

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 4);
            Assert.IsFalse(string.IsNullOrWhiteSpace(messages));
            Assert.IsTrue(messages.Contains(Messages.NullOrAny(nameof(FakeObject.Roles))));
            Assert.IsTrue(messages.Contains(Messages.NullOrAny(nameof(FakeObject.Roles) + FakeObject.FIELD_NAME)));
            Assert.IsTrue(messages.Contains(Messages.NullOrAny(nameof(FakeObject.Permissions))));
            Assert.IsTrue(messages.Contains(Messages.NullOrAny(nameof(FakeObject.Permissions) + FakeObject.FIELD_NAME)));
        }

        [TestMethod]
        public void IsEqual_PassingInvalidArgumentsReturnErrors()
        {
            var valueCompare = FakeTypes.ColecctionsTypes.List_New();

            var mock = new FakeObject
            {
                Roles = FakeTypes.ColecctionsTypes.List_CountZero
            };

            var validator = new FakeObjectValidator();

            validator.RuleFor(_ => _.Roles).IsEqual(valueCompare);
            validator.RuleFor(_ => _.Roles).IsEqual(valueCompare, FakeObject.FIELD_NAME);
            validator.RuleFor(_ => _.Roles).IsEqual(valueCompare, nameof(FakeObject.Roles) + FakeObject.FIELD_NAME, FakeObject.COMPARE_FIELD_NAME);

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
        public void IsGreater_PassingInvalidArgumentsReturnErrors()
        {
            var max = FakeTypes.ValueTypes.SimpleTypes.Int_Min;

            var mock = new FakeObject
            {
                Point = FakeTypes.ValueTypes.SimpleTypes.Int_Max,
                Name = FakeTypes.ReferenceTypes.ClassTypes.String_New(),
                Roles = FakeTypes.ColecctionsTypes.List_New()
            };

            var validator = new FakeObjectValidator();

            validator.RuleFor(_ => _.Point).IsGreater(max).IsGreater(max, nameof(FakeObject.Point) + FakeObject.FIELD_NAME);
            validator.RuleFor(_ => _.Name).IsGreater(max).IsGreater(max, nameof(FakeObject.Name) + FakeObject.FIELD_NAME);
            validator.RuleFor(_ => _.Roles).IsGreater(max).IsGreater(max, nameof(FakeObject.Roles) + FakeObject.FIELD_NAME);

            // Validate
            var result = validator.Validate(mock);
            var messages = result.ToString();

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 6);
            Assert.IsFalse(string.IsNullOrWhiteSpace(messages));
            Assert.IsTrue(messages.Contains(Messages.Greater(max, nameof(FakeObject.Point))));
            Assert.IsTrue(messages.Contains(Messages.Greater(max, nameof(FakeObject.Point) + FakeObject.FIELD_NAME)));
            Assert.IsTrue(messages.Contains(Messages.Greater(max, nameof(FakeObject.Name))));
            Assert.IsTrue(messages.Contains(Messages.Greater(max, nameof(FakeObject.Name) + FakeObject.FIELD_NAME)));
            Assert.IsTrue(messages.Contains(Messages.Greater(max, nameof(FakeObject.Roles))));
            Assert.IsTrue(messages.Contains(Messages.Greater(max, nameof(FakeObject.Roles) + FakeObject.FIELD_NAME)));

        }

        [TestMethod]
        public void IsLess_PassingInvalidArgumentsReturnErrors()
        {
            var min = FakeTypes.ValueTypes.SimpleTypes.Int_Max;

            var mock = new FakeObject
            {
                Point = FakeTypes.ValueTypes.SimpleTypes.Int_Min,
                Name = FakeTypes.ReferenceTypes.ClassTypes.String_New(),
                Roles = FakeTypes.ColecctionsTypes.List_New()
            };

            var validator = new FakeObjectValidator();

            validator.RuleFor(_ => _.Point).IsLess(min).IsLess(min, nameof(FakeObject.Point) + FakeObject.FIELD_NAME);
            validator.RuleFor(_ => _.Name).IsLess(min).IsLess(min, nameof(FakeObject.Name) + FakeObject.FIELD_NAME);
            validator.RuleFor(_ => _.Roles).IsLess(min).IsLess(min, nameof(FakeObject.Roles) + FakeObject.FIELD_NAME);

            // Validate
            var result = validator.Validate(mock);
            var messages = result.ToString();

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 6);
            Assert.IsFalse(string.IsNullOrWhiteSpace(messages));
            Assert.IsTrue(messages.Contains(Messages.Less(min, nameof(FakeObject.Point))));
            Assert.IsTrue(messages.Contains(Messages.Less(min, nameof(FakeObject.Point) + FakeObject.FIELD_NAME)));
            Assert.IsTrue(messages.Contains(Messages.Less(min, nameof(FakeObject.Name))));
            Assert.IsTrue(messages.Contains(Messages.Less(min, nameof(FakeObject.Name) + FakeObject.FIELD_NAME)));
            Assert.IsTrue(messages.Contains(Messages.Less(min, nameof(FakeObject.Roles))));
            Assert.IsTrue(messages.Contains(Messages.Less(min, nameof(FakeObject.Roles) + FakeObject.FIELD_NAME)));
        }

        [TestMethod]
        public void IsNullOrEmpty_PassingInvalidArgumentsReturnErrors()
        {
            var mock = new FakeObject
            {
                Id = FakeTypes.ValueTypes.StructureTypes.Guid_Null,
                Point = FakeTypes.ValueTypes.SimpleTypes.Int_Null,
                Name = FakeTypes.ReferenceTypes.ClassTypes.String_Null,
                Roles = FakeTypes.ColecctionsTypes.List_Null
            };

            var validator = new FakeObjectValidator();

            validator.RuleFor(_ => _.Id).IsNullOrEmpty().IsNullOrEmpty(nameof(FakeObject.Id) + FakeObject.FIELD_NAME);
            validator.RuleFor(_ => _.Point).IsNullOrEmpty().IsNullOrEmpty(nameof(FakeObject.Point) + FakeObject.FIELD_NAME);
            validator.RuleFor(_ => _.Name).IsNullOrEmpty().IsNullOrEmpty(nameof(FakeObject.Name) + FakeObject.FIELD_NAME);
            validator.RuleFor(_ => _.Roles).IsNullOrAny().IsNullOrAny(nameof(FakeObject.Roles) + FakeObject.FIELD_NAME);

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
            var min = FakeTypes.ValueTypes.SimpleTypes.Int_Max;
            var max = FakeTypes.ValueTypes.SimpleTypes.Int_Min;

            var mock = new FakeObject
            {
                Point = FakeTypes.ValueTypes.SimpleTypes.Int_Max,
                Name = FakeTypes.ReferenceTypes.ClassTypes.String_New(),
                Roles = FakeTypes.ColecctionsTypes.List_New(10)
            };

            var validator = new FakeObjectValidator();

            validator.RuleFor(_ => _.Point).IsRange(min, max).IsRange(min, max, nameof(FakeObject.Point) + FakeObject.FIELD_NAME);
            validator.RuleFor(_ => _.Name).IsRange(min, max).IsRange(min, max, nameof(FakeObject.Name) + FakeObject.FIELD_NAME);
            validator.RuleFor(_ => _.Roles).IsRange(min, max).IsRange(min, max, nameof(FakeObject.Roles) + FakeObject.FIELD_NAME);

            // Validate
            var result = validator.Validate(mock);
            var messages = result.ToString();

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 6);
            Assert.IsFalse(string.IsNullOrWhiteSpace(messages));
            Assert.IsTrue(messages.Contains(Messages.Range(min, max, nameof(FakeObject.Point))));
            Assert.IsTrue(messages.Contains(Messages.Range(min, max, nameof(FakeObject.Point) + FakeObject.FIELD_NAME)));
            Assert.IsTrue(messages.Contains(Messages.Range(min, max, nameof(FakeObject.Name))));
            Assert.IsTrue(messages.Contains(Messages.Range(min, max, nameof(FakeObject.Name) + FakeObject.FIELD_NAME)));
            Assert.IsTrue(messages.Contains(Messages.Range(min, max, nameof(FakeObject.Roles))));
            Assert.IsTrue(messages.Contains(Messages.Range(min, max, nameof(FakeObject.Roles) + FakeObject.FIELD_NAME)));
        }

        [TestMethod]
        public void IsRegex_PassingInvalidArgumentsReturnErrors()
        {
            var mock = new FakeObject
            {
                Name = FakeTypes.ReferenceTypes.ClassTypes.String_New(),
                Email = FakeTypes.ReferenceTypes.ClassTypes.String_New(),
                Subdomain = FakeTypes.ReferenceTypes.ClassTypes.String_New(),
            };

            var validator = new FakeObjectValidator();

            validator.RuleFor(_ => _.Name).IsRegex(Shared.Util.Regexp.ForName).IsRegex(Shared.Util.Regexp.ForName, nameof(FakeObject.Name) + FakeObject.FIELD_NAME);
            validator.RuleFor(_ => _.Email).IsRegex(Shared.Util.Regexp.ForEmail).IsRegex(Shared.Util.Regexp.ForEmail, nameof(FakeObject.Email) + FakeObject.FIELD_NAME);
            validator.RuleFor(_ => _.Subdomain).IsRegex(Shared.Util.Regexp.ForSubdomain).IsRegex(Shared.Util.Regexp.ForSubdomain, nameof(FakeObject.Subdomain) + FakeObject.FIELD_NAME);

            // Validate
            var result = validator.Validate(mock);
            var messages = result.ToString();

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 6);
            Assert.IsFalse(string.IsNullOrWhiteSpace(messages));
            Assert.IsTrue(messages.Contains(Messages.Regex(nameof(FakeObject.Name))));
            Assert.IsTrue(messages.Contains(Messages.Regex(nameof(FakeObject.Name) + FakeObject.FIELD_NAME)));
            Assert.IsTrue(messages.Contains(Messages.Regex(nameof(FakeObject.Email))));
            Assert.IsTrue(messages.Contains(Messages.Regex(nameof(FakeObject.Email) + FakeObject.FIELD_NAME)));
            Assert.IsTrue(messages.Contains(Messages.Regex(nameof(FakeObject.Subdomain))));
            Assert.IsTrue(messages.Contains(Messages.Regex(nameof(FakeObject.Subdomain) + FakeObject.FIELD_NAME)));
        }

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
        public void IsFileExtension_PassingInvalidArgumentsReturnErrors()
        {
            var mock = new FakeObject
            {
                FilePathExtension = FakeTypes.ReferenceTypes.ClassTypes.String_New(),
            };

            var validator = new FakeObjectValidator();

            validator.RuleFor(_ => _.FilePathExtension)
                .IsFileExtension()
                .IsFileExtension(nameof(FakeObject.FilePathExtension) + FakeObject.FIELD_NAME);

            // Validate
            var result = validator.Validate(mock);
            var messages = result.ToString();

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 2);
            Assert.IsFalse(string.IsNullOrWhiteSpace(messages));
            Assert.IsTrue(messages.Contains(Messages.FileExtension(mock.FilePathExtension, "File Path Extension")));
            Assert.IsTrue(messages.Contains(Messages.FileExtension(mock.FilePathExtension, nameof(FakeObject.FilePathExtension) + FakeObject.FIELD_NAME)));
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