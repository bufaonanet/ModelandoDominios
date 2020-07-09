using System;
using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Command;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler :
    Notifiable,
    IHandler<CreateBoletoSubscriptionCommand>,
    IHandler<CreatePayPalSubscriptionCommand>,
    IHandler<CreateCredCardSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository repository, IEmailService email)
        {
            _repository = repository;
            _emailService = email;
        }

        public ICommandResult Handler(CreateBoletoSubscriptionCommand command)
        {
            //Fail Fast Validate
            command.Validate();
            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Mão foi possível realizar sua assinatura");
            }

            //Verificar se documento já foi cadastrado
            if (_repository.DocumentExists(command.Document))
            {
                AddNotification("Document", "Esse cpf já está em uso");
            }

            //Verificar se email já foi cadastrado
            if (_repository.EmailExists(command.Email))
            {
                AddNotification("Email", "Esse email já está em uso");
            }

            //Gerar VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.cpf);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number,
            command.Neighborhood, command.City, command.Country, command.Country, command.ZipCode);

            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(
                command.Barcode,
                command.BoletoNumber,
                command.PaiDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                command.Payer,
                new Document(command.PayerDocument, command.PayerDocumentType),
                address,
                email
            );

            //Gerar Entidades
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar validações
            AddNotifications(name, document, email, student, subscription, payment);

            //Checar validações 
            if (Invalid)
                return new CommandResult(false, "Não foi possível realizar sua inscrição");


            //Salvar informações no banco
            _repository.CreateSubscription(student);

            //Enviar Email
            _emailService.Send(name.ToString(), student.Email.Address,
               "Bem vindo", "Sua assinatura foi criada");

            return new CommandResult(true, "Assinatura realizada com sucesso!");
        }

        public ICommandResult Handler(CreatePayPalSubscriptionCommand command)
        {
            //Implementar Validate
            // command.Validate();
            // if (command.Invalid)
            // {
            //     AddNotifications(command);
            //     return new CommandResult(false, "Mão foi possível realizar sua assinatura");
            // }

            //Verificar se documento já foi cadastrado
            if (_repository.DocumentExists(command.Document))
            {
                AddNotification("Document", "Esse cpf já está em uso");
            }

            //Verificar se email já foi cadastrado
            if (_repository.EmailExists(command.Email))
            {
                AddNotification("Email", "Esse email já está em uso");
            }

            //Gerar VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.cpf);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number,
            command.Neighborhood, command.City, command.Country, command.Country, command.ZipCode);

            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PayPalPayment(
                command.TransactionCode,
                command.PaiDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                command.Payer,
                new Document(command.PayerDocument, command.PayerDocumentType),
                address,
                email
            );

            //Gerar Entidades
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar validações
            AddNotifications(name, document, email, student, subscription, payment);

            //Salvar informações no banco
            _repository.CreateSubscription(student);

            //Enviar Email
            _emailService.Send(name.ToString(), student.Email.Address,
               "Bem vindo", "Sua assinatura foi criada");

            return new CommandResult(true, "Assinatura realizada com sucesso!");
        }

        public ICommandResult Handler(CreateCredCardSubscriptionCommand command)
        {
            //Implementar Validate
            // command.Validate();
            // if (command.Invalid)
            // {
            //     AddNotifications(command);
            //     return new CommandResult(false, "Mão foi possível realizar sua assinatura");
            // }

            //Verificar se documento já foi cadastrado
            if (_repository.DocumentExists(command.Document))
            {
                AddNotification("Document", "Esse cpf já está em uso");
            }

            //Verificar se email já foi cadastrado
            if (_repository.EmailExists(command.Email))
            {
                AddNotification("Email", "Esse email já está em uso");
            }

            //Gerar VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.cpf);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number,
            command.Neighborhood, command.City, command.Country, command.Country, command.ZipCode);

            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));

            var payment = new CredCardPayment(
                command.CardHolderName,
                command.CardNumber,
                command.LastTransactionNumber,
                command.PaiDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                command.Payer,
                new Document(command.PayerDocument, command.PayerDocumentType),
                address,
                email
            );

            //Gerar Entidades
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar validações
            AddNotifications(name, document, email, student, subscription, payment);

            //Salvar informações no banco
            _repository.CreateSubscription(student);

            //Enviar Email
            _emailService.Send(name.ToString(), student.Email.Address,
               "Bem vindo", "Sua assinatura foi criada");

            return new CommandResult(true, "Assinatura realizada com sucesso!");
        }
    }
}