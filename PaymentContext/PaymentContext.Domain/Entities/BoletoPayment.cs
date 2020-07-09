using System;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities
{

    public class BoletoPayment : Payment
    {
        public BoletoPayment(
            string barcode,
            string boletoNumber,
            DateTime paidDate,
            DateTime expiredDate,
            decimal total,
            decimal totalPaid,
            string payer,
            Document document,
            Address address,
            Email email) : base(
                paidDate,
                expiredDate,
                total,
                totalPaid,
                payer,
                document,
                address,
                email)
        {
            Barcode = barcode;
            BoletoNumber = boletoNumber;
        }

        public string Barcode { get; private set; }
        public string BoletoNumber { get; private set; }
    }


}