using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Text.Json;
using Microsoft.Azure.Devices.Client;

namespace SimulatedIoTEdgeDevice
{

    internal class Program
    {
        private static DeviceClient edgeDeviceClient;
        private static string connectionString = "HostName=RetailRoboHub.azure-devices.net;DeviceId=RetailRoboEdgeDev;SharedAccessKey=DjflxR4sPd7rGpBVFSwvBASL7FXWnpYxEPU+5OewRdU=";
        
        private static readonly TransportType transportType = TransportType.Mqtt;
        // Connection string for device auth with your IoT hub.
        
        //"HostName=HostName=RetailRoboHub.azure-devices.net;DeviceId=RetailRoboEdgeDev;SharedAccessKey=DjflxR4sPd7rGpBVFSwvBASL7FXWnpYxEPU+5OewRdU=";
		

        private static async Task Main(string[] args)
        {
            Console.WriteLine("IoT Hub  - Simulated IoT Edge Device.");
            CheckConnectionString(args);

            // Connect to the IoT hub using the MQTT protocol
            edgeDeviceClient = DeviceClient.CreateFromConnectionString(connectionString, transportType);

            // Set up a condition to quit simulation
            Console.WriteLine("Press control-C to exit.");
            using var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                eventArgs.Cancel = true;
                cts.Cancel();
                Console.WriteLine("Exiting...");
            };

            // Run the telemetry loop
            await SendDeviceToCloudMessagesAsync(cts.Token);

            edgeDeviceClient.Dispose();
            Console.WriteLine("Device simulator finished.");
        }

        private static void CheckConnectionString(string[] args)
        {
            if (args.Any())
            {
                try
                {
                    var cs = IotHubConnectionStringBuilder.Create(args[0]);
                    connectionString = cs.ToString();
                }
                catch (Exception)
                {
                    Console.WriteLine($"Error: Unrecognizable parameter '{args[0]}' as connection string.");
                    Environment.Exit(1);
                }
            }
            else
            {
                try
                {
                    _ = IotHubConnectionStringBuilder.Create(connectionString);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error whiles validating connection string");
                    Environment.Exit(1);
                }
            }
        }

        // Async method to send simulated telemetry
        private static async Task SendDeviceToCloudMessagesAsync(CancellationToken ct)
        {
    
            AssetTracker assetTracker = new AssetTracker();
            RFIDCheckoutGate rFIDCheckoutGate = new RFIDCheckoutGate(); 

            while (!ct.IsCancellationRequested)
            {

                // Generate mock data
                AssetTrackerData newAssetTrackerData = assetTracker.newAssetData();
                RFIDCheckoutGateData newRFIDCheckoutGateData = rFIDCheckoutGate.newCheckoutData();

                // Create JSON message
                string messageBody = JsonSerializer.Serialize(
                    new
                    {
                        assetTrackerData = newAssetTrackerData,
                        rfidCheckoutData = newRFIDCheckoutGateData,
                    });
                using var message = new Message(Encoding.ASCII.GetBytes(messageBody))
                {
                    ContentType = "application/json",
                    ContentEncoding = "utf-8",
                };

                // Send the telemetry message
                await edgeDeviceClient.SendEventAsync(message);
                Console.WriteLine($"{DateTime.Now} > Sending message: {messageBody}");

                await Task.Delay(4000);
            }
        }
    }
}
