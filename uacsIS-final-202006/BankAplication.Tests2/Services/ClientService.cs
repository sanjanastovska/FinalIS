using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BankApplication.Data.DTOs;

namespace BankApplication.Tests2.Services
{
    class ClientService
    {
        static HttpClient client;
        //RestApiClient restApiClient = new RestApiClient();
        private const string Url = "https://localhost:44370/api/Client/";


        public ClientService()
        {
            client = new HttpClient { BaseAddress = new Uri(Url) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public ClientService(string baseUrl)
        {
            client = new HttpClient { BaseAddress = new Uri(baseUrl) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Calls Service on specified route
        /// </summary>
        /// <param name="path">relative path or route name or template</param>
        /// <returns>Task of http response message</returns>
        public async Task<HttpResponseMessage> GetClient(string path = "Get")
        {
            return await client.GetAsync(path);
        }

        /// <summary>
        /// Makes POST on specified route
        /// </summary>
        /// <param name="client">object to be serialized</param>
        /// <param name="path">relative path or route name or template</param>
        /// <returns>Task of http response message</returns>
        public async Task<HttpResponseMessage> NewClient(ClientDTO client, string path = "NewClient")
        {
            var json = JsonConvert.SerializeObject(client);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            return await client.PostAsync(path, data);
        }

        /// <summary>
        /// Makes PUT on specified route
        /// </summary>
        /// <param name="client">object to be serialized</param>
        /// <param name="path">relative path or route name or template</param>
        /// <returns>Task of http response message</returns>
        public async Task<HttpResponseMessage> UpdateClient(ClientDTO client, string path)
        {
            var json = JsonConvert.SerializeObject(client);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            return await client.PutAsync(path, data);
        }


        /// <summary>
        /// Makes DELETE on specified route
        /// </summary>
        /// <param name="path">relative path or route name or template</param>
        /// <returns>Task of http response message</returns>
        public async Task<HttpResponseMessage> DeleteClient(string path)
        {
            return await client.DeleteAsync(path);
        }

    }
}
