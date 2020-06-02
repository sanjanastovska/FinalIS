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
    class AccountService
    {
        static HttpClient client;
        //RestApiClient restApiClient = new RestApiClient();
        private const string Url = "https://localhost:44370/api/Account/";

        public AccountService()
        {
            client = new HttpClient { BaseAddress = new Uri(Url) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public AccountService(string baseUrl)
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
        public async Task<HttpResponseMessage> GetAccount(string path = "Get")
        {
            return await client.GetAsync(path);
        }

        /// <summary>
        /// Makes POST on specified route
        /// </summary>
        /// <param name="client">object to be serialized</param>
        /// <param name="path">relative path or route name or template</param>
        /// <returns>Task of http response message</returns>
        public async Task<HttpResponseMessage> NewAccount(AccountDTO account, string path = "NewAccount")
        {
            var json = JsonConvert.SerializeObject(account);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            return await client.PostAsync(path, data);
        }

        /// <summary>
        /// Makes PUT on specified route
        /// </summary>
        /// <param name="client">object to be serialized</param>
        /// <param name="path">relative path or route name or template</param>
        /// <returns>Task of http response message</returns>
        public async Task<HttpResponseMessage> UpdateAccount(AccountDTO account, string path)
        {
            var json = JsonConvert.SerializeObject(account);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            return await client.PutAsync(path, data);
        }


        /// <summary>
        /// Makes DELETE on specified route
        /// </summary>
        /// <param name="path">relative path or route name or template</param>
        /// <returns>Task of http response message</returns>
        public async Task<HttpResponseMessage> DeleteAccount(string path)
        {
            return await client.DeleteAsync(path);
        }

    }
}
