namespace Ultra_Powers;
internal static class Assets {
    internal static List<string> SpriteAssets = new();
    internal static List<(string, string, int)> RendererAssets = new();

    [HarmonyPatch(typeof(Factory), nameof(Factory.FindAndSetupPrototypeAsync))]
    public static class DisplayFactory {
        [HarmonyPrefix]
        public static bool Prefix(Factory __instance, string objectId, Il2CppSystem.Action<UnityDisplayNode> onComplete) {
            foreach (var curAsset in RendererAssets) {
                if (objectId.Equals(curAsset.Item1)) {
                    UnityDisplayNode udn = null!;
                    __instance.FindAndSetupPrototypeAsync(curAsset.Item2, new Action<UnityDisplayNode>(btdUdn => {
                        var instance = Object.Instantiate(btdUdn, __instance.PrototypeRoot);
                        instance.name = objectId + "(Clone)";
                        instance.RecalculateGenericRenderers();

                        instance.genericRenderers[curAsset.Item3].material.mainTexture = objectId.GetEmbeddedResource().ToTexture();

                        udn = instance;
                        onComplete.Invoke(udn);
                    }));
                    return false;
                }
            }
            return true;
        }
    }


    [HarmonyPatch(typeof(ResourceLoader), nameof(ResourceLoader.LoadSpriteFromSpriteReferenceAsync))]
    public static class ResourceLoader_Patch {
        [HarmonyPostfix]
        public static void Postfix(SpriteReference reference, Image image) {
            if (reference != null && SpriteAssets.Contains(reference.guidRef)) {
                try {
                    var texture = reference.guidRef.GetEmbeddedResource().ToTexture();
                    image.canvasRenderer.SetTexture(texture);
                    image.sprite = Sprite.Create(texture, new(0, 0, texture.width, texture.height), new(), 10.2f);
                } catch {}
            }
        }
    }
}