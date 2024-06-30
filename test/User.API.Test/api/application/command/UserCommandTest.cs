using Moq;
using User.API.application.command;
using User.API.application.service;
using User.domain.model;
using User.Domain.exceptions;
using User.Domain.seedWork;

namespace User.API.Test.application.command;

public class UserCommandTest
{
    private readonly Mock<IUserRepository> _repositoryMock;
    private readonly Mock<ICryptoPasswordService> _cryptoMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CreateUserCommandHandler _createUserCommandHandler;
    private readonly UserUpdateCommandHandler _userUpdateCommandHandler;

    public UserCommandTest()
    {
        this._repositoryMock = new Mock<IUserRepository>();
        this._cryptoMock = new Mock<ICryptoPasswordService>();
        this._unitOfWorkMock = new Mock<IUnitOfWork>();
        this._createUserCommandHandler = new(_repositoryMock.Object, _cryptoMock.Object);
        this._userUpdateCommandHandler = new(_repositoryMock.Object);
    }
    [Fact]
    public async Task Handle_ShouldCreateUserAndReturnTrue()
    {
        //arrange
        CreateUserCommand command = new("satoshi", "satoshi@sample.com", "satoshi", 10);

        var hashPassword = "hash_satoshi";
        var salt = "salt_satoshi";
        _cryptoMock.Setup(m => m.HashPassword(command.Password)).ReturnsAsync((hashPassword, salt));

        _repositoryMock.Setup(m => m.CreateAsync(It.IsAny<UserAggregate>()))
            .Returns(Task.CompletedTask);

        _unitOfWorkMock.Setup(m => m.SaveEntityAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _repositoryMock.Setup(m => m.UnitOfWork).Returns(_unitOfWorkMock.Object);

        var result = await _createUserCommandHandler.Handle(command, CancellationToken.None);

        Assert.True(result);
        _cryptoMock.Verify(m => m.HashPassword(command.Password), Times.Once);
        _repositoryMock.Verify(m => m.CreateAsync(It.IsAny<UserAggregate>()), Times.Once);
        _unitOfWorkMock.Verify(m => m.SaveEntityAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCreateUserAndReturnFalse()
    {
        //arrange
        CreateUserCommand command = new("satoshi", "satoshi@sample.com", "satoshi", 10);

        var hashPassword = "hash_satoshi";
        var salt = "salt_satoshi";
        _cryptoMock.Setup(m => m.HashPassword(command.Password)).ReturnsAsync((hashPassword, salt));

        _repositoryMock.Setup(m => m.CreateAsync(It.IsAny<UserAggregate>()))
            .Returns(Task.CompletedTask);

        _unitOfWorkMock.Setup(m => m.SaveEntityAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _repositoryMock.Setup(m => m.UnitOfWork).Returns(_unitOfWorkMock.Object);

        var result = await _createUserCommandHandler.Handle(command, CancellationToken.None);

        Assert.False(result);
        _cryptoMock.Verify(m => m.HashPassword(command.Password), Times.Once);
        _repositoryMock.Verify(m => m.CreateAsync(It.IsAny<UserAggregate>()), Times.Once);
        _unitOfWorkMock.Verify(m => m.SaveEntityAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCreateUserAndReturnThrowException_WhenEmailAlreadyExist()
    {
        //arrange
        CreateUserCommand command = new("satoshi", "satoshi@sample.com", "satoshi", 10);
        UserAggregate existUser = new(Guid.NewGuid());
        existUser.setUser(command.Name, command.Email, command.Password, command.Age, existUser.UserAggregateId);

        _repositoryMock.Setup(m => m.FindUserByEmail(command.Email)).ReturnsAsync(existUser);

        //act & assert
        await Assert.ThrowsAsync<UserDomainException>(async () => await _createUserCommandHandler.Handle(command, CancellationToken.None));
        _repositoryMock.Verify(m => m.FindUserByEmail(command.Email), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldUpdateUserAndReturnTrue_WhenUserExist()
    {
        //arrange
        UserUpdateCommand command = new(Guid.NewGuid(), "pikachu", 10);

        UserAggregate aggregate = new(command.AggregateId);
        aggregate.setUser("pikachu", "pikachu@sample.com", "pikachu", 11, command.AggregateId);

        _repositoryMock.Setup(m => m.FindUserByAggregateId(command.AggregateId))
            .ReturnsAsync(aggregate);
        _repositoryMock.Setup(m => m.Update(It.IsAny<UserAggregate>()));

        _unitOfWorkMock.Setup(m => m.SaveEntityAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        _repositoryMock.Setup(m => m.UnitOfWork).Returns(_unitOfWorkMock.Object);

        //act
        var result = await _userUpdateCommandHandler.Handle(command, CancellationToken.None);

        //assert
        Assert.True(result);
        _repositoryMock.Verify(m => m.FindUserByAggregateId(command.AggregateId), Times.Once);
        _repositoryMock.Verify(m => m.Update(It.IsAny<UserAggregate>()), Times.Once);
        _unitOfWorkMock.Setup(m => m.SaveEntityAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
    }

    [Fact]
    public async Task Handle_ShouldUpdateUserAndReturnThrowException_WhenUserNotExist()
    {
        //arrange
        UserUpdateCommand command = new(Guid.NewGuid(), "pikachu", 10);

        _repositoryMock.Setup(m => m.FindUserByAggregateId(command.AggregateId))
            .ReturnsAsync((UserAggregate)null);

        //act & assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await _userUpdateCommandHandler.Handle(command, CancellationToken.None));

        _repositoryMock.Verify(m => m.FindUserByAggregateId(command.AggregateId), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldUpdateUserAndReturnFalse()
    {
        //arrange
        UserUpdateCommand command = new(Guid.NewGuid(), "pikachu", 10);

        UserAggregate aggregate = new(command.AggregateId);
        aggregate.setUser("pikachu", "pikachu@sample.com", "pikachu", 11, command.AggregateId);

        _repositoryMock.Setup(m => m.FindUserByAggregateId(command.AggregateId))
            .ReturnsAsync(aggregate);
        _repositoryMock.Setup(m => m.Update(It.IsAny<UserAggregate>()));

        _unitOfWorkMock.Setup(m => m.SaveEntityAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        _repositoryMock.Setup(m => m.UnitOfWork).Returns(_unitOfWorkMock.Object);

        //act
        var result = await _userUpdateCommandHandler.Handle(command, CancellationToken.None);

        //assert
        Assert.False(result);
        _repositoryMock.Verify(m => m.FindUserByAggregateId(command.AggregateId), Times.Once);
        _repositoryMock.Verify(m => m.Update(It.IsAny<UserAggregate>()), Times.Once);
        _unitOfWorkMock.Setup(m => m.SaveEntityAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
    }
}