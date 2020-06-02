using AutoMapper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using BankApplication.Data.DTOs;
using BankApplication.Data.Models;
using BankApplication.Service.Service;
using BankApplication.Tests2.Internal;
using BankApplication.Service.Repositories;
using System.Threading.Tasks;
using System.Linq;

namespace BankApplication.Tests2.Services
{
    [TestFixture]
    public class ClientServiceTests
    {
        private IClientsRepository _service;
        private readonly IMapper _mapper;

        public ClientServiceTests()
        {
            var config = new MapperConfiguration(mc =>
            {
                mc.AddMaps("BankApplication.Data");
            });
            _mapper = config.CreateMapper();
        }

        [Test, Category("DB"), Category("Service")]
        public async Task GetById_Should_Return_Correct_Client()
        {
            // Arrange
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            _service = new Service.Service.ClientService(dbContext, _mapper);
            var clientId = 1;

            // Act
            var actual = await _service.GetClient(clientId);

            // Assert
            Assert.AreEqual(clientId, actual.Id);
        }

        [Test, Category("DB"), Category("Service")]
        public async Task GetById_Should_Return_Null_Client()
        {
            // Arrange
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            _service = new Service.Service.ClientService(dbContext, _mapper);
            var clientId = 9;

            // Act
            var actual = await _service.GetClient(clientId);

            // Assert
            Assert.IsNull(actual);
        }

        [Test, Category("DB"), Category("Service")]
        public async Task GetClient_Should_Return_Correct_Count()
        {
            // Arrange
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            _service = new Service.Service.ClientService(dbContext, _mapper);
            const int clientCount = 4;

            // Act
            var actual = _service.GetClients();

            // Assert **
            Assert.AreEqual(clientCount, actual.Count());
        }

        [Test, Category("DB"), Category("Service")]
        public async Task ShouldBeAbleToAddClientAsync()
        {
            // Arrange 
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            _service = new Service.Service.ClientService(dbContext, _mapper);
            ClientDTO client = new ClientDTO()
            {
                Name = "Client",
                PhoneNumber = "543890530",
                Mail = "client@gmail.com",
               // Type = 
                AddressId = 1
            
            };

            //Act
            var response = _service.SaveClient(client);
            var item = dbContext.Clients.Find(response.Id);

            // Assert
            Assert.AreEqual(item.Name, response.Name);
            Assert.AreEqual(item.PhoneNumber, response.PhoneNumber);
          //  Assert.AreEqual(item.Mail, response.Mail);
          //  Assert.AreEqual(item.Type, response.Type);
            Assert.AreEqual(item.AddressId, response.AddressId);
        }

        [Test, Category("DB"), Category("Service")]
        public async Task ShouldBeAbleToDeleteClientAsync()
        {
            // Arrange 
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            _service = new Service.Service.ClientService(dbContext, _mapper);
            int clientId = 1;

            //Act
            var response = _service.DeleteClient(clientId);

            // Assert
            Assert.IsTrue(response);
            Assert.AreEqual(3, dbContext.Clients.Count());
            Assert.IsNull(dbContext.Clients.Find(clientId));
        }

        [Test, Category("DB"), Category("Service")]
        public async Task ShouldNotToDeleteClientAsync()
        {
            // Arrange 
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            _service = new Service.Service.ClientService(dbContext, _mapper);
            int clientId = 9;

            //Act
            var response = _service.DeleteClient(clientId);

            // Assert
            Assert.IsFalse(response);
            Assert.AreEqual(4, dbContext.Clients.Count());
            Assert.IsNull(dbContext.Clients.Find(clientId));
        }

        [Test, Category("DB"), Category("Service")]
        public async Task ShouldBeAbleToUpdateClientAsync()
        {
            // Arrange 
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            _service = new Service.Service.ClientService(dbContext, _mapper);
            Client clientEntity = new Client()
            {
                Name = "Client",
                PhoneNumber = "543890530",
                Mail = "client@gmail.com",
                // Type = 
                AddressId = 1
            };
            dbContext.Clients.Add(clientEntity);
            dbContext.SaveChanges();

            ClientDTO clientDto = new ClientDTO()
            {
                Name = "Edited",
                PhoneNumber = "5490530",
                Mail = "clientedit@gmail.com",
               // Type = "jygg"
                AddressId = 1,
                Id = clientEntity.Id
            };

            //Act
            var response = _service.PutClient(clientEntity.Id, clientDto);

            // Assert
            Assert.AreEqual(clientDto.Name, response.Name);
            Assert.AreEqual(clientDto.PhoneNumber, response.PhoneNumber);
            Assert.AreEqual(clientDto.Mail, response.Mail);
           // Assert.AreEqual(clientDto.Type, response.Type);
            Assert.AreEqual(clientDto.AddressId, response.AddressId);
        }

        [Test, Category("DB"), Category("Service")]
        public async Task ShouldNotUpdateClientAsync()
        {
            // Arrange 
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            _service = new Service.Service.ClientService(dbContext, _mapper);
            ClientDTO clientDto = new ClientDTO()
            {

                Name = "Edited",
                PhoneNumber = "5490530",
                Mail = "clientedit@gmail.com",
                // Type = "jygg"
                AddressId = 1,
                Id = 9
            };

            //Act
            var ex = Assert.Throws<Exception>(() => _service.PutClient(clientDto.Id, clientDto));

            // Assert
            Assert.That(ex.Message == "Client not found");
        }


    }
}
