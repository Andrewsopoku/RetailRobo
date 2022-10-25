using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimulatedIoTEdgeDevice
{
    public class Item{
        public int id{ get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public int shelveId { get; set; }
    }

public class Purchase{
    public List<Item> itemsPurchased { get; set; }
    public double totalCost { get; set; }
}

public class RFIDCheckoutGateData{
    public int customerID{ get; set; }
    public Purchase purchase{ get; set; }
    
}

    class RFIDCheckoutGate{

        List<Item> items = new List<Item>(){
            new Item() {id=1,name="Pop corn",price=1.0,shelveId=1},
            new Item() {id=2,name="Coca cola",price=2.0,shelveId=1},
            new Item() {id=3,name="Shaving Stick",price=5.0,shelveId=2},
            new Item() {id=4,name="Water Bottle",price=10.0,shelveId=2},
            new Item() {id=5,name="Plate",price=7.0,shelveId=3},
        };

        public List<int> itemsPicker(int to,int numberOfElement)
        {
            var random = new Random();
            HashSet<int> numbers = new HashSet<int>();
            while (numbers.Count < numberOfElement)
            {
                numbers.Add(random.Next(0, to));
            }
            return numbers.ToList();
        }
       
        public RFIDCheckoutGateData newCheckoutData(){

            var itemsIndex = itemsPicker(items.Count,3);
        
            Double totalCost = 0;
            List<Item> itemsPurchased = new List<Item>();

            for(int i=0;i<itemsIndex.Count;i++){

                itemsPurchased.Add(items[itemsIndex[i]]);
                totalCost = totalCost+items[itemsIndex[i]].price;
            }

            Purchase purchase = new Purchase(){
                itemsPurchased = itemsPurchased,
                totalCost = totalCost
            };

            var rand = new Random();

            RFIDCheckoutGateData rFIDCheckoutGateData = new RFIDCheckoutGateData(){
                customerID = rand.Next(1,10),
                purchase = purchase
            };
            return rFIDCheckoutGateData;

        }
    }


}