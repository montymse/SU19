using DIKUArcade.EventBus;

namespace SpaceTaxi_1 {
    public static class SpaceTaxiBus {
        private static GameEventBus<object> eventBus;

        public static GameEventBus<object> GetBus() {
            return SpaceTaxiBus.eventBus ?? (SpaceTaxiBus.eventBus =
                       new GameEventBus<object>());
        }
    }
}