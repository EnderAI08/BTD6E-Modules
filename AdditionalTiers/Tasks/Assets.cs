using System.Threading;

using Assets.Scripts.Simulation.Behaviors;

namespace AdditionalTiers.Tasks {
    public sealed class Assets {
        [HideFromIl2Cpp]
        public static Dictionary<string, Type> Types { get; set; } = new() {
            { "WhitesnakeProj", Il2CppType.Of<AnimatedEnergyTexture>() },
            { "WhitesnakePheonixProj", Il2CppType.Of<AnimatedFlameTexture>() },
            { "WhitesnakeDarkPheonixProj", Il2CppType.Of<AnimatedDarkFlameTexture>() }
        };
        [HideFromIl2Cpp]
        public static Dictionary<string, Action<UnityDisplayNode>> Actions { get; set; } = new() {
            {
                "GlaiveDominusSilver",
                udn => {
                    var light = udn.gameObject.AddComponent<Light>();
                    light.type = LightType.Directional;
                    light.renderMode = LightRenderMode.ForceVertex;

                    var assets = Particles;
                    var _obj = assets[1].Cast<GameObject>();
                    var obj = Object.Instantiate(_obj, udn.transform);
                    obj.SetActive(true);
                    var ps = obj.transform.GetComponentInChildren<ParticleSystem>();
                    ps.emissionRate = 15;
                    ps.transform.localScale = new(10, 10, 10);
                }
            },
            {
                "VitaminC",
                udn => {
                    var assets = ParticleSystems;
                    var _obj = assets[0].Cast<GameObject>();
                    var obj = Object.Instantiate(_obj, udn.transform);
                    obj.SetActive(true);
                    var ps = obj.transform.GetComponent<ParticleSystem>();
                    ps.transform.localScale = new(15, 15, 15);
                }
            },
            {
                "APMGold2",
                udn => {
                    for (int i = 0; i < udn.genericRenderers.Length; i++) {
                        udn.genericRenderers[i].material.SetFloat("_OutlineWidth", 0.02f);
                    }
                }
            },
            {
                "MrRoboto",
                udn => {
                    for (int i = 0; i < udn.genericRenderers.Length; i++) {
                        udn.genericRenderers[i].material.SetFloat("_OutlineWidth", 0.02f);
                    }
                    var assets = Particles;
                    var _obj = assets[0].Cast<GameObject>();
                    var obj = Object.Instantiate(_obj, udn.transform);
                    obj.SetActive(true);
                    var ps = obj.transform.GetComponentInChildren<ParticleSystem>();
                    ps.transform.localScale = new(10, 10, 10);
                }
            },
            {
                "MrRobotoAbility",
                udn => {
                    var pss = udn.transform.GetComponentsInChildren<ParticleSystem>();
                    foreach (var ps in pss)
                        ps.startColor = new Color(1f, 0.8f, 0);
                }
            },
            {
                "GlaiveDominusSilverOrbit",
                udn => {
                    var ps = udn.transform.GetComponentInChildren<ParticleSystem>();
                    ps.startColor = new Color(1, 0.25f, 0);
                    udn.gameObject.AddComponent<FastRotation>();
                }
            },
            {
                "GlaiveDominusSilverOrbit2",
                udn => {
                    var ps = udn.transform.GetComponentInChildren<ParticleSystem>();
                    ps.startColor = new Color(1, 1, 1);
                    for (int i = 0; i < 2; i++)
                        udn.gameObject.AddComponent<FastRotation>();
                }
            },
            {
                "GlaiveDominusSilverOrbit3",
                udn => {
                    var ps = udn.transform.GetComponentInChildren<ParticleSystem>();
                    ps.startColor = new Color(0.3f, 1, 0.3f);
                    for (int i = 0; i < 5; i++)
                        udn.gameObject.AddComponent<FastRotation>();
                }
            },
            {
                "GlaiveDominusSilverAbility",
                udn => {
                    var ps = udn.transform.GetComponentInChildren<ParticleSystem>();
                    ps.startColor = new Color(1f, 0, 0);
                }
            },
            {
                "VitaminCTotemParticles",
                udn => {
                    var ps = udn.transform.GetComponentsInChildren<ParticleSystem>();

                    for (int i = 0; i < ps.Length; i++)
                        ps[i].startColor = new Color(0, 1, 0);
                }
            },
            {
                "BTD4SunGod",
                udn => {
                    udn.transform.GetComponentInChildren<ParticleSystem>().gameObject.active = false;
                    udn.gameObject.AddComponent<SetHeight1>();
                }
            },
            {
                "BTD4SunGodV",
                udn => {
                    udn.transform.GetComponentInChildren<ParticleSystem>().gameObject.active = false;
                    udn.gameObject.AddComponent<SetHeight1>();
                }
            },
            {
                "DaftPunkProjectile",
                udn => {
                    var ps = udn.transform.GetComponentInChildren<ParticleSystem>();
                    ps.startColor = new Color(1, 0.25f, 0.25f);
                    var tr = udn.transform.GetComponentInChildren<TrailRenderer>();
                    tr.startColor = new Color(1f, 0.25f, 0.25f);
                    tr.endColor = new Color(0.4f, 0.1f, 0.1f);
                }
            },
            {
                "DaftPunkTurboProjectile",
                udn => {
                    var ps = udn.transform.GetComponentInChildren<ParticleSystem>();
                    ps.startColor = new Color(1, 0.25f, 0.25f);
                    var tr = udn.transform.GetComponentInChildren<TrailRenderer>();
                    tr.startColor = new Color(1f, 0.25f, 0.25f);
                    tr.endColor = new Color(0.4f, 0.1f, 0.1f);
                }
            },
            {
                "DaftPunkOrbit",
                udn => {
                    var ps = udn.transform.GetComponentInChildren<ParticleSystem>();
                    ps.startColor = new Color(1f, .1f, .1f);
                    for (int i = 0; i < 5; i++)
                        udn.gameObject.AddComponent<FastRotation>();
                }
            },
            {
                "FMTTM",
                udn => {
                    for (int i = 0; i < udn.genericRenderers.Length; i++) {
                        udn.genericRenderers[i].material.SetFloat("_OutlineWidth", 1f);
                        udn.genericRenderers[i].material.SetColor("_OutlineColor", new Color32(177, 135, 255, 255));
                    }
                }
            },
            {
                "FMTTM2",
                udn => {
                    for (int i = 0; i < udn.genericRenderers.Length; i++) {
                        udn.genericRenderers[i].material.SetFloat("_OutlineWidth", 1f);
                        udn.genericRenderers[i].material.SetColor("_OutlineColor", new Color32(177, 135, 255, 255));
                    }
                }
            },
            {
                "FMTTM3",
                udn => {
                    for (int i = 0; i < udn.genericRenderers.Length; i++) {
                        udn.genericRenderers[i].material.SetFloat("_OutlineWidth", 1f);
                        udn.genericRenderers[i].material.SetColor("_OutlineColor", new Color32(177, 135, 255, 255));
                    }
                }
            },
            {
                "BurningDownTheHouse",
                udn => {
                    for (int i = 0; i < udn.genericRenderers.Length; i++) {
                        udn.genericRenderers[i].material.SetFloat("_OutlineWidth", 0.05f);
                        udn.genericRenderers[i].material.SetColor("_OutlineColor", new Color32(0, 254, 254, 255));
                    }
                }
            },
            {
                "SheerHeartAttack",
                udn => {
                    for (int i = 0; i < udn.genericRenderers.Length; i++) {
                        udn.genericRenderers[i].material.SetFloat("_OutlineWidth", 0.05f);
                        udn.genericRenderers[i].material.SetColor("_OutlineColor", new Color32(206, 141, 0, 255));
                    }
                }
            },
            {
                "AOBTD",
                udn => {
                    for (int i = 0; i < udn.genericRenderers.Length; i++) {
                        udn.genericRenderers[i].material.SetFloat("_OutlineWidth", 0.02f);
                        udn.genericRenderers[i].material.SetColor("_OutlineColor", new Color32(162, 0, 255, 255));
                    }
                }
            },
            {
                "HeyYa",
                udn => {
                    for (int i = 0; i < udn.genericRenderers.Length; i++)
                        udn.genericRenderers[i].material.SetFloat("_OutlineWidth", 1);
                }
            },
            {
                "SpaceTruckin",
                udn => {
                    for (int i = 0; i < udn.genericRenderers.Length; i++)
                        udn.genericRenderers[i].material.SetFloat("_OutlineWidth", 0.02f);
                }
            },
            {
                "PlanetWaves",
                udn => {
                    for (int i = 0; i < udn.genericRenderers.Length; i++)
                        udn.genericRenderers[i].material.SetFloat("_OutlineWidth", 0.05f);
                }
            },
            {
                "GreenDay",
                udn => {
                    for (int i = 0; i < udn.genericRenderers.Length; i++)
                        udn.genericRenderers[i].material.SetFloat("_OutlineWidth", 0.05f);
                }
            },
            {
                "Dynamite",
                udn => {
                    for (int i = 0; i < udn.genericRenderers.Length; i++)
                        udn.genericRenderers[i].material.SetFloat("_OutlineWidth", 0.05f);
                }
            },
            {
                "TooCold",
                udn => {
                    var main = T6SpecificAssets.LoadAsset("TooColdMain");
                    var sub = T6SpecificAssets.LoadAsset("TooColdSub");
                    var @static = T6SpecificAssets.LoadAsset("TooColdStatic");

                    {
                        var obj = Object.Instantiate(main.Cast<GameObject>(), udn.transform);
                        obj.SetActive(true);
                        var ps = obj.transform.GetComponentInChildren<ParticleSystem>();
                        ps.transform.localScale = new(10, 0, 10);

                        var pos = ps.transform.position;

                        pos.y += 2.1f;

                        ps.transform.position = pos;
                    }
                    {
                        var obj = Object.Instantiate(sub.Cast<GameObject>(), udn.transform);
                        obj.SetActive(true);
                        var ps = obj.transform.GetComponentInChildren<ParticleSystem>();
                        ps.transform.localScale = new(10, 0, 10);

                        var pos = ps.transform.position;

                        pos.y += 2.2f;

                        ps.transform.position = pos;
                    }
                    {
                        var obj = Object.Instantiate(@static.Cast<GameObject>(), udn.transform);
                        obj.SetActive(true);
                        var ps = obj.transform.GetComponentInChildren<ParticleSystem>();
                        ps.transform.localScale = new(10, 0, 10);

                        var pos = ps.transform.position;

                        pos.y += 2.0f;

                        ps.transform.position = pos;
                    }

                    for (int i = 0; i < udn.genericRenderers.Length; i++) {
                        udn.genericRenderers[i].material.SetFloat("_OutlineWidth", 0.005f);
                        udn.genericRenderers[i].material.SetColor("_OutlineColor", SpecialColors["TooCold"]);
                    }
                }
            },
            {
                "KillerQueen",
                udn => {
                    for (int i = 0; i < udn.genericRenderers.Length; i++) {
                        udn.genericRenderers[i].material.SetFloat("_OutlineWidth", 0.5f);
                    }
                }
            },
            {
                "CrazyDiamond",
                udn => {
                    var main = T6SpecificAssets.LoadAsset("DSparkle");

                    var obj = Object.Instantiate(main.Cast<GameObject>(), udn.transform);
                    obj.SetActive(true);
                    var ps = obj.transform.GetComponentInChildren<ParticleSystem>();
                    ps.transform.localScale = new(10, 10, 10);
                }
            }
        };
        [HideFromIl2Cpp]
        public static Dictionary<string, Func<string, Sprite>> SpriteCreation { get; set; } = new() {
            { "ScaryMonstersProj", objectId => SpriteBuilder.createProjectile(CacheBuilder.Get(objectId), 10.8f) },
            { "GlaiveDominusSilverOrbit2", objectId => SpriteBuilder.createProjectile(CacheBuilder.Get(objectId), 10.8f) },
            { "VitaminCBlast", objectId => SpriteBuilder.createProjectile(CacheBuilder.Get(objectId), 7.6f) },
            { "BTD4SunGod", objectId => SpriteBuilder.createProjectile(CacheBuilder.Get(objectId), 43.2f, pivoty: 0.7f) },
            { "BTD4SunGodV", objectId => SpriteBuilder.createProjectile(CacheBuilder.Get(objectId), 43.2f, pivoty: 0.7f) },
            { "BlackYellowMissile", objectId => SpriteBuilder.createProjectile(CacheBuilder.Get(objectId), 10.8f) },
            { "BlackYellowBullet", objectId => SpriteBuilder.createProjectile(CacheBuilder.Get(objectId), 10.8f) }
        };


