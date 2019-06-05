using System;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;

namespace SpaceTaxi_1.Customer {
    public class CustomerEntity {
        private string filename;
        private string[] customerInfo;
        private string customerName; //
        private string counter; //
        private string platform;
        public string destination; //
        public string timelimit; //
        public string points; //
        public Entity Entity; //
        private TimedEvent countTime; //
        public TimedEvent timeToDrop; //
        public bool pickedUp = false;


        public CustomerEntity(string filename) {
            this.filename = filename;
            customerInfo = CustomerInfo.SplitCustomerInfo(this.filename);
            this.customerName = customerInfo[1];
            counter = customerInfo[2];
            platform = customerInfo[3];
            destination = customerInfo[4];
            timelimit = customerInfo[5];
            points = customerInfo[6];
            countTime = new TimedEvent(TimeSpanType.Seconds, Int32.Parse(counter),
                "", "", "");
            timeToDrop = new TimedEvent(TimeSpanType.Seconds, Int32.Parse(timelimit),
                "", "", "");
            Entity = new Entity(new DynamicShape(new Vec2F(
                        CustomerInfo.PickupPosition(this.filename).Item1,
                        CustomerInfo.PickupPosition(this.filename).Item2),
                    new Vec2F(0.06f, 0.06f)),
                new Image(Path.Combine("Assets", "Images", "CustomerStandRight.png")));
            Entity.Shape.AsDynamicShape().Direction = new Vec2F(0, 0);

            countTime.ResetTimer();
            timeToDrop.ResetTimer();
        }


        /// <summary>
        /// Checks if customer should pop up and render in game
        /// </summary>
        /// <returns>
        /// If this is true game should render customer.
        /// </returns>
    
        public bool CountHasExpired() {
            return countTime.HasExpired();
        }

        
        /// <summary>
        /// Time limit of dropping customer of a picking them of. 
        /// </summary>
        /// <returns>
        /// True if time to drop cusotmer of has expired.
        /// </returns>
        /// <remarks>
        /// If this is true, game has ended.
        /// (because the taxi did not make it in time)
        /// </remarks>
        public bool TimeToDropHasExpired() {
            return timeToDrop.HasExpired();
        }
    }
}