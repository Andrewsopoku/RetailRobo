using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace FunctionApp.Tests{
    public class AssetLocation
    {
        public Double latitude { get; set; }
        public Double longitude { get; set; }
    }

    public class AssetTrackerData
    {
        public AssetLocation assetLocation { get; set; }
        public string assetId { get; set; }
    }

    public class Item
    {
        public int id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public int shelveId { get; set; }
    }

    public class Purchase
    {
        public List<Item> itemsPurchased { get; set; }
        public double totalCost { get; set; }
    }

    public class RFIDCheckoutGateData
    {
        public int customerID { get; set; }
        public Purchase purchase { get; set; }

    }

    public class RetailRoboStruct
    {
        [JsonProperty("id")]
        public string Id {get; set;}
        public AssetTrackerData assetTrackerData { get; set; }
        public RFIDCheckoutGateData rfidCheckoutData { get; set; }
    }


    public class Summary
    {
        public int numCheckout { get; set; }
        public double totalCheckout  { get; set; }
    }
}


