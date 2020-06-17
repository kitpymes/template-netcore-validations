using Kitpymes.Core.Validations.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kitpymes.Core.Validations.Tests
{
    [TestClass]
    public class ValidatorSharedTests 
    {
        public const string MESSAGE = "Esto es un mensaje";
        public const string FIELD_NAME = "FIELD_NAME";
        public const string COMPARE_FIELD_NAME = "COMPARE_FIELD_NAME";
        public const string RULE_FIELD_NAME = "RULE_FIELD_NAME";
        public const string CUSTOM_FIELD_NAME = "CUSTOM_FIELD_NAME";
        public const long MAX = long.MinValue;
        public const long MIN = long.MaxValue;

        [TestMethod]
        public void IsAny_AddRulesPassingInvalidArgumentsReturnErrors()
        {
            var result = Validator
                .AddRule(FakeTypes.ColecctionsTypes.List_Null, x => x.IsAny())
                .AddRule(FakeTypes.ReferenceTypes.MatrizTypes.Array_Default, x => x.IsAny(FIELD_NAME))
                .AddRule(FakeTypes.ColecctionsTypes.Stack_CountZero, x => x.IsAnyWithMessage(MESSAGE))
                .AddRule(FakeTypes.ColecctionsTypes.Collection_Null, x => x.IsAny().WithRuleFieldName(RULE_FIELD_NAME))
                .AddRule(FakeTypes.ColecctionsTypes.ArrayClass_CountZero, x => x.IsAny(CUSTOM_FIELD_NAME).WithRuleFieldName(RULE_FIELD_NAME));

            Assert.IsFalse(result.IsValid);

            var exception = Assert.ThrowsException<ValidationsException>(() => result.Throw());

            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Count == 5);
            Assert.IsTrue(exception.Contains(Messages.Any()));
            Assert.IsTrue(exception.Contains(Messages.Any(FIELD_NAME)));
            Assert.IsTrue(exception.Contains(MESSAGE));
            Assert.IsTrue(exception.Contains(Messages.Any(RULE_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Any(CUSTOM_FIELD_NAME)));
        }

        [TestMethod]
        public void IsCustom_AddRulesPassingInvalidArgumentsReturnErrors()
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

            Assert.IsTrue(exception.Count == 2);
            Assert.IsTrue(exception.Contains(MESSAGE));
            Assert.IsTrue(exception.Contains(MESSAGE + "PEPE"));
        }

        [TestMethod]
        public void IsEqual_PassingInvalidArgumentsReturnErrors()
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
            Assert.IsTrue(exception.Count == 5);
            Assert.IsTrue(exception.Contains(Messages.Equal()));
            Assert.IsTrue(exception.Contains(Messages.EqualWithFieldsName(FIELD_NAME, FIELD_NAME_COMPARE)));
            Assert.IsTrue(exception.Contains(MESSAGE));
            Assert.IsTrue(exception.Contains(Messages.Equal(RULE_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.EqualWithFieldsName(CUSTOM_FIELD_NAME, FIELD_NAME_COMPARE)));
        }

        [TestMethod]
        public void IsMax_AddRulesPassingInvalidArgumentsReturnErrors()
        {
            var max = FakeTypes.ValueTypes.SimpleTypes.Int_Min;

            var result = Validator
                .AddRule(FakeTypes.ColecctionsTypes.List_Null, x => x.IsMax(max))
                .AddRule(FakeTypes.ReferenceTypes.MatrizTypes.Array_Default, x => x.IsMax(max, FIELD_NAME))
                .AddRule(FakeTypes.ColecctionsTypes.Stack_CountZero, x => x.IsMaxWithMessage(max, MESSAGE))
                .AddRule(FakeTypes.ColecctionsTypes.Collection_Null, x => x.IsMax(max).WithRuleFieldName(RULE_FIELD_NAME))
                .AddRule(FakeTypes.ColecctionsTypes.ArrayClass_CountZero, x => x.IsMax(max, CUSTOM_FIELD_NAME).WithRuleFieldName(RULE_FIELD_NAME));

            Assert.IsFalse(result.IsValid);
         
            var exception = Assert.ThrowsException<ValidationsException>(() => result.Throw());

            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Count == 5);
            Assert.IsTrue(exception.Contains(Messages.Max(max)));
            Assert.IsTrue(exception.Contains(Messages.Max(max, FIELD_NAME)));
            Assert.IsTrue(exception.Contains(MESSAGE));
            Assert.IsTrue(exception.Contains(Messages.Max(max, RULE_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Max(max, CUSTOM_FIELD_NAME)));
        }

        [TestMethod]
        public void IsMin_PassingInvalidArgumentsReturnErrors()
        {
            var min = FakeTypes.ValueTypes.SimpleTypes.Int_Max;

            var result = Validator
                .AddRule(FakeTypes.ColecctionsTypes.List_Null, x => x.IsMin(min))
                .AddRule(FakeTypes.ReferenceTypes.MatrizTypes.Array_Default, x => x.IsMin(min, FIELD_NAME))
                .AddRule(FakeTypes.ColecctionsTypes.Stack_CountZero, x => x.IsMinWithMessage(min, MESSAGE))
                .AddRule(FakeTypes.ColecctionsTypes.Collection_Null, x => x.IsMin(min).WithRuleFieldName(RULE_FIELD_NAME))
                .AddRule(FakeTypes.ColecctionsTypes.ArrayClass_CountZero, x => x.IsMin(min, CUSTOM_FIELD_NAME).WithRuleFieldName(RULE_FIELD_NAME));

            Assert.IsFalse(result.IsValid);

            var exception = Assert.ThrowsException<ValidationsException>(() => result.Throw());

            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Count == 5);
            Assert.IsTrue(exception.Contains(Messages.Min(min)));
            Assert.IsTrue(exception.Contains(Messages.Min(min, FIELD_NAME)));
            Assert.IsTrue(exception.Contains(MESSAGE));
            Assert.IsTrue(exception.Contains(Messages.Min(min, RULE_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Min(min, CUSTOM_FIELD_NAME)));
        }

        [TestMethod]
        public void IsNullOrEmpty_PassingInvalidArgumentsReturnErrors()
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
            Assert.IsTrue(exception.Count == 5);
            Assert.IsTrue(exception.Contains(Messages.NullOrEmpty()));
            Assert.IsTrue(exception.Contains(Messages.NullOrEmpty(FIELD_NAME)));
            Assert.IsTrue(exception.Contains(MESSAGE));
            Assert.IsTrue(exception.Contains(Messages.NullOrEmpty(RULE_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.NullOrEmpty(CUSTOM_FIELD_NAME)));
        }

        [TestMethod]
        public void IsRange_PassingInvalidArgumentsReturnErrors()
        {
            var min = FakeTypes.ValueTypes.SimpleTypes.Int_Max;
            var max = FakeTypes.ValueTypes.SimpleTypes.Int_Min;

            var result = Validator
                .AddRule(FakeTypes.ValueTypes.SimpleTypes.Int_New(), x => x.IsRange(min, max))
                .AddRule(FakeTypes.ValueTypes.StructureTypes.Guid_New(), x => x.IsRange(min, max, FIELD_NAME))
                .AddRule(FakeTypes.ColecctionsTypes.Stack_CountZero, x => x.IsRangeWithMessage(min, max, MESSAGE))
                .AddRule(FakeTypes.ColecctionsTypes.Collection_Null, x => x.IsRange(min, max).WithRuleFieldName(RULE_FIELD_NAME))
                .AddRule(FakeTypes.ColecctionsTypes.ArrayClass_CountZero, x => x.IsRange(min, max, CUSTOM_FIELD_NAME).WithRuleFieldName(RULE_FIELD_NAME));

            Assert.IsFalse(result.IsValid);

            var exception = Assert.ThrowsException<ValidationsException>(() => result.Throw());

            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Count == 5);
            Assert.IsTrue(exception.Contains(Messages.Range(min, max)));
            Assert.IsTrue(exception.Contains(Messages.Range(min, max, FIELD_NAME)));
            Assert.IsTrue(exception.Contains(MESSAGE));
            Assert.IsTrue(exception.Contains(Messages.Range(min, max, RULE_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Range(min, max, CUSTOM_FIELD_NAME)));
        }

        [TestMethod]
        public void IsRegex_PassingInvalidArgumentsReturnErrors()
        {
            var regex = Regexp.ForEmail;

            var result = Validator
                .AddRule(FakeTypes.ColecctionsTypes.List_Null, x => x.IsRegex(regex))
                .AddRule(FakeTypes.ReferenceTypes.MatrizTypes.Array_Default, x => x.IsRegex(regex, FIELD_NAME))
                .AddRule(FakeTypes.ColecctionsTypes.Stack_CountZero, x => x.IsRegexWithMessage(regex, MESSAGE))
                .AddRule(FakeTypes.ColecctionsTypes.Collection_Null, x => x.IsRegex(regex).WithRuleFieldName(RULE_FIELD_NAME))
                .AddRule(FakeTypes.ColecctionsTypes.ArrayClass_CountZero, x => x.IsRegex(regex, CUSTOM_FIELD_NAME).WithRuleFieldName(RULE_FIELD_NAME));

            Assert.IsFalse(result.IsValid);

            var exception = Assert.ThrowsException<ValidationsException>(() => result.Throw());

            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Count == 5);
            Assert.IsTrue(exception.Contains(Messages.Regex()));
            Assert.IsTrue(exception.Contains(Messages.Regex(FIELD_NAME)));
            Assert.IsTrue(exception.Contains(MESSAGE));
            Assert.IsTrue(exception.Contains(Messages.Regex(RULE_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Regex(CUSTOM_FIELD_NAME)));
        }

        [TestMethod]
        public void Example_Rules()
        {
            var min = FakeTypes.ValueTypes.SimpleTypes.Int_Max;
            var max = FakeTypes.ValueTypes.SimpleTypes.Int_Min;

            var result = Validator
                .AddRule(FakeTypes.ValueTypes.SimpleTypes.Int_Null, x => x
                    .IsNullOrEmpty()
                    .IsMax(max))
                .AddRule(FakeTypes.ValueTypes.SimpleTypes.Decimal_Null, x => x
                    .IsNullOrEmpty()
                    .IsMax(max)
                    .WithRuleFieldName(RULE_FIELD_NAME))
                .AddRule(FakeTypes.ValueTypes.SimpleTypes.Decimal_Null, x => x
                    .IsNullOrEmpty(FIELD_NAME)
                    .IsMax(max, FIELD_NAME)
                    .WithRuleFieldName(RULE_FIELD_NAME))
                .AddRule(FakeTypes.ValueTypes.SimpleTypes.Int_Null, x => x
                    .IsNullOrEmptyWithMessage(MESSAGE)
                    .IsMaxWithMessage(max, MESSAGE));

            Assert.IsFalse(result.IsValid);

            var exception = Assert.ThrowsException<ValidationsException>(() => result.Throw());

            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Count == 8);
            Assert.IsTrue(exception.Contains(Messages.NullOrEmpty()));
            Assert.IsTrue(exception.Contains(Messages.Max(max)));
            Assert.IsTrue(exception.Contains(Messages.NullOrEmpty(RULE_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Max(max, RULE_FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.NullOrEmpty(FIELD_NAME)));
            Assert.IsTrue(exception.Contains(Messages.Max(max, FIELD_NAME)));
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
                    .IsMax(max))
                .AddRule(FakeTypes.ValueTypes.SimpleTypes.Decimal_Null, x => x
                    .IsNullOrEmpty()
                    .IsMax(max)
                    .WithRuleFieldName(RULE_FIELD_NAME))
                .AddRule(FakeTypes.ValueTypes.SimpleTypes.Decimal_Null, x => x
                    .IsNullOrEmpty(FIELD_NAME)
                    .IsMax(max, FIELD_NAME)
                    .WithRuleFieldName(RULE_FIELD_NAME))
                .AddRule(FakeTypes.ValueTypes.SimpleTypes.Int_Null, x => x
                    .IsNullOrEmptyWithMessage(MESSAGE)
                    .IsMaxWithMessage(max, MESSAGE))
                .StopFirstError();

            Assert.IsFalse(result.IsValid);

            var exception = Assert.ThrowsException<ValidationsException>(() => result.Throw());

            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Count == 1);
            Assert.IsTrue(exception.Contains(Messages.NullOrEmpty()));
        }
    }
}