        [HideFromIl2Cpp]
        public static Dictionary<string, Func<Object, bool>> SpecialShaderIndicies { get; set; } = new() {
            { "GlaiveDominusSilver", obj => obj.name.StartsWith("Unlit/Metallic") }
        };

        [HideFromIl2Cpp]
        public static Dictionary<string, Color> SpecialColors { get; set; } = new() {
            { "FMTTM", new Color32(177, 135, 255, 255) },
            { "FMTTM2", new Color32(177, 135, 255, 255) },
            { "FMTTM3", new Color32(177, 135, 255, 255) },
            { "BurningDownTheHouse", new Color32(0, 254, 254, 255) },
            { "SheerHeartAttack", new Color32(206, 141, 0, 255) },
            { "AOBTD", new Color32(162, 0, 255, 255) },
            { "SpaceTruckin", new Color32(255, 25, 25, 255) },
            { "TooCold", new Color32(55, 182, 237, 255) }
        };

        public static Color GetResetColor(DisplayBehavior beh) {
            if (beh?.displayModel?.display != null && SpecialColors.ContainsKey(beh.displayModel.display))
                return SpecialColors[beh.displayModel.display];
            return Color.black;
        }

        private static AssetBundle _shader;

        public static AssetBundle ShaderBundle {
            get {
                if (_shader == null)
                    _shader = AssetBundle.LoadFromMemory(Images.shader);
                return _shader;
            }
        }

