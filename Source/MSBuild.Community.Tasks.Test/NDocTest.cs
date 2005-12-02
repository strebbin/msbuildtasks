// $Id$

using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSBuild.Community.Tasks.Test
{
    /// <summary>
    /// Summary description for NDocTest
    /// </summary>
    [TestClass]
    public class NDocTest
    {
        public NDocTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void NDocExecute()
        {
            NDoc task = new NDoc();
            task.BuildEngine = new MockBuild();
            task.ProjectFilePath = @"..\..\..\MSBuild.Community.Tasks\MSBuild.Community.Tasks.ndoc";
            task.Documenter = "MSDN";
            Assert.IsTrue(task.Execute(), "Execute Failed");
        }
    }
}