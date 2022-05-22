namespace GodlyTowers.Towers {
    public class Godzilla {
        public static string name = "Godzilla: The King of all Monsters";

        public static UpgradeModel[] GetUpgrades() {
            return new UpgradeModel[]
            {
                    new("Atomic Blasts", 7840, 0, new("GodzillaPortrait"), 0, 0, 0, "", "Atomic Blasts"),
                    new("Thermonuclear Instability", 19850, 0, new("GodzillaPortrait2"), 0, 1, 0, "", "Thermonuclear Instability")
            };
        }

        public static (TowerModel, ShopTowerDetailsModel, TowerModel[], UpgradeModel[]) GetTower(GameModel gameModel) {
            var godzillaDetails = gameModel.towerSet[0].Clone().Cast<ShopTowerDetailsModel>();
            godzillaDetails.towerId = name;
            godzillaDetails.towerIndex = GlobalTowerIndex.Index;


            if (!LocalizationManager.Instance.textTable.ContainsKey("Atomic Blasts Description"))
                LocalizationManager.Instance.textTable.Add("Atomic Blasts Description", "The radiation within Godzilla flares greatly, only to be ejected as a breath-like attack.");
            if (!LocalizationManager.Instance.textTable.ContainsKey("Thermonuclear Instability Description"))
                LocalizationManager.Instance.textTable.Add("Thermonuclear Instability Description", "With the power of Mothra's sacrifice, Godzilla has gained enough radiation to release Thermonuclear blasts, destroying anything in his way, and empowering all attacks.");

            return (GetT0(gameModel), godzillaDetails, new[] { GetT0(gameModel), GetT1(gameModel), GetT2(gameModel) }, GetUpgrades());
        }

        public static TowerModel GetT0(GameModel gameModel) {
            var godzilla = gameModel.towers[0].Clone().Cast<TowerModel>();

            godzilla.name = name;
            godzilla.baseId = name;
            godzilla.display = "Godzilla0";
            godzilla.portrait = new("GodzillaPortrait");
            godzilla.icon = new("GodzillaPortrait");
            godzilla.towerSet = "Magic";
            godzilla.emoteSpriteLarge = new("Movie");
            godzilla.radius = 15;
            godzilla.cost = 1750;
            godzilla.range = 50;
            godzilla.towerSize = TowerModel.TowerSize.XL;
            godzilla.footprint.ignoresPlacementCheck = true;
            godzilla.cachedThrowMarkerHeight = 10;
            godzilla.areaTypes = new(4);
            godzilla.areaTypes[0] = AreaType.ice;
            godzilla.areaTypes[1] = AreaType.land;
            godzilla.areaTypes[2] = AreaType.track;
            godzilla.areaTypes[3] = AreaType.water;
            godzilla.upgrades = new UpgradePathModel[] { new("Atomic Blasts", name + "-100") };
            for (var i = 0; i < godzilla.behaviors.Count; i++) {
                var b = godzilla.behaviors[i];
                if (b.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                    var att = gameModel.towers.First(a => a.name.Contains("Sauda 20"))
                        .behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<AttackModel>()).Clone()
                        .Cast<AttackModel>();
                    att.weapons[0].name = "Swipe";
                    att.weapons[0].rate = 0.95f * 4;
                    att.weapons[0].rateFrames = 56 * 4;
                    att.range = 50;
                    att.weapons[0].projectile.behaviors = att.weapons[0].projectile.behaviors
                        .Add(new KnockbackModel("KnockbackModel_", 0.2f, 0.15f, 0.3f, 29, "Knockback",
                            new("KnockbackKnockback", 0.3f, 0.2f, 0.15f)).Cast<Model>());
                    for (var j = 0; j < att.weapons[0].projectile.behaviors.Length; j++) {
                        var pb = att.weapons[0].projectile.behaviors[j];
                        if (pb.GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                            var d = pb.Cast<DamageModel>();

                            d.damage = 25;
                            d.maxDamage = 50;

                            pb = d;
                        }
                    }
                    godzilla.behaviors[i] = att;
                }

                if (b.GetIl2CppType() == Il2CppType.Of<DisplayModel>()) {
                    var display = b.Cast<DisplayModel>();
                    display.display = "Godzilla0";
                    b = display;
                }
            }

            var link = gameModel.towers.First(a => a.name.Contains("Sauda 20"))
                .behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<LinkProjectileRadiusToTowerRangeModel>())
                .Clone().Cast<LinkProjectileRadiusToTowerRangeModel>();
            link.projectileModel.behaviors =
                link.projectileModel.behaviors.Remove(a =>
                    a.name.Equals("AddBehaviorToBloonModel_MoabDeathSound"));
            link.baseTowerRange = 50;
            godzilla.behaviors = godzilla.behaviors.Add(link);
            return godzilla;
        }

        public static TowerModel GetT1(GameModel gameModel) {
            var godzilla = gameModel.towers[0].Clone().Cast<TowerModel>();

            godzilla.name = name + "-100";
            godzilla.baseId = name;
            godzilla.tier = 1;
            godzilla.tiers = new int[] { 1, 0, 0 };
            godzilla.display = "Godzilla1";
            godzilla.portrait = new("GodzillaPortrait");
            godzilla.icon = new("GodzillaPortrait");
            godzilla.towerSet = "Magic";
            godzilla.emoteSpriteLarge = new("Movie");
            godzilla.radius = 15;
            godzilla.range = 127;
            godzilla.towerSize = TowerModel.TowerSize.XL;
            godzilla.footprint.ignoresPlacementCheck = true;
            godzilla.cachedThrowMarkerHeight = 10;
            godzilla.areaTypes = new(4);
            godzilla.areaTypes[0] = AreaType.ice;
            godzilla.areaTypes[1] = AreaType.land;
            godzilla.areaTypes[2] = AreaType.track;
            godzilla.areaTypes[3] = AreaType.water;
            godzilla.appliedUpgrades = new(new[] { "Atomic Blasts" });
            godzilla.upgrades = new[] { new UpgradePathModel("Thermonuclear Instability", name + "-200") };
            for (var i = 0; i < godzilla.behaviors.Count; i++) {
                var b = godzilla.behaviors[i];
                if (b.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                    var att = gameModel.towers.First(a => a.name.Contains("SuperMonkey-200"))
                        .behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<AttackModel>()).Clone()
                        .Cast<AttackModel>();
                    att.weapons[0].rate = 0.95f * 5;
                    att.weapons[0].rateFrames = 56 * 5;
                    att.range = 127;
                    att.weapons[0].projectile.display = "GodzillaBreath";
                    att.weapons[0].projectile.behaviors = att.weapons[0].projectile.behaviors
                        .Add(new KnockbackModel("KnockbackModel_", 0.7f, 0.85f, 0.5f, 29, "Knockback",
                            new("KnockbackKnockback", 0.5f, 0.7f, 0.85f)).Cast<Model>());
                    for (var j = 0; j < att.weapons[0].projectile.behaviors.Length; j++) {
                        var pb = att.weapons[0].projectile.behaviors[j];
                        if (pb.GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                            var d = pb.Cast<DamageModel>();

                            d.damage = 30;
                            d.maxDamage = 50;

                            pb = d;
                        }
                    }
                    att.weapons[0].name = "Breath";
                    var projectile = gameModel.towers.FirstOrDefault(a => a.name.Equals("BombShooter-300")).behaviors.First((Model a) => a.GetIl2CppType() == Il2CppType.Of<AttackModel>()).Clone().Cast<AttackModel>().weapons[0].projectile;
                    var cpocm = projectile.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<CreateProjectileOnContactModel>()).Clone();
                    var csopcm = projectile.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<CreateSoundOnProjectileCollisionModel>()).Clone();
                    var ceocm = projectile.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<CreateEffectOnContactModel>()).Clone();
                    att.weapons[0].projectile.behaviors = att.weapons[0].projectile.behaviors.Add(cpocm, csopcm, ceocm);
                    godzilla.behaviors[i] = att;
                }

                if (b.GetIl2CppType() == Il2CppType.Of<DisplayModel>()) {
                    var display = b.Cast<DisplayModel>();
                    display.display = "Godzilla1";
                    b = display;
                }
            }
            var aa = GetT0(gameModel).behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<AttackModel>()).Clone().Cast<AttackModel>();
            var link = gameModel.towers.First(a => a.name.Contains("Sauda 20"))
                .behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<LinkProjectileRadiusToTowerRangeModel>())
                .Clone().Cast<LinkProjectileRadiusToTowerRangeModel>();
            link.projectileModel.radius = 50;
            link.projectileModel.pierce = 150;
            link.projectileModel.maxPierce = 1500;
            link.projectileModel.behaviors =
                link.projectileModel.behaviors.Remove(a =>
                    a.name.Equals("AddBehaviorToBloonModel_MoabDeathSound"));
            for (var i = 0; i < link.projectileModel.behaviors.Length; i++)
                if (link.projectileModel.behaviors[i].GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                    var dm = link.projectileModel.behaviors[i].Cast<DamageModel>();
                    dm.damage = 150;
                    dm.maxDamage = 200;
                }

            aa.Cast<AttackModel>().weapons[0].projectile = link.projectileModel;
            godzilla.behaviors = godzilla.behaviors.Add(aa, new OverrideCamoDetectionModel("OverrideCamoDetectionModel_", true));

            return godzilla;
        }

        public static TowerModel GetT2(GameModel gameModel) {
            var godzilla = gameModel.towers[0].Clone().Cast<TowerModel>();
            var aa = GetT0(gameModel).behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<AttackModel>()).Clone().Cast<AttackModel>();

            godzilla.name = name + "-200";
            godzilla.baseId = name;
            godzilla.tier = 2;
            godzilla.tiers = new int[] { 2, 0, 0 };
            godzilla.display = "Godzilla2";
            godzilla.portrait = new("GodzillaPortrait2");
            godzilla.icon = new("GodzillaPortrait2");
            godzilla.towerSet = "Magic";
            godzilla.emoteSpriteLarge = new("Movie");
            godzilla.radius = 15;
            godzilla.range = 150;
            godzilla.towerSize = TowerModel.TowerSize.XL;
            godzilla.footprint.ignoresPlacementCheck = true;
            godzilla.cachedThrowMarkerHeight = 10;
            godzilla.areaTypes = new(4);
            godzilla.areaTypes[0] = AreaType.ice;
            godzilla.areaTypes[1] = AreaType.land;
            godzilla.areaTypes[2] = AreaType.track;
            godzilla.areaTypes[3] = AreaType.water;
            godzilla.appliedUpgrades = new(new[] { "Atomic Blasts", "Thermonuclear Instability" });
            godzilla.upgrades = new(0);
            for (var i = 0; i < godzilla.behaviors.Count; i++) {
                var b = godzilla.behaviors[i];
                if (b.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                    var att = gameModel.towers.First(a => a.name.Contains("SuperMonkey-200"))
                        .behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<AttackModel>()).Clone()
                        .Cast<AttackModel>();
                    att.weapons[0].rate = 0.95f * 3;
                    att.weapons[0].rateFrames = 56 * 3;
                    att.range = 150;
                    att.weapons[0].projectile.display = "GodzillaBreath2";
                    for (var j = 0; j < att.weapons[0].projectile.behaviors.Length; j++) {
                        var pb = att.weapons[0].projectile.behaviors[j];
                        if (pb.GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                            var d = pb.Cast<DamageModel>();
                            d.damage = 350;
                            d.maxDamage = 500;
                            pb = d;
                        }
                    }
                    att.weapons[0].name = "Breath";
                    var projectile = gameModel.towers.FirstOrDefault(a => a.name.Equals("BombShooter-300")).behaviors.First((Model a) => a.GetIl2CppType() == Il2CppType.Of<AttackModel>()).Clone().Cast<AttackModel>().weapons[0].projectile;
                    var cpocm = projectile.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<CreateProjectileOnContactModel>()).Clone().Cast<CreateProjectileOnContactModel>();

                    cpocm.projectile.pierce = 50;

                    for (var j = 0; j < cpocm.projectile.behaviors.Length; j++)
                        if (cpocm.projectile.behaviors[j].GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                            var dm = cpocm.projectile.behaviors[j].Cast<DamageModel>();

                            dm.damage = 375;
                            dm.maxDamage = 500;

                            cpocm.projectile.behaviors[j] = dm;
                        }

                    var csopcm = projectile.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<CreateSoundOnProjectileCollisionModel>()).Clone();
                    var ceocm = projectile.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<CreateEffectOnContactModel>()).Clone();
                    var abtbm = gameModel.towers.First(a => a.name.Equals("MortarMonkey-002")).behaviors
                        .First(a => a.GetIl2CppType() == Il2CppType.Of<AttackModel>()).Clone().Cast<AttackModel>()
                        .weapons[0].projectile.behaviors.First(a =>
                            a.GetIl2CppType() == Il2CppType.Of<CreateProjectileOnExhaustFractionModel>()).
                        Cast<CreateProjectileOnExhaustFractionModel>().projectile.behaviors.
                        First(a => a.GetIl2CppType() == Il2CppType.Of<AddBehaviorToBloonModel>()).Cast<AddBehaviorToBloonModel>();
                    var dotm = abtbm.behaviors[0].Clone().Cast<DamageOverTimeModel>();
                    dotm.damage = 10;
                    abtbm.behaviors[0] = dotm;
                    cpocm.projectile.behaviors = cpocm.projectile.behaviors.Add(abtbm);
                    att.weapons[0].projectile.behaviors = att.weapons[0].projectile.behaviors.Add(cpocm, csopcm, ceocm,
                        new KnockbackModel("KnockbackModel_", 0.7f, 0.85f, 0.5f, 29, "Knockback",
                        new("KnockbackKnockback", 0.5f, 0.7f, 0.85f)).Cast<Model>(),
                    new TrackTargetWithinTimeModel("TrackTargetWithinTimeModel_",
                        9999999, true, false, 365, false, 9999999, false, 3.48f, true).Cast<Model>());
                    godzilla.behaviors[i] = att;
                }

                if (b.GetIl2CppType() == Il2CppType.Of<DisplayModel>()) {
                    var display = b.Cast<DisplayModel>();
                    display.display = "Godzilla2";
                    b = display;
                }
            }
            var link = gameModel.towers.First(a => a.name.Contains("Sauda 20"))
                .behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<LinkProjectileRadiusToTowerRangeModel>())
                .Clone().Cast<LinkProjectileRadiusToTowerRangeModel>();
            link.projectileModel.radius = 50;
            link.projectileModel.pierce = 150;
            link.projectileModel.maxPierce = 1500;
            link.projectileModel.behaviors =
                link.projectileModel.behaviors.Remove(a =>
                    a.name.Equals("AddBehaviorToBloonModel_MoabDeathSound"));
            for (var i = 0; i < link.projectileModel.behaviors.Length; i++)
                if (link.projectileModel.behaviors[i].GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                    var dm = link.projectileModel.behaviors[i].Cast<DamageModel>();
                    dm.damage = 150;
                    dm.maxDamage = 200;
                }

            aa.Cast<AttackModel>().weapons[0].projectile = link.projectileModel;
            var ab = gameModel.towers.First(a => a.name.Equals("SuperMonkey-250")).behaviors
                .First(a => a.GetIl2CppType() == Il2CppType.Of<AbilityModel>()).Clone().Cast<AbilityModel>();
            ab.name = "Thermonuclear Exhaust";
            ab.displayName = "Thermonuclear Exhaust";
            ab.icon = new("ThermoNuclearCharge");
            var aam = ab.behaviors[0].Cast<ActivateAttackModel>();
            var dam = aam.attacks[0].weapons[0].projectile.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<DamageModel>()).Cast<DamageModel>();
            dam.damage *= 50;
            godzilla.behaviors = godzilla.behaviors.Add(aa, new OverrideCamoDetectionModel("OverrideCamoDetectionModel_", true), ab);

            return godzilla;
        }

        [HarmonyPatch(typeof(Factory), nameof(Factory.FindAndSetupPrototypeAsync))]
        public sealed class PrototypeUDN_Patch {
            public static Dictionary<string, UnityDisplayNode> protos = new();

            [HarmonyPrefix]
            public static bool Prefix(Factory __instance, string objectId,
                Il2CppSystem.Action<UnityDisplayNode> onComplete) {
                if (!protos.ContainsKey(objectId) && objectId.Equals("Godzilla0")) {
                    var udn = GetGodzilla(__instance.PrototypeRoot);
                    udn.name = "Godzilla";
                    var a = Assets.LoadAsset("GodzillaMaterial");
                    udn.genericRenderers[0].material = a.Cast<Material>();
                    udn.RecalculateGenericRenderers();
                    udn.isSprite = false;
                    onComplete.Invoke(udn);
                    protos.Add(objectId, udn);
                    return false;
                }

                if (!protos.ContainsKey(objectId) && objectId.Equals("Godzilla1")) {
                    var udn = GetGodzilla(__instance.PrototypeRoot);
                    udn.name = "Godzilla1";
                    udn.RecalculateGenericRenderers();
                    udn.isSprite = false;
                    var a = Assets.LoadAsset("GodzillaMaterialBlu");
                    udn.genericRenderers[0].material = a.Cast<Material>();
                    onComplete.Invoke(udn);
                    protos.Add(objectId, udn);
                    return false;
                }

                if (!protos.ContainsKey(objectId) && objectId.Equals("Godzilla2")) {
                    var udn = GetGodzilla(__instance.PrototypeRoot);
                    udn.name = "Godzilla2";
                    udn.RecalculateGenericRenderers();
                    udn.isSprite = false;
                    var a = Assets.LoadAsset("GodzillaMaterialRed");
                    udn.genericRenderers[0].material = a.Cast<Material>();
                    onComplete.Invoke(udn);
                    protos.Add(objectId, udn);
                    return false;
                }

                if (objectId.Equals("GodzillaBreath")) {
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
                                    var text = Assets.LoadAsset("BluePlasma").Cast<Texture2D>();
                                    smr.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new(0.5f, 0.5f), 5.4f);
                                    nudn.genericRenderers[i] = smr;
                                }
                            }

                            udn = nudn;
                            onComplete.Invoke(udn);
                        }));
                    return false;
                }

                if (objectId.Equals("GodzillaBreath2")) {
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
                                    var text = Assets.LoadAsset("RedPlasma").Cast<Texture2D>();
                                    smr.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new(0.5f, 0.5f), 2.7f);
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

        public static UnityDisplayNode GetGodzilla(Transform transform) {
            var udn = Object.Instantiate(Assets.LoadAsset("Godzilla").Cast<GameObject>(), transform).AddComponent<UnityDisplayNode>();
            udn.Active = false;
            udn.transform.position = new(-3000, 0);
            udn.gameObject.AddComponent<SetScaleG>();
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
                if (reference != null && reference.guidRef.Equals("GodzillaPortrait"))
                    try {
                        var b = Assets.LoadAsset("GodzillaPortrait");
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
                    } catch { }
                if (reference != null && reference.guidRef.Equals("GodzillaPortrait2"))
                    try {
                        var b = Assets.LoadAsset("GodzillaPortrait2");
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
                    } catch { }
                if (reference != null && reference.guidRef.Equals("ThermoNuclearCharge"))
                    try {
                        var b = Assets.LoadAsset("ThermoNuclearCharge");
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
                    if (__instance.weaponModel.name.EndsWith("Swipe")) {
                        __instance.attack.tower.Node.graphic.GetComponent<Animator>().StopPlayback();
                        __instance.attack.tower.Node.graphic.GetComponent<Animator>().Play("swipe");
                    }
                    if (__instance.weaponModel.name.EndsWith("Breath")) {
                        __instance.attack.tower.Node.graphic.GetComponent<Animator>().StopPlayback();
                        __instance.attack.tower.Node.graphic.GetComponent<Animator>().Play("fireball");
                    }
                } catch (Exception) { }
            }
        }
    }
}