using System.Runtime.CompilerServices;

using AdditionalTiers.Tasks.Towers.Tier6s;

[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
[assembly: MelonInfo(typeof(AdditionalTiers.AdditionalTiers), "Additional Tier Addon", "1.6", "1330 Studios LLC")]

namespace AdditionalTiers {
    public sealed class AdditionalTiers : MelonMod {
        public static TowerTask[] Towers;
        public static string Version;

        public override void OnApplicationStart() {
            ErrorHandler.VALUE.Initialize();

            var mia = Assembly.GetCustomAttribute<MelonInfoAttribute>();

            if (mia != null)
                Version = mia.Version;
            else
                Version = "1.?";

            List<TowerTask> towers = new() {

                new HeyYa(),
                new SpaceTruckin(),
                new PlanetWaves(),
                new GreenDay(),
                new TooCold(),
                new PaintItBlack(),
                new Dynamite(),
                new Gold(),
                new GoldenApexPlasmaMaster(),
                new Tusk(),
                new CrazyDiamond(),
                new KillerQueen()
            };
            /*Assembly?.GetTypes().AsParallel().ForAll(type => {
                if (typeof(TowerTask).IsAssignableFrom(type) && !typeof(TowerTask).FullName.Equals(type?.FullName)) {
                    var tower = (TowerTask)Activator.CreateInstance(type);
                    while (tower is null)
                        tower = (TowerTask)Activator.CreateInstance(type);

                    if ((long)tower?.tower != -1)
                        towers?.Add(tower);
                }
            });

            Towers = towers?.ToArray();*/

            Towers = towers.ToArray();

            if (!MelonPreferences.HasEntry("Additional Tier Addon Config", "Tier 6 required pop count multiplier")) {
                MelonPreferences.CreateCategory("Additional Tier Addon Config", "Additional Tier Addon Config");
                MelonPreferences.CreateEntry("Additional Tier Addon Config", "Tier 6 required pop count multiplier", (float)1);
                MelonPreferences.CreateEntry("Additional Tier Addon Config", "Display Format", ADisplay.style);
            }

            Globals.Load();

            HarmonyInstance?.Patch(Method(typeof(Tower), nameof(Tower.Hilight)), postfix: new HarmonyMethod(Method(typeof(HighlightManager), nameof(HighlightManager.Highlight))));
            HarmonyInstance?.Patch(Method(typeof(Tower), nameof(Tower.UnHighlight)), postfix: new HarmonyMethod(Method(typeof(HighlightManager), nameof(HighlightManager.UnHighlight))));

            LoggerInstance?.Msg(ConsoleColor.Red, "Additional Tier Addon Loaded!");

            Logger13.Log("Success!");

            InternalVerification.Verify();

            CacheBuilder.Build();
            DisplayFactory.Build();

            UpdateHelper.Init();
        }

        public override void OnApplicationQuit() {
            LoggerInstance?.Msg($"Last Win32 Error - {Marshal.GetLastWin32Error()}");
            DisplayFactory.Flush();
            CacheBuilder.Flush();
        }

        public override void OnGUI() {
            Watermark();
            ErrorHandler.VALUE.OnGUI();
            //UpdateHelper.OnGUI();
        }

        public override void OnUpdate() {
            if (!DisplayFactory.hasBeenBuilt)
                DisplayFactory.Build();

            if (InGame.instance == null || InGame.instance.bridge == null || InGame.instance.bridge.GetAllTowers() == null) return;

            var allAdditionalTiers = Towers;
            for (var indexOfTowers = 0; indexOfTowers < InGame.instance?.bridge?.GetAllTowers().Count; indexOfTowers++) {
                var towerToSimulation = InGame.instance?.bridge?.GetAllTowers()?.ToArray()?[indexOfTowers];
                if (towerToSimulation?.destroyed == false) {
                    foreach (var addedTier in allAdditionalTiers) {
                        if (towerToSimulation != null && !addedTier.requirements(towerToSimulation)) continue;

                        var popsNeeded = (int) ((int) addedTier.tower * Globals.SixthTierPopCountMulti);

                        if (popsNeeded < towerToSimulation?.damageDealt) {
                            if (!TransformationManager.VALUE.Contains(towerToSimulation.tower))
                                addedTier?.onComplete(towerToSimulation);
                            else if (TransformationManager.VALUE.Contains(towerToSimulation.tower)) addedTier.recurring(towerToSimulation);
                        } else if (towerToSimulation != null && !TransformationManager.VALUE.Contains(towerToSimulation.tower)) {
                            ADisplay.towerdata.Add((addedTier.identifier, towerToSimulation.damageDealt, popsNeeded));
                        }
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Watermark() {
            var guiCol = GUI.color;
            GUI.color = new Color32(255, 255, 255, 50);
            var guiStyle = new GUIStyle {
                normal =
                {
                    textColor = Color.white
                }
            };
            GUI.Label(new Rect(10, Screen.height - 20, 100, 90), $"Additional Tiers Addon v{Version}", guiStyle);
            GUI.color = guiCol;
        }
    }
}