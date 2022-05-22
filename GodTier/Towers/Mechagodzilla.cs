namespace GodlyTowers.Towers;
internal static class Mechagodzilla {
    public static string name = "Mechagodzilla";

    public static UpgradeModel[] GetUpgrades() {
        return new UpgradeModel[] {
            new UpgradeModel("Stronger Engineering", 500, 0, "MG_strongerengineering".GetSpriteReference(), 0, 0, 0, "", "Stronger Engineering"),
            new UpgradeModel("Motion Sensors", 1460, 0, "MG_motionsensors".GetSpriteReference(), 0, 1, 0, "", "Motion Sensors"),
            new UpgradeModel("Laser Beams", 3888, 0, "MG_laserbeams".GetSpriteReference(), 0, 2, 0, "", "Laser Beams"),
            new UpgradeModel("Simultaneous Multithreading", 12500, 0, "MG_hyperthreading".GetSpriteReference(), 0, 3, 0, "", "Simultaneous Multithreading"),
            new UpgradeModel("Golden Shrouds", 34750, 0, "MG_goldplate".GetSpriteReference(), 0, 4, 0, "", "Golden Shrouds")
        };
    }

    public static (TowerModel, ShopTowerDetailsModel, TowerModel[], UpgradeModel[]) GetTower(GameModel gameModel) {
        var godzillaDetails = gameModel.towerSet[0].Clone().Cast<ShopTowerDetailsModel>();
        godzillaDetails.towerId = name;
        godzillaDetails.towerIndex = GlobalTowerIndex.Index;

        LocalizationManager.Instance.textTable["Stronger Engineering Description"] = "Stronger engineering with tighter bolts allow for more aggressive, faster, movement.";
        LocalizationManager.Instance.textTable["Motion Sensors Description"] = "Motion sensors allow for more accurate detection of bloons. This allows MechaGodzilla to make more precise moves on the bloon's weakpoints.";
        LocalizationManager.Instance.textTable["Laser Beams Description"] = "Driver updates enabled a hidden feature, a Waterproof 10 Petawatt 635 Nanometer Laser beam! Pop those bloons in style.";
        LocalizationManager.Instance.textTable["Simultaneous Multithreading Description"] = "A drop in upgrade to MechaGodzilla's hardware, simultaneous multithreading has been made available. Compute where bloons are and where to strike twice as fast with use of hardware multithreading.";
        LocalizationManager.Instance.textTable["Golden Shrouds Description"] = "After some budget cuts, we can now afford plating MechaGodzilla in gold. With this, MechaGodzilla gains exceptional corrosion protection, enhanced durability, heat protection, and stopping fretting degradation.";

        return (GetT0(gameModel), godzillaDetails, new[] { GetT0(gameModel), GetT1(gameModel), GetT2(gameModel), GetT3(gameModel), GetT4(gameModel), GetT5(gameModel) }, GetUpgrades());
    }

    public static TowerModel GetT0(GameModel gameModel) {
        var godzilla = gameModel.towers[0].Clone().Cast<TowerModel>();

        godzilla.name = name;
        godzilla.baseId = name;
        godzilla.display = "MechaGodzilla_Standard";
        godzilla.portrait = "MechagodzillaPortrait".GetSpriteReference();
        godzilla.icon = "MechagodzillaPortrait".GetSpriteReference();
        godzilla.towerSet = "Magic";
        godzilla.emoteSpriteLarge = new("Movie");
        godzilla.radius = 15;
        godzilla.cost = 2000;
        godzilla.range = 35;
        godzilla.towerSize = TowerModel.TowerSize.XL;
        godzilla.footprint.ignoresPlacementCheck = true;
        godzilla.areaTypes = new(4);
        godzilla.areaTypes[0] = AreaType.ice;
        godzilla.areaTypes[1] = AreaType.land;
        godzilla.areaTypes[2] = AreaType.track;
        godzilla.areaTypes[3] = AreaType.water;
        godzilla.cachedThrowMarkerHeight = 10;
        godzilla.upgrades = new UpgradePathModel[] { new("Stronger Engineering", name + "-100") };

        for (int i = 0; i < godzilla.behaviors.Length; i++) {
            if (godzilla.behaviors[i].Is<DisplayModel>(out var dm)) {
                dm.display = godzilla.display;
            }

            if (godzilla.behaviors[i].Is<AttackModel>(out var am)) {
                am.range = 35;
                am.weapons[0].Rate = 2;
                am.weapons[0].name = "MGPunch";
                am.weapons[0].emission = new InstantDamageEmissionModel("InstantDamageEmissionModel_", null);
                am.weapons[0].projectile.display = "";
                am.weapons[0].projectile.radius *= 5;
                am.weapons[0].projectile.pierce = 9999999;
                am.weapons[0].projectile.ignorePierceExhaustion = true;
                for (int j = 0; j < am.weapons[0].projectile.behaviors.Length; j++) {
                    if (am.weapons[0].projectile.behaviors[j].Is<TravelStraitModel>(out var tsm)) {
                        tsm.Lifespan = 2f;
                        tsm.Speed = 0.001f;
                    }
                    if (am.weapons[0].projectile.behaviors[j].Is<DamageModel>(out var damage)) {
                        damage.damage = 15;
                        damage.immuneBloonProperties = BloonProperties.None;
                        damage.immuneBloonPropertiesOriginal = BloonProperties.None;
                    }
                }
            }
        }

        return godzilla;
    }

