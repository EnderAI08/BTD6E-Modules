namespace GodlyTowers.Towers {
    public sealed class Shrek {
        public static string name = "Shrek";

        public static UpgradeModel[] GetUpgrades() => new UpgradeModel[] {
            new UpgradeModel("Onions Have Layers", 422, 0, new("OnionsHaveLayers"), 0, 0, 0, "", "Onions Have Layers"),
            new UpgradeModel("Stronger Punches", 945, 0, new("StrongerPunches"), 0, 1, 0, "", "Stronger Punches"),
            new UpgradeModel("'Splosive Onions", 1246, 0, new("SplosiveOnions"), 0, 2, 0, "", "'Splosive Onions"),
            new UpgradeModel("Diced Onions", 6874, 0, new("DicedOnions"), 0, 3, 0, "", "Diced Onions"),
            new UpgradeModel("Shrek Is Love", 16548, 0, new("ShrekIsLove"), 0, 4, 0, "", "Shrek Is Life")
        };

        public static (TowerModel, ShopTowerDetailsModel, TowerModel[], UpgradeModel[]) GetTower(GameModel gameModel) {
            var ShrekDetails = gameModel.towerSet[0].Clone().Cast<ShopTowerDetailsModel>();
            ShrekDetails.towerId = name;
            ShrekDetails.towerIndex = GlobalTowerIndex.Index;

            if (!LocalizationManager.Instance.textTable.ContainsKey("Onions Have Layers Description"))
                LocalizationManager.Instance.textTable.Add("Onions Have Layers Description", "Onions have layers. Ogres have layers. Onions have layers. You get it? We both have layers.");
            if (!LocalizationManager.Instance.textTable.ContainsKey("Stronger Punches Description"))
                LocalizationManager.Instance.textTable.Add("Stronger Punches Description", "Muscle of an ogre. Next to none.");
            if (!LocalizationManager.Instance.textTable.ContainsKey("'Splosive Onions Description"))
                LocalizationManager.Instance.textTable.Add("'Splosive Onions Description", "Splodin' Onions. Enough to make a grown man cry.");
            if (!LocalizationManager.Instance.textTable.ContainsKey("Diced Onions Description"))
                LocalizationManager.Instance.textTable.Add("Diced Onions Description", "Slice and dice them!");
            if (!LocalizationManager.Instance.textTable.ContainsKey("Shrek Is Life Description"))
                LocalizationManager.Instance.textTable.Add("Shrek Is Life Description", "Shrek Is Life. :)");


            return (GetT0(gameModel), ShrekDetails, new[] { GetT0(gameModel), GetT1(gameModel), GetT2(gameModel), GetT3(gameModel), GetT4(gameModel), GetT5(gameModel) }, GetUpgrades());
        }

        public static unsafe TowerModel GetT0(GameModel gameModel) {
            var Shrek = gameModel.towers[0].Clone().Cast<TowerModel>();

            Shrek.name = name;
            Shrek.baseId = name;
            Shrek.display = "Shrek";
            Shrek.portrait = new("ShrekPortrait");
            Shrek.icon = new("ShrekPortrait");
            Shrek.towerSet = "Primary";
            Shrek.emoteSpriteLarge = new("Movie");
            Shrek.radius = 8;
            Shrek.cost = 1250;
            Shrek.range = 35;
            Shrek.mods = Array.Empty<ApplyModModel>();
            Shrek.upgrades = new UpgradePathModel[] { new("Onions Have Layers", name + "-100") };

            for (var i = 0; i < Shrek.behaviors.Count; i++) {
                var b = Shrek.behaviors[i];
                if (b.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                    var att = gameModel.towers.First(a => a.name.Contains("Sauda 20")).behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<AttackModel>()).Clone().Cast<AttackModel>();
                    att.range = 35;
                    att.weapons[0].name = "ShrekHit";
                    att.weapons[0].rate = 1.6f;
                    att.weapons[0].projectile.pierce *= 5;
                    att.weapons[0].projectile.radius *= 2;
                    att.weapons[0].projectile.ignorePierceExhaustion = true;
                    att.weapons[0].projectile.behaviors = att.weapons[0].projectile.behaviors.Remove(a => a.GetIl2CppType() == Il2CppType.Of<AddBehaviorToBloonModel>());
                    for (var j = 0; j < att.weapons[0].projectile.behaviors.Length; j++) {
                        var pb = att.weapons[0].projectile.behaviors[j];

                        if (pb.GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                            var d = pb.Cast<DamageModel>();

                            d.damage = 4;

                            pb = d;
                        }
                    }
                    Shrek.behaviors[i] = att;
                }

                if (b.GetIl2CppType() == Il2CppType.Of<DisplayModel>()) {
                    var display = b.Cast<DisplayModel>();
                    display.display = "Shrek";
                    b = display;
                }
            }

            var link = gameModel.towers.First(a => a.name.Contains("Sauda 20")).behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<LinkProjectileRadiusToTowerRangeModel>()).Clone().Cast<LinkProjectileRadiusToTowerRangeModel>();
            link.projectileModel.behaviors = link.projectileModel.behaviors.Remove(a => a.GetIl2CppType() == Il2CppType.Of<AddBehaviorToBloonModel>());
            link.baseTowerRange = 35;

            Shrek.behaviors = Shrek.behaviors.Add(link, new OverrideCamoDetectionModel("OCDM_", true));

            return Shrek;
        }

        public static unsafe TowerModel GetT1(GameModel gameModel) {
            var Shrek = GetT0(gameModel).Clone().Cast<TowerModel>();

            Shrek.name = name + "-100";
            Shrek.baseId = name;
            Shrek.display = "Shrek";
            Shrek.portrait = new("ShrekPortrait");
            Shrek.icon = new("ShrekPortrait");
            Shrek.towerSet = "Primary";
            Shrek.tier = 1;
            Shrek.tiers = new[] { 1, 0, 0 };
            Shrek.range = 75;
            Shrek.mods = Array.Empty<ApplyModModel>();
            Shrek.upgrades = new UpgradePathModel[] { new("Stronger Punches", name + "-200") };

            var add = GetT0(gameModel).behaviors.First(a => a.Is<AttackModel>(out _)).CloneCast<AttackModel>();

            add.range = 20;

            var dartAM = gameModel.towers.First(a => a.name.Contains("DartMonkey-030")).behaviors.First(a => a.Is<AttackModel>(out _)).CloneCast<AttackModel>();

            for (var i = 0; i < Shrek.behaviors.Count; i++) {

                if (Shrek.behaviors[i].Is<AttackModel>(out _)) {
                    Shrek.behaviors[i] = dartAM;

                    dartAM.range = 75;
                    dartAM.weapons[0].name = "ShrekThrow";
                    dartAM.weapons[0].projectile.display = "Onion";
                    dartAM.weapons[0].rate = 1.25f;
                    dartAM.weapons[0].emission.Cast<ArcEmissionModel>().count = 1;

                    for (var k = 0; k < dartAM.weapons[0].projectile.behaviors.Length; k++) {
                        var pb = dartAM.weapons[0].projectile.behaviors[k];

                        if (pb.GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                            var d = pb.Cast<DamageModel>();

                            d.damage = 18 * 4;
                            d.immuneBloonProperties = BloonProperties.None;
                        }

                        if (pb.GetIl2CppType() == Il2CppType.Of<TravelStraitModel>()) {
                            var tsm = pb.Cast<TravelStraitModel>();

                            tsm.lifespan *= 2;
                            tsm.speed *= 1.5f;
                        }
                    }

                    Shrek.behaviors[i] = dartAM;
                }

                if (Shrek.behaviors[i].Is<DisplayModel>(out var dm)) {
                    dm.display = "Shrek";
                }
            }

            Shrek.behaviors = Shrek.behaviors.Add(add);

            return Shrek;
        }

        public static unsafe TowerModel GetT2(GameModel gameModel) {
            var Shrek = GetT1(gameModel).Clone().Cast<TowerModel>();

            Shrek.name = name + "-200";
            Shrek.baseId = name;
            Shrek.display = "Shrek";
            Shrek.portrait = new("ShrekPortrait");
            Shrek.icon = new("ShrekPortrait");
            Shrek.towerSet = "Primary";
            Shrek.tier = 2;
            Shrek.tiers = new[] { 2, 0, 0 };
            Shrek.range = 85;
            Shrek.upgrades = new UpgradePathModel[] { new("'Splosive Onions", name + "-300") };

            for (int i = 0; i < Shrek.behaviors.Count; i++) {
                if (Shrek.behaviors[i].Is<AttackModel>(out var att)) {

                    if (att.weapons[0].name.Contains("Throw")) {
                        att.range = 85;
                        att.weapons[0].name = "ShrekThrow";
                        att.weapons[0].projectile.display = "Onion";
                        att.weapons[0].rate = 1.333333333333333f;
                        for (var j = 0; j < att.weapons[0].projectile.behaviors.Length; j++) {
                            var pb = att.weapons[0].projectile.behaviors[j];

                            if (pb.GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                                var d = pb.Cast<DamageModel>();

                                d.damage = 11 * 2;

                                pb = d;
                            }

                            att.weapons[0].projectile.behaviors[j] = pb;
                        }
                        Shrek.behaviors[i] = att;
                    }
                }
            }

            var last = Shrek.behaviors.Last(a=>a.Is<AttackModel>()).Cast<AttackModel>();
            last.weapons[0].projectile.behaviors.First(a=>a.Is<DamageModel>()).Cast<DamageModel>().damage *= 5;

            return Shrek;
        }

        public static unsafe TowerModel GetT3(GameModel gameModel) {
            var Shrek = GetT2(gameModel).Clone().Cast<TowerModel>();

            Shrek.name = name + "-300";
            Shrek.baseId = name;
            Shrek.display = "Shrek";
            Shrek.portrait = new("ShrekPortrait");
            Shrek.icon = new("ShrekPortrait");
            Shrek.towerSet = "Primary";
            Shrek.tier = 3;
            Shrek.tiers = new[] { 3, 0, 0 };
            Shrek.range = 85;
            Shrek.upgrades = new UpgradePathModel[] { new("Diced Onions", name + "-400") };

            var bombShooterProj = gameModel.towers.First(a => a.name.StartsWith("BombShooter")).CloneCast().behaviors.First(a => a.Is<AttackModel>()).Cast<AttackModel>().weapons[0].projectile;

            var cpocm = bombShooterProj.behaviors.First(a => a.Is<CreateProjectileOnContactModel>()).Cast<CreateProjectileOnContactModel>();
            var ceocm = bombShooterProj.behaviors.First(a => a.Is<CreateEffectOnContactModel>()).Cast<CreateEffectOnContactModel>();

            for (var i = 0; i < Shrek.behaviors.Count; i++) {
                var b = Shrek.behaviors[i];
                if (b.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                    var att = b.Cast<AttackModel>();

                    att.weapons[0].Rate -= 0.1f;

                    if (att.weapons[0].name.Contains("Throw"))
                        att.weapons[0].projectile.display = "EOnion";

                    for (var k = 0; k < att.weapons[0].projectile.behaviors.Length; k++) {
                        var pb = att.weapons[0].projectile.behaviors[k];

                        if (pb.GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                            var d = pb.Cast<DamageModel>();

                            d.damage *= 3;
                        }
                    }

                    att.weapons[0].projectile.behaviors = att.weapons[0].projectile.behaviors.Add(cpocm, ceocm);

                    Shrek.behaviors[i] = att;
                }
            }

            return Shrek;
        }

        public static unsafe TowerModel GetT4(GameModel gameModel) {
            var Shrek = GetT3(gameModel).Clone().Cast<TowerModel>();

            Shrek.name = name + "-400";
            Shrek.baseId = name;
            Shrek.display = "Shrek";
            Shrek.portrait = new("ShrekPortrait");
            Shrek.icon = new("ShrekPortrait");
            Shrek.towerSet = "Primary";
            Shrek.tier = 4;
            Shrek.tiers = new[] { 4, 0, 0 };
            Shrek.range = 85;
            Shrek.upgrades = new UpgradePathModel[] { new UpgradePathModel("Shrek Is Love", name + "-500") };


            for (var i = 0; i < Shrek.behaviors.Count; i++) {
                if (Shrek.behaviors[i].Is<AttackModel>(out var am)) {
                    if (am.weapons[0].emission.Is<ArcEmissionModel>(out var aem)) {
                        aem.count = 5;
                        aem.angle = 45;
                    }

                    for (var k = 0; k < am.weapons[0].projectile.behaviors.Length; k++) {
                        var pb = am.weapons[0].projectile.behaviors[k];

                        if (pb.GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                            var d = pb.Cast<DamageModel>();

                            d.damage *= 3;
                        }
                    }

                    am.weapons[0].Rate -= 0.1f;
                    am.weapons[0].projectile.ignorePierceExhaustion = true;
                }

                if (Shrek.behaviors[i].Is<DisplayModel>(out var dm)) {
                    dm.display = "Shrek";
                }
            }

            return Shrek;
        }

        public static unsafe TowerModel GetT5(GameModel gameModel) {
            var Shrek = GetT4(gameModel).Clone().Cast<TowerModel>();

            Shrek.name = name + "-500";
            Shrek.baseId = name;
            Shrek.display = "GShrek";
            Shrek.portrait = new("ShrekPortrait");
            Shrek.icon = new("ShrekPortrait");
            Shrek.towerSet = "Primary";
            Shrek.radius = 8;
            Shrek.cost = 800;
            Shrek.range = 115;
            Shrek.tier = 5;
            Shrek.tiers = new[] { 5, 0, 0 };
            Shrek.mods = Array.Empty<ApplyModModel>();
            Shrek.upgrades = Array.Empty<UpgradePathModel>();

            for (var i = 0; i < Shrek.behaviors.Count; i++) {
                var b = Shrek.behaviors[i];
                if (b.Is<AttackModel>(out var am)) {
                    am.range = 115;
                    if (am.weapons[0].emission.Is<ArcEmissionModel>(out var aem)) {
                        aem.count = 5;
                        aem.angle = 45;
                    }

                    for (var k = 0; k < am.weapons[0].projectile.behaviors.Length; k++) {
                        var pb = am.weapons[0].projectile.behaviors[k];

                        if (pb.GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                            var d = pb.Cast<DamageModel>();

                            d.damage *= 7;
                        }
                    }

                    am.weapons[0].Rate = 0;
                }

                if (b.GetIl2CppType() == Il2CppType.Of<DisplayModel>()) {
                    var display = b.Cast<DisplayModel>();
                    display.display = "GShrek";
                    b = display;
                }

                if (b.Is<LinkProjectileRadiusToTowerRangeModel>(out var lprttrm)) {
                    lprttrm.baseTowerRange = Shrek.range;
                }
            }

            return Shrek;
        }

        [HarmonyPatch(typeof(Factory), nameof(Factory.FindAndSetupPrototypeAsync))]
        public static class PrototypeUDN_Patch {
            public static Dictionary<string, UnityDisplayNode> protos = new();

            [HarmonyPrefix]
            public static bool Prefix(Factory __instance, string objectId, Il2CppSystem.Action<UnityDisplayNode> onComplete) {
                if (!protos.ContainsKey(objectId) && objectId.Contains("GShrek")) {
                    var udn = GetGShrek(__instance.PrototypeRoot);
                    udn.name = "GShrek";
                    udn.RecalculateGenericRenderers();
                    udn.isSprite = false;
                    onComplete.Invoke(udn);
                    protos.Add(objectId, udn);
                    return false;
                }

                if (!protos.ContainsKey(objectId) && objectId.Contains("Shrek")) {
                    var udn = GetShrek(__instance.PrototypeRoot);
                    udn.name = "Shrek";
                    udn.RecalculateGenericRenderers();
                    udn.isSprite = false;
                    onComplete.Invoke(udn);
                    protos.Add(objectId, udn);
                    return false;
                }

                if (objectId.Equals("EOnion")) {
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
                                    var text = Assets.LoadAsset("SplosiveOnions").Cast<Texture2D>();
                                    smr.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new(0.5f, 0.5f), 2.7f * 4f);
                                    nudn.genericRenderers[i] = smr;
                                }
                            }

                            udn = nudn;
                            onComplete.Invoke(udn);
                        }));
                    return false;
                }

                if (objectId.Equals("Onion")) {
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
                                    var text = Assets.LoadAsset("OnionProj").Cast<Texture2D>();
                                    smr.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new(0.5f, 0.5f), 2.7f * 4f);
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

        public static UnityDisplayNode GetGShrek(Transform transform) {
            var udn = Object.Instantiate(Assets.LoadAsset("ShrekObjGod").Cast<GameObject>(), transform).AddComponent<UnityDisplayNode>();
            udn.Active = false;
            udn.transform.position = new(-3000, 0);
            udn.gameObject.AddComponent<SetScale9>();
            return udn;
        }

        public static UnityDisplayNode GetShrek(Transform transform) {
            var udn = Object.Instantiate(Assets.LoadAsset("ShrekObj").Cast<GameObject>(), transform).AddComponent<UnityDisplayNode>();
            udn.Active = false;
            udn.transform.position = new(-3000, 0);
            udn.gameObject.AddComponent<SetScale9>();
            return udn;
        }

        [HarmonyPatch(typeof(Factory), nameof(Factory.ProtoFlush))]
        public static class PrototypeFlushUDN_Patch {
            [HarmonyPostfix]
            public static void Postfix() {
                foreach (var proto in PrototypeUDN_Patch.protos.Values)
                    Object.Destroy(proto.gameObject);
                PrototypeUDN_Patch.protos.Clear();
            }
        }


        [HarmonyPatch(typeof(ResourceLoader), nameof(ResourceLoader.LoadSpriteFromSpriteReferenceAsync))]
        public static class ResourceLoader_Patch {
            [HarmonyPostfix]
            public static void Postfix(SpriteReference reference, ref Image image) {
                if (reference != null && reference.guidRef.Equals("DicedOnions"))
                    try {
                        var b = Assets.LoadAsset(reference.guidRef);
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            text.filterMode = FilterMode.Point;
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
                    } catch { }
                if (reference != null && reference.guidRef.Equals("OnionsHaveLayers"))
                    try {
                        var b = Assets.LoadAsset(reference.guidRef);
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            text.filterMode = FilterMode.Point;
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
                    } catch { }
                if (reference != null && reference.guidRef.Equals("ShrekIsLove"))
                    try {
                        var b = Assets.LoadAsset(reference.guidRef);
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            text.filterMode = FilterMode.Point;
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
                    } catch { }
                if (reference != null && reference.guidRef.Equals("SplosiveOnions"))
                    try {
                        var b = Assets.LoadAsset(reference.guidRef);
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            text.filterMode = FilterMode.Point;
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
                    } catch { }
                if (reference != null && reference.guidRef.Equals("StrongerPunches"))
                    try {
                        var b = Assets.LoadAsset(reference.guidRef);
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            text.filterMode = FilterMode.Point;
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
                    } catch { }
                if (reference != null && reference.guidRef.Equals("ShrekPortrait"))
                    try {
                        var b = Assets.LoadAsset("Shrek");
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            text.filterMode = FilterMode.Point;
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
                    } catch { }
            }

            private static Texture2D LoadTextureFromBytes(byte[] FileData) {
                Texture2D Tex2D = new(2, 2);
                if (ImageConversion.LoadImage(Tex2D, FileData)) return Tex2D;

                return null;
            }

            private static Sprite LoadSprite(Texture2D text) {
                return Sprite.Create(text, new(0, 0, text.width, text.height), new());
            }
        }

        [HarmonyPatch(typeof(Weapon), nameof(Weapon.SpawnDart))]
        public static class WI {
            private static readonly Dictionary<int, float> remaining = new();

            [HarmonyPrefix]
            public static bool Prefix_SwitchWeapons(ref Weapon __instance) {
                if (__instance == null) return true;
                if (__instance.weaponModel == null) return true;
                if (__instance.weaponModel.name == null) return true;
                if (__instance.attack == null) return true;
                if (__instance.attack.tower == null) return true;
                if (__instance.attack.tower.Node == null) return true;
                if (__instance.attack.tower.Node.graphic == null) return true;

                try {
                    if (__instance.weaponModel.name.EndsWith("ShrekThrow")) {
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().StopPlayback();
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().speed = 32;
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().Play("Throw");
                    }
                } catch (Exception) { }

                try {
                    if (__instance.weaponModel.name.EndsWith("ShrekHit")) {
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().StopPlayback();
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().speed = 16;
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().Play("Punch");
                    }
                } catch (Exception) { }

                return true;
            }

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
                    if (__instance.weaponModel.name.EndsWith("ShrekThrow")) {
                        if (remaining.ContainsKey(__instance.attack.tower.Id))
                            remaining.Remove(__instance.attack.tower.Id);
                        remaining.Add(__instance.attack.tower.Id, 1111);
                        await Task.Run(async () => {
                            while (remaining.ContainsKey(__instance.attack.tower.Id) && remaining[__instance.attack.tower.Id] > 0) {
                                remaining[__instance.attack.tower.Id] -= TimeManager.timeScaleWithoutNetwork + 1;
                                await Task.Delay(1);
                            }
                        });
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().Play("Idle");
                    }
                } catch (Exception) { }

                try {
                    if (__instance.weaponModel.name.EndsWith("ShrekHit")) {
                        if (remaining.ContainsKey(__instance.attack.tower.Id))
                            remaining.Remove(__instance.attack.tower.Id);
                        remaining.Add(__instance.attack.tower.Id, 1111);
                        await Task.Run(async () => {
                            while (remaining.ContainsKey(__instance.attack.tower.Id) && remaining[__instance.attack.tower.Id] > 0) {
                                remaining[__instance.attack.tower.Id] -= TimeManager.timeScaleWithoutNetwork + 1;
                                await Task.Delay(1);
                            }
                        });
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().Play("Idle");
                    }
                } catch (Exception) { }
            }
        }
    }
}