        private static AssetBundle _particle = null;

        public static AssetBundle ParticleBundle {
            get {
                if (_particle == null)
                    _particle = AssetBundle.LoadFromMemory(Images.particle);
                return _particle;
            }
        }

        private static AssetBundle _particlesystem = null;

        public static AssetBundle ParticleSystemBundle {
            get {
                if (_particlesystem == null)
                    _particlesystem = AssetBundle.LoadFromMemory(Images.particlesystem);
                return _particlesystem;
            }
        }

        private static AssetBundle _t6specificassets = null;

        public static AssetBundle T6SpecificAssets {
            get {
                if (_t6specificassets == null)
                    _t6specificassets = AssetBundle.LoadFromMemory(Images.t6);
                return _t6specificassets;
            }
        }

        private static Object[] _particles = null;
        public static Object[] Particles {
            get {
                if (_particles == null)
                    _particles = ParticleBundle.LoadAllAssets();
                return _particles;
            }
        }

        private static Object[] _particlesystems = null;
        public static Object[] ParticleSystems {
            get {
                if (_particlesystems == null)
                    _particlesystems = ParticleSystemBundle.LoadAllAssets();
                return _particlesystems;
            }
        }

        [HarmonyPatch(typeof(Factory), nameof(Factory.FindAndSetupPrototypeAsync))]
        public sealed class DisplayFactory {
            public static bool hasBeenBuilt;
            public static List<AssetInfo> allAssetsKnown = new();
            public static Dictionary<string, UnityDisplayNode> cachedSRs = new();

