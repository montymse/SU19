using DIKUArcade.EventBus;

namespace SpaceTaxi_1 {
    public static class GalagaBus {
        private static GameEventBus<object> eventBus;

        public static GameEventBus<object> GetBus() {
            return GalagaBus.eventBus ?? (GalagaBus.eventBus =
                       new GameEventBus<object>());
        }
    }
}