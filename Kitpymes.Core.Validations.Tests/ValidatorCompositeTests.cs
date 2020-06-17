using Kitpymes.Core.Validations.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kitpymes.Core.Validations.Tests
{
    [TestClass]
    public class ValdiatorCompositeTests
    {
        public const string MESSAGE = "Esto es un mensaje";
        public const string FIELD_NAME = "FIELD_NAME";
        public const string COMPARE_FIELD_NAME = "COMPARE_FIELD_NAME";
        public const string RULE_FIELD_NAME = "RULE_FIELD_NAME";
        public const string CUSTOM_FIELD_NAME = "CUSTOM_FIELD_NAME";
        public const long MAX = long.MinValue;
        public const long MIN = long.MaxValue;

        [TestMethod]
        public void IsDirectoryAddRulesPassingInvalidArgumentsReturnErrors()
        {
            var value = FakeTypes.ReferenceTypes.ClassTypes.String_New();

            var result = Validator
                .AddRule(value, x => x.IsDirectory())
                .AddRule(value, x => x.IsDirectory(FIELD_NAME))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Default, x => x.IsDirectory())
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Empty, x => x.IsDirectory(FIELD_NAME))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Empty, x => x.IsDirectoryWithMessage(MESSAGE))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Null, x => x.IsDirectory(CUSTOM_FIELD_NAME).WithRuleFieldName(RULE_FIELD_NAME))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Null, x => x.IsDirectory().WithRuleFieldName(RULE_FIELD_NAME));

            Assert.IsFalse(result.IsValid);

            var exception = Assert.ThrowsException<ValidationsException>(() => result.Throw());

            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Count == 7);
            Assert.IsTrue(exception.Contains(Messages.Directory(value)));
            Assert.IsTrue(exception.Contains(Messages.Directory(value, FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Directory()));
            Assert.IsTrue(exception.Contains(Messages.Directory(fieldName: FIELD_NAME)));
            Assert.IsTrue(exception.Contains(MESSAGE));
            Assert.IsTrue(exception.Contains(Messages.Directory(fieldName: CUSTOM_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Directory(fieldName: RULE_FIELD_NAME)));
        }

        [TestMethod]
        public void Email_AddRulesPassingInvalidArgumentsReturnErrors()
        {
            var value = FakeTypes.ReferenceTypes.ClassTypes.String_New();

            var result = Validator
                .AddRule(value, x => x.IsEmail())
                .AddRule(value, x => x.IsEmail(FIELD_NAME))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Default, x => x.IsEmail())
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Empty, x => x.IsEmail(FIELD_NAME))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Empty, x => x.IsEmailWithMessage(MESSAGE))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Null, x => x.IsEmail(CUSTOM_FIELD_NAME).WithRuleFieldName(RULE_FIELD_NAME))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Null, x => x.IsEmail().WithRuleFieldName(RULE_FIELD_NAME));

            Assert.IsFalse(result.IsValid);

            var exception = Assert.ThrowsException<ValidationsException>(() => result.Throw());

            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Count == 7);
            Assert.IsTrue(exception.Contains(Messages.Email(value)));
            Assert.IsTrue(exception.Contains(Messages.Email(value, FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Email()));
            Assert.IsTrue(exception.Contains(Messages.Email(fieldName: FIELD_NAME)));
            Assert.IsTrue(exception.Contains(MESSAGE));
            Assert.IsTrue(exception.Contains(Messages.Email(fieldName: CUSTOM_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Email(fieldName: RULE_FIELD_NAME)));
        }

        [TestMethod]
        public void Extension_AddRulesPassingInvalidArgumentsReturnErrors()
        {
            var value = FakeTypes.ReferenceTypes.ClassTypes.String_New();

            var result = Validator
                .AddRule(value, x => x.IsExtension())
                .AddRule(value, x => x.IsExtension(FIELD_NAME))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Default, x => x.IsExtension())
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Empty, x => x.IsExtension(FIELD_NAME))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Empty, x => x.IsExtensionWithMessage(MESSAGE))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Null, x => x.IsExtension(CUSTOM_FIELD_NAME).WithRuleFieldName(RULE_FIELD_NAME))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Null, x => x.IsExtension().WithRuleFieldName(RULE_FIELD_NAME));

            Assert.IsFalse(result.IsValid);

            var exception = Assert.ThrowsException<ValidationsException>(() => result.Throw());

            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Count == 7);
            Assert.IsTrue(exception.Contains(Messages.Extension(value)));
            Assert.IsTrue(exception.Contains(Messages.Extension(value, FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Extension()));
            Assert.IsTrue(exception.Contains(Messages.Extension(fieldName: FIELD_NAME)));
            Assert.IsTrue(exception.Contains(MESSAGE));
            Assert.IsTrue(exception.Contains(Messages.Extension(fieldName: CUSTOM_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Extension(fieldName: RULE_FIELD_NAME)));
        }

        [TestMethod]
        public void File_AddRulesPassingInvalidArgumentsReturnErrors()
        {
            var value = FakeTypes.ReferenceTypes.ClassTypes.String_New();

            var result = Validator
                .AddRule(value, x => x.IsFile())
                .AddRule(value, x => x.IsFile(FIELD_NAME))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Default, x => x.IsFile())
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Empty, x => x.IsFile(FIELD_NAME))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Empty, x => x.IsFileWithMessage(MESSAGE))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Null, x => x.IsFile(CUSTOM_FIELD_NAME).WithRuleFieldName(RULE_FIELD_NAME))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Null, x => x.IsFile().WithRuleFieldName(RULE_FIELD_NAME));

            Assert.IsFalse(result.IsValid);

            var exception = Assert.ThrowsException<ValidationsException>(() => result.Throw());

            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Count == 7);
            Assert.IsTrue(exception.Contains(Messages.File(value)));
            Assert.IsTrue(exception.Contains(Messages.File(value, FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.File()));
            Assert.IsTrue(exception.Contains(Messages.File(fieldName: FIELD_NAME)));
            Assert.IsTrue(exception.Contains(MESSAGE));
            Assert.IsTrue(exception.Contains(Messages.File(fieldName: CUSTOM_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.File(fieldName: RULE_FIELD_NAME)));
        }

        [TestMethod]
        public void Name_AddRulesPassingInvalidArgumentsReturnErrors()
        {
            var value = FakeTypes.ReferenceTypes.ClassTypes.String_New();

            var result = Validator
                .AddRule(value, x => x.IsName())
                .AddRule(value, x => x.IsName(FIELD_NAME))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Default, x => x.IsName())
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Empty, x => x.IsName(FIELD_NAME))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Empty, x => x.IsNameWithMessage(MESSAGE))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Null, x => x.IsName(CUSTOM_FIELD_NAME).WithRuleFieldName(RULE_FIELD_NAME))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Null, x => x.IsName().WithRuleFieldName(RULE_FIELD_NAME));

            Assert.IsFalse(result.IsValid);

            var exception = Assert.ThrowsException<ValidationsException>(() => result.Throw());

            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Count == 7);
            Assert.IsTrue(exception.Contains(Messages.Name(value)));
            Assert.IsTrue(exception.Contains(Messages.Name(value, FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Name()));
            Assert.IsTrue(exception.Contains(Messages.Name(fieldName: FIELD_NAME)));
            Assert.IsTrue(exception.Contains(MESSAGE));
            Assert.IsTrue(exception.Contains(Messages.Name(fieldName: CUSTOM_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Name(fieldName: RULE_FIELD_NAME)));
        }

        [TestMethod]
        public void Password_AddRulesPassingInvalidArgumentsReturnErrors()
        {
            var min = FakeTypes.ValueTypes.SimpleTypes.Int_Max;

            var result = Validator
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Default, x => x.IsPassword(min))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Empty, x => x.IsPassword(min, FIELD_NAME))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Empty, x => x.IsPasswordWithMessage(min, MESSAGE))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Null, x => x.IsPassword(min, CUSTOM_FIELD_NAME).WithRuleFieldName(RULE_FIELD_NAME))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Null, x => x.IsPassword(min).WithRuleFieldName(RULE_FIELD_NAME));

            Assert.IsFalse(result.IsValid);

            var exception = Assert.ThrowsException<ValidationsException>(() => result.Throw());

            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Count == 5);
            Assert.IsTrue(exception.Contains(Messages.Password(min)));
            Assert.IsTrue(exception.Contains(Messages.Password(min, FIELD_NAME)));
            Assert.IsTrue(exception.Contains(MESSAGE));
            Assert.IsTrue(exception.Contains(Messages.Password(min, CUSTOM_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Password(min, RULE_FIELD_NAME)));
        }

        [TestMethod]
        public void Subdomain_AddRulesPassingInvalidArgumentsReturnErrors()
        {
            var value = FakeTypes.ReferenceTypes.ClassTypes.String_New();

            var result = Validator
                .AddRule(value, x => x.IsSubdomain())
                .AddRule(value, x => x.IsSubdomain(FIELD_NAME))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Default, x => x.IsSubdomain())
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Empty, x => x.IsSubdomain(FIELD_NAME))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Empty, x => x.IsSubdomainWithMessage(MESSAGE))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Null, x => x.IsSubdomain(CUSTOM_FIELD_NAME).WithRuleFieldName(RULE_FIELD_NAME))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Null, x => x.IsSubdomain().WithRuleFieldName(RULE_FIELD_NAME));

            Assert.IsFalse(result.IsValid);

            var exception = Assert.ThrowsException<ValidationsException>(() => result.Throw());

            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Count == 7);
            Assert.IsTrue(exception.Contains(Messages.Subdomain(value)));
            Assert.IsTrue(exception.Contains(Messages.Subdomain(value, FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Subdomain()));
            Assert.IsTrue(exception.Contains(Messages.Subdomain(fieldName: FIELD_NAME)));
            Assert.IsTrue(exception.Contains(MESSAGE));
            Assert.IsTrue(exception.Contains(Messages.Subdomain(fieldName: CUSTOM_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Subdomain(fieldName: RULE_FIELD_NAME)));
        }

        [TestMethod]
        public void Example_Rules()
        {
            var min = FakeTypes.ValueTypes.SimpleTypes.Int_Max;

            var result = Validator
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Default, x => x
                    .IsDirectory()
                    .IsEmail()
                    .IsExtension()
                    .IsFile()
                    .IsName()
                    .IsPassword(min)
                    .IsSubdomain())
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Null, x => x
                    .IsDirectory()
                    .IsEmail()
                    .IsExtension()
                    .IsFile()
                    .IsName()
                    .IsPassword(min)
                    .IsSubdomain()
                    .WithRuleFieldName(RULE_FIELD_NAME))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_New(), x => x
                    .IsDirectory(FIELD_NAME)
                    .IsEmail(FIELD_NAME)
                    .IsExtension(FIELD_NAME)
                    .IsFile(FIELD_NAME)
                    .IsName(FIELD_NAME)
                    .IsPassword(min, FIELD_NAME)
                    .IsSubdomain(FIELD_NAME)
                    .WithRuleFieldName(RULE_FIELD_NAME))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Empty, x => x
                    .IsDirectoryWithMessage(MESSAGE)
                    .IsEmailWithMessage(MESSAGE)
                    .IsExtensionWithMessage(MESSAGE)
                    .IsFileWithMessage(MESSAGE)
                    .IsNameWithMessage(MESSAGE)
                    .IsPasswordWithMessage(min, MESSAGE)
                    .IsSubdomainWithMessage(MESSAGE));

            Assert.IsFalse(result.IsValid);

            var exception = Assert.ThrowsException<ValidationsException>(() => result.Throw());

            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Count == 28);
            Assert.IsTrue(exception.Contains(Messages.Directory(FakeTypes.ReferenceTypes.ClassTypes.String_Default)));
            Assert.IsTrue(exception.Contains(Messages.Email(FakeTypes.ReferenceTypes.ClassTypes.String_Default)));
            Assert.IsTrue(exception.Contains(Messages.Extension(FakeTypes.ReferenceTypes.ClassTypes.String_Default)));
            Assert.IsTrue(exception.Contains(Messages.File(FakeTypes.ReferenceTypes.ClassTypes.String_Default)));
            Assert.IsTrue(exception.Contains(Messages.Name(FakeTypes.ReferenceTypes.ClassTypes.String_Default)));
            Assert.IsTrue(exception.Contains(Messages.Password(min)));
            Assert.IsTrue(exception.Contains(Messages.Subdomain(FakeTypes.ReferenceTypes.ClassTypes.String_Default)));

            Assert.IsTrue(exception.Contains(Messages.Directory(FakeTypes.ReferenceTypes.ClassTypes.String_Null, RULE_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Email(FakeTypes.ReferenceTypes.ClassTypes.String_Null, RULE_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Extension(FakeTypes.ReferenceTypes.ClassTypes.String_Null, RULE_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.File(FakeTypes.ReferenceTypes.ClassTypes.String_Null, RULE_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Name(FakeTypes.ReferenceTypes.ClassTypes.String_Null, RULE_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Password(min, RULE_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Subdomain(FakeTypes.ReferenceTypes.ClassTypes.String_Null, RULE_FIELD_NAME)));

            Assert.IsTrue(exception.Contains(Messages.Directory(FakeTypes.ReferenceTypes.ClassTypes.String_New(), FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Email(FakeTypes.ReferenceTypes.ClassTypes.String_New(), FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Extension(FakeTypes.ReferenceTypes.ClassTypes.String_New(), FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.File(FakeTypes.ReferenceTypes.ClassTypes.String_New(), FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Name(FakeTypes.ReferenceTypes.ClassTypes.String_New(), FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Password(min, FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Subdomain(FakeTypes.ReferenceTypes.ClassTypes.String_New(), FIELD_NAME)));

            Assert.IsTrue(exception.Contains(MESSAGE));
        }
    }
}