            [HarmonyPrefix]
            public static bool Prefix(Factory __instance, string objectId, Il2CppSystem.Action<UnityDisplayNode> onComplete) {
                var assets = ShaderBundle.LoadAllAssets();

                if (cachedSRs.ContainsKey(objectId)) {
                    onComplete.Invoke(cachedSRs[objectId]);
                    return false;
                }

                if (objectId.StartsWith("MAKE_AQUA_")) {
                    UnityDisplayNode udn = null;
                    __instance.FindAndSetupPrototypeAsync(objectId.Replace("MAKE_AQUA_", ""), new Action<UnityDisplayNode>(btdUdn => {
                        var instance = Object.Instantiate(btdUdn, __instance.PrototypeRoot);
                        instance.name = objectId + "(Clone)";
                        instance.isSprite = true;
                        instance.RecalculateGenericRenderers();


                        for (var i = 0; i < instance.genericRenderers.Length; i++) {
                            if (instance.genericRenderers[i].GetIl2CppType() == Il2CppType.Of<SpriteRenderer>()) {
                                var sr = instance.genericRenderers[i].Cast<SpriteRenderer>();
                                var tex = sr.sprite.texture;
                                var texture = new Texture2D(tex.width, tex.height);

                                var pixels = tex.GetPixels();
                                for (var aa = 0; aa < pixels.Length; aa++)
                                    pixels[aa] = new Color(pixels[aa].r.Map(float.Epsilon, 1, float.Epsilon, .2f), pixels[aa].g.Map(float.Epsilon, 1, float.Epsilon, .5f), pixels[aa].b, 255);

                                texture.SetPixels(pixels);
                                texture.Apply();

                                sr.sprite = SpriteBuilder.createProjectile(texture);
                            }
                        }

                        cachedSRs[objectId] = instance;

                        udn = instance;
                        onComplete.Invoke(udn);
                    }));
                    return false;
                }

                foreach (var curAsset in allAssetsKnown) {
                    if (objectId.Equals(curAsset.CustomAssetName)) {
                        UnityDisplayNode udn = null;
                        __instance.FindAndSetupPrototypeAsync(curAsset.BTDAssetName,
                            new Action<UnityDisplayNode>(
                                btdUdn => {
                                    var instance = Object.Instantiate(btdUdn, __instance.PrototypeRoot);
                                    instance.name = objectId + "(Clone)";
                                    if (curAsset.RendererType == RendererType.SPRITERENDERER)
                                        instance.isSprite = true;
                                    instance.RecalculateGenericRenderers();

                                    Type rendererType = null;
                                    switch (curAsset.RendererType) {
                                        case RendererType.MESHRENDERER:
                                            rendererType = Il2CppType.Of<MeshRenderer>();
                                            break;
                                        case RendererType.SPRITERENDERER:
                                            rendererType = Il2CppType.Of<SpriteRenderer>();
                                            break;
                                        case RendererType.SKINNEDMESHRENDERER:
                                            rendererType = Il2CppType.Of<SkinnedMeshRenderer>();
                                            break;
                                        case RendererType.PARTICLESYSTEMRENDERER:
                                            rendererType = Il2CppType.Of<ParticleSystemRenderer>();
                                            break;
                                    }

                                    if (rendererType == null && curAsset.RendererType != RendererType.SKINNEDANDUNSKINNEDMESHRENDERER)
                                        throw new NullReferenceException("rendererType is still null, don't leave things unset.");

                                    for (var i = 0; i < instance.genericRenderers.Length; i++) {
                                        if (instance.genericRenderers[i].GetIl2CppType() == rendererType) {
                                            if (curAsset.RendererType != RendererType.SPRITERENDERER && curAsset.RendererType != RendererType.SKINNEDANDUNSKINNEDMESHRENDERER && curAsset.RendererType != RendererType.PARTICLESYSTEMRENDERER) {
                                                var renderer = instance.genericRenderers[i].Cast<Renderer>();
                                                if (!SpecialShaderIndicies.ContainsKey(objectId))
                                                    renderer.material.shader = assets.First(a => a.name.StartsWith("Unlit/CelShading")).Cast<Shader>();
                                                else
                                                    renderer.material.shader = assets.First(SpecialShaderIndicies[objectId]).Cast<Shader>();
                                                renderer.material.SetColor("_OutlineColor", Color.black);
                                                renderer.material.mainTexture = CacheBuilder.Get(objectId);
                                            } else if (curAsset.RendererType == RendererType.SPRITERENDERER) {
                                                var spriteRenderer = instance.genericRenderers[i].Cast<SpriteRenderer>();
                                                if (SpriteCreation.ContainsKey(objectId))
                                                    spriteRenderer.sprite = SpriteCreation[objectId](objectId);
                                                else
                                                    spriteRenderer.sprite = SpriteBuilder.createProjectile(CacheBuilder.Get(objectId));
                                                if (Types.ContainsKey(objectId)) {
                                                    spriteRenderer.gameObject.AddComponent(Types[objectId]);
                                                }
                                            }
                                        } else if (curAsset.RendererType == RendererType.SKINNEDANDUNSKINNEDMESHRENDERER) {
                                            if (instance.genericRenderers[i].GetIl2CppType() == Il2CppType.Of<SkinnedMeshRenderer>()) {
                                                var skinnedRenderer = instance.genericRenderers[i].Cast<SkinnedMeshRenderer>();
                                                if (!SpecialShaderIndicies.ContainsKey(objectId))
                                                    skinnedRenderer.material.shader = assets.First(a => a.name.StartsWith("Unlit/CelShading")).Cast<Shader>();
                                                else
                                                    skinnedRenderer.material.shader = assets.First(SpecialShaderIndicies[objectId]).Cast<Shader>();
                                                skinnedRenderer.material.SetColor("_OutlineColor", Color.black);
                                                skinnedRenderer.material.mainTexture = CacheBuilder.Get(objectId);

                                            } else if (instance.genericRenderers[i].GetIl2CppType() == Il2CppType.Of<MeshRenderer>()) {
                                                var meshRenderer = instance.genericRenderers[i].Cast<MeshRenderer>();
                                                if (!SpecialShaderIndicies.ContainsKey(objectId))
                                                    meshRenderer.material.shader = assets.First(a => a.name.StartsWith("Unlit/CelShading")).Cast<Shader>();
                                                else
                                                    meshRenderer.material.shader = assets.First(SpecialShaderIndicies[objectId]).Cast<Shader>();
                                                meshRenderer.material.SetColor("_OutlineColor", Color.black);
                                                meshRenderer.material.mainTexture = CacheBuilder.Get(objectId);
                                            }
                                        }
                                    }


                                    if (Actions.ContainsKey(objectId))
                                        Actions[objectId](instance);

                                    if (curAsset.RendererType == RendererType.SPRITERENDERER)
                                        cachedSRs[objectId] = instance;

                                    udn = instance;
                                    onComplete.Invoke(udn);
                                }));
                        return false;
                    }
                }

                if (objectId.Equals("UpgradedText")) {
                    UnityDisplayNode udn = null;
                    __instance.FindAndSetupPrototypeAsync("3dcdbc19136c60846ab944ada06695c0",
                        new Action<UnityDisplayNode>(oudn => {
                            var nudn = Object.Instantiate(oudn, __instance.PrototypeRoot);
                            nudn.name = objectId + "(Clone)";
                            nudn.isSprite = true;
                            nudn.RecalculateGenericRenderers();
                            var nktmp = nudn.GetComponentInChildren<NK_TextMeshPro>();
                            nktmp.m_fontColorGradient = new(Color.red, Color.red, new(255, 255, 0), Color.white);
                            nktmp.capitalize = false;
                            udn = nudn;
                            onComplete.Invoke(udn);
                        }));
                    return false;
                }
                if (objectId.Equals("JackpotText")) {
                    UnityDisplayNode udn = null;
                    __instance.FindAndSetupPrototypeAsync("3dcdbc19136c60846ab944ada06695c0",
                        new Action<UnityDisplayNode>(oudn => {
                            var nudn = Object.Instantiate(oudn, __instance.PrototypeRoot);
                            nudn.name = objectId + "(Clone)";
                            nudn.isSprite = true;
                            nudn.RecalculateGenericRenderers();
                            var nktmp = nudn.GetComponentInChildren<NK_TextMeshPro>();
                            nktmp.m_fontColorGradient = new(new(237, 171, 2), new(255, 200, 15), new(255, 255, 0), Color.white);
                            nktmp.capitalize = false;
                            udn = nudn;
                            onComplete.Invoke(udn);
                        }));
                    return false;
                }
                if (objectId.Equals("CashText")) {
                    UnityDisplayNode udn = null;
                    __instance.FindAndSetupPrototypeAsync("3dcdbc19136c60846ab944ada06695c0",
                        new Action<UnityDisplayNode>(oudn => {
                            var nudn = Object.Instantiate(oudn, __instance.PrototypeRoot);
                            nudn.name = objectId + "(Clone)";
                            nudn.isSprite = true;
                            nudn.RecalculateGenericRenderers();
                            var nktmp = nudn.GetComponentInChildren<NK_TextMeshPro>();
                            nktmp.m_fontColorGradient = new(Color.green, Color.green, new(35, 255, 35), Color.white);
                            nktmp.capitalize = false;
                            udn = nudn;
                            onComplete.Invoke(udn);
                        }));
                    return false;
                }

                return true;
            }

