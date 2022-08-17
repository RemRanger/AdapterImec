using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AdapterImec.Application.Test.Messages.Repositories
{
    public class GetByCustomerValueTest : IClassFixture<DatabaseFixture>
    {
        private readonly CancellationToken cancellationToken = CancellationToken.None;
        private readonly DatabaseFixture fixture;

        public GetByCustomerValueTest(DatabaseFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task Should_Return_Messages_On_Happy_Flow()
        {
            // Arrange
            var dateTimeStart = DateTime.UtcNow.AddYears(-1);
            var dateTimeEnd = DateTime.UtcNow.AddYears(1);
            var dateTimeModified = (DateTime?)null;

            // Act
            var messages = await fixture.Repository.GetByCustomerValueAsync(fixture.Customer, dateTimeStart, dateTimeEnd, dateTimeModified, fixture.Provider, cancellationToken);

            // Assert
            messages.Should().HaveCount(3);
            messages[0].Id.Should().Be(1);
            messages[1].Id.Should().Be(3);
            messages[2].Id.Should().Be(5);
        }

        [Fact]
        public async Task Should_Return_Messages_Within_TimeRange()
        {
            // Arrange
            var dateTimeStart = DateTime.UtcNow.AddMonths(-3);
            var dateTimeEnd = DateTime.UtcNow.AddMonths(3);
            var dateTimeModified = (DateTime?)null;

            // Act
            var messages = await fixture.Repository.GetByCustomerValueAsync(fixture.Customer, dateTimeStart, dateTimeEnd, dateTimeModified, fixture.Provider, cancellationToken);

            // Assert
            messages.Should().HaveCount(1);
            messages[0].Id.Should().Be(3);
        }

        [Fact]
        public async Task Should_Return_Messages_After_ModifiedDate()
        {
            // Arrange
            var dateTimeStart = DateTime.UtcNow.AddMonths(-3);
            var dateTimeEnd = DateTime.UtcNow.AddMonths(3);
            var dateTimeModified = DateTime.UtcNow.AddMonths(-1);

            // Act
            var messages = await fixture.Repository.GetByCustomerValueAsync(fixture.Customer, dateTimeStart, dateTimeEnd, dateTimeModified, fixture.Provider, cancellationToken);

            // Assert
            messages.Should().HaveCount(2);
            messages[0].Id.Should().Be(1);
            messages[1].Id.Should().Be(3);
        }
    }
}
