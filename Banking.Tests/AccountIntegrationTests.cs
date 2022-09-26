using Banking.Domain;
using Banking.Domain.Contracts;
using Banking.Domain.Queries;
using Banking.Storage.Exceptions;
using Banking.WebAPI.Commands;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Text;

namespace Banking.Tests
{
    [TestClass]
    public class AccountIntegrationTests
    {
        private WebApplicationFactory<Program> application;
        private HttpClient client;

        [TestInitialize]
        public void TestInitialize()
        {
            application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    //Fake storage etc. but since its already in memory, no need.
                });

            client = application.CreateClient();
        }

        [TestMethod]
        public async Task CreateAccountTest()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var initialBalance = 150;

            //Act
            var response = await client.PostAsync($"api/v1/accounts/create", new StringContent(JsonConvert.SerializeObject(new CreateAccountCommand(userId, initialBalance)), Encoding.UTF8, "application/json"));

            //Assert
            response.EnsureSuccessStatusCode();
            var account = await GetAccountFromStorage(await response.Content.ReadAsStringAsync());
            Assert.AreEqual(initialBalance, account.Balance);
            Assert.AreEqual(userId, account.UserId);
        }

        [TestMethod]
        public async Task CreateAccount_NegativeTestWhenLessThanMinimum()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var initialBalance = 90;

            //Act
            var response = await client.PostAsync($"api/v1/accounts/create", new StringContent(JsonConvert.SerializeObject(new CreateAccountCommand(userId, initialBalance)), Encoding.UTF8, "application/json"));

            //Assert
            Assert.AreEqual(System.Net.HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public async Task DepositTest()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var balance = 1000;
            var accountNumber = Guid.NewGuid().ToString();
            await CreateAccountInStorage(balance, userId, accountNumber);
            var amountToDeposit = 500;

            //Act
            var response = await client.PostAsync($"api/v1/accounts/deposit", new StringContent(JsonConvert.SerializeObject(new DepositCommand(accountNumber, amountToDeposit)), Encoding.UTF8, "application/json"));

            //Assert
            response.EnsureSuccessStatusCode();
            var account = await GetAccountFromStorage(accountNumber);
            Assert.AreEqual(balance + amountToDeposit, account.Balance);
            Assert.AreEqual(userId, account.UserId);
        }

        [TestMethod]
        public async Task Deposit_NegativeTestWhenMoreThanMaximum()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var balance = 1000;
            var accountNumber = Guid.NewGuid().ToString();
            await CreateAccountInStorage(balance, userId, accountNumber);
            var amountToDeposit = 10000;

            //Act
            var response = await client.PostAsync($"api/v1/accounts/deposit", new StringContent(JsonConvert.SerializeObject(new DepositCommand(accountNumber, amountToDeposit)), Encoding.UTF8, "application/json"));

            //Assert
            Assert.AreEqual(System.Net.HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public async Task Deposit_NegativeTestWhenAccountNotFound()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var balance = 1000;
            var accountNumber = Guid.NewGuid().ToString();
            await CreateAccountInStorage(balance, userId, accountNumber);
            var amountToDeposit = 10000;

            //Act
            var response = await client.PostAsync($"api/v1/accounts/deposit", new StringContent(JsonConvert.SerializeObject(new DepositCommand("NonExistingAccountNumber", amountToDeposit)), Encoding.UTF8, "application/json"));

            //Assert
            Assert.AreEqual(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task WithdrawTest()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var balance = 1000;
            var accountNumber = Guid.NewGuid().ToString();
            await CreateAccountInStorage(balance, userId, accountNumber);
            var amountToWithdraw = 500;

            //Act
            var response = await client.PostAsync($"api/v1/accounts/withdraw", new StringContent(JsonConvert.SerializeObject(new DepositCommand(accountNumber, amountToWithdraw)), Encoding.UTF8, "application/json"));

            //Assert
            response.EnsureSuccessStatusCode();
            var account = await GetAccountFromStorage(accountNumber);
            Assert.AreEqual(balance - amountToWithdraw, account.Balance);
            Assert.AreEqual(userId, account.UserId);
        }

        [TestMethod]
        public async Task Withdraw_NegativeTestWhenAccountNotFound()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var balance = 500;
            var accountNumber = Guid.NewGuid().ToString();
            await CreateAccountInStorage(balance, userId, accountNumber);
            var amountToWithdraw = 100;

            //Act
            var response = await client.PostAsync($"api/v1/accounts/withdraw", new StringContent(JsonConvert.SerializeObject(new DepositCommand("NonExistingAccountNumber", amountToWithdraw)), Encoding.UTF8, "application/json"));

            //Assert
            Assert.AreEqual(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task Withdraw_NegativeTestWhenBalanceLessThanMinimum()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var balance = 150;
            var accountNumber = Guid.NewGuid().ToString();
            await CreateAccountInStorage(balance, userId, accountNumber);
            var amountToWithdraw = 51;

            //Act
            var response = await client.PostAsync($"api/v1/accounts/withdraw", new StringContent(JsonConvert.SerializeObject(new DepositCommand(accountNumber, amountToWithdraw)), Encoding.UTF8, "application/json"));

            //Assert
            Assert.AreEqual(System.Net.HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public async Task Withdraw_NegativeTestWhenWithdrawLimitExceeded()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var balance = 1100;
            var accountNumber = Guid.NewGuid().ToString();
            await CreateAccountInStorage(balance, userId, accountNumber);
            var amountToWithdraw = 999;

            //Act
            var response = await client.PostAsync($"api/v1/accounts/withdraw", new StringContent(JsonConvert.SerializeObject(new DepositCommand(accountNumber, amountToWithdraw)), Encoding.UTF8, "application/json"));

            //Assert
            Assert.AreEqual(System.Net.HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public async Task DeleteAccountTest()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var balance = 1000;
            var accountNumber = Guid.NewGuid().ToString();
            await CreateAccountInStorage(balance, userId, accountNumber);

            //Act
            var response = await client.PostAsync($"api/v1/accounts/delete", new StringContent(JsonConvert.SerializeObject(new DeleteAccountCommand(accountNumber)), Encoding.UTF8, "application/json"));

            //Assert
            response.EnsureSuccessStatusCode();
            await Assert.ThrowsExceptionAsync<EntityNotFoundException>(async () => await GetAccountFromStorage(accountNumber));
        }

        [TestMethod]
        public async Task DeleteAccount_NegativeTestWhenAccountNotFound()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var balance = 1000;
            var accountNumber = Guid.NewGuid().ToString();
            await CreateAccountInStorage(balance, userId, accountNumber);

            //Act
            var response = await client.PostAsync($"api/v1/accounts/delete", new StringContent(JsonConvert.SerializeObject(new DeleteAccountCommand("NonExistingAccountNumber")), Encoding.UTF8, "application/json"));

            //Assert
            Assert.AreEqual(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task DeleteAllAccountsTest()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var balance = 1000;
            var accountNumber = Guid.NewGuid().ToString();
            var accountNumber2 = Guid.NewGuid().ToString();
            await CreateAccountInStorage(balance, userId, accountNumber);
            await CreateAccountInStorage(balance, userId, accountNumber2);

            //Act
            var response = await client.PostAsync($"api/v1/accounts/deleteAll", new StringContent(JsonConvert.SerializeObject(new DeleteAllAccountsCommand(userId)), Encoding.UTF8, "application/json"));

            //Assert
            response.EnsureSuccessStatusCode();
            await Assert.ThrowsExceptionAsync<EntityNotFoundException>(async () => await GetAccountFromStorage(accountNumber));
            await Assert.ThrowsExceptionAsync<EntityNotFoundException>(async () => await GetAccountFromStorage(accountNumber2));
        }

        [TestMethod]
        public async Task DeleteAllAccounts_NegativeTestWhenUserNotFound()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var balance = 1000;
            var accountNumber = Guid.NewGuid().ToString();
            var accountNumber2 = Guid.NewGuid().ToString();
            await CreateAccountInStorage(balance, userId, accountNumber);
            await CreateAccountInStorage(balance, userId, accountNumber2);

            //Act
            var response = await client.PostAsync($"api/v1/accounts/deleteAll", new StringContent(JsonConvert.SerializeObject(new DeleteAllAccountsCommand(Guid.NewGuid())), Encoding.UTF8, "application/json"));

            //Assert
            Assert.AreEqual(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task GetAccountTest()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var balance = 1000;
            var accountNumber = Guid.NewGuid().ToString();
            await CreateAccountInStorage(balance, userId, accountNumber);

            //Act
            var response = await client.GetAsync($"api/v1/accounts/{accountNumber}");
            var responseContent = await response.Content.ReadAsStringAsync();
            var accountView = JsonConvert.DeserializeObject<AccountView>(responseContent);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(balance, accountView.Balance);
            Assert.AreEqual(accountNumber, accountView.AccountNumber);
            Assert.AreEqual(userId, accountView.UserId);
        }

        [TestMethod]
        public async Task GetAccount_NegativeTestWhenAccountNotFound()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var balance = 1000;
            var accountNumber = Guid.NewGuid().ToString();
            await CreateAccountInStorage(balance, userId, accountNumber);

            //Act
            var response = await client.GetAsync($"api/v1/accounts/NonExistingAccountNumber");

            //Assert
            Assert.AreEqual(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task GetAccountsByUserIdTest()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var balance = 1000;
            var accountNumber = Guid.NewGuid().ToString();
            var accountNumber2 = Guid.NewGuid().ToString();
            await CreateAccountInStorage(balance, userId, accountNumber);
            await CreateAccountInStorage(balance, userId, accountNumber2);

            //Act
            var response = await client.GetAsync($"api/v1/accounts/users/{userId}");
            var responseContent = await response.Content.ReadAsStringAsync();
            var accountViews = JsonConvert.DeserializeObject<IEnumerable<AccountView>>(responseContent);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(2, accountViews.Count());

            //Account 1
            var accountView = accountViews.Single(x => x.AccountNumber == accountNumber);
            Assert.AreEqual(balance, accountView.Balance);
            Assert.AreEqual(userId, accountView.UserId);

            //Account 2
            var accountView2 = accountViews.Single(x => x.AccountNumber == accountNumber);
            Assert.AreEqual(balance, accountView2.Balance);
            Assert.AreEqual(userId, accountView2.UserId);
        }

        [TestMethod]
        public async Task GetAccountsByUserId_NegativeTestWhenUserNotFound()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var balance = 1000;
            var accountNumber = Guid.NewGuid().ToString();
            var accountNumber2 = Guid.NewGuid().ToString();
            await CreateAccountInStorage(balance, userId, accountNumber);
            await CreateAccountInStorage(balance, userId, accountNumber2);

            //Act
            var response = await client.GetAsync($"api/v1/accounts/users/NonExistingUserId");

            //Assert
            Assert.AreEqual(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }


        private async Task CreateAccountInStorage(double amount, Guid userId, string accountNumber)
        {
            var unitOfWork = application.Services.CreateScope().ServiceProvider.GetRequiredService<IUnitOfWork>();
            unitOfWork.AccountRepository.Create(new Account(amount, userId, accountNumber));
            await unitOfWork.SaveChangesAsync();
        }

        private async Task<AccountView> GetAccountFromStorage(string accountNumber)
        {
            var queries = application.Services.CreateScope().ServiceProvider.GetRequiredService<IAccountQueries>();
            return await queries.GetAccount(accountNumber);
        }
    }
}