    public static TowerModel GetT1(GameModel gameModel) {
        var godzilla = GetT0(gameModel);

        godzilla.name = name + "-100";
        godzilla.baseId = name;
        godzilla.tier = 1;
        godzilla.tiers = new int[] { 1, 0, 0 };
        godzilla.display = "MechaGodzilla_Standard";
        godzilla.portrait = new("MechagodzillaPortrait");
        godzilla.icon = new("MechagodzillaPortrait");
        godzilla.towerSet = "Magic";
        godzilla.emoteSpriteLarge = new("Movie");
        godzilla.towerSize = TowerModel.TowerSize.XL;
        godzilla.footprint.ignoresPlacementCheck = true;
        godzilla.cachedThrowMarkerHeight = 10;
        godzilla.upgrades = new[] { new UpgradePathModel("Motion Sensors", name + "-200") };

        for (int i = 0; i < godzilla.behaviors.Length; i++) {
            if (godzilla.behaviors[i].Is<DisplayModel>(out var dm)) {
                dm.display = godzilla.display;
            }

            if (godzilla.behaviors[i].Is<AttackModel>(out var am)) {
                am.weapons[0].Rate *= 0.9f;
            }
        }

        return godzilla;
    }

    public static TowerModel GetT2(GameModel gameModel) {
        var godzilla = GetT1(gameModel);

        godzilla.name = name + "-200";
        godzilla.baseId = name;
        godzilla.tier = 2;
        godzilla.tiers = new int[] { 2, 0, 0 };
        godzilla.display = "MechaGodzilla_Standard";
        godzilla.portrait = new("MechagodzillaPortrait");
        godzilla.icon = new("MechagodzillaPortrait");
        godzilla.towerSet = "Magic";
        godzilla.emoteSpriteLarge = new("Movie");
        godzilla.footprint.ignoresPlacementCheck = true;
        godzilla.cachedThrowMarkerHeight = 10;
        godzilla.upgrades = new[] { new UpgradePathModel("Laser Beams", name + "-300") };

        for (int i = 0; i < godzilla.behaviors.Length; i++) {
            if (godzilla.behaviors[i].Is<DisplayModel>(out var dm)) {
                dm.display = godzilla.display;
            }

            if (godzilla.behaviors[i].Is<AttackModel>(out var am)) {
                for (int j = 0; j < am.weapons[0].projectile.behaviors.Length; j++) {
                    if (am.weapons[0].projectile.behaviors[j].Is<DamageModel>(out var damage)) {
                        damage.damage = 30;
                        damage.immuneBloonProperties = BloonProperties.None;
                        damage.immuneBloonPropertiesOriginal = BloonProperties.None;
                    }
                }
            }
        }

        godzilla.behaviors = godzilla.behaviors.Add(new OverrideCamoDetectionModel("OCDM_", true));

        return godzilla;
    }

