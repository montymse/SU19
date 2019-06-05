using System;
using SpaceTaxi_1.Customer;

namespace SpaceTaxi_1.TaxiTour {
    public class PickUp {
        
        /// <summary>
        /// Handles the customers taxi tour
        /// (the timers and the customer getting picked up)
        /// </summary>
        public void TaxiTour(Player player, CustomerEntity customer) {
            if (player.physics.IsGrounded &&
                Math.Abs(customer.Entity.Shape.Position.Y - player.Entity.Shape.Position.Y) < 0.05
                && customer.CountHasExpired()) {
                customer.pickedUp = true;
                customer.timeToDrop.ResetTimer();
            }

            if (customer.pickedUp && customer.TimeToDropHasExpired()) {
                player.Entity.DeleteEntity();
            }
        }
    }
}