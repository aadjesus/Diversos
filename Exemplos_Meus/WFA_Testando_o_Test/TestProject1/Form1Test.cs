using WFA_Testando_o_Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestProject1
{
    
    
    /// <summary>
    ///This is a test class for Form1Test and is intended
    ///to contain all Form1Test Unit Tests
    ///</summary>
    [TestClass()]
    public class Form1Test
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
        ///A test for Form1 Constructor
        ///</summary>
        [TestMethod()]
        public void Form1ConstructorTest()
        {
            Form1 target = new Form1();
            //Assert.Inconclusive( "TODO: Implement code to verify target");
            Assert.AreEqual(target, null,"Erro");

        }

        /// <summary>
        ///A test for Dispose
        ///</summary>
        [TestMethod()]
        [DeploymentItem("WFA_Testando_o_Test.exe")]
        public void DisposeTest()
        {
            Form1_Accessor target = new Form1_Accessor(); // TODO: Initialize to an appropriate value
            bool disposing = false; // TODO: Initialize to an appropriate value
            target.Dispose(disposing);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for InitializeComponent
        ///</summary>
        [TestMethod()]
        [DeploymentItem("WFA_Testando_o_Test.exe")]
        public void InitializeComponentTest()
        {
            Form1_Accessor target = new Form1_Accessor(); // TODO: Initialize to an appropriate value
            target.InitializeComponent();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Teste_NomeMetodo
        ///</summary>
        [TestMethod()]
        [DeploymentItem("WFA_Testando_o_Test.exe")]
        public void Teste_NomeMetodoTest()
        {
            Form1_Accessor target = new Form1_Accessor(); // TODO: Initialize to an appropriate value
            target.Teste_NomeMetodo();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
