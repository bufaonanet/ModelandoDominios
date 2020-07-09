using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentTests
    {
        private readonly Name _name;
        private readonly Document _document;
        private readonly Email _email;
        private readonly Address _address;
        private readonly Subscription _subscription;
        private readonly Student _student;

        public StudentTests()
        {
            _name = new Name("bufão", "nanet");
            _document = new Document("05585524046", EDocumentType.cpf);
            _email = new Email("bufao@email.com");
            _address = new Address("rua dos bobos", "sem número", "expansão", "guanhães", "mg", "br", "39741000");
            _student = new Student(_name, _document, _email);

            _subscription = new Subscription(null);
        }

        [TestMethod]
        public void RetornarErroComMaisDeUmaInscricaoAtiva()
        {
            var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5),
            10, 10, "bufao na net", _document, _address, _email);

            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void RetornarSucessoComUmaInscricaoAtiva()
        {
            var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5),
            10, 10, "bufao na net", _document, _address, _email);

            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Valid);
        }

        [TestMethod]
        public void RetornarErroComInscricaoSemPagamento()
        {
            _student.AddSubscription(_subscription);
            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void RetornarSucessoComInscricaoComPagamento()
        {
            var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5),
           10, 10, "bufao na net", _document, _address, _email);

            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);
            Assert.IsTrue(_student.Valid);
        }

    }
}