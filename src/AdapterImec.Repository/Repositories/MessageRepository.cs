using AdapterImec.Domain.Abstract;
using AdapterImec.Domain.Entities;
using AdapterImec.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdapterImec.Repository.Repositories
{
    internal class MessageRepository : IMessageRepository
    {
        private readonly DataContext dataContext;

        public MessageRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<bool> CheckMessageExistsAsync(string messageId, string providingCompanyScheme, string providingCompanyValue, CancellationToken cancellationToken)
        {
            return await dataContext.Messages
                .Where(x => x.MessageId == messageId && x.ProvidingCompanyScheme == providingCompanyScheme && x.ProvidingCompanyValue == providingCompanyValue)
                .AnyAsync(cancellationToken);
        }

        public async Task CreateAsync(Message message, CancellationToken cancellationToken = default)
        {
            dataContext.Messages.Add(message);
            await dataContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Message>> GetByCustomerValueAsync(CompanyId customer, DateTime dateTimeStart, DateTime dateTimeEnd, DateTime? dateTimeModified, CompanyId provider, CancellationToken cancellationToken = default)
        {
            var query = dataContext.Messages
                .Where(x => x.CustomerScheme.Equals(customer.Scheme) && x.CustomerValue.Equals(customer.Value))
                .Where(x => x.ProvidingCompanyScheme.Equals(provider.Scheme) && x.ProvidingCompanyValue.Equals(provider.Value));

            if (dateTimeModified.HasValue)
            {
                query = query.Where(x => x.DateCreated >= dateTimeModified.Value);
            }
            else
            {
                query = query.Where(x => x.DateCreated >= dateTimeStart && x.DateCreated <= dateTimeEnd);
            };

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<Message> GetByMessageIdAsync(string id, string providingCompanyScheme, string providingCompanyValue, CancellationToken cancellationToken = default)
        {
            return await dataContext.Messages
                .FirstOrDefaultAsync(x => x.MessageId == id && x.ProvidingCompanyScheme == providingCompanyScheme && x.ProvidingCompanyValue == providingCompanyValue, cancellationToken);
        }

        public async Task UpdateAsync(Message message, CancellationToken cancellationToken = default)
        {
            await dataContext.SaveChangesAsync(cancellationToken);
        }
    }
}
