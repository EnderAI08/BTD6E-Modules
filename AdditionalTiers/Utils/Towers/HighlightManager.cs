﻿namespace AdditionalTiers.Utils.Towers {
    internal static class HighlightManager {
        public static void Highlight(ref Tower __instance) {
            if (__instance?.Node?.graphic?.genericRenderers != null) {
                for (int i = 0; i < __instance.Node.graphic.genericRenderers.Count; i++)
                    __instance.Node.graphic.genericRenderers[i].material.SetColor("_OutlineColor", Color.white);
            }

            if (__instance?.attackBehaviorsInDependants != null) {
                for (int i = 0; i < __instance.attackBehaviorsInDependants.Count; i++) {
                    try {
                        for (int i1 = 0; i1 < __instance.attackBehaviorsInDependants[i]?.entity?.displayBehaviorCache?.node?.graphic?.genericRenderers.Count; i1++)
                            __instance.attackBehaviorsInDependants[i].entity.displayBehaviorCache.node.graphic.genericRenderers[i1].material.SetColor("_OutlineColor", Color.white);
                    } catch (NullReferenceException e) {
                        Logger13.Warn(e.Message);
                    }
                }
            }
        }

        public static void UnHighlight(ref Tower __instance) {
            if (__instance?.Node?.graphic?.genericRenderers != null) {
                for (int i = 0; i < __instance.Node.graphic.genericRenderers.Count; i++)
                    __instance.Node.graphic.genericRenderers[i].material.SetColor("_OutlineColor", GetResetColor(__instance.display));
            }

            if (__instance?.attackBehaviorsInDependants != null) {
                for (int i = 0; i < __instance.attackBehaviorsInDependants.Count; i++) {
                    try {
                        for (int i1 = 0; i1 < __instance.attackBehaviorsInDependants[i]?.entity?.displayBehaviorCache?.node?.graphic?.genericRenderers.Count; i1++)
                            __instance.attackBehaviorsInDependants[i].entity.displayBehaviorCache.node.graphic.genericRenderers[i1].material.SetColor("_OutlineColor", GetResetColor(__instance.attackBehaviorsInDependants[i].entity.displayBehaviorCache));
                    } catch (NullReferenceException e) {
                        Logger13.Warn(e.Message);
                    }
                }
            }
        }
    }
}