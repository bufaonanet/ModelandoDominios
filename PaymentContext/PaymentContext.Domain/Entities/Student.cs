using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        private readonly IList<Subscription> _substriptions;
        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;
            _substriptions = new List<Subscription>();

            AddNotifications(Name, Document, Email);
        }

        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }
        public IReadOnlyCollection<Subscription> Subscriptions { get => _substriptions.ToArray(); }

        public void AddSubscription(Subscription subscription)
        {
            var hasSubscription = false;

            foreach (var sub in _substriptions)
            {
                if (sub.Active)
                    hasSubscription = true;
            }

            AddNotifications(new Contract()
               .Requires()
               .IsFalse(hasSubscription, "Student.Subscriptions", "Estudante já tem uma assinatura ativa")
               .IsGreaterThan(subscription.Payments.Count, 0, "Student.Payments", "Inscrição sem pagamento")
            );

            //Alternativa para notificação
            // if (hasSubscription)
            //     AddNotification("Student.Subscriptions", "Estudante já tem uma assinatura ativa");

            _substriptions.Add(subscription);
        }



    }

}