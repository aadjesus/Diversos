using TFSObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestProject1
{
    
    
    /// <summary>
    ///This is a test class for ChamadoTest and is intended
    ///to contain all ChamadoTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ChamadoTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for AdicionaBH
        ///</summary>
        [TestMethod()]
        [DeploymentItem("TFSObjects.dll")]
        public void AdicionaBHTest()
        {

            Chamado_Accessor target = new Chamado_Accessor();
            DateTime d = new DateTime(2011,12,9,17,50,0); // TODO: Initialize to an appropriate value
            TimeSpan s = new TimeSpan(0,9,0); // TODO: Initialize to an appropriate value
            DateTime expected = new DateTime(2011, 12, 9, 17, 59, 0); // TODO: Initialize to an appropriate value
            
            DateTime actual;
            actual = target.AdicionaBH(d, s);
            Assert.AreEqual(expected, actual);
            
        }


        // <summary>
        ///A test for AdicionaBH
        ///</summary>
        [TestMethod()]
        [DeploymentItem("TFSObjects.dll")]
        public void AdicionaBHTest1()
        {

            Chamado_Accessor target = new Chamado_Accessor();
            DateTime d = new DateTime(2011, 12, 8, 17, 50, 0); // TODO: Initialize to an appropriate value
            TimeSpan s = new TimeSpan(0, 11, 0); // TODO: Initialize to an appropriate value
            DateTime expected = new DateTime(2011, 12, 9, 9, 1, 0); // TODO: Initialize to an appropriate value

            DateTime actual;
            actual = target.AdicionaBH(d, s);
            Assert.AreEqual(expected, actual);

        }



        // <summary>
        ///A test for AdicionaBH
        ///</summary>
        [TestMethod()]
        [DeploymentItem("TFSObjects.dll")]
        public void AdicionaBHTest2()
        {

            Chamado_Accessor target = new Chamado_Accessor();
            DateTime d = new DateTime(2011, 12, 8, 17, 50, 0); // TODO: Initialize to an appropriate value
            TimeSpan s = new TimeSpan(9, 11, 0); // TODO: Initialize to an appropriate value
            DateTime expected = new DateTime(2011, 12, 12, 9, 1, 0); // TODO: Initialize to an appropriate value

            DateTime actual;
            actual = target.AdicionaBH(d, s);
            Assert.AreEqual(expected, actual);

        }


        // <summary>
        ///A test for AdicionaBH
        ///</summary>
        [TestMethod()]
        [DeploymentItem("TFSObjects.dll")]
        public void AdicionaBHTest3()
        {

            Chamado_Accessor target = new Chamado_Accessor();
            DateTime d = new DateTime(2011, 4, 21, 17, 50, 0); // TODO: Initialize to an appropriate value
            TimeSpan s = new TimeSpan(0, 10, 0); // TODO: Initialize to an appropriate value
            DateTime expected = new DateTime(2011, 4, 22, 9, 9, 0); // TODO: Initialize to an appropriate value

            DateTime actual;
            actual = target.AdicionaBH(d, s);
            Assert.AreEqual(expected, actual);

        }


        // <summary>
        ///A test for AdicionaBH
        ///</summary>
        [TestMethod()]
        [DeploymentItem("TFSObjects.dll")]
        public void AdicionaBHTest4()
        {

            Chamado_Accessor target = new Chamado_Accessor();
            DateTime d = new DateTime(2011, 4, 20, 17, 50, 0); // TODO: Initialize to an appropriate value
            TimeSpan s = new TimeSpan(0, 11, 0); // TODO: Initialize to an appropriate value
            DateTime expected = new DateTime(2011, 4, 22, 9, 1, 0); // TODO: Initialize to an appropriate value

            DateTime actual;
            actual = target.AdicionaBH(d, s);
            Assert.AreEqual(expected, actual);

        }
    }
}
