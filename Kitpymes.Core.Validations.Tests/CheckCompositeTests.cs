using Kitpymes.Core.Validations.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kitpymes.Core.Validations.Tests
{
    [TestClass]
    public class CheckCompositeTests
    {
        [TestMethod]
        public void IsDirectory_PassingInvalidArgumentsReturnHasErrorsAndCount()
        {
            var (HasErrors, Count) = Check.IsDirectory
            (
                FakeTypes.ReferenceTypes.ClassTypes.String_New(),
                FakeTypes.ReferenceTypes.ClassTypes.String_Empty,
                FakeTypes.ReferenceTypes.ClassTypes.String_Null,
                FakeTypes.ReferenceTypes.ClassTypes.String_Default
            );

            Assert.IsTrue(HasErrors);
            Assert.IsTrue(Count == 4);
        }

        [TestMethod]
        public void IsEmail_PassingInvalidArgumentsReturnHasErrorsAndCount()
        {
            var (HasErrors, Count) = Check.IsEmail
            (
                FakeTypes.ReferenceTypes.ClassTypes.String_New(),
                FakeTypes.ReferenceTypes.ClassTypes.String_Empty,
                FakeTypes.ReferenceTypes.ClassTypes.String_Null,
                FakeTypes.ReferenceTypes.ClassTypes.String_Default
            );

            Assert.IsTrue(HasErrors);
            Assert.IsTrue(Count == 4);
        }

        [TestMethod]
        public void IsExtension_PassingInvalidArgumentsReturnHasErrorsAndCount()
        {
            var (HasErrors, Count) = Check.IsDirectory
            (
                FakeTypes.ReferenceTypes.ClassTypes.String_New(),
                FakeTypes.ReferenceTypes.ClassTypes.String_Empty,
                FakeTypes.ReferenceTypes.ClassTypes.String_Null,
                FakeTypes.ReferenceTypes.ClassTypes.String_Default
            );

            Assert.IsTrue(HasErrors);
            Assert.IsTrue(Count == 4);
        }

        [TestMethod]
        public void IsFile_PassingInvalidArgumentsReturnHasErrorsAndCount()
        {
            var (HasErrors, Count) = Check.IsFile
            (
                FakeTypes.ReferenceTypes.ClassTypes.String_New(),
                FakeTypes.ReferenceTypes.ClassTypes.String_Empty,
                FakeTypes.ReferenceTypes.ClassTypes.String_Null,
                FakeTypes.ReferenceTypes.ClassTypes.String_Default
            );

            Assert.IsTrue(HasErrors);
            Assert.IsTrue(Count == 4);
        }

        [TestMethod]
        public void IsName_PassingInvalidArgumentsReturnHasErrorsAndCount()
        {
            var (HasErrors, Count) = Check.IsName
            (
                FakeTypes.ReferenceTypes.ClassTypes.String_New(),
                FakeTypes.ReferenceTypes.ClassTypes.String_Empty,
                FakeTypes.ReferenceTypes.ClassTypes.String_Null,
                FakeTypes.ReferenceTypes.ClassTypes.String_Default
            );

            Assert.IsTrue(HasErrors);
            Assert.IsTrue(Count == 4);
        }

        [TestMethod]
        public void IsPassword_PassingInvalidArgumentsReturnHasErrorsAndCount()
        {
            var min = FakeTypes.ValueTypes.SimpleTypes.Int_Max;

            var (HasErrors, Count) = Check.IsPassword
            (
                min,
                FakeTypes.ReferenceTypes.ClassTypes.String_New(),
                FakeTypes.ReferenceTypes.ClassTypes.String_Empty,
                FakeTypes.ReferenceTypes.ClassTypes.String_Null,
                FakeTypes.ReferenceTypes.ClassTypes.String_Default
            );

            Assert.IsTrue(HasErrors);
            Assert.IsTrue(Count == 4);
        }

        [TestMethod]
        public void IsSubdomain_PassingInvalidArgumentsReturnHasErrorsAndCount()
        {
            var (HasErrors, Count) = Check.IsSubdomain
            (
                FakeTypes.ReferenceTypes.ClassTypes.String_New(),
                FakeTypes.ReferenceTypes.ClassTypes.String_Empty,
                FakeTypes.ReferenceTypes.ClassTypes.String_Null,
                FakeTypes.ReferenceTypes.ClassTypes.String_Default
            );

            Assert.IsTrue(HasErrors);
            Assert.IsTrue(Count == 4);
        }
    }
}