            public static void Build() {
                for (var i = 0; i < AdditionalTiers.Towers.Length; i++) {
                    var assets = AdditionalTiers.Towers?[i]?.assetsToRead;
                    if (assets != null) {
                        foreach (var asset in assets) {
                            if (asset != null && !allAssetsKnown.Contains(asset))
                                allAssetsKnown.Add(asset);
                        }
                    }
                }

                hasBeenBuilt = true;
            }

            public static void Flush() => allAssetsKnown.Clear();
        }

        [HarmonyPatch(typeof(ResourceLoader), nameof(ResourceLoader.LoadSpriteFromSpriteReferenceAsync))]
        public sealed class ResourceLoader_Patch {
            [HarmonyPostfix]
            public static void Postfix(SpriteReference reference, Image image) {
                if (reference != null) {
                    if (Images.ResourceManager.GetObject(reference.guidRef) is byte[] bitmap) {
                        var texture = new Texture2D(0, 0);
                        ImageConversion.LoadImage(texture, bitmap);
                        image.canvasRenderer.SetTexture(texture);
                        image.sprite = Sprite.Create(texture, new(0, 0, texture.width, texture.height), new(), 10.2f);
                    } else {
                        var b = Images.ResourceManager.GetObject(reference.guidRef);
                        if (b != null) {
                            var bm = (byte[])new ImageConverter().ConvertTo(b, typeof(byte[]));
                            var texture = new Texture2D(0, 0);
                            ImageConversion.LoadImage(texture, bm);
                            image.canvasRenderer.SetTexture(texture);
                            image.sprite = Sprite.Create(texture, new(0, 0, texture.width, texture.height), new(), 10.2f);
                        }
                    }
                }
            }
        }

