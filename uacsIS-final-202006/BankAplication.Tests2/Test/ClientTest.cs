//using BankApplication.Data.DTOs;
//using BankApplication.Data.Models;
//using BankApplication.Service.Service;
//using Newtonsoft.Json;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;
//using BankApplication.Tests2.Services;



//namespace BankApplication.Tests2.Test
//{

//    [TestFixture]
//    class ClientTest
//    {
//        private readonly ClientService ClientService;

//        public ClientTest()
//        {
//            ClientService = new ClientService();
//        }

//        [Test, Category("API")]
//        public async Task ShouldReturnAllClientsAsync()
//        {
//            // Arrange 

//            //Act
//            var response = await ClientService.GetClient();

//            // Assert
//            Assert.AreEqual(true, response.IsSuccessStatusCode);
//            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

//            var clientResponse = JsonConvert.DeserializeObject<List<ClientDTO>>(await response.Content.ReadAsStringAsync());
//            Assert.AreEqual(5, clientResponse.Count);

//        }

//        [Test, Category("API")]
//        public async Task ShouldReturnSpecificClientAsync()
//        {
//            // Arrange 
//            const int clientId = 1;

//            //Act
//            var response = await ClientService.GetClient($"GetAll/{clientId}");

//            // Assert
//            Assert.AreEqual(true, response.IsSuccessStatusCode);
//            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

//            var clientResponse = JsonConvert.DeserializeObject<ClientDTO>(await response.Content.ReadAsStringAsync());
//            Assert.IsNotNull(clientResponse);
//            Assert.AreEqual(clientId, clientResponse.Id);
//        }

//        [Test, Category("API")]
//        public async Task ShouldBeAbleToDeleteClientAsync()
//        {
//            // Arrange 
//            const int clientId = 6;

//            //Act
//            var response = await ClientService.DeleteClient($"RemoveClient/{clientId}");

//            // Assert
//            Assert.AreEqual(true, response.IsSuccessStatusCode);
//            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

//            var deleteResponse = JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
//            Assert.AreEqual("true", deleteResponse);
//        }

//        [Test, Category("API")]
//        public async Task ShouldBeAbleToUpdateClientAsync()
//        {
//            // Arrange 
//            ClientDTO student = new ClientDTO()
//            {
//                Name = "Client",
//                PhoneNumber = "543890530",
//                Mail = "client@gmail.com",
//                // Type = 
//                AddressId = 1
//            };

//            //Act
//            var response = await ClientService.UpdateClient(client, $"UpdateClient/6");

//            // Assert
//            Assert.AreEqual(true, response.IsSuccessStatusCode);
//            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

//            var clientResponse = JsonConvert.DeserializeObject<ClientDTO>(await response.Content.ReadAsStringAsync());
//            Assert.AreEqual("client@gmail.com", clientResponse.Mail);
//            Assert.AreEqual(1, clientResponse.AddressId);

//        }

//        [Test, Category("API")]
//        public async Task ShouldBeAbleToAddNewClient()
//        {
//            // Arrange 
//            ClientDTO client = new ClientDTO()
//            {
//                Name = "Client",
//                PhoneNumber = "543890530",
//                Mail = "client@gmail.com",
//                // Type = 
//                AddressId = 1
//            };

//            //Act
//            var response = await ClientService.NewClient(client);

//            // Assert
//            Assert.AreEqual(true, response.IsSuccessStatusCode);
//            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

//            var clientResponse = JsonConvert.DeserializeObject<ClientDTO>(await response.Content.ReadAsStringAsync());
//            Assert.AreEqual("Client", clientResponse.Name);
//            Assert.AreEqual("543890530", clientResponse.PhoneNumber);
//        }

//    }
//}
