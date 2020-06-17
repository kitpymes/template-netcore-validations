using Kitpymes.Core.Validations.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kitpymes.Core.Validations.Tests
{
    [TestClass]
    public class CheckSharedTests
    {
        [TestMethod]
        public void IsAny_PassingInvalidArgumentsReturnHasErrorsAndCount()
        {
            var (HasErrors, Count) = Check.IsAny
            (
                // Se pueden enviar valores de tipo String a un parámetro de tipo IEnumerable
                FakeTypes.ReferenceTypes.ClassTypes.String_Empty,
                FakeTypes.ReferenceTypes.ClassTypes.String_Null,
                FakeTypes.ReferenceTypes.ClassTypes.String_Default,

                FakeTypes.ReferenceTypes.MatrizTypes.Array_Null,
                FakeTypes.ReferenceTypes.MatrizTypes.Array_Default,
                FakeTypes.ReferenceTypes.MatrizTypes.Array_CountZero,

                FakeTypes.ColecctionsTypes.Enumerable_Null,
                FakeTypes.ColecctionsTypes.Enumerable_Default,
                FakeTypes.ColecctionsTypes.Enumerable_CountZero,

                FakeTypes.ColecctionsTypes.List_Null,
                FakeTypes.ColecctionsTypes.List_Default,
                FakeTypes.ColecctionsTypes.List_CountZero,

                FakeTypes.ColecctionsTypes.HashSet_Null,
                FakeTypes.ColecctionsTypes.HashSet_Default,
                FakeTypes.ColecctionsTypes.HashSet_CountZero,

                FakeTypes.ColecctionsTypes.Collection_Null,
                FakeTypes.ColecctionsTypes.Collection_Default,
                FakeTypes.ColecctionsTypes.Collection_CountZero,

                FakeTypes.ColecctionsTypes.Queue_Null,
                FakeTypes.ColecctionsTypes.Queue_Default,
                FakeTypes.ColecctionsTypes.Queue_CountZero,

                FakeTypes.ColecctionsTypes.SortedSet_Null,
                FakeTypes.ColecctionsTypes.SortedSet_Default,
                FakeTypes.ColecctionsTypes.SortedSet_CountZero,

                FakeTypes.ColecctionsTypes.Stack_Null,
                FakeTypes.ColecctionsTypes.Stack_Default,
                FakeTypes.ColecctionsTypes.Stack_CountZero,

                FakeTypes.ColecctionsTypes.ArrayClass_Null,
                FakeTypes.ColecctionsTypes.ArrayClass_Default,
                FakeTypes.ColecctionsTypes.ArrayClass_CountZero,

                FakeTypes.ColecctionsTypes.ArrayList_Null,
                FakeTypes.ColecctionsTypes.ArrayList_Default,
                FakeTypes.ColecctionsTypes.ArrayList_CountZero
            );

            Assert.IsTrue(HasErrors);
            Assert.IsTrue(Count == 33);
        }

        [TestMethod]
        public void IsEqual_PassingInvalidArgumentsReturnHasErrorsAndCount()
        {
            var (HasErrors, Count) = Check.IsEqual
            (
                value: FakeTypes.ReferenceTypes.ClassTypes.String_New(),
                
                FakeTypes.ReferenceTypes.ClassTypes.String_Empty,
                FakeTypes.ReferenceTypes.ClassTypes.String_Default,
                FakeTypes.ReferenceTypes.ClassTypes.String_Null,
                FakeTypes.ReferenceTypes.ClassTypes.String_New(value: '-')
            );

            Assert.IsTrue(HasErrors);
            Assert.IsTrue(Count == 4);
        }

        [TestMethod]
        public void IsMax_PassingInvalidArgumentsReturnHasErrorsAndCount()
        {
            var max = FakeTypes.ValueTypes.SimpleTypes.Int_Min;

            var (HasErrors, Count) = Check.IsMax
            (
                max,
                FakeTypes.ValueTypes.SimpleTypes.Int_New(), 
                FakeTypes.ValueTypes.EnumerationTypes.ExampleEnumeration.a,
                FakeTypes.ValueTypes.StructureTypes.Guid_New(), 
                FakeTypes.ReferenceTypes.ClassTypes.String_New(),
                FakeTypes.ReferenceTypes.MatrizTypes.Array_New(), 
                FakeTypes.ColecctionsTypes.List_New(), 
                FakeTypes.ColecctionsTypes.Stack_New()
            );

            Assert.IsTrue(HasErrors);
            Assert.IsTrue(Count == 7);
        }

        [TestMethod]
        public void IsMin_PassingInvalidArgumentsReturnHasErrorsAndCount()
        {
            var min = FakeTypes.ValueTypes.SimpleTypes.Int_Max;

            var (HasErrors, Count) = Check.IsMin
            (
                min,
                FakeTypes.ValueTypes.SimpleTypes.Int_New(),
                FakeTypes.ValueTypes.EnumerationTypes.ExampleEnumeration.a,
                FakeTypes.ValueTypes.StructureTypes.Guid_New(),
                FakeTypes.ReferenceTypes.ClassTypes.String_New(),
                FakeTypes.ReferenceTypes.MatrizTypes.Array_New(),
                FakeTypes.ColecctionsTypes.List_New(),
                FakeTypes.ColecctionsTypes.Stack_New()
            );

            Assert.IsTrue(HasErrors);
            Assert.IsTrue(Count == 7);
        }

        [TestMethod]
        public void IsNullOrEmpty_PassingInvalidArgumentsReturnHasErrorsAndCount()
        {
            var (HasErrors, Count) = Check.IsNullOrEmpty
            (
                FakeTypes.ValueTypes.SimpleTypes.SByte_Null,
                FakeTypes.ValueTypes.SimpleTypes.SByte_Default,
                FakeTypes.ValueTypes.SimpleTypes.Short_Null,
                FakeTypes.ValueTypes.SimpleTypes.Short_Default,
                FakeTypes.ValueTypes.SimpleTypes.Int_Null,
                FakeTypes.ValueTypes.SimpleTypes.Int_Default,
                FakeTypes.ValueTypes.SimpleTypes.Long_Null,
                FakeTypes.ValueTypes.SimpleTypes.Long_Default,
                FakeTypes.ValueTypes.SimpleTypes.Byte_Null,
                FakeTypes.ValueTypes.SimpleTypes.Byte_Default,
                FakeTypes.ValueTypes.SimpleTypes.UShort_Null,
                FakeTypes.ValueTypes.SimpleTypes.UShort_Default,
                FakeTypes.ValueTypes.SimpleTypes.UInt_Null,
                FakeTypes.ValueTypes.SimpleTypes.UInt_Default,
                FakeTypes.ValueTypes.SimpleTypes.ULong_Null,
                FakeTypes.ValueTypes.SimpleTypes.ULong_Default,
                FakeTypes.ValueTypes.SimpleTypes.Char_Null,
                FakeTypes.ValueTypes.SimpleTypes.Char_Default,
                FakeTypes.ValueTypes.SimpleTypes.Float_Null,
                FakeTypes.ValueTypes.SimpleTypes.Float_Default,
                FakeTypes.ValueTypes.SimpleTypes.Double_Null,
                FakeTypes.ValueTypes.SimpleTypes.Double_Default,
                FakeTypes.ValueTypes.SimpleTypes.Decimal_Null,
                FakeTypes.ValueTypes.SimpleTypes.Decimal_Default,
                FakeTypes.ValueTypes.SimpleTypes.Bool_Null,
                FakeTypes.ValueTypes.EnumerationTypes.ExampleEnumeration.a,
                new FakeTypes.ValueTypes.StructureTypes.ExampleStructure(),
                FakeTypes.ValueTypes.StructureTypes.Guid_Empty,
                FakeTypes.ValueTypes.StructureTypes.Guid_Null,

                FakeTypes.ReferenceTypes.ClassTypes.String_Empty,
                FakeTypes.ReferenceTypes.ClassTypes.String_Null,
                FakeTypes.ReferenceTypes.ClassTypes.String_Default,
                FakeTypes.ReferenceTypes.ClassTypes.Class_Null,
                FakeTypes.ReferenceTypes.ClassTypes.Class_Default,
                FakeTypes.ReferenceTypes.ClassTypes.Object_Null,
                FakeTypes.ReferenceTypes.ClassTypes.Object_Default,
                FakeTypes.ReferenceTypes.MatrizTypes.Array_Null,
                FakeTypes.ReferenceTypes.MatrizTypes.Array_Default,

                FakeTypes.ColecctionsTypes.Enumerable_Null,
                FakeTypes.ColecctionsTypes.Enumerable_Default,
                FakeTypes.ColecctionsTypes.List_Null,
                FakeTypes.ColecctionsTypes.List_Default,
                FakeTypes.ColecctionsTypes.HashSet_Null,
                FakeTypes.ColecctionsTypes.HashSet_Default,
                FakeTypes.ColecctionsTypes.Collection_Null,
                FakeTypes.ColecctionsTypes.Collection_Default,
                FakeTypes.ColecctionsTypes.Queue_Null,
                FakeTypes.ColecctionsTypes.Queue_Default,
                FakeTypes.ColecctionsTypes.SortedSet_Null,
                FakeTypes.ColecctionsTypes.SortedSet_Default,
                FakeTypes.ColecctionsTypes.Stack_Null,
                FakeTypes.ColecctionsTypes.Stack_Default,
                FakeTypes.ColecctionsTypes.ArrayClass_Null,
                FakeTypes.ColecctionsTypes.ArrayClass_Default,
                FakeTypes.ColecctionsTypes.ArrayList_Null,
                FakeTypes.ColecctionsTypes.ArrayList_Default
            );

            Assert.IsTrue(HasErrors);
            Assert.IsTrue(Count == 56);
        }

        [TestMethod]
        public void IsRange_PassingInvalidArgumentsReturnErrorsAndCount()
        {
            var min = FakeTypes.ValueTypes.SimpleTypes.Int_Max;
            var max = FakeTypes.ValueTypes.SimpleTypes.Int_Min;

            var (HasErrors, Count) = Check.IsRange
            (
               min, max,
               FakeTypes.ValueTypes.SimpleTypes.Int_New(), 
               FakeTypes.ValueTypes.EnumerationTypes.ExampleEnumeration.a, 
               FakeTypes.ValueTypes.StructureTypes.Guid_New(), 
               FakeTypes.ReferenceTypes.ClassTypes.String_New(), 
               FakeTypes.ReferenceTypes.MatrizTypes.Array_New(), 
               FakeTypes.ColecctionsTypes.List_New(), 
               FakeTypes.ColecctionsTypes.Stack_New()
           );

            Assert.IsTrue(HasErrors);
            Assert.IsTrue(Count == 7);
        }

        [TestMethod]
        public void IsRegex_PassingInvalidArgumentsReturnHasErrorsAndCount()
        {
            var (HasErrors, Count) = Check.IsRegex
            (
                Regexp.ForEmail,
                FakeTypes.ReferenceTypes.ClassTypes.String_New(), 
                FakeTypes.ReferenceTypes.ClassTypes.String_New('-')
            );

            Assert.IsTrue(HasErrors);
            Assert.IsTrue(Count == 2);
        }
    }
}
