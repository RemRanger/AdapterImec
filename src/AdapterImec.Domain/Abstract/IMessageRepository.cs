using AdapterImec.Domain.Entities;
using AdapterImec.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AdapterImec.Domain.Abstract
{
    public interface IMessageRepository
    {
        Task CreateAsync(Message message, CancellationToken cancellationToken = default);
        Task UpdateAsync(Message message, CancellationToken cancellationToken = default);
        Task<Message> GetByMessageIdAsync(string messageId, string providingCompanyScheme, string providingCompanyValue, CancellationToken cancellationToken = default);
        Task<List<Message>> GetByCustomerValueAsync(CompanyId customer, DateTime dateTimeStart, DateTime dateTimeEnd, DateTime? dateTimeModified, CompanyId provider, CancellationToken cancellationToken = default);
        Task<bool> CheckMessageExistsAsync(string messageId, string providerScheme, string providerValue, CancellationToken cancellationToken);
    }
}