    public static TowerModel GetT3(GameModel gameModel) {
        var godzilla = GetT2(gameModel);

        godzilla.name = name + "-300";
        godzilla.baseId = name;
        godzilla.tier = 3;
        godzilla.tiers = new int[] { 3, 0, 0 };
        godzilla.display = "MechaGodzilla_Standard";
        godzilla.portrait = new("MechagodzillaPortrait");
        godzilla.icon = new("MechagodzillaPortrait");
        godzilla.towerSet = "Magic";
        godzilla.emoteSpriteLarge = new("Movie");
        godzilla.footprint.ignoresPlacementCheck = true;
        godzilla.cachedThrowMarkerHeight = 10;
        godzilla.range = 115;
        godzilla.upgrades = new[] { new UpgradePathModel("Simultaneous Multithreading", name + "-400") };

        var am = gameModel.towers[0].CloneCast().behaviors.First(a => a.Is<AttackModel>()).Cast<AttackModel>();

        am.range = 115;
        am.weapons[0].Rate *= 2;
        am.weapons[0].name = "MGLaser";
        am.weapons[0].projectile.display = "MechaGodzilla_Laser";
        am.weapons[0].ejectY = 3;
        am.weapons[0].projectile.behaviors = am.weapons[0].projectile.behaviors.Add(new DamageModifierForTagModel("DamageModifierForTagModel_", "Ceramic", 2, 0, false, true));
        am.weapons[0].animationOffset = 0.65f;
        am.weapons[0].projectile.pierce = 9999999;
        am.weapons[0].projectile.ignorePierceExhaustion = true;

        for (int j = 0; j < am.weapons[0].projectile.behaviors.Length; j++) {
            if (am.weapons[0].projectile.behaviors[j].Is<TravelStraitModel>(out var tsm)) {
                tsm.Lifespan /= 3.5f;
                tsm.Speed *= 3f;
            }
            if (am.weapons[0].projectile.behaviors[j].Is<DamageModel>(out var damage)) {
                damage.damage = 5;
                damage.immuneBloonProperties = BloonProperties.None;
                damage.immuneBloonPropertiesOriginal = BloonProperties.None;
            }
        }

        for (int i = 0; i < godzilla.behaviors.Length; i++) {
            if (godzilla.behaviors[i].Is<DisplayModel>(out var dm)) {
                dm.display = godzilla.display;
            }
        }

        godzilla.behaviors = godzilla.behaviors.Add(am);

        return godzilla;
    }

    public static TowerModel GetT4(GameModel gameModel) {
        var godzilla = GetT3(gameModel);

        godzilla.name = name + "-400";
        godzilla.baseId = name;
        godzilla.tier = 4;
        godzilla.tiers = new int[] { 4, 0, 0 };
        godzilla.display = "MechaGodzilla_Standard";
        godzilla.portrait = new("MechagodzillaPortrait");
        godzilla.icon = new("MechagodzillaPortrait");
        godzilla.towerSet = "Magic";
        godzilla.emoteSpriteLarge = new("Movie");
        godzilla.footprint.ignoresPlacementCheck = true;
        godzilla.cachedThrowMarkerHeight = 10;
        godzilla.upgrades = new[] { new UpgradePathModel("Golden Shrouds", name + "-500") };

        for (int i = 0; i < godzilla.behaviors.Length; i++) {
            if (godzilla.behaviors[i].Is<DisplayModel>(out var dm)) {
                dm.display = godzilla.display;
            }

            if (godzilla.behaviors[i].Is<AttackModel>(out var am)) {
                am.weapons[0].Rate *= 0.5f;

                if (am.weapons[0].name.Contains("MGLaser", StringComparison.OrdinalIgnoreCase)) {
                    am.weapons[0].emission = new ArcEmissionModel("AEM_", 3, 0, 15, null, false);
                }

                for (int j = 0; j < am.weapons[0].projectile.behaviors.Length; j++) {
                    if (am.weapons[0].projectile.behaviors[j].Is<DamageModel>(out var damage)) {
                        damage.damage *= 2;
                    }
                }
            }
        }

        return godzilla;
    }

    public static TowerModel GetT5(GameModel gameModel) {
        var godzilla = GetT4(gameModel);

        godzilla.name = name + "-500";
        godzilla.baseId = name;
        godzilla.tier = 5;
        godzilla.tiers = new int[] { 5, 0, 0 };
        godzilla.display = "MechaGodzilla_Gold";
        godzilla.portrait = new("MechagodzillaPortrait");
        godzilla.icon = new("MechagodzillaPortrait");
        godzilla.towerSet = "Magic";
        godzilla.emoteSpriteLarge = new("Movie");
        godzilla.footprint.ignoresPlacementCheck = true;
        godzilla.cachedThrowMarkerHeight = 10;
        godzilla.upgrades = new(0);

        for (int i = 0; i < godzilla.behaviors.Length; i++) {
            if (godzilla.behaviors[i].Is<DisplayModel>(out var dm)) {
                dm.display = godzilla.display;
            }

            if (godzilla.behaviors[i].Is<AttackModel>(out var am)) {
                am.weapons[0].Rate *= 0.75f;

                if (am.weapons[0].name.Contains("MGLaser", StringComparison.OrdinalIgnoreCase)) {
                    am.weapons[0].emission = new ArcEmissionModel("AEM_", 5, 0, 20, null, false);
                    am.weapons[0].projectile.display = "MechaGodzilla_BLaser";
                }

                for (int j = 0; j < am.weapons[0].projectile.behaviors.Length; j++) {
                    if (am.weapons[0].projectile.behaviors[j].Is<DamageModel>(out var damage)) {
                        damage.damage *= 500;
                        damage.immuneBloonProperties = BloonProperties.None;
                        damage.immuneBloonPropertiesOriginal = BloonProperties.None;
                    }

                    if (am.weapons[0].name.Contains("MGLaser", StringComparison.OrdinalIgnoreCase) && am.weapons[0].projectile.behaviors[j].Is<TravelStraitModel>(out var tsm)) {
                        tsm.Lifespan += 2;
                    }
                }
            }
        }

        return godzilla;
    }

