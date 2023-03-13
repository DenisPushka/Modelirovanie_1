using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Modelirovanie_1.Translate
{
    [TestClass]
    public class TestCalculation
    {
        [TestMethod]
        public void OperationPlusMinus()
        {
            var main = new TranslateToPostfix(new MainForm());
            var result = main.Translate("2+3-4+5");
            Assert.AreEqual(6, result.Result);
        }
        
        [TestMethod]
        public void OperationMultiplication()
        {
            var main = new TranslateToPostfix(new MainForm());
            var result = main.Translate("2+(3-4)*5");
            Assert.AreEqual(-3, result.Result);
        }
        
        [TestMethod]
        public void OperationSinCosArcSinArcCos()
        {
            var main = new TranslateToPostfix(new MainForm());
            var result = main.Translate("sin(cos(2+3/4))-5+6*(7*8-9)");
            Assert.AreEqual(276.2018, result.Result);
        }
        
        [TestMethod]
        public void OperationDegree1()
        {
            var main = new TranslateToPostfix(new MainForm());
            var result = main.Translate("2+(3^4+5)");
            Assert.AreEqual(88, result.Result);
        }
        
        [TestMethod]
        public void OperationDegree2()
        {
            var main = new TranslateToPostfix(new MainForm());
            var result = main.Translate("2+3^(4*2)");
            Assert.AreEqual(6563, result.Result);
        }
        
        [TestMethod]
        public void OperationHard()
        {
            var main = new TranslateToPostfix(new MainForm());
            var result = main.Translate("sin(cos(2+3/4))-5^(6/7)+8*(9*10-11)");
            Assert.AreEqual(627.222, result.Result);
        }
        
         
        [TestMethod]
        public void Operation()
        {
            var main = new TranslateToPostfix(new MainForm());
            var result = main.Translate("2-cos(3)*4");
            Assert.AreEqual(5.95997, result.Result);
        }
    }
}