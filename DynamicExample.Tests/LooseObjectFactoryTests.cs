using System;
using ConsoleApplication1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicExampleTests
{
    [TestClass]
    public class LooseObjectFactoryTests
    {
        private LooseObjectFactory factory;
        private readonly dynamic expectedAnonymousStructure = new
        {
            name = "blar",
            child = new
            {
                name = "blar"
            }
        };

        private readonly dynamic expectedJsonStructure = new
        {
            balance = "$1,893.00",
            picture = "http://placehold.it/32x32",
            age = 31,
            name = "Moon Ruiz",
            gender = "male",
        };

        [TestInitialize()]
        public void Initialize()
        {
            factory = new LooseObjectFactory();
        }


        [TestMethod]
        public void Test_CanRead_Properties_Of_AnonymousType_JsonResult()
        {
            dynamic resultToTest = factory.GetAnonymousTypeJsonResult().Data;

            Assert.AreEqual(resultToTest.name, expectedAnonymousStructure.name);
            Assert.AreEqual(resultToTest.child.name, expectedAnonymousStructure.child.name);
        }

        public void Test_CanRead_Properties_Of_DecodedJson_JsonResult()
        {
            dynamic resultToTest = factory.GetParsedJsonResult().Data;

            // verifying some important properties are there
            // considering we have a dynamic result:
            //  only if its dynamic so its structure is easily modified
            // we may be simply creating ourselves maintainence by testing this structure
            //  well defined request / response contracts 
            //  (IMO) work well for representing your application from different aspects
            //  whether those aspects be related to your user, which view they are looking at, etc
            Assert.AreEqual(resultToTest.balance, expectedJsonStructure.balance);
            Assert.AreEqual(resultToTest.picture, expectedJsonStructure.picture);
            Assert.AreEqual(resultToTest.age, expectedJsonStructure.age);
            Assert.AreEqual(resultToTest.name, expectedJsonStructure.name);
            Assert.AreEqual(resultToTest.gender, expectedJsonStructure.gender);

        }
    }
}
