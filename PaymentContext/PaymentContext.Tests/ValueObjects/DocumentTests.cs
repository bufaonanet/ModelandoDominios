using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.ValueObjects
{
    [TestClass]
    public class DocumentTests
    {
        //Metodologia Red, Green e Refactor para testes

        [TestMethod]
        public void RetornarErroComCNPJInvalido()
        {
            var doc = new Document("123", EDocumentType.cnpj);
            Assert.IsTrue(doc.Invalid);
        }

        [TestMethod]
        public void RetornarSucessoComCNPJValido()
        {
            var doc = new Document("87165787000120", EDocumentType.cnpj);
            Assert.IsTrue(doc.Valid);
        }

        [TestMethod]
        public void RetornarErroComCPFInvalido()
        {
            var doc = new Document("123", EDocumentType.cpf);
            Assert.IsTrue(doc.Invalid);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("96901919000")]
        [DataRow("45950326091")]
        [DataRow("05585524046")]
        public void RetornarSucessoComCPFValido(string cpf)
        {
            var doc = new Document(cpf, EDocumentType.cpf);
            Assert.IsTrue(doc.Valid);
        }
    }
}
