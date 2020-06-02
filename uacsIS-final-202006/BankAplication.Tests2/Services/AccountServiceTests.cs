using AutoMapper;
using BankApplication.Data.DTOs;
using BankApplication.Data.Models;
using BankApplication.Service.Repositories;
using BankApplication.Tests2.Internal;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Tests2.Services
{

    [TestFixture]
    public class AccountServiceTests
    {
        private IAccountsRepository _service;
        private readonly IMapper _mapper;


        public AccountServiceTests()
        {
            var config = new MapperConfiguration(mc =>
            {
                mc.AddMaps("BankApplication.Data");
            });
            _mapper = config.CreateMapper();
        }

        [Test, Category("DB"), Category("Service")]
        public async Task GetById_Should_Return_Correct_Account()
        {
            // Arrange
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            _service = new Service.Service.AccountsService(dbContext, _mapper);
            var accountId = 1;

            // Act
            var actual = await _service.GetAccount(accountId);

            // Assert
            Assert.AreEqual(accountId, actual.Id);
        }

        [Test, Category("DB"), Category("Service")]
        public async Task GetById_Should_Return_Null_Account()
        {
            // Arrange
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            _service = new Service.Service.AccountsService(dbContext, _mapper);
            var accountsId = 9;

            // Act
            var actual = await _service.GetAccount(accountsId);

            // Assert
            Assert.IsNull(actual);
        }

        [Test, Category("DB"), Category("Service")]
        public async Task GetAccounts_Should_Return_Correct_Count()
        {
            // Arrange
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            _service = new Service.Service.AccountsService(dbContext, _mapper);
            const int accountsCount = 8;

            // Act
            var actual = _service.GetAccounts();

            // Assert
            Assert.AreEqual(accountsCount, actual.Count());
        }

        [Test, Category("DB"), Category("Service")]
        public async Task ShouldBeAbleToAddAccountAsync()
        {
            // Arrange 
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            _service = new Service.Service.AccountsService(dbContext, _mapper);
            AccountDTO account = new AccountDTO()
            {
                Name = "blhs",
                // Balance = 1,5 ,
                // Type = ,
                IsActive = true,
                ClientId = 1

            };

            //Act
            var response = _service.SaveAccount(account);
            var item = dbContext.Accounts.Find(response.Id);

            // Assert
            Assert.AreEqual(item.Name, response.Name);
            Assert.AreEqual(item.Balance, response.Balance);
            Assert.AreEqual(item.Type, response.Type);
            Assert.AreEqual(item.IsActive, response.IsActive);
            Assert.AreEqual(item.ClientId, response.ClientId);
        }

        [Test, Category("DB"), Category("Service")]
        public async Task ShouldBeAbleToDeleteAccountAsync()
        {
            // Arrange 
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            _service = new Service.Service.AccountsService(dbContext, _mapper);
            int accountId = 1;

            //Act
            var response = _service.DeleteAccount(accountId);

            // Assert
            Assert.IsTrue(response);
            Assert.AreEqual(7, dbContext.Accounts.Count());
            Assert.IsNull(dbContext.Accounts.Find(accountId));
        }

        [Test, Category("DB"), Category("Service")]
        public async Task ShouldNotToDeleteAccountAsync()
        {
            // Arrange 
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            _service = new Service.Service.AccountsService(dbContext, _mapper);
            int accountId = 9;

            //Act
            var response = _service.DeleteAccount(accountId);

            // Assert
            Assert.IsFalse(response);
            Assert.AreEqual(8, dbContext.Accounts.Count());
            Assert.IsNull(dbContext.Accounts.Find(accountId));
        }

        [Test, Category("DB"), Category("Service")]
        public async Task ShouldBeAbleToUpdateAccountAsync()
        {
            // Arrange 
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            _service = new Service.Service.AccountsService(dbContext, _mapper);
            Account accountEntity = new Account()
            {
                Name = "blhs",
                // Balance = 1,5 ,
                // Type = ,
                IsActive = true,
                ClientId = 1
            };
            dbContext.Accounts.Add(accountEntity);
            dbContext.SaveChanges();

            AccountDTO accountDTO = new AccountDTO()
            {
                Name = "Edited",
                // Balance = 1,8 ,
                // Type = ,
                IsActive = false,
                ClientId = 1
            };

            //Act
            var response = _service.PutAccount(accountEntity.Id, accountDTO);

            // Assert
            Assert.AreEqual(accountDTO.Name, response.Name);
            Assert.AreEqual(accountDTO.Balance, response.Balance);
            Assert.AreEqual(accountDTO.Type, response.Type);
            Assert.AreEqual(accountDTO.IsActive, response.IsActive);
            Assert.AreEqual(accountDTO.ClientId, response.ClientId);
        }

        [Test, Category("DB"), Category("Service")]
        public async Task ShouldNotUpdateAccountAsync()
        {
            // Arrange 
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            _service = new Service.Service.AccountsService(dbContext, _mapper);
            AccountDTO accountDto = new AccountDTO()
            {
                Name = "Edited",
                // Balance = 1,8 ,
                // Type = ,
                IsActive = false,
                ClientId = 1
            };

            //Act
            var ex = Assert.Throws<Exception>(() => _service.PutAccount(accountDto.Id, accountDto));

            // Assert
            Assert.That(ex.Message == "Account not found");
        }
    }
}