        public static class AnimatedAssets {
            private static readonly List<Sprite> _energySprites = new();

            public static List<Sprite> EnergySprites {
                get {
                    if (_energySprites == null || _energySprites.Count == 0) for (var index = 0; index < 64; index++) _energySprites.Add(SpriteBuilder.createProjectile(CacheBuilder.Get(string.Format("energy{0}", index))));
                    return _energySprites;
                }
            }
            private static readonly List<Sprite> _flameSprites = new();

            public static List<Sprite> FlameSprites {
                get {
                    if (_flameSprites == null || _flameSprites.Count == 0) for (var index = 0; index < 10; index++) _flameSprites.Add(SpriteBuilder.createProjectile(CacheBuilder.Get(string.Format("Flame{0}", index)), 7.6f, 0.5f, 0));
                    return _flameSprites;
                }
            }
            private static readonly List<Sprite> _darkFlameSprites = new();

            public static List<Sprite> DarkFlameSprites {
                get {
                    if (_darkFlameSprites == null || _darkFlameSprites.Count == 0) for (var index = 0; index < 10; index++) _darkFlameSprites.Add(SpriteBuilder.createProjectile(CacheBuilder.Get(string.Format("DarkFlame{0}", index)), 7.6f, 0.5f, 0));
                    return _darkFlameSprites;
                }
            }

            static AnimatedAssets() {
                _ = EnergySprites;
                _ = FlameSprites;
                _ = DarkFlameSprites;
            }
        }
    }
}