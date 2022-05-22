
using System;

using Assets.Scripts.Data.MapSets;
using Assets.Scripts.Models.Map;
using Assets.Scripts.Models.Map.Gizmos;
using Assets.Scripts.Models.Map.Triggers;
using Assets.Scripts.Simulation.SimulationBehaviors;
using Assets.Scripts.Simulation.SMath;
using Assets.Scripts.Unity.Map;
using Assets.Scripts.Unity.UI_New;

using HarmonyLib;

using Il2CppSystem.Collections.Generic;

using Maps.Util;

using UnityEngine.SceneManagement;

using Vector2 = Assets.Scripts.Simulation.SMath.Vector2;

namespace Maps.Maps {
    public sealed class CursedMeadows : Instanced<CursedMeadows>, MapImpl {
        private static MapDetails map;

        public void Create(out MapDetails mapDetails) {
            map = new() {
                id = Name,
                isDebug = false,
                difficulty = MapDifficulty.Beginner,
                coopMapDivisionType = CoopDivision.FREE_FOR_ALL,
                unlockDifficulty = MapDifficulty.Beginner,
                mapMusic = "MusicUpbeat1A",
                mapSprite = new(Name)
            };
            mapDetails = map;
        }

        public MapDetails GetCreated() {
            if (map == null)
                Create(out map);
            return map;
        }

        public void Destroy() => map = null;

        [HarmonyPatch(typeof(MapLoader), nameof(MapLoader.Load))]
        public static class MapLoad_Patch {
            [HarmonyPrefix]
            public static bool Prefix(MapLoader __instance) {
                if (__instance.currentMapName.Equals(Name)) {
                    List<PathModel> paths = new();
                    List<string> names = new();
                    Random random = new Random(DateTime.Now.Millisecond);

                    for (int i = 0; i < 1000; i++) {
                        var initPath = new List<PointInfo>();

                        for (int j = 0; j < 5; j++) {
                            initPath.Add(new() { point = new(random.Next(-150, 150), random.Next(-115, 115)), bloonScale = (float)System.Math.Max(random.NextDouble(), 0.01), moabScale = (float)System.Math.Max(random.NextDouble(), 0.01) });
                        }

                        initPath.Add(new() { point = new(-17, 110), bloonScale = 1, moabScale = 1 });
                        initPath.Add(new() { point = new(-17, 115), bloonScale = 1, moabScale = 1 });
                        initPath.Add(new() { point = new(-17, 130), bloonScale = 1, moabScale = 1 });

                        paths.Add(new PathModel($"Path{i + 1}", new(initPath.ToArray()), true, false, new(-1000, -1000, -1000), new(-1000, -1000, -1000), null, null));
                        names.Add($"Path{i + 1}");
                    }

                    var mapModel = new MapModel(__instance.currentMapName, new AreaModel[] {
                            new("Whole", AreaWhole, new UnhollowerBaseLib.Il2CppReferenceArray<Polygon>(0), 0, AreaType.track),
                            new("Whole2", AreaWhole, new UnhollowerBaseLib.Il2CppReferenceArray<Polygon>(0), 0, AreaType.land)
                        }, new BlockerModel[0], new CoopAreaLayoutModel[] {
                            new(new CoopAreaModel[] {new(0, AreaWhole, new())}, AreaLayoutType.FREE_FOR_ALL)
                        },
                        new(paths.ToArray()), new RemoveableModel[0], new MapGizmoModel[0], 0
                        , new("", new("", new(names.ToArray())),
                        new("", new(names.ToArray())))
                        , new MapEventModel[0], 1);

                    SceneManager.LoadScene(__instance.currentMapName, new LoadSceneParameters {
                        loadSceneMode = LoadSceneMode.Additive,
                        localPhysicsMode = LocalPhysicsMode.None
                    });

                    return false;
                }

                return true;
            }

            [HarmonyPatch(typeof(UI), nameof(UI.DestroyAndUnloadMapScene))]
            public static class MapClear_Patch {
                [HarmonyPrefix]
                public static bool Prefix(UI __instance) {
                    if (__instance.mapLoader.currentMapName.Equals(Name)) {
                        SceneManager.UnloadScene(Name);
                        return false;
                    }

                    return true;
                }
            }
        }

        [HarmonyPatch(typeof(PreGamePrep), nameof(PreGamePrep.AddRoadSpikesToEndOfTrack))]
        public sealed class PreGamePrep_AddRoadSpikesToEndOfTrack {
            [HarmonyPrefix]
            public static bool Prefix(PreGamePrep __instance) {
                return !__instance.Sim.map.mapModel.mapName.Equals(Name);
            }
        }

        [HarmonyPatch(typeof(PreGamePrep), nameof(PreGamePrep.DoAfterMapLoad))]
        public sealed class PreGamePrep_DoAfterMapLoad {
            [HarmonyPrefix]
            public static bool Prefix(PreGamePrep __instance) {
                return !__instance.Sim.map.mapModel.mapName.Equals(Name);
            }
        }

        private static Polygon _areaWhole;

        public static Polygon AreaWhole {
            get {
                if (_areaWhole == null) {
                    var initArea = new List<Vector2>();
                    initArea.Add(new(-330, -330));
                    initArea.Add(new(-330, 330));
                    initArea.Add(new(330, 330));
                    initArea.Add(new(330, -330));
                    _areaWhole = new(initArea);
                }

                return _areaWhole;
            }
        }

        public static string Name = "CursedMeadows";
    }
}