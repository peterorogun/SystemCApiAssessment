using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemCApiAssessment
{
    public class SystemCApiAssessment
    {
        private string url = "https://qarecruitment.azurewebsites.net";

        [Test]
        public void GetAllProducts()
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest("/v1/products", Method.GET);
            var response = client.Execute<List<Root>>(request);

            Assert.AreEqual(200, (int)response.StatusCode);
            Assert.AreEqual(152, response.Data.Count);
        }

        [Test]
        public void GetSpecificProduct()
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest("/v1/product/01a1498f-ed85-48d2-869d-c686c6e185bb", Method.GET);
            var response = client.Execute<List<Root>>(request);

            Assert.AreEqual(200, (int)response.StatusCode);
            Assert.AreEqual(1, response.Data.Count);
        }

        [Test]
        public async Task PostProduct()
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest("/v1/product", Method.POST);
            request.RequestFormat = DataFormat.Json;
            var payload = request.AddJsonBody(new Root()
            {
                id = "110",
                name = "Mercedes Bez 120",
                price = 100,
                itemCount = 25,
                isActive = true,
                createdDateTime = DateTime.Now,
                modifiedDateTime = DateTime.Now
            });
            var response = client.Execute<List<Root>>(request);

            Assert.AreEqual(200, (int)response.StatusCode);
            Assert.AreEqual(1, response.Data.Count);
            await Task.Delay(2000);
        }

        [Test]
        public void GetNewPostProduct()
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest("/v1/products", Method.GET);
            var response = client.Execute<List<Root>>(request)
                .Data.Where(x => x.name.Equals("Mercedes Bez 102"));


            Assert.AreEqual("Mercedes Bez 102", response.FirstOrDefault().name);
            //Assert.AreEqual(142, response.Data.Count);
        }


        [Test]
        public async Task DeleteProduct()
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest("/v1/product", Method.POST);
            request.RequestFormat = DataFormat.Json;
            var payload = request.AddJsonBody(new Root()
            {
                id = "102",
                name = "Mercedes Bez 103",
                price = 100,
                itemCount = 25,
                isActive = true,
                createdDateTime = DateTime.Now,
                modifiedDateTime = DateTime.Now
            });
            var response = client.Execute<List<Root>>(request)
                .Data.Where(x => x.name.Equals("Mercedes Bez 103")).FirstOrDefault().id;

            await Task.Delay(2000);

            var client2 = new RestClient(url);
            var request2 = new RestRequest($"/v1/product/{response}", Method.DELETE);

            var response2 = client2.Execute<List<Root>>(request2);
            Assert.AreEqual(200, (int)response2.StatusCode);
            Assert.AreEqual(false, response2.Data[0].isActive);
        }

        [Test]
        public async Task PUTRequest()
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest("/v1/product", Method.POST);
            request.RequestFormat = DataFormat.Json;
            var payload = request.AddJsonBody(new Root()
            {
                id = "105",
                name = "Mercedes Bez 106",
                price = 100,
                itemCount = 25,
                isActive = true,
                createdDateTime = DateTime.Now,
                modifiedDateTime = DateTime.Now
            });
            var response = client.Execute<List<Root>>(request)
                .Data.Where(x => x.name.Equals("Mercedes Bez 106")).FirstOrDefault().id;

            await Task.Delay(2000);

            var client2 = new RestClient(url);
            var request2 = new RestRequest($"/v1/product/{response}", Method.PUT);

            request2.RequestFormat = DataFormat.Json;
            var payload2 = request2.AddJsonBody(new Root()
            {
                id = $"{response}",
                name = "Mercedes Bez 108",
                price = 400,
                itemCount = 20,
                isActive = true,
                createdDateTime = DateTime.Now,
                modifiedDateTime = DateTime.Now
            });

            var response2 = client2.Execute<List<Root>>(request2);
            Assert.AreEqual(200, (int)response2.StatusCode);
            Assert.AreEqual(400, response2.Data[0].price);
            Assert.AreEqual(20, response2.Data[0].itemCount);
        }

    }
}