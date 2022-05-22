using System.Diagnostics.CodeAnalysis;

using AdditionalBloons.Resources;
using AdditionalBloons.Utils;

namespace AdditionalBloons.Tasks {
    public sealed class Assets {
        [HarmonyPatch(typeof(Factory), nameof(Factory.FindAndSetupPrototypeAsync))]
        public static class DisplayFactory {
            private static List<AssetInfo> allAssetsKnown = new();

            [HarmonyPrefix]
            public static bool Prefix(Factory __instance, string objectId, Il2CppSystem.Action<UnityDisplayNode> onComplete) {
                foreach (var curAsset in allAssetsKnown) {
                    if (objectId.Equals(curAsset.CustomAssetName)) {
                        if (curAsset.RendererType == RendererType.SPRITERENDERER) {
                            GameObject obj = Object.Instantiate(new GameObject(objectId + "(Clone)"), __instance.PrototypeRoot);
                            var sr = obj.AddComponent<SpriteRenderer>();
                            sr.sprite = SpriteBuilder.createBloon(CacheBuilder.Get(objectId));
                            var udn = obj.AddComponent<UnityDisplayNode>();
                            udn.transform.position = new(-3000, 10);

                            if (objectId.Contains("JailBars"))
                                udn.gameObject.AddComponent<MoveUp>();

                            onComplete.Invoke(udn);

                            return false;
                        }
                        if (curAsset.RendererType == RendererType.SKINNEDMESHRENDERER) {
                            UnityDisplayNode udn = null!;
                            __instance.FindAndSetupPrototypeAsync(curAsset.BTDAssetName, new Action<UnityDisplayNode>(btdUdn => {
                                var instance = Object.Instantiate(btdUdn, __instance.PrototypeRoot);
                                instance.name = objectId + "(Clone)";
                                instance.RecalculateGenericRenderers();

                                for (var i = 0; i < instance.genericRenderers.Length; i++) {
                                    instance.genericRenderers[i].material.mainTexture = CacheBuilder.Get(objectId);
                                    if (objectId.StartsWith("FireBAD", StringComparison.OrdinalIgnoreCase))
                                        instance.genericRenderers[i].material.SetColor("_OutlineColor", new Color32(150, 0, 0, 255));
                                    else if (objectId.StartsWith("CopBAD", StringComparison.OrdinalIgnoreCase))
                                        instance.genericRenderers[i].material.SetColor("_OutlineColor", new Color32(0, 12, 38, 255));
                                }

                                udn = instance;
                                onComplete.Invoke(udn);
                            }));
                            return false;
                        }
                    }
                }
                return true;
            }

            public static void Build() {
                for (var en = BloonCreator.assets.GetEnumerator(); en.MoveNext();)
                    allAssetsKnown.Add(en.Current);
            }

            public static void Flush() => allAssetsKnown.Clear();
        }

        [HarmonyPatch(typeof(ResourceLoader), nameof(ResourceLoader.LoadSpriteFromSpriteReferenceAsync))]
        public static class ResourceLoader_Patch {
            [HarmonyPostfix]
            [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Windows rules linux drools!")]
            public static void Postfix(SpriteReference reference, Image image) {
                if (reference != null) {
                    var bitmap = BloonSprites.ResourceManager.GetObject(reference.guidRef) as byte[];
                    if (bitmap != null) {
                        var texture = new Texture2D(0, 0);
                        ImageConversion.LoadImage(texture, bitmap);
                        image.canvasRenderer.SetTexture(texture);
                        image.sprite = Sprite.Create(texture, new(0, 0, texture.width, texture.height), new(), 10.2f);
                    } else {
                        var b = BloonSprites.ResourceManager.GetObject(reference.guidRef);
                        if (b != null) {
                            var bm = new ImageConverter().ConvertTo(b, typeof(byte[])) as byte[];
                            var texture = new Texture2D(0, 0);
                            ImageConversion.LoadImage(texture, bm);
                            image.canvasRenderer.SetTexture(texture);
                            image.sprite = Sprite.Create(texture, new(0, 0, texture.width, texture.height), new(), 10.2f);
                        }
                    }
                }
            }
        }
    }
}