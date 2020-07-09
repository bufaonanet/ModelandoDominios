using System;
using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Subscription : Entity
    {
        private readonly IList<Payment> _payment;

        public Subscription(DateTime? expireTime)
        {
            CreateDate = DateTime.Now;
            LastUpdate = DateTime.Now;
            ExpireTime = expireTime;
            Active = true;
            _payment = new List<Payment>();
        }

        public DateTime CreateDate { get; private set; }
        public DateTime LastUpdate { get; private set; }
        public DateTime? ExpireTime { get; private set; }
        public bool Active { get; private set; }
        public IReadOnlyCollection<Payment> Payments { get => _payment.ToArray(); }

        public void AddPayment(Payment payment)
        {
            AddNotifications(new Contract()
                .Requires()
                .IsGreaterOrEqualsThan(payment.PaidDate, DateTime.Now, "Subscription.Payments", "A data do pagamento deve ser futura")
            );

            _payment.Add(payment);
        }

        public void Activate()
        {
            Active = true;
            LastUpdate = DateTime.Now;
        }

        public void Inactive()
        {
            Active = false;
            LastUpdate = DateTime.Now;
        }

    }

}