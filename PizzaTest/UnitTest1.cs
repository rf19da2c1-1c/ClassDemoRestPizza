using System.Collections.Generic;
using ClassDemoRestPizza.Controllers;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PizzaLib.model;

namespace PizzaTest
{
    [TestClass]
    public class UnitTest1
    {
        private PizzasController cntr = null;

        [TestInitialize]
        public void BeforeEachTest()
        {
            cntr = new PizzasController();
        }

        [TestMethod]
        public void TestMethod1()
        {
            // arrange
            // i beforeeachtest

            // act
            List<Pizza> liste = new List<Pizza>(cntr.Get());

            //Assert
            Assert.AreEqual(8,liste.Count);

        }
    }
}
