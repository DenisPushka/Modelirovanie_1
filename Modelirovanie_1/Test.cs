using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Modelirovanie_1
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void OperationPlusMinus()
        {
            var main = new MainForm();
            var result = main.TranslateToPostfix("A+B-C+D");
            Assert.AreEqual("AB+C-D+", result.Result);
        }
        
        [TestMethod]
        public void OperationMultiplication()
        {
            var main = new MainForm();
            var result = main.TranslateToPostfix("A+(B-C)*D");
            Assert.AreEqual("ABC-D*+", result.Result);
        }
        
        [TestMethod]
        public void OperationSinCosArcSinArcCos()
        {
            var main = new MainForm();
            var result = main.TranslateToPostfix("sin(cos(A+B/C))-D+E*(F*G-H)");
            Assert.AreEqual("ABC/+баD-EFG*H-*+", result.Result);
        }
        
        [TestMethod]
        public void OperationDegree1()
        {
            var main = new MainForm();
            var result = main.TranslateToPostfix("A+B^C+D");
            Assert.AreEqual("ABCдD++", result.Result);
        }
        
        [TestMethod]
        public void OperationDegree2()
        {
            var main = new MainForm();
            var result = main.TranslateToPostfix("A+B^C*D");
            Assert.AreEqual("ABCD*д+", result.Result);
        }
        
        [TestMethod]
        public void OperationHard()
        {
            var main = new MainForm();
            var result = main.TranslateToPostfix("sin(cos(A+B/C))-D+E*(F*G-H)^I");
            Assert.AreEqual("ABC/+баD-EFG*H-Iд*+", result.Result);
        }
    }
}