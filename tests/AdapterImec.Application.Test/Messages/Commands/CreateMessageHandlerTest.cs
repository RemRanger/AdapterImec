//using AdapterImec.Application.Test.TestData;

namespace AdapterImec.Application.Test.Messages.Commands
{
    public class CreateMessageHandlerTest
    {
        //private readonly CancellationToken cancellationToken = CancellationToken.None;

        //[Fact]
        //public async Task Should_Return_MessageIds_On_Happy_Flow()
        //{
        //    // Arrange
        //    var command = new CreateMessageCommand()
        //    {
        //        Customer = new Domain.ValueObjects.CompanyId("nl.ubn", "12345678"),
        //        Provider = new Domain.ValueObjects.CompanyId("nl.kvk", "16050353"),
        //        Creator = "farmer-jack",
        //        MessageType = "imec",
        //        Content = JsonDocument.Parse(TestMessages.Example_X_20220621)
        //    };

        //    var messageRepository = new Mock<IMessageRepository>();
        //    messageRepository.Setup(x => x.CreateAsync(It.IsAny<Message>(), cancellationToken));
        //    messageRepository.Setup(x => x.CheckMessageExistsAsync(TestMessages.Example_X_20220621_Id1, command.Provider.Scheme, command.Provider.Value, cancellationToken)).ReturnsAsync(false);

        //    var logger = new NullLogger<CreateMessageHandler>();

        //    // Act
        //    var target = new CreateMessageHandler(messageRepository.Object, logger);
        //    var result = await target.Handle(command, cancellationToken);

        //    //Assert
        //    var messageIds = result.Split(',').Select(i => i.Trim()).ToArray();
        //    messageIds.Should().HaveCount(1);
        //    messageIds[0].Should().Be(TestMessages.Example_X_20220621_Id1);

        //    messageRepository.Verify(x => x.CheckMessageExistsAsync(TestMessages.Example_X_20220621_Id1, command.Provider.Scheme, command.Provider.Value, cancellationToken), Times.Once);
        //    messageRepository.Verify(x => x.CreateAsync(It.IsAny<Message>(), cancellationToken), Times.Exactly(1));
        //}

        //[Fact]
        //public async Task Should_Throw_ValidationException_When_Message_Exists()
        //{
        //    // Arrange
        //    var command = new CreateMessageCommand()
        //    {
        //        Customer = new Domain.ValueObjects.CompanyId("nl.ubn", "12345678"),
        //        Provider = new Domain.ValueObjects.CompanyId("nl.kvk", "16050353"),
        //        Creator = "farmer-jack",
        //        MessageType = "imec",
        //        Content = JsonDocument.Parse(TestMessages.Example_X_20220621)
        //    };

        //    var messageRepository = new Mock<IMessageRepository>();
        //    messageRepository.Setup(x => x.CreateAsync(It.IsAny<Message>(), cancellationToken));
        //    messageRepository.Setup(x => x.CheckMessageExistsAsync(TestMessages.Example_X_20220621_Id1, command.Provider.Scheme, command.Provider.Value, cancellationToken)).ReturnsAsync(true);

        //    var logger = new NullLogger<CreateMessageHandler>();

        //    // Act
        //    var target = new CreateMessageHandler(messageRepository.Object, logger);
        //    var exception = await Assert.ThrowsAsync<ValidationException>(() => target.Handle(command, cancellationToken));

        //    // Assert
        //    exception.Message.Should().Be($"Message with id = '{TestMessages.Example_X_20220621_Id1}' already exists");

        //    messageRepository.Verify(x => x.CheckMessageExistsAsync(TestMessages.Example_X_20220621_Id1, command.Provider.Scheme, command.Provider.Value, cancellationToken), Times.Once);
        //    messageRepository.Verify(x => x.CreateAsync(It.IsAny<Message>(), cancellationToken), Times.Never);
        //}

        //[Fact]
        //public async Task Should_Throw_ValidationException_When_Id_Missing()
        //{
        //    // Arrange
        //    var command = new CreateMessageCommand()
        //    {
        //        Customer = new Domain.ValueObjects.CompanyId("nl.ubn", "12345678"),
        //        Provider = new Domain.ValueObjects.CompanyId("nl.kvk", "16050353"),
        //        Creator = "farmer-jack",
        //        MessageType = "imec",
        //        Content = JsonDocument.Parse(TestMessages.Example_X_20220621.Replace(@"""id""", @"""ida"""))
        //    };

