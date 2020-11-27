using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using EksamensOpgave.Util;


namespace ValidationsTests
{
    [TestClass]
    public class ValidationsTest
    {
        [TestMethod]
        public void ValidationsUsernameIsNull()
        {
            Validations _validations = ValidationsInit();

            string s = null;

            bool result = _validations.ValidateName(s);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidationsUsernameIsEmpty()
        {
            Validations _validations = ValidationsInit();

            string s = "";

            bool result = _validations.ValidateUserName(s);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidationsUsernameContainAllowChars()
        {
            Validations _validations = ValidationsInit();

            string s = "12ascsa_";

            bool result = _validations.ValidateUserName(s);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidationsUsernameContainCapitalLetters()
        {
            Validations _validations = ValidationsInit();

            string s = "12asAcsa_";

            bool result = _validations.ValidateUserName(s);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidationsUsernameContainSpace()
        {
            Validations _validations = ValidationsInit();

            string s = "12as csa_";

            bool result = _validations.ValidateUserName(s);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidationsUsernameContainSigns()
        {
            Validations _validations = ValidationsInit();

            string s = "12as,csa_";

            bool result = _validations.ValidateUserName(s);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidationsNameIsNull()
        {
            Validations _validations = ValidationsInit();

            string s = null;

            bool result = _validations.ValidateName(s);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidationsNameIsEmpty()
        {
            Validations _validations = ValidationsInit();

            string s = "";

            bool result = _validations.ValidateName(s);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidationsNameIsNotNullOrEmpty()
        {
            Validations _validations = ValidationsInit();

            string s = "asd";

            bool result = _validations.ValidateName(s);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidationsEmailIsValid()
        {
            Validations _validations = ValidationsInit();

            string s = "a_-.sdASAK.-_V1_2.31-2@asd1-.asd8a.dk";

            bool result = _validations.ValidateEmail(s);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidationsEmailIsDomainEndsWithSigns()
        {
            Validations _validations = ValidationsInit();

            string s = "asdASAKV_-12312@asd1_.asd8a.dk_";

            bool result = _validations.ValidateEmail(s);

            Assert.IsFalse(result);
        }



        Validations ValidationsInit()
        {
            return new Validations(
                new Regex(@"^[a-z\d_]+$"),
                new Regex(@"^[\w\.\-]+@[a-zA-Z0-9][a-zA-Z0-9\.\-]+\.[a-zA-Z0-9]+$")
                );
        }
    }
}
