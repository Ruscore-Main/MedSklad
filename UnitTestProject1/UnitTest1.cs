using MedStorage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(ValidateTest.ValidateRegistration("lg", "1234", "Name", "Surname"), "Длина логина должна быть больше 3х символов");
        }

        [TestMethod]
        public void TestMethod2()
        {
            Assert.AreEqual(ValidateTest.ValidateRegistration("login", "123", "Name", "Surname"), "Длина пароля должна быть больше 4х символов");
        }

        [TestMethod]
        public void TestMethod3()
        {
            Assert.AreEqual(ValidateTest.ValidateRegistration("login", "12345", "N", "Surname"), "Имя должна быть больше 1-го символов");
        }

        [TestMethod]
        public void TestMethod4()
        {
            Assert.AreEqual(ValidateTest.ValidateRegistration("login", "12345", "Name", "Sur"), "Фамилия должна быть больше 4х символов");

        }

        [TestMethod]
        public void TestMethod5()
        {
            Assert.AreEqual(ValidateTest.ValidateRegistration("login", "12345", "Name", "Surname"), "Успешно");

        }
    }
}
