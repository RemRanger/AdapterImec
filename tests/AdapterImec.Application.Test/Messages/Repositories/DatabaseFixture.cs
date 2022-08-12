using AdapterImec.Domain.Entities;
using AdapterImec.Domain.ValueObjects;
using AdapterImec.Repository;
using AdapterImec.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace AdapterImec.Application.Test.Messages.Repositories
{
    public class DatabaseFixture : IDisposable
    {
        private readonly CompanyId anotherCustomer = new("nl.kvk", "70000002");
        private readonly CompanyId anotherProvider = new("nl.kvk", "42000002");

        private readonly DataContext context;

        internal MessageRepository Repository { get; }
        internal CompanyId Customer = new("nl.kvk", "70000001");
        internal CompanyId Provider = new("nl.kvk", "42000001");

        public DatabaseFixture()
        {
            var now = DateTime.UtcNow;

            var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName: "MockMessages").Options;
            context = new DataContext(options);
            context.Messages.Add(new Message
            {
                Id = 1,
                CustomerScheme = Customer.Scheme,
                CustomerValue = Customer.Value,
                ProvidingCompanyScheme = Provider.Scheme,
                ProvidingCompanyValue = Provider.Value,
                DateCreated = now.AddMonths(6)
            });
            context.Messages.Add(new Message
            {
                Id = 2,
                CustomerScheme = anotherCustomer.Scheme,
                CustomerValue = anotherCustomer.Value,
                ProvidingCompanyScheme = Provider.Scheme,
                ProvidingCompanyValue = Provider.Value,
                DateCreated = now
            });
            context.Messages.Add(new Message
            {
                Id = 3,
                CustomerScheme = Customer.Scheme,
                CustomerValue = Customer.Value,
                ProvidingCompanyScheme = Provider.Scheme,
                ProvidingCompanyValue = Provider.Value,
                DateCreated = now
            });
            context.Messages.Add(new Message
            {
                Id = 4,
                CustomerScheme = Customer.Scheme,
                CustomerValue = Customer.Value,
                ProvidingCompanyScheme = anotherProvider.Scheme,
                ProvidingCompanyValue = anotherProvider.Value,
                DateCreated = now
            });
            context.Messages.Add(new Message
            {
                Id = 5,
                CustomerScheme = Customer.Scheme,
                CustomerValue = Customer.Value,
                ProvidingCompanyScheme = Provider.Scheme,
                ProvidingCompanyValue = Provider.Value,
                DateCreated = now.AddMonths(-6)
            });

            context.SaveChanges();

            Repository = new MessageRepository(new DataContext(options));
        }

        public void Dispose() => context.Dispose();
    }
}