        //    var messageRepository = new Mock<IMessageRepository>();
        //    messageRepository.Setup(x => x.CreateAsync(It.IsAny<Message>(), cancellationToken));
        //    messageRepository.Setup(x => x.CheckMessageExistsAsync(TestMessages.Example_X_20220621_Id1, command.Provider.Scheme, command.Provider.Value, cancellationToken)).ReturnsAsync(false);

        //    var logger = new NullLogger<CreateMessageHandler>();

        //    // Act
        //    var target = new CreateMessageHandler(messageRepository.Object, logger);
        //    var exception = await Assert.ThrowsAsync<ValidationException>(() => target.Handle(command, cancellationToken));

        //    // Assert
        //    exception.Message.Should().Be("'id' element is missing");

        //    messageRepository.Verify(x => x.CheckMessageExistsAsync(TestMessages.Example_X_20220621_Id1, command.Provider.Scheme, command.Provider.Value, cancellationToken), Times.Never);
        //    messageRepository.Verify(x => x.CreateAsync(It.IsAny<Message>(), cancellationToken), Times.Never);
        //}

        //[Fact]
        //public async Task Should_Throw_ValidationException_When_Meta_Block_Missing()
        //{
        //    // Arrange
        //    var command = new CreateMessageCommand()
        //    {
        //        Customer = new Domain.ValueObjects.CompanyId("nl.ubn", "12345678"),
        //        Provider = new Domain.ValueObjects.CompanyId("nl.kvk", "16050353"),
        //        Creator = "farmer-jack",
        //        MessageType = "imec",
        //        Content = JsonDocument.Parse(TestMessages.Example_X_20220621.Replace(@"""meta""", @"""meuta"""))
        //    };

        //    var messageRepository = new Mock<IMessageRepository>();
        //    messageRepository.Setup(x => x.CreateAsync(It.IsAny<Message>(), cancellationToken));
        //    messageRepository.Setup(x => x.CheckMessageExistsAsync(TestMessages.Example_X_20220621_Id1, command.Provider.Scheme, command.Provider.Value, cancellationToken)).ReturnsAsync(false);

        //    var logger = new NullLogger<CreateMessageHandler>();

        //    // Act
        //    var target = new CreateMessageHandler(messageRepository.Object, logger);
        //    var exception = await Assert.ThrowsAsync<ValidationException>(() => target.Handle(command, cancellationToken));

        //    // Assert
        //    exception.Message.Should().Be("'meta' block is missing");

        //    messageRepository.Verify(x => x.CheckMessageExistsAsync(TestMessages.Example_X_20220621_Id1, command.Provider.Scheme, command.Provider.Value, cancellationToken), Times.Never);
        //    messageRepository.Verify(x => x.CreateAsync(It.IsAny<Message>(), cancellationToken), Times.Never);
        //}

        //[Fact]
        //public async Task Should_Throw_ValidationException_When_Created_Missing()
        //{
        //    // Arrange
        //    var command = new CreateMessageCommand()
        //    {
        //        Customer = new Domain.ValueObjects.CompanyId("nl.ubn", "12345678"),
        //        Provider = new Domain.ValueObjects.CompanyId("nl.kvk", "16050353"),
        //        Creator = "farmer-jack",
        //        MessageType = "imec",
        //        Content = JsonDocument.Parse(TestMessages.Example_X_20220621.Replace(@"""created""", @"""croated"""))
        //    };

        //    var messageRepository = new Mock<IMessageRepository>();
        //    messageRepository.Setup(x => x.CreateAsync(It.IsAny<Message>(), cancellationToken));
        //    messageRepository.Setup(x => x.CheckMessageExistsAsync(TestMessages.Example_X_20220621_Id1, command.Provider.Scheme, command.Provider.Value, cancellationToken)).ReturnsAsync(false);

        //    var logger = new NullLogger<CreateMessageHandler>();

        //    // Act
        //    var target = new CreateMessageHandler(messageRepository.Object, logger);
        //    var exception = await Assert.ThrowsAsync<ValidationException>(() => target.Handle(command, cancellationToken));

        //    // Assert
        //    exception.Message.Should().Be("'created' element is missing in 'meta' block");

        //    messageRepository.Verify(x => x.CheckMessageExistsAsync(TestMessages.Example_X_20220621_Id1, command.Provider.Scheme, command.Provider.Value, cancellationToken), Times.Never);
        //    messageRepository.Verify(x => x.CreateAsync(It.IsAny<Message>(), cancellationToken), Times.Never);
        //}
    }
}
