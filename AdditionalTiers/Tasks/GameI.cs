namespace AdditionalTiers.Tasks {
    public sealed class GameI {
        [HarmonyPatch(typeof(GameModelLoader), nameof(GameModelLoader.Load))]
        public static class Loaded {
            public static GameModel Model { get; set; } = null!;

            [HarmonyPostfix]
            public static void Postfix(ref GameModel __result) {
                Model = __result;

                if (AdditionalTiers.Towers is not null)
                    foreach (var tower in AdditionalTiers.Towers) {
                        var nextOne = tower?.identifier;
                        try {
                            tower?.gameLoad(__result);
                        } catch (Exception ex) {
                            Logger13.Error($"Error when trying to initialize {nextOne}, {ex.Message}");
                        }
                    }
                else
                    Logger13.Error("AdditionalTiers.Towers is null! This is not good!");
            }
        }
    }
}