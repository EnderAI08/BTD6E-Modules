namespace GodlyTowers.Towers {
    public sealed class Venom {
        public static string name = "Venom";

        public static TowerModel T5 = null;
        private static WeaponModel WM = null;

        public static UpgradeModel[] GetUpgrades() => new UpgradeModel[] {
            new("DeadlyPunch", 980, 0, new("VenomUpgrade1"), 0, 0, 0, "", "DeadlyPunch"),
            new("SymbioticGrowth", 1280, 0, new("VenomUpgrade2"), 0, 1, 0, "", "SymbioticGrowth"),
            new("LethalProtector", 4700, 0, new("VenomUpgrade3"), 0, 2, 0, "", "LethalProtector"),
            new("TheMadness", 15666, 0, new("VenomUpgrade4"), 0, 3, 0, "", "TheMadness"),
            new("AntiVenom", 49999, 0, new("AntiVenomPortrait"), 0, 4, 0, "", "AntiVenom"),
        };

        public static (TowerModel, ShopTowerDetailsModel, TowerModel[], UpgradeModel[]) GetTower(GameModel gameModel) {
            var VenomDetails = gameModel.towerSet[0].Clone().Cast<ShopTowerDetailsModel>();
            VenomDetails.towerId = name;
            VenomDetails.towerIndex = GlobalTowerIndex.Index;

            if (!LocalizationManager.Instance.textTable.ContainsKey("Venom"))
                LocalizationManager.Instance.textTable.Add("Venom", "Venom (Earth 120703)");
            if (!LocalizationManager.Instance.textTable.ContainsKey("DeadlyPunch"))
                LocalizationManager.Instance.textTable.Add("DeadlyPunch", "Deadly Punch");
            if (!LocalizationManager.Instance.textTable.ContainsKey("DeadlyPunch Description"))
                LocalizationManager.Instance.textTable.Add("DeadlyPunch Description", "Ooo, Decisions, Decisions! What Should We Break First? Your Nose, Your Neck...Or What Passes For Your Spine?");
            if (!LocalizationManager.Instance.textTable.ContainsKey("SymbioticGrowth"))
                LocalizationManager.Instance.textTable.Add("SymbioticGrowth", "Symbiotic Growth");
            if (!LocalizationManager.Instance.textTable.ContainsKey("SymbioticGrowth Description"))
                LocalizationManager.Instance.textTable.Add("SymbioticGrowth Description", "Don't You Losers Ever Learn...That If You Mess With Venom...You Get Your Brains Eaten!");
            if (!LocalizationManager.Instance.textTable.ContainsKey("LethalProtector"))
                LocalizationManager.Instance.textTable.Add("LethalProtector", "Lethal Protector");
            if (!LocalizationManager.Instance.textTable.ContainsKey("LethalProtector Description"))
                LocalizationManager.Instance.textTable.Add("LethalProtector Description", "No One Threatens Those Under Our Protection And Walks Away! They Don't Crawl Away, Either!");
            if (!LocalizationManager.Instance.textTable.ContainsKey("TheMadness"))
                LocalizationManager.Instance.textTable.Add("TheMadness", "The Madness");
            if (!LocalizationManager.Instance.textTable.ContainsKey("TheMadness Description"))
                LocalizationManager.Instance.textTable.Add("TheMadness Description", "Venom Man! Venom Man! Friendly Symbiote Venom Man! Spins A Web, Any Size! Catches Thieves! Makes Them Die!");
            if (!LocalizationManager.Instance.textTable.ContainsKey("AntiVenom"))
                LocalizationManager.Instance.textTable.Add("AntiVenom", "Anti-Venom");
            if (!LocalizationManager.Instance.textTable.ContainsKey("AntiVenom Description"))
                LocalizationManager.Instance.textTable.Add("AntiVenom Description", "It's not a black monster from space - it's a black demon sent from the underworld. You are an addict - a damaged and wounded animal - you have a sickness. I am the cure!");

            return (GetT0(gameModel), VenomDetails, new[] { GetT0(gameModel), GetT1(gameModel), GetT2(gameModel), GetT3(gameModel), GetT4(gameModel), GetT5(gameModel) }, GetUpgrades());
        }

        public static unsafe TowerModel GetT0(GameModel gameModel) {
            var Venom = gameModel.towers.First(a=>a.name.Equals("SniperMonkey")).Clone().Cast<TowerModel>();

            Venom.name = name;
            Venom.baseId = name;
            Venom.display = "Venom";
            Venom.portrait = new("VenomPortrait");
            Venom.icon = new("VenomPortrait");
            Venom.towerSet = "Military";
            Venom.emoteSpriteLarge = new("Marvel");
            Venom.tier = 0;
            Venom.tiers = new[] { 0, 0, 0 };
            Venom.radius = 15;
            Venom.range = 30;
            Venom.cost = 1200;
            Venom.upgrades = new UpgradePathModel[] {new UpgradePathModel("DeadlyPunch", name + "-100") };

            for (var i = 0; i < Venom.behaviors.Count; i++) {
                var b = Venom.behaviors[i];
                if (b.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                    var att = b.Cast<AttackModel>();
                    att.weapons[0].name = "vscratch";
                    att.weapons[0].projectile.pierce = 1;
                    att.weapons[0].rate = 1.9f;
                    att.weapons[0].behaviors = new WeaponBehaviorModel[0];
                    WM = att.weapons[0].Clone().Cast<WeaponModel>();
                    att.range = 30;
                    Venom.behaviors[i] = att;
                }
                if (b.GetIl2CppType() == Il2CppType.Of<DisplayModel>()) {
                    var display = b.Cast<DisplayModel>();
                    display.display = "Venom";
                }
            }

            Venom.behaviors = Venom.behaviors.Add(new OverrideCamoDetectionModel("OCDM_", true));

            return Venom;
        }

        public static unsafe TowerModel GetT1(GameModel gameModel) {
            var Venom = GetT0(gameModel).Clone().Cast<TowerModel>();

            Venom.name = name + "-100";
            Venom.baseId = name;
            Venom.tier = 1;
            Venom.tiers = new[] { 1, 0, 0 };
            Venom.upgrades = new UpgradePathModel[] { new UpgradePathModel("SymbioticGrowth", name + "-200") };
            Venom.appliedUpgrades = new[] { "DeadlyPunch" };

            for (var i = 0; i < Venom.behaviors.Count; i++) {
                var b = Venom.behaviors[i];
                if (b.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                    var att = b.Cast<AttackModel>();
                    att.weapons[0].name = "vpunch";
                    for (int j = 0; j < att.weapons[0].projectile.behaviors.Length; j++) {
                        var pbeh = att.weapons[0].projectile.behaviors[j];
                        if (pbeh.GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                            var dm = pbeh.Cast<DamageModel>();
                            dm.damage = 5;
                            att.weapons[0].projectile.behaviors[j] = dm;
                        }
                    }
                    att.weapons[0].projectile.behaviors = att.weapons[0].projectile.behaviors.Add(new KnockbackModel("KnockbackModel_", 1.1f, 1.1f, 1f, 1, "KnockbackKnockback"));
                    Venom.behaviors[i] = att;
                }
            }

            return Venom;
        }

        public static unsafe TowerModel GetT2(GameModel gameModel) {
            var Venom = GetT1(gameModel).Clone().Cast<TowerModel>();

            Venom.name = name + "-200";
            Venom.baseId = name;
            Venom.display = "Venom";
            Venom.portrait = new("VenomPortrait");
            Venom.icon = new("VenomPortrait");
            Venom.towerSet = "Military";
            Venom.emoteSpriteLarge = new("Movie");
            Venom.tier = 2;
            Venom.tiers = new[] { 2, 0, 0 };
            Venom.range = 30;
            Venom.upgrades = new UpgradePathModel[] { new UpgradePathModel("LethalProtector", name + "-300") };
            Venom.appliedUpgrades = new[] { "DeadlyPunch", "SymbioticGrowth" };

            for (var i = 0; i < Venom.behaviors.Count; i++) {
                var b = Venom.behaviors[i];
                if (b.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                    var att = b.Cast<AttackModel>();
                    WM.rate *= 1.35f;
                    att.weapons = att.weapons.Add(WM);
                    Venom.behaviors[i] = att;
                }
                if (b.GetIl2CppType() == Il2CppType.Of<DisplayModel>()) {
                    var display = b.Cast<DisplayModel>();
                    display.display = "Venom";
                }
            }

            return Venom;
        }

        public static unsafe TowerModel GetT3(GameModel gameModel) {
            var Venom = GetT2(gameModel).Clone().Cast<TowerModel>();

            Venom.name = name + "-300";
            Venom.baseId = name;
            Venom.display = "Venom";
            Venom.portrait = new("VenomPortrait");
            Venom.icon = new("VenomPortrait");
            Venom.towerSet = "Military";
            Venom.emoteSpriteLarge = new("Movie");
            Venom.tier = 3;
            Venom.tiers = new[] { 3, 0, 0 };
            Venom.range = 30;
            Venom.upgrades = new UpgradePathModel[] { new UpgradePathModel("TheMadness", name + "-400") };
            Venom.appliedUpgrades = new[] { "DeadlyPunch", "SymbioticGrowth", "LethalProtector" };

            for (var i = 0; i < Venom.behaviors.Count; i++) {
                var b = Venom.behaviors[i];
                if (b.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                    var att = b.Cast<AttackModel>();
                    for (int j = 0; j < att.weapons.Length; j++) {
                        for (int k = 0; k < att.weapons[j].projectile.behaviors.Length; k++) {
                            var pbeh = att.weapons[j].projectile.behaviors[k];
                            if (pbeh.GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                                var dm = pbeh.Cast<DamageModel>();
                                dm.damage *= 5;
                                att.weapons[j].projectile.behaviors[k] = dm;
                            }
                        }
                    }
                    Venom.behaviors[i] = att;
                }
                if (b.GetIl2CppType() == Il2CppType.Of<DisplayModel>()) {
                    var display = b.Cast<DisplayModel>();
                    display.display = "Venom";
                }
            }

            return Venom;
        }

        public static unsafe TowerModel GetT4(GameModel gameModel) {
            var Venom = GetT3(gameModel).Clone().Cast<TowerModel>();

            Venom.name = name + "-400";
            Venom.baseId = name;
            Venom.display = "Venom";
            Venom.portrait = new("VenomPortrait");
            Venom.icon = new("VenomPortrait");
            Venom.towerSet = "Military";
            Venom.emoteSpriteLarge = new("Movie");
            Venom.tier = 4;
            Venom.tiers = new[] { 4, 0, 0 };
            Venom.range = 30;
            Venom.upgrades = new UpgradePathModel[] { new UpgradePathModel("AntiVenom", name + "-500") };
            Venom.appliedUpgrades = new[] { "DeadlyPunch", "SymbioticGrowth", "LethalProtector", "TheMadness" };

            for (var i = 0; i < Venom.behaviors.Count; i++) {
                var b = Venom.behaviors[i];
                if (b.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                    var att = b.Cast<AttackModel>();
                    for (int j = 0; j < att.weapons.Length; j++) {
                        att.weapons[j].rate /= 1.2f;
                        for (int k = 0; k < att.weapons[j].projectile.behaviors.Length; k++) {
                            var pbeh = att.weapons[j].projectile.behaviors[k];
                            if (pbeh.GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                                var dm = pbeh.Cast<DamageModel>();
                                dm.damage *= 15;
                                att.weapons[j].projectile.behaviors[k] = dm;
                            }
                        }
                    }
                    Venom.behaviors[i] = att;
                }
                if (b.GetIl2CppType() == Il2CppType.Of<DisplayModel>()) {
                    var display = b.Cast<DisplayModel>();
                    display.display = "Venom";
                }
            }

            return Venom;
        }

        public static unsafe TowerModel GetT5(GameModel gameModel) {
            var Venom = GetT4(gameModel).Clone().Cast<TowerModel>();

            Venom.name = name + "-500";
            Venom.baseId = name;
            Venom.display = "AVenom";
            Venom.portrait = new("AntiVenomPortrait");
            Venom.icon = new("AntiVenomPortrait");
            Venom.towerSet = "Military";
            Venom.emoteSpriteLarge = new("Movie");
            Venom.tier = 5;
            Venom.tiers = new[] { 5, 0, 0 };
            Venom.range = 50;
            Venom.upgrades = new UpgradePathModel[0];
            Venom.appliedUpgrades = new[] { "DeadlyPunch", "SymbioticGrowth", "LethalProtector", "TheMadness", "AntiVenom" };

            GodTier.GodTier.CustomUpgrades.Add("AntiVenom", GodTier.GodTier.UpgradeBG.AntiVenom);

            for (var i = 0; i < Venom.behaviors.Count; i++) {
                var b = Venom.behaviors[i];
                if (b.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                    var att = b.Cast<AttackModel>();
                    att.range = 50;
                    for (int j = 0; j < att.weapons.Length; j++) {
                        att.weapons[j].name = att.weapons[j].name.Replace("vscratch", "avscratch").Replace("vpunch", "avpunch");
                        att.weapons[j].rate /= 2f;
                        for (int k = 0; k < att.weapons[j].projectile.behaviors.Length; k++) {
                            var pbeh = att.weapons[j].projectile.behaviors[k];
                            if (pbeh.GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                                var dm = pbeh.Cast<DamageModel>();
                                dm.damage *= 150;
                                att.weapons[j].projectile.behaviors[k] = dm;
                            }
                        }
                    }
                    Venom.behaviors[i] = att;
                }
                if (b.GetIl2CppType() == Il2CppType.Of<DisplayModel>()) {
                    var display = b.Cast<DisplayModel>();
                    display.display = "AVenom";
                }
            }

            var MV520 = gameModel.towers.First(a => a.name.Equals("MonkeyVillage-520")).Clone().Cast<TowerModel>();
            var pierce = MV520.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<PierceSupportModel>()).Cast<PierceSupportModel>();
            var pspeed = MV520.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<ProjectileSpeedSupportModel>()).Cast<ProjectileSpeedSupportModel>();

            pierce.filters = new TowerFilterModel[0];
            pspeed.filters = new TowerFilterModel[0];

            Venom.behaviors = Venom.behaviors.Add(MV520.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<VisibilitySupportModel>()),
                MV520.behaviors.First(a=>a.GetIl2CppType() == Il2CppType.Of<AddBehaviorToBloonInZoneModel>()),
                pierce, pspeed);

            return Venom;
        }

        public static Texture2D LoadTextureFromBytes(byte[] FileData) {
            Texture2D Tex2D = new(2, 2);
            if (ImageConversion.LoadImage(Tex2D, FileData)) return Tex2D;

            return null;
        }

        public static Sprite LoadSprite(Texture2D text) {
            return Sprite.Create(text, new(0, 0, text.width, text.height), new());
        }

        [HarmonyPatch(typeof(Factory), nameof(Factory.FindAndSetupPrototypeAsync))]
        public class PrototypeUDN_Patch {
            public static Dictionary<string, UnityDisplayNode> protos = new();

            [HarmonyPrefix]
            public static bool Prefix(Factory __instance, string objectId, Il2CppSystem.Action<UnityDisplayNode> onComplete) {
                if (!protos.ContainsKey(objectId) && objectId.Equals("Venom")) {
                    var udn = GetVenom(__instance.PrototypeRoot);
                    udn.name = "Venom";
                    udn.RecalculateGenericRenderers();
                    udn.isSprite = false;
                    onComplete.Invoke(udn);
                    protos.Add(objectId, udn);
                    return false;
                }
                if (!protos.ContainsKey(objectId) && objectId.Equals("AVenom")) {
                    var udn = GetAVenom(__instance.PrototypeRoot);
                    udn.name = "Anti-Venom";
                    udn.RecalculateGenericRenderers();
                    udn.isSprite = false;
                    onComplete.Invoke(udn);
                    protos.Add(objectId, udn);
                    return false;
                }

                if (protos.ContainsKey(objectId)) {
                    onComplete.Invoke(protos[objectId]);
                    return false;
                }

                return true;
            }
        }

        private static AssetBundle __asset;

        public static AssetBundle Assets {
            get => __asset;
            set => __asset = value;
        }

        public static UnityDisplayNode GetVenom(Transform transform) {
            var udn = Object.Instantiate(Assets.LoadAsset("Venom").Cast<GameObject>(), transform).AddComponent<UnityDisplayNode>();
            udn.Active = false;
            udn.transform.position = new(-3000, 0);
            udn.gameObject.AddComponent<SetScale2>();
            return udn;
        }

        public static UnityDisplayNode GetAVenom(Transform transform) {
            var udn = Object.Instantiate(Assets.LoadAsset("AntiVenom").Cast<GameObject>(), transform).AddComponent<UnityDisplayNode>();
            udn.Active = false;
            udn.transform.position = new(-3000, 0);
            udn.gameObject.AddComponent<SetScale2>();
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
                if (reference != null && reference.guidRef.Equals("VenomPortrait"))
                    try {
                        var b = Assets.LoadAsset("VenomPortrait");
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
                    } catch { }
                if (reference != null && reference.guidRef.Equals("AntiVenomPortrait"))
                    try {
                        var b = Assets.LoadAsset("AntiVenomPortrait");
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
                    } catch { }
                if (reference != null && reference.guidRef.Equals("VenomUpgrade1"))
                    try {
                        var b = Assets.LoadAsset("UPGRADE1");
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
                    } catch { }
                if (reference != null && reference.guidRef.Equals("VenomUpgrade2"))
                    try {
                        var b = Assets.LoadAsset("UPGRADE2");
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
                    } catch { }
                if (reference != null && reference.guidRef.Equals("VenomUpgrade3"))
                    try {
                        var b = Assets.LoadAsset("UPGRADE3");
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
                    } catch { }
                if (reference != null && reference.guidRef.Equals("VenomUpgrade4"))
                    try {
                        var b = Assets.LoadAsset("UPGRADE4");
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
                    } catch { }
                if (reference != null && reference.guidRef.Equals("AntiVenomUBG"))
                    try {
                        var text = LoadTextureFromBytes(Properties.Resources.AVenomUBG);
                        image.canvasRenderer.SetTexture(text);
                        image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                    } catch { }
            }
        }

        [HarmonyPatch(typeof(Weapon), nameof(Weapon.SpawnDart))]
        public static class WI {
            private static readonly Dictionary<int, float> remaining = new();

            [HarmonyPostfix]
            public static void Postfix(ref Weapon __instance) => RunAnimations(__instance);

            private static async Task RunAnimations(Weapon __instance) {
                if (__instance == null) return;
                if (__instance.weaponModel == null) return;
                if (__instance.weaponModel.name == null) return;
                if (__instance.attack == null) return;
                if (__instance.attack.tower == null) return;
                if (__instance.attack.tower.Node == null) return;
                if (__instance.attack.tower.Node.graphic == null) return;

                try {
                    if (__instance.weaponModel.name.EndsWith("vpunch")) {
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().StopPlayback();
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().Play("PUNCH");
                        if (remaining.ContainsKey(__instance.attack.tower.Id))
                            remaining.Remove(__instance.attack.tower.Id);
                        remaining.Add(__instance.attack.tower.Id, 2300);
                        await Task.Run(async () => {
                            while (remaining.ContainsKey(__instance.attack.tower.Id) && remaining[__instance.attack.tower.Id] > 0) {
                                remaining[__instance.attack.tower.Id] -= TimeManager.timeScaleWithoutNetwork + 1;
                                await Task.Delay(1);
                            }
                            return;
                        });
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().Play("IDLE");
                    }
                    if (__instance.weaponModel.name.EndsWith("avpunch")) {
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().StopPlayback();
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().Play("APUNCH");
                        if (remaining.ContainsKey(__instance.attack.tower.Id))
                            remaining.Remove(__instance.attack.tower.Id);
                        remaining.Add(__instance.attack.tower.Id, 2300);
                        await Task.Run(async () => {
                            while (remaining.ContainsKey(__instance.attack.tower.Id) && remaining[__instance.attack.tower.Id] > 0) {
                                remaining[__instance.attack.tower.Id] -= TimeManager.timeScaleWithoutNetwork + 1;
                                await Task.Delay(1);
                            }
                            return;
                        });
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().Play("IDLE");
                    }
                    if (__instance.weaponModel.name.EndsWith("vscratch")) {
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().StopPlayback();
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().Play("SCRATCH");
                        if (remaining.ContainsKey(__instance.attack.tower.Id))
                            remaining.Remove(__instance.attack.tower.Id);
                        remaining.Add(__instance.attack.tower.Id, 2300);
                        await Task.Run(async () => {
                            while (remaining.ContainsKey(__instance.attack.tower.Id) && remaining[__instance.attack.tower.Id] > 0) {
                                remaining[__instance.attack.tower.Id] -= TimeManager.timeScaleWithoutNetwork + 1;
                                await Task.Delay(1);
                            }
                            return;
                        });
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().Play("IDLE");
                    }
                    if (__instance.weaponModel.name.EndsWith("avscratch")) {
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().StopPlayback();
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().Play("ASCRATCH");
                        if (remaining.ContainsKey(__instance.attack.tower.Id))
                            remaining.Remove(__instance.attack.tower.Id);
                        remaining.Add(__instance.attack.tower.Id, 2300);
                        await Task.Run(async () => {
                            while (remaining.ContainsKey(__instance.attack.tower.Id) && remaining[__instance.attack.tower.Id] > 0) {
                                remaining[__instance.attack.tower.Id] -= TimeManager.timeScaleWithoutNetwork + 1;
                                await Task.Delay(1);
                            }
                            return;
                        });
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().Play("IDLE");
                    }
                } catch (Exception) { }
            }
        }
    }
}
