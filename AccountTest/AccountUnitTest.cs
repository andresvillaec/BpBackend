using Account.Application.Data;
using Account.Application.UseCases.BankAccount.Commands.Create;
using Account.Domain.Exceptions;
using Account.Domain.Interfaces;
using Moq;

namespace AccountTest
{
    [TestClass]
    public class AccountUnitTest
    {
        private Mock<IAccountRepository> _accountRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private CreateAccountCommandHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            _accountRepositoryMock = new Mock<IAccountRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new CreateAccountCommandHandler(_accountRepositoryMock.Object, _unitOfWorkMock.Object);
        }

        [TestMethod]
        public async Task Handle_ShouldCreateAccount_WhenAccountNumberIsUnique()
        {
            // Arrange
            var command = new CreateAccountCommand(
                "1234567890",
                Account.Domain.Enums.AccountTypes.Saving,
                1000m,
                1000m,
                1
            );

            _accountRepositoryMock
                .Setup(repo => repo.ExistsAccountNumber(It.IsAny<string>()))
                .ReturnsAsync(false);

            _unitOfWorkMock
                .Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Callback(() => { return; });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(command.Number, result.Number);
            Assert.AreEqual(command.AccountType, result.AccountType);
            Assert.AreEqual(command.OpeningDeposit, result.OpeningDeposit);
            Assert.AreEqual(command.Balance, result.Balance);
            Assert.AreEqual(command.OpeningDeposit, result.Balance);
            Assert.AreEqual(command.ClientId, result.ClientId);

            _accountRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Account.Domain.Entities.Account>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task Handle_ShouldCreateAccount_FailedWhenDuplicatedAccount()
        {
            var command = new CreateAccountCommand(
                "1234567890",
                Account.Domain.Enums.AccountTypes.Saving,
                1000m,
                1000m,
                1
            );

            _accountRepositoryMock
                .Setup(repo => repo.ExistsAccountNumber(It.IsAny<string>()))
                .ReturnsAsync(true);

            await Assert.ThrowsExceptionAsync<DuplicatedAccountException>(
                async () => await _handler.Handle(command, CancellationToken.None));
        }
    }
}