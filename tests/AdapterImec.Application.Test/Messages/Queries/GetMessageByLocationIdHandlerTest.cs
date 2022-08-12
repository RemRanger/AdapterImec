using AdapterImec.Application.Messages.Queries.GetMessageByLocationId;
using AdapterImec.Domain.Abstract;
using AdapterImec.Domain.Entities;
using AdapterImec.Domain.ValueObjects;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AdapterImec.Application.Test.Messages.Queries
{
    public class GetMessageByLocationIdHandlerTest
    {
        private readonly CancellationToken cancellationToken = CancellationToken.None;

        [Fact]
        public async Task Should_Return_Messages_On_Happy_Flow()
        {
            // Arrange 
            DateTime StartDate = new(2022, 01, 01);
            DateTime EndDate = DateTime.Now;
            DateTime ModifiedDate = new(2022, 02, 10, 08, 00, 00);

            var provider = new CompanyId("nl.kvk", "35431234");

            var parameters = new GetMessageByLocationIdParameters()
            {
                DateTimeStart = StartDate,
                DateTimeEnd = EndDate,
                DateTimeModified = ModifiedDate,
                ProviderCompanyScheme = provider.Scheme,
                ProviderCompanyId = provider.Value
            };
            GetMessageByLocationIdQuery request = new("nl.kvk", "12343543543", parameters);

            List<Message> messages = new() {
                new Message() { FileContent = "{\"meta\":{\"created\": \"2022-06-15T09:07:52.791309+00:00\"}, \"id\": \"8143fd73-9d28-48a3-8623-df0191c8ce96\"}" },
                new Message() { FileContent = "{\"meta\":{\"created\": \"2022-06-15T23:07:09.298105+00:00\"}, \"id\": \"eb43aa80-b1ed-4dd5-bc29-305047a335eb\"}" }
            };

            var messageRepository = new Mock<IMessageRepository>();
            messageRepository.Setup(x => x.GetByCustomerValueAsync(request.CustomerId, StartDate, EndDate, ModifiedDate, provider, cancellationToken)).ReturnsAsync(messages);

            // Act
            var target = new GetMessageByLocationIdHandler(messageRepository.Object, new NullLogger<GetMessageByLocationIdHandler>());
            var response = await target.Handle(request, cancellationToken);

            //Assert
            response.Should().HaveCount(2);

            messageRepository.Verify(x => x.GetByCustomerValueAsync(request.CustomerId, StartDate, EndDate, ModifiedDate, provider, cancellationToken), Times.Once);
            messageRepository.VerifyNoOtherCalls();
        }
    }
}
