using System;
using DIKUArcade.Entities;
using DIKUArcade.Timers;

namespace SpaceTaxi_1.Customer {
    public class CustomerEntity {

        private string filename;
        private string[] customerInfo;
        private string customerName; //
        private string counter; //
        private string platform;
        private string destination;
        private string timelimit; //
        public string points; //
        public Entity customer; 
        private TimedEvent countTime; //
        private TimedEvent timeToDrop; //


        public CustomerEntity(string filename, string customerName) {
            this.filename = filename;
            customerInfo = CustomerInfo.SplitCustomerInfo(this.filename);
            this.customerName = customerInfo[1];
            counter = customerInfo[2];
            platform = customerInfo[3];
            destination = customerInfo[4];
            timelimit = customerInfo[5];
            points = customerInfo[6];
            countTime=new TimedEvent(TimeSpanType.Seconds, Int32.Parse(counter),
                "","","" );
            timeToDrop=new TimedEvent(TimeSpanType.Seconds, Int32.Parse(timelimit),
                "","","" );
        }


        //If this is true game should render customer.
        public bool CountHasExpired() {
            return countTime.HasExpired();
        }
        
        //If this is true, game has ended. (because the taxi did not make it in time
        public bool TimeToDropHasExpired() {
            return timeToDrop.HasExpired();
        }
        
        
    }
}