    [HarmonyPatch(typeof(Factory), nameof(Factory.FindAndSetupPrototypeAsync))]
    public sealed class PrototypeUDN_Patch {
        public static Dictionary<string, UnityDisplayNode> protos = new();

        [HarmonyPrefix]
        public static bool Prefix(Factory __instance, string objectId, Il2CppSystem.Action<UnityDisplayNode> onComplete) {
            if (!protos.ContainsKey(objectId) && objectId.Equals("MechaGodzilla_Standard")) {
                var udn = GetMechaGodzilla_Standard(__instance.PrototypeRoot);
                udn.name = "MechaGodzilla Standard";
                udn.RecalculateGenericRenderers();
                udn.isSprite = false;
                onComplete.Invoke(udn);
                protos.Add(objectId, udn);
                return false;
            }
            if (!protos.ContainsKey(objectId) && objectId.Equals("MechaGodzilla_Gold")) {
                var udn = GetMechaGodzilla_Gold(__instance.PrototypeRoot);
                udn.name = "MechaGodzilla Gold";
                udn.RecalculateGenericRenderers();
                udn.isSprite = false;
                onComplete.Invoke(udn);
                protos.Add(objectId, udn);
                return false;
            }

            if (objectId.Equals("MechaGodzilla_Laser")) {
                UnityDisplayNode udn = null;
                __instance.FindAndSetupPrototypeAsync("bdbeaa256e6c63b45829535831843376",
                    new Action<UnityDisplayNode>(oudn => {
                        var nudn = Object.Instantiate(oudn, __instance.PrototypeRoot);
                        nudn.name = objectId + "(Clone)";
                        nudn.isSprite = true;
                        nudn.RecalculateGenericRenderers();
                        for (var i = 0; i < nudn.genericRenderers.Length; i++) {
                            if (nudn.genericRenderers[i].GetIl2CppType() == Il2CppType.Of<SpriteRenderer>()) {
                                var smr = nudn.genericRenderers[i].Cast<SpriteRenderer>();
                                var text = Assets.LoadAsset("Laser").Cast<Texture2D>();
                                smr.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new(0.5f, 0.5f), 5.4f);
                                nudn.genericRenderers[i] = smr;
                            }
                        }

                        udn = nudn;
                        onComplete.Invoke(udn);
                    }));
                return false;
            }

            if (objectId.Equals("MechaGodzilla_BLaser")) {
                UnityDisplayNode udn = null;
                __instance.FindAndSetupPrototypeAsync("bdbeaa256e6c63b45829535831843376",
                    new Action<UnityDisplayNode>(oudn => {
                        var nudn = Object.Instantiate(oudn, __instance.PrototypeRoot);
                        nudn.name = objectId + "(Clone)";
                        nudn.isSprite = true;
                        nudn.RecalculateGenericRenderers();
                        for (var i = 0; i < nudn.genericRenderers.Length; i++) {
                            if (nudn.genericRenderers[i].GetIl2CppType() == Il2CppType.Of<SpriteRenderer>()) {
                                var smr = nudn.genericRenderers[i].Cast<SpriteRenderer>();
                                var text = Assets.LoadAsset("BLaser").Cast<Texture2D>();
                                smr.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new(0.5f, 0.5f), 5.4f);
                                nudn.genericRenderers[i] = smr;
                            }
                        }

                        udn = nudn;
                        onComplete.Invoke(udn);
                    }));
                return false;
            }

            if (protos.ContainsKey(objectId)) {
                onComplete.Invoke(protos[objectId]);
                return false;
            }

