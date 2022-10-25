using System.Net.Http;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.OpenApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
namespace FunctionApp.Tests
{
 

    [TestClass]
    public class HttpTriggerTests
    {
        private HttpClient _http;

        [TestInitialize]
        public void Initialize()
        {
            this._http = new HttpClient();
        }

        [TestCleanup]
        public void Cleanup()
        {
            this._http.Dispose();
        }

        [TestMethod]
        public async Task TestCustomerCheckout()
        {
            var response = await this._http.GetStringAsync("http://localhost:7071/api/checkout").ConfigureAwait(false);

            var doc = JsonConvert.DeserializeObject<List<RFIDCheckoutGateData>>(response);

            // Check to see if not empty
            doc.Should().NotBeNull();
           
            // Check the type to confirm if it is RFIDCheckoutGateData DataClass
            doc[0].Should().BeOfType<RFIDCheckoutGateData>();

            // Check the total purchase cost is greater than 0
            doc[0].purchase.totalCost.Should().BeGreaterThan(0);
        
        }

        [TestMethod]
        public async Task TestAssetsLocation()
        {
            var response = await this._http.GetStringAsync("http://localhost:7071/api/asset").ConfigureAwait(false);

            var doc = JsonConvert.DeserializeObject<List<AssetTrackerData>>(response);

            // Check to see if not empty
            doc.Should().NotBeNull();
           
            // Check the type to confirm if it is AssetTrackerData DataClass
            doc[0].Should().BeOfType<AssetTrackerData>();
        
        }
    }
}