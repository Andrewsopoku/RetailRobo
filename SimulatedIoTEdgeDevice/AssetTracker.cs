using System.Text.Json;
using System;

namespace SimulatedIoTEdgeDevice
{
    public class AssetLocation{
        public Double latitude { get; set; }
        public Double longitude { get; set; }
    }

    public class AssetTrackerData{
        public AssetLocation assetLocation { get; set; }
        public string assetId { get; set; }
    }
    class AssetTracker{

        public static double NextDouble(double MinValue, double MaxValue){
            var rand = new Random();
            return rand.NextDouble() * (MaxValue - MinValue) + MinValue;
        }
        public AssetTrackerData newAssetData(){

            var rand = new Random();

//             Latitude: 53.228265 / N 53° 13' 41.754''
// Longitude: -0.553932 / W 0° 33' 14.155''

// 53.22746406408652,-0.5519127845764161

// 53.22849168492203, -0.5511832237243653

// 53.22906971080426,-0.5537152290344239

           AssetLocation assetLocation = new AssetLocation{
                latitude = NextDouble(53.2272,53.24),
                longitude = NextDouble(-0.551,-0.554)}; 

           AssetTrackerData assetTrackerData = new AssetTrackerData{
               assetLocation = assetLocation,
               assetId = rand.Next(50).ToString()
           };
            return assetTrackerData;
        }
    }


}