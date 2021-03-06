using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

using FluentAssertions;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using NLSL.SKS.Package.Services.DTOs;

using NUnit.Framework;

namespace NLSL.SKS.Package.IntegrationTests
{
    public class ParcelWebHookApiBehaviour
    {
        private HttpClient _httpClient;
        private HttpListener _listener;
        private Parcel _testParceL;
        private string baseUrl;

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            baseUrl = TestContext.Parameters.Get("baseUrl", "https://localhost:5001");
            _httpClient = new HttpClient
                          {
                              BaseAddress = new Uri(baseUrl)
                          };
            _testParceL = new Parcel
                          {
                              Weight = 1,
                              Recipient = new Recipient
                                          {
                                              City = "Berlin",
                                              Country = "Germany",
                                              Name = "Max Mustermann",
                                              Street = "Berliner Str. 44",
                                              PostalCode = "10713"
                                          },
                              Sender = new Recipient
                                       {
                                           City = "Wien",
                                           Country = "Österreich",
                                           Name = "Maxi Musti",
                                           Street = "Wienerbergstraße 20",
                                           PostalCode = "A-1120"
                                       }
                          };
        }
        
        [SetUp]
        public void Setup()
        {
          
        }

        [Test]
        public async Task AddWebHook_Success()
        {
            StringContent content = new StringContent(File.ReadAllText("warehouse_test_data"), Encoding.UTF8, "application/json");
            HttpResponseMessage warehouserequest = await _httpClient.PostAsync("/warehouse", content);
            if (!warehouserequest.IsSuccessStatusCode)
            {
                Assert.Fail();
            }

            content = new StringContent(JsonConvert.SerializeObject(_testParceL), Encoding.UTF8, "application/json");
            HttpResponseMessage resultSubmit = await _httpClient.PostAsync("/parcel", content);
            if (!resultSubmit.IsSuccessStatusCode)
            {
                Assert.Fail();
            }

            JObject obj = JObject.Parse(await resultSubmit.Content.ReadAsStringAsync());
            string trackingID = (string)obj["trackingId"];

            HttpResponseMessage resultaAddWebhook = await _httpClient.PostAsync("/parcel/" + trackingID + "/webhooks?url=test.com", null);

            if (!resultaAddWebhook.IsSuccessStatusCode)
            {
                Assert.Fail();
            }

            WebhookMessage parsedResponse = JsonConvert.DeserializeObject<WebhookMessage>(await resultaAddWebhook.Content.ReadAsStringAsync());

            HttpResponseMessage webhooks = await _httpClient.GetAsync("/parcel/" + trackingID + "/webhooks");

            string result = await webhooks.Content.ReadAsStringAsync();
            //throw new Exception(result);
            IList<WebhookResponse> listOfWebhooks = JsonConvert.DeserializeObject<IList<WebhookResponse>>(result);

            listOfWebhooks.Count.Should().Be(1);
            //throw new Exception(JsonSerializer.Serialize(listOfWebhooks[0]));
            listOfWebhooks[0].Url.Should().Be("test.com");
            listOfWebhooks[0].TrackingId.Should().Be(trackingID);
        }

        [Test]
        public async Task RemoveWebHook_Success()
        {
            StringContent content = new StringContent(File.ReadAllText("warehouse_test_data"), Encoding.UTF8, "application/json");
            HttpResponseMessage warehouserequest = await _httpClient.PostAsync("/warehouse", content);
            if (!warehouserequest.IsSuccessStatusCode)
            {
                Assert.Fail();
            }
            content = new StringContent(JsonConvert.SerializeObject(_testParceL), Encoding.UTF8, "application/json");
            HttpResponseMessage resultSubmit = await _httpClient.PostAsync("/parcel", content);
            if (!resultSubmit.IsSuccessStatusCode)
            {
                Assert.Fail();
            }

            JObject obj = JObject.Parse(await resultSubmit.Content.ReadAsStringAsync());
            string trackingID = (string)obj["trackingId"];

            HttpResponseMessage resultaAddWebhook = await _httpClient.PostAsync("/parcel/" + trackingID + "/webhooks?url=test.com", null);

            if (!resultaAddWebhook.IsSuccessStatusCode)
            {
                Assert.Fail();
            }

            WebhookMessage parsedResponse = JsonConvert.DeserializeObject<WebhookMessage>(await resultaAddWebhook.Content.ReadAsStringAsync());

            HttpResponseMessage webhooks = await _httpClient.GetAsync("/parcel/" + trackingID + "/webhooks");

            WebhookResponses listOfWebhooks = JsonConvert.DeserializeObject<WebhookResponses>(await webhooks.Content.ReadAsStringAsync());

            listOfWebhooks.Count.Should().Be(1);
            listOfWebhooks[0].Url.Should().Be("test.com");
            listOfWebhooks[0].TrackingId.Should().Be(trackingID);

            HttpResponseMessage resultDelete = await _httpClient.DeleteAsync("/parcel/webhooks/" + listOfWebhooks[0].Id);

            if (!resultDelete.IsSuccessStatusCode)
            {
                Assert.Fail();
            }

            HttpResponseMessage webhooksDeleted = await _httpClient.GetAsync("/parcel/" + trackingID + "/webhooks");

            WebhookResponses listOfWebhooksDelete = JsonConvert.DeserializeObject<WebhookResponses>(await webhooksDeleted.Content.ReadAsStringAsync());

            listOfWebhooksDelete.Count.Should().Be(0);
        }
    }
}