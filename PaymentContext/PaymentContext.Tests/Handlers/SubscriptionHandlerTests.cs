using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.Handlers
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        //Metodologia Red, Green e Refactor para testes

        [TestMethod]
        public void RetornarErroQuandoDocumentoJaExiste()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();

            command.FirstName = "bufao";
            command.LastName = "nanet";
            command.Document = "99999999999";
            command.Email = "sdfasd";
            command.Barcode = "sdfsad";
            command.BoletoNumber = "sfda";
            command.PaymentNumber = "sdfs";
            command.PaiDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 10;
            command.TotalPaid = 10;
            command.Payer = "bufao";
            command.PayerDocument = "1235313";
            command.PayerDocumentType = EDocumentType.cpf;
            command.PayerEmail = "safsdfsa";
            command.Street = "asdfasdfsd";
            command.Number = "asdfasdfsd";
            command.Neighborhood = "asdfasdfsad";
            command.City = "asdfasdfsa";
            command.State = "sadfasdf";
            command.Country = "asdfasdfasd";
            command.ZipCode = "asdfasdfs";

            handler.Handler(command);
            Assert.AreEqual(false, handler.Valid);
        }


    }
}
