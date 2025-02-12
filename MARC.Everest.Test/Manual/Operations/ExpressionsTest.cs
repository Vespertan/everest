﻿/**
 * Copyright 2008-2014 Mohawk College of Applied Arts and Technology
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); you 
 * may not use this file except in compliance with the License. You may 
 * obtain a copy of the License at 
 * 
 * http://www.apache.org/licenses/LICENSE-2.0 
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
 * License for the specific language governing permissions and limitations under 
 * the License.
 * 
 * User: fyfej
 * Date: 3-6-2013
 */
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;
using MARC.Everest.Connectors;
using MARC.Everest.DataTypes.Primitives;

namespace MARC.Everest.Test.DataTypes.Manual
{
    /// <summary>
    /// Summary description for ExpressionsTest
    /// </summary>
    [TestClass]
    public class ExpressionsTest
    {
        public ExpressionsTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     NullFlavor      : Derived
        /// And, the rest of the variables are Nullified:
        ///     Quantity        : The initial code
        ///     Expression      : The code system the code was picked from
        /// </summary>
   
        [TestMethod]
        public void ExpressionTest01()
        {
            INT expressionOnly = new INT(){ NullFlavor = NullFlavor.Derived };
                
            XmlDocument mathDoc = new XmlDocument();
            mathDoc.LoadXml(@"<math xmlns='http://www.w3.org/1998/Match/MathML'>
            <mrow>
                <mi>a</mi>
                <mo>&#2062;</mo>
                <msup><mi>x</mi><mn>2</mn></msup>
                <mo>+</mo>
                <mi>b</mi>
                <mo>&#2062;</mo>
                <mi>x</mi>
                <mo>+</mo>
                <mi>c</mi>
            </mrow>
            </math>");

            expressionOnly.Expression = new ED();
            expressionOnly.Expression.XmlData = mathDoc.DocumentElement;
            expressionOnly.Expression.MediaType = "application/mathml+xml";

            Assert.IsTrue(expressionOnly.Validate());
        }
    }
}