            return true;
        }
    }

    public static AssetBundle Assets { get; set; }

    public static UnityDisplayNode GetMechaGodzilla_Standard(Transform transform) {
        var udn = Object.Instantiate(Assets.LoadAsset("MechaGodzilla_Standard").Cast<GameObject>(), transform).AddComponent<UnityDisplayNode>();
        udn.Active = false;
        udn.transform.position = new(-3000, 0);
        udn.gameObject.AddComponent<SetScaleMG>();
        return udn;
    }

    public static UnityDisplayNode GetMechaGodzilla_Gold(Transform transform) {
        var udn = Object.Instantiate(Assets.LoadAsset("MechaGodzilla_Gold").Cast<GameObject>(), transform).AddComponent<UnityDisplayNode>();
        udn.Active = false;
        udn.transform.position = new(-3000, 0);
        udn.gameObject.AddComponent<SetScaleMG>();
        return udn;
    }

    [HarmonyPatch(typeof(Factory), nameof(Factory.ProtoFlush))]
    public sealed class PrototypeFlushUDN_Patch {
        [HarmonyPostfix]
        public static void Postfix() {
            foreach (var proto in PrototypeUDN_Patch.protos.Values)
                Object.Destroy(proto.gameObject);
            PrototypeUDN_Patch.protos.Clear();
        }
    }



    [HarmonyPatch(typeof(ResourceLoader), nameof(ResourceLoader.LoadSpriteFromSpriteReferenceAsync))]
    public sealed class ResourceLoader_Patch {
        [HarmonyPostfix]
        public static void Postfix(SpriteReference reference, ref Image image) {
            if (reference != null && reference.guidRef.Equals("MechagodzillaPortrait"))
                try {
                    var b = Assets.LoadAsset("MG_Por");
                    if (b != null) {
                        var text = b.Cast<Texture2D>();
                        image.canvasRenderer.SetTexture(text);
                        image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                    }
                } catch { }
            if (reference != null && reference.guidRef.Equals("MG_goldplate"))
                try {
                    var b = Assets.LoadAsset("goldplate");
                    if (b != null) {
                        var text = b.Cast<Texture2D>();
                        image.canvasRenderer.SetTexture(text);
                        image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                    }
                } catch { }
            if (reference != null && reference.guidRef.Equals("MG_hyperthreading"))
                try {
                    var b = Assets.LoadAsset("hyperthreading");
                    if (b != null) {
                        var text = b.Cast<Texture2D>();
                        image.canvasRenderer.SetTexture(text);
                        image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                    }
                } catch { }
            if (reference != null && reference.guidRef.Equals("MG_laserbeams"))
                try {
                    var b = Assets.LoadAsset("laserbeams");
                    if (b != null) {
                        var text = b.Cast<Texture2D>();
                        image.canvasRenderer.SetTexture(text);
                        image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                    }
                } catch { }
            if (reference != null && reference.guidRef.Equals("MG_motionsensors"))
                try {
                    var b = Assets.LoadAsset("motionsensors");
                    if (b != null) {
                        var text = b.Cast<Texture2D>();
                        image.canvasRenderer.SetTexture(text);
                        image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                    }
                } catch { }
            if (reference != null && reference.guidRef.Equals("MG_strongerengineering"))
                try {
                    var b = Assets.LoadAsset("strongerengineering");
                    if (b != null) {
                        var text = b.Cast<Texture2D>();
                        image.canvasRenderer.SetTexture(text);
                        image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                    }
                } catch { }
        }
    }

    [HarmonyPatch(typeof(Weapon), nameof(Weapon.SpawnDart))]
    public static class WI {
        [HarmonyPostfix]
        private static void Postfix(Weapon __instance) {
            if (__instance == null) return;
            if (__instance.weaponModel == null) return;
            if (__instance.weaponModel.name == null) return;
            if (__instance.attack == null) return;
            if (__instance.attack.tower == null) return;
            if (__instance.attack.tower.Node == null) return;
            if (__instance.attack.tower.Node.graphic == null) return;

            try {

                if (__instance.weaponModel.name.EndsWith("MGPunch")) {
                    __instance.attack.tower.Node.graphic.GetComponent<Animator>().StopPlayback();
                    __instance.attack.tower.Node.graphic.GetComponent<Animator>().Play("drill");
                }

                if (__instance.weaponModel.name.EndsWith("MGLaser")) {
                    __instance.attack.tower.Node.graphic.GetComponent<Animator>().StopPlayback();
                    __instance.attack.tower.Node.graphic.GetComponent<Animator>().Play("laser");
                }

            } catch { }
        }
    }
}
