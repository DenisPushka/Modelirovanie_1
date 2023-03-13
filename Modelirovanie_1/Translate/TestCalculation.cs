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
            Assert.AreEqual(288, result.Result);
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
        //
        // [TestMethod]
        // public void OperationHard()
        // {
        //     var main = new TranslateToPostfix(new MainForm());
        //     var result = main.Translate("sin(cos(A+B/C))-D^(I/K)+E*(F*G-H)");
        //     Assert.AreEqual("ABC/+баDIK/д-EFG*H-*+", result.Result);
        // }
        //
        //  
        // [TestMethod]
        // public void Operation()
        // {
        //     var main = new TranslateToPostfix(new MainForm());
        //     var result = main.Translate("A-cos(B)*D");
        //     Assert.AreEqual("ABбD*-", result.Result);
        // }
    }
}