using Kitpymes.Core.Validations.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kitpymes.Core.Validations.Tests
{
    [TestClass]
    public class ValidatorTests 
    {
        public const string MESSAGE = "Esto es un mensaje";
        public const string FIELD_NAME = "FIELD_NAME";
        public const string COMPARE_FIELD_NAME = "COMPARE_FIELD_NAME";
        public const string RULE_FIELD_NAME = "RULE_FIELD_NAME";
        public const string CUSTOM_FIELD_NAME = "CUSTOM_FIELD_NAME";
        public const long MAX = long.MinValue;
        public const long MIN = long.MaxValue;

        [TestMethod]
        public void IsNullOrAny_AddRules_Passing_InvalidArguments_Returns_Errors()
        {
            var result = Validator
                .AddRule(FakeTypes.ColecctionsTypes.List_Null, x => x.IsNullOrAny())
                .AddRule(FakeTypes.ReferenceTypes.MatrizTypes.Array_Default, x => x.IsNullOrAny(FIELD_NAME))
                .AddRule(FakeTypes.ColecctionsTypes.Stack_CountZero, x => x.IsNullOrAnyWithMessage(MESSAGE))
                .AddRule(FakeTypes.ColecctionsTypes.Collection_Null, x => x.IsNullOrAny().WithRuleFieldName(RULE_FIELD_NAME))
                .AddRule(FakeTypes.ColecctionsTypes.ArrayClass_CountZero, x => x.IsNullOrAny(CUSTOM_FIELD_NAME).WithRuleFieldName(RULE_FIELD_NAME));

            Assert.IsFalse(result.IsValid);

            var exception = Assert.ThrowsException<ValidationsException>(() => result.Throw());

            Assert.IsNotNull(exception);

            Assert.IsTrue(exception.Contains(Messages.NullOrAny()));
            Assert.IsTrue(exception.Contains(Messages.NullOrAny(FIELD_NAME)));
            Assert.IsTrue(exception.Contains(MESSAGE));
            Assert.IsTrue(exception.Contains(Messages.NullOrAny(RULE_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.NullOrAny(CUSTOM_FIELD_NAME)));
        }

        [TestMethod]
        public void IsCustom_AddRules_Passing_InvalidArguments_Returns_Errors()
        {
            var result = Validator
                .AddRule(() => FakeTypes.ColecctionsTypes.List_Null is null, MESSAGE)
                .AddRule(() =>
                {
                    if (FakeTypes.ColecctionsTypes.Stack_CountZero.Count == 0)
                    {
                        return FakeTypes.ColecctionsTypes.List_Null is null;
                    }

                    return false;
                }, MESSAGE + "PEPE");

            Assert.IsFalse(result.IsValid);

            var exception = Assert.ThrowsException<ValidationsException>(() => result.Throw());

            Assert.IsTrue(exception.Contains(MESSAGE));
            Assert.IsTrue(exception.Contains(MESSAGE + "PEPE"));
        }

        [TestMethod]
        public void IsEqual_AddRules_Passing_InvalidArguments_Returns_Errors()
        {
            var FIELD_NAME_COMPARE = "JOB EMAIL";

            var result = Validator
                .AddRule(FakeTypes.ColecctionsTypes.List_Null, x => x.IsEqual(FakeTypes.ColecctionsTypes.Stack_CountZero))
                .AddRule(FakeTypes.ReferenceTypes.MatrizTypes.Array_Default, x => x.IsEqual(FakeTypes.ColecctionsTypes.List_Null, (FIELD_NAME, FIELD_NAME_COMPARE)))
                .AddRule(FakeTypes.ColecctionsTypes.Stack_CountZero, x => x.IsEqualWithMessage(FakeTypes.ColecctionsTypes.List_Null, MESSAGE))
                .AddRule(FakeTypes.ColecctionsTypes.Collection_Null, x => x.IsEqual(FakeTypes.ColecctionsTypes.ArrayClass_CountZero).WithRuleFieldName(RULE_FIELD_NAME))
                .AddRule(FakeTypes.ColecctionsTypes.ArrayClass_CountZero, x => x.IsEqual(FakeTypes.ColecctionsTypes.List_Null, (CUSTOM_FIELD_NAME, FIELD_NAME_COMPARE)).WithRuleFieldName(RULE_FIELD_NAME));

            Assert.IsFalse(result.IsValid);

            var exception = Assert.ThrowsException<ValidationsException>(() => result.Throw());

            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Contains(Messages.Equal()));
            Assert.IsTrue(exception.Contains(Messages.EqualWithFieldsName(FIELD_NAME, FIELD_NAME_COMPARE)));
            Assert.IsTrue(exception.Contains(MESSAGE));
            Assert.IsTrue(exception.Contains(Messages.Equal(RULE_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.EqualWithFieldsName(CUSTOM_FIELD_NAME, FIELD_NAME_COMPARE)));
        }

        [TestMethod]
        public void IsGreater_AddRules_Passing_InvalidArguments_Returns_Errors()
        {
            var max = FakeTypes.ValueTypes.SimpleTypes.Int_Min;

            var result = Validator
                .AddRule(FakeTypes.ColecctionsTypes.List_Null, x => x.IsGreater(max))
                .AddRule(FakeTypes.ReferenceTypes.MatrizTypes.Array_Default, x => x.IsGreater(max, FIELD_NAME))
                .AddRule(FakeTypes.ColecctionsTypes.Stack_CountZero, x => x.IsGreaterWithMessage(max, MESSAGE))
                .AddRule(FakeTypes.ColecctionsTypes.Collection_Null, x => x.IsGreater(max).WithRuleFieldName(RULE_FIELD_NAME))
                .AddRule(FakeTypes.ColecctionsTypes.ArrayClass_CountZero, x => x.IsGreater(max, CUSTOM_FIELD_NAME).WithRuleFieldName(RULE_FIELD_NAME));

            Assert.IsFalse(result.IsValid);
         
            var exception = Assert.ThrowsException<ValidationsException>(() => result.Throw());

            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Contains(Messages.Greater(max)));
            Assert.IsTrue(exception.Contains(Messages.Greater(max, FIELD_NAME)));
            Assert.IsTrue(exception.Contains(MESSAGE));
            Assert.IsTrue(exception.Contains(Messages.Greater(max, RULE_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Greater(max, CUSTOM_FIELD_NAME)));
        }

        [TestMethod]
        public void IsLess_AddRules_Passing_InvalidArguments_Returns_Errors()
        {
            var min = FakeTypes.ValueTypes.SimpleTypes.Int_Max;

            var result = Validator
                .AddRule(FakeTypes.ColecctionsTypes.List_Null, x => x.IsLess(min))
                .AddRule(FakeTypes.ReferenceTypes.MatrizTypes.Array_Default, x => x.IsLess(min, FIELD_NAME))
                .AddRule(FakeTypes.ColecctionsTypes.Stack_CountZero, x => x.IsLessWithMessage(min, MESSAGE))
                .AddRule(FakeTypes.ColecctionsTypes.Collection_Null, x => x.IsLess(min).WithRuleFieldName(RULE_FIELD_NAME))
                .AddRule(FakeTypes.ColecctionsTypes.ArrayClass_CountZero, x => x.IsLess(min, CUSTOM_FIELD_NAME).WithRuleFieldName(RULE_FIELD_NAME));

            Assert.IsFalse(result.IsValid);

            var exception = Assert.ThrowsException<ValidationsException>(() => result.Throw());

            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Contains(Messages.Less(min)));
            Assert.IsTrue(exception.Contains(Messages.Less(min, FIELD_NAME)));
            Assert.IsTrue(exception.Contains(MESSAGE));
            Assert.IsTrue(exception.Contains(Messages.Less(min, RULE_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Less(min, CUSTOM_FIELD_NAME)));
        }

        [TestMethod]
        public void IsNullOrEmpty_AddRules_Passing_InvalidArguments_Returns_Errors()
        {
            var result = Validator
                .AddRule(FakeTypes.ColecctionsTypes.List_Null, x => x.IsNullOrEmpty())
                .AddRule(FakeTypes.ReferenceTypes.MatrizTypes.Array_Default, x => x.IsNullOrEmpty(FIELD_NAME))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Default, x => x.IsNullOrEmptyWithMessage(MESSAGE))
                .AddRule(FakeTypes.ColecctionsTypes.Collection_Null, x => x.IsNullOrEmpty().WithRuleFieldName(RULE_FIELD_NAME))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.Object_Null, x => x.IsNullOrEmpty(CUSTOM_FIELD_NAME).WithRuleFieldName(RULE_FIELD_NAME));

            Assert.IsFalse(result.IsValid);

            var exception = Assert.ThrowsException<ValidationsException>(() => result.Throw());

            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Contains(Messages.NullOrEmpty()));
            Assert.IsTrue(exception.Contains(Messages.NullOrEmpty(FIELD_NAME)));
            Assert.IsTrue(exception.Contains(MESSAGE));
            Assert.IsTrue(exception.Contains(Messages.NullOrEmpty(RULE_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.NullOrEmpty(CUSTOM_FIELD_NAME)));
        }

        [TestMethod]
        public void IsRange_AddRules_Passing_InvalidArguments_Returns_Errors()
        {
            var min = FakeTypes.ValueTypes.SimpleTypes.Int_Max;
            var max = FakeTypes.ValueTypes.SimpleTypes.Int_Min;

            var result = Validator
                .AddRule(FakeTypes.ValueTypes.SimpleTypes.Int_New(), x => x.IsRange(min, max))
                .AddRule(FakeTypes.ValueTypes.SimpleTypes.Char_New(), x => x.IsRange(min, max, FIELD_NAME))
                .AddRule(FakeTypes.ColecctionsTypes.Stack_CountZero, x => x.IsRangeWithMessage(min, max, MESSAGE))
                .AddRule(FakeTypes.ColecctionsTypes.Collection_Null, x => x.IsRange(min, max).WithRuleFieldName(RULE_FIELD_NAME))
                .AddRule(FakeTypes.ColecctionsTypes.ArrayClass_CountZero, x => x.IsRange(min, max, CUSTOM_FIELD_NAME).WithRuleFieldName(RULE_FIELD_NAME));

            Assert.IsFalse(result.IsValid);

            var exception = Assert.ThrowsException<ValidationsException>(() => result.Throw());

            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Contains(Messages.Range(min, max)));
            Assert.IsTrue(exception.Contains(Messages.Range(min, max, FIELD_NAME)));
            Assert.IsTrue(exception.Contains(MESSAGE));
            Assert.IsTrue(exception.Contains(Messages.Range(min, max, RULE_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Range(min, max, CUSTOM_FIELD_NAME)));
        }

        [TestMethod]
        public void IsRegex_AddRules_Passing_InvalidArguments_Returns_Errors()
        {
            var regex = Shared.Util.Regexp.ForEmail;

            var result = Validator
                .AddRule(FakeTypes.ColecctionsTypes.List_Null, x => x.IsRegex(regex))
                .AddRule(FakeTypes.ReferenceTypes.MatrizTypes.Array_Default, x => x.IsRegex(regex, FIELD_NAME))
                .AddRule(FakeTypes.ColecctionsTypes.Stack_CountZero, x => x.IsRegexWithMessage(regex, MESSAGE))
                .AddRule(FakeTypes.ColecctionsTypes.Collection_Null, x => x.IsRegex(regex).WithRuleFieldName(RULE_FIELD_NAME))
                .AddRule(FakeTypes.ColecctionsTypes.ArrayClass_CountZero, x => x.IsRegex(regex, CUSTOM_FIELD_NAME).WithRuleFieldName(RULE_FIELD_NAME));

            Assert.IsFalse(result.IsValid);

            var exception = Assert.ThrowsException<ValidationsException>(() => result.Throw());

            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Contains(Messages.Regex()));
            Assert.IsTrue(exception.Contains(Messages.Regex(FIELD_NAME)));
            Assert.IsTrue(exception.Contains(MESSAGE));
            Assert.IsTrue(exception.Contains(Messages.Regex(RULE_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Regex(CUSTOM_FIELD_NAME)));
        }

        [TestMethod]
        public void IsDirectoryAddRules_Passing_InvalidArguments_Returns_Errors()
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
            Assert.IsTrue(exception.Contains(Messages.Directory(value)));
            Assert.IsTrue(exception.Contains(Messages.Directory(value, FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Directory()));
            Assert.IsTrue(exception.Contains(Messages.Directory(fieldName: FIELD_NAME)));
            Assert.IsTrue(exception.Contains(MESSAGE));
            Assert.IsTrue(exception.Contains(Messages.Directory(fieldName: CUSTOM_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Directory(fieldName: RULE_FIELD_NAME)));
        }

        [TestMethod]
        public void Email_AddRules_Passing_InvalidArguments_Returns_Errors()
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
            Assert.IsTrue(exception.Contains(Messages.Email(value)));
            Assert.IsTrue(exception.Contains(Messages.Email(value, FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Email()));
            Assert.IsTrue(exception.Contains(Messages.Email(fieldName: FIELD_NAME)));
            Assert.IsTrue(exception.Contains(MESSAGE));
            Assert.IsTrue(exception.Contains(Messages.Email(fieldName: CUSTOM_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Email(fieldName: RULE_FIELD_NAME)));
        }

        [TestMethod]
        public void FileExtension_AddRules_Passing_InvalidArguments_Returns_Errors()
        {
            var value = FakeTypes.ReferenceTypes.ClassTypes.String_New();

            var result = Validator
                .AddRule(value, x => x.IsFileExtension())
                .AddRule(value, x => x.IsFileExtension(FIELD_NAME))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Default, x => x.IsFileExtension())
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Empty, x => x.IsFileExtension(FIELD_NAME))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Empty, x => x.IsFileExtensionWithMessage(MESSAGE))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Null, x => x.IsFileExtension(CUSTOM_FIELD_NAME).WithRuleFieldName(RULE_FIELD_NAME))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Null, x => x.IsFileExtension().WithRuleFieldName(RULE_FIELD_NAME));

            Assert.IsFalse(result.IsValid);

            var exception = Assert.ThrowsException<ValidationsException>(() => result.Throw());

            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Contains(Messages.FileExtension(value)));
            Assert.IsTrue(exception.Contains(Messages.FileExtension(value, FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.FileExtension()));
            Assert.IsTrue(exception.Contains(Messages.FileExtension(fieldName: FIELD_NAME)));
            Assert.IsTrue(exception.Contains(MESSAGE));
            Assert.IsTrue(exception.Contains(Messages.FileExtension(fieldName: CUSTOM_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.FileExtension(fieldName: RULE_FIELD_NAME)));
        }

        [TestMethod]
        public void File_AddRules_Passing_InvalidArguments_Returns_Errors()
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
            Assert.IsTrue(exception.Contains(Messages.File(value)));
            Assert.IsTrue(exception.Contains(Messages.File(value, FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.File()));
            Assert.IsTrue(exception.Contains(Messages.File(fieldName: FIELD_NAME)));
            Assert.IsTrue(exception.Contains(MESSAGE));
            Assert.IsTrue(exception.Contains(Messages.File(fieldName: CUSTOM_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.File(fieldName: RULE_FIELD_NAME)));
        }

        [TestMethod]
        public void Name_AddRules_Passing_InvalidArguments_Returns_Errors()
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
            Assert.IsTrue(exception.Contains(Messages.Name(value)));
            Assert.IsTrue(exception.Contains(Messages.Name(value, FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Name()));
            Assert.IsTrue(exception.Contains(Messages.Name(fieldName: FIELD_NAME)));
            Assert.IsTrue(exception.Contains(MESSAGE));
            Assert.IsTrue(exception.Contains(Messages.Name(fieldName: CUSTOM_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Name(fieldName: RULE_FIELD_NAME)));
        }

        [TestMethod]
        public void Subdomain_AddRules_Passing_InvalidArguments_Returns_Errors()
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
            var max = FakeTypes.ValueTypes.SimpleTypes.Int_Min;

            var result = Validator
                .AddRule(FakeTypes.ValueTypes.SimpleTypes.Int_Null, x => x
                    .IsNullOrEmpty()
                    .IsGreater(max))
                .AddRule(FakeTypes.ValueTypes.SimpleTypes.Decimal_Null, x => x
                    .IsNullOrEmpty()
                    .IsGreater(max)
                    .WithRuleFieldName(RULE_FIELD_NAME))
                .AddRule(FakeTypes.ValueTypes.SimpleTypes.Decimal_Null, x => x
                    .IsNullOrEmpty(FIELD_NAME)
                    .IsGreater(max, FIELD_NAME)
                    .WithRuleFieldName(RULE_FIELD_NAME))
                .AddRule(FakeTypes.ValueTypes.SimpleTypes.Int_Null, x => x
                    .IsNullOrEmptyWithMessage(MESSAGE)
                    .IsGreaterWithMessage(max, MESSAGE));

            Assert.IsFalse(result.IsValid);

            var exception = Assert.ThrowsException<ValidationsException>(() => result.Throw());

            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Contains(Messages.NullOrEmpty()));
            Assert.IsTrue(exception.Contains(Messages.Greater(max)));
            Assert.IsTrue(exception.Contains(Messages.NullOrEmpty(RULE_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Greater(max, RULE_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.NullOrEmpty(FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Greater(max, FIELD_NAME)));
            Assert.IsTrue(exception.Contains(MESSAGE));
        }

        [TestMethod]
        public void Example_RulesStopFirstError()
        {
            var min = FakeTypes.ValueTypes.SimpleTypes.Int_Max;
            var max = FakeTypes.ValueTypes.SimpleTypes.Int_Min;

            var result = Validator
                .AddRule(FakeTypes.ValueTypes.SimpleTypes.Int_Null, x => x
                    .IsNullOrEmpty()
                    .IsGreater(max))
                .AddRule(FakeTypes.ValueTypes.SimpleTypes.Decimal_Null, x => x
                    .IsNullOrEmpty()
                    .IsGreater(max)
                    .WithRuleFieldName(RULE_FIELD_NAME))
                .AddRule(FakeTypes.ValueTypes.SimpleTypes.Decimal_Null, x => x
                    .IsNullOrEmpty(FIELD_NAME)
                    .IsGreater(max, FIELD_NAME)
                    .WithRuleFieldName(RULE_FIELD_NAME))
                .AddRule(FakeTypes.ValueTypes.SimpleTypes.Int_Null, x => x
                    .IsNullOrEmptyWithMessage(MESSAGE)
                    .IsGreaterWithMessage(max, MESSAGE))
                .StopFirstError();

            Assert.IsFalse(result.IsValid);

            var exception = Assert.ThrowsException<ValidationsException>(() => result.Throw());

            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Contains(Messages.NullOrEmpty()));
        }

        [TestMethod]
        public void Example_Rules1()
        {
            var min = FakeTypes.ValueTypes.SimpleTypes.Int_Max;

            var result = Validator
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Default, x => x
                    .IsDirectory()
                    .IsEmail()
                    .IsFileExtension()
                    .IsFile()
                    .IsName()
                    .IsSubdomain())
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Null, x => x
                    .IsDirectory()
                    .IsEmail()
                    .IsFileExtension()
                    .IsFile()
                    .IsName()
                    .IsSubdomain()
                    .WithRuleFieldName(RULE_FIELD_NAME))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_New(), x => x
                    .IsDirectory(FIELD_NAME)
                    .IsEmail(FIELD_NAME)
                    .IsFileExtension(FIELD_NAME)
                    .IsFile(FIELD_NAME)
                    .IsName(FIELD_NAME)
                    .IsSubdomain(FIELD_NAME)
                    .WithRuleFieldName(RULE_FIELD_NAME))
                .AddRule(FakeTypes.ReferenceTypes.ClassTypes.String_Empty, x => x
                    .IsDirectoryWithMessage(MESSAGE)
                    .IsEmailWithMessage(MESSAGE)
                    .IsFileExtensionWithMessage(MESSAGE)
                    .IsFileWithMessage(MESSAGE)
                    .IsNameWithMessage(MESSAGE)
                    .IsSubdomainWithMessage(MESSAGE));

            Assert.IsFalse(result.IsValid);

            var exception = Assert.ThrowsException<ValidationsException>(() => result.Throw());

            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Contains(Messages.Directory(FakeTypes.ReferenceTypes.ClassTypes.String_Default)));
            Assert.IsTrue(exception.Contains(Messages.Email(FakeTypes.ReferenceTypes.ClassTypes.String_Default)));
            Assert.IsTrue(exception.Contains(Messages.FileExtension(FakeTypes.ReferenceTypes.ClassTypes.String_Default)));
            Assert.IsTrue(exception.Contains(Messages.File(FakeTypes.ReferenceTypes.ClassTypes.String_Default)));
            Assert.IsTrue(exception.Contains(Messages.Name(FakeTypes.ReferenceTypes.ClassTypes.String_Default)));
            Assert.IsTrue(exception.Contains(Messages.Subdomain(FakeTypes.ReferenceTypes.ClassTypes.String_Default)));

            Assert.IsTrue(exception.Contains(Messages.Directory(FakeTypes.ReferenceTypes.ClassTypes.String_Null, RULE_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Email(FakeTypes.ReferenceTypes.ClassTypes.String_Null, RULE_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.FileExtension(FakeTypes.ReferenceTypes.ClassTypes.String_Null, RULE_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.File(FakeTypes.ReferenceTypes.ClassTypes.String_Null, RULE_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Name(FakeTypes.ReferenceTypes.ClassTypes.String_Null, RULE_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Subdomain(FakeTypes.ReferenceTypes.ClassTypes.String_Null, RULE_FIELD_NAME)));

            Assert.IsTrue(exception.Contains(Messages.Directory(FakeTypes.ReferenceTypes.ClassTypes.String_New(), FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Email(FakeTypes.ReferenceTypes.ClassTypes.String_New(), FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.FileExtension(FakeTypes.ReferenceTypes.ClassTypes.String_New(), FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.File(FakeTypes.ReferenceTypes.ClassTypes.String_New(), FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Name(FakeTypes.ReferenceTypes.ClassTypes.String_New(), FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Subdomain(FakeTypes.ReferenceTypes.ClassTypes.String_New(), FIELD_NAME)));

            Assert.IsTrue(exception.Contains(MESSAGE));
        }
    }
}
