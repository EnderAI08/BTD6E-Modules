
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
    public sealed class PVZGarden : Instanced<PVZGarden>, MapImpl {
        private static MapDetails map;

        public void Create(out MapDetails mapDetails) {
            map = new() {
                id = Name,
                isDebug = false,
                difficulty = MapDifficulty.Expert,
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
                    var mapModel = new MapModel(__instance.currentMapName, new AreaModel[] {
                            new("Whole", AreaWhole, new UnhollowerBaseLib.Il2CppReferenceArray<Polygon>(0), 0, AreaType.unplaceable),
                            new("Track", AreaLand, new UnhollowerBaseLib.Il2CppReferenceArray<Polygon>(0), 0, AreaType.track),
                            new("Land", AreaLand, new UnhollowerBaseLib.Il2CppReferenceArray<Polygon>(0), 0, AreaType.land)
                        }, new BlockerModel[0], new CoopAreaLayoutModel[] {
                            new(new CoopAreaModel[] {new(0, AreaWhole, new())}, AreaLayoutType.FREE_FOR_ALL)
                        },
                        new PathModel[] {
                            new("Path1", Path1, true, false, new(-1000, -1000, -1000), new(-1000, -1000, -1000), null, null),
                            new("Path2", Path2, true, false, new(-1000, -1000, -1000), new(-1000, -1000, -1000), null, null),
                            new("Path3", Path3, true, false, new(-1000, -1000, -1000), new(-1000, -1000, -1000), null, null),
                            new("Path4", Path4, true, false, new(-1000, -1000, -1000), new(-1000, -1000, -1000), null, null),
                            new("Path5", Path5, true, false, new(-1000, -1000, -1000), new(-1000, -1000, -1000), null, null),
                        }, new RemoveableModel[0], new MapGizmoModel[0], 0
                        , new("", new("", new string[] { "Path1", "Path2", "Path3", "Path4", "Path5" }),
                        new("", new string[] { "Path1", "Path2", "Path3", "Path4", "Path5" }))
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

        private static PointInfo[] _path1;
        private static PointInfo[] _path2;
        private static PointInfo[] _path3;
        private static PointInfo[] _path4;
        private static PointInfo[] _path5;
        private static Polygon _areaWhole;
        private static Polygon _areaLand;

        public static PointInfo[] Path1 {
            get {
                if (_path1 == null) {
                    var initPath = new List<PointInfo>();

                    #region initPath

                    initPath.Add(new() { point = new(107, -54), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-78, -55), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-86, -55), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-90, -55), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-92, -54), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-92, -50), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-93, -46), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-94, -43), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-94, -39), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-94, -30), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-96, -17), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-96, 0), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-99, 27), bloonScale = 1, moabScale = 1 });

                    initPath.Add(new() { point = new(-109, 27), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-114, 21), bloonScale = 0, moabScale = 0 });
                    initPath.Add(new() { point = new(-200, 12), bloonScale = 0, moabScale = 0 });

                    #endregion

                    _path1 = initPath.ToArray();
                }

                return _path1;
            }
        }

        public static PointInfo[] Path2 {
            get {
                if (_path2 == null) {
                    var initPath = new List<PointInfo>();

                    #region initPath

                    initPath.Add(new() { point = new(110, -24), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-77, -24), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-89, -25), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-92, -25), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-92, -25), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-94, -23), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-94, -22), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-94, -18), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-95, -16), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-95, -11), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-97, 30), bloonScale = 1, moabScale = 1 });

                    initPath.Add(new() { point = new(-109, 27), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-114, 21), bloonScale = 0, moabScale = 0 });
                    initPath.Add(new() { point = new(-200, 12), bloonScale = 0, moabScale = 0 });

                    #endregion

                    _path2 = initPath.ToArray();
                }

                return _path2;
            }
        }

        public static PointInfo[] Path3 {
            get {
                if (_path3 == null) {
                    var initPath = new List<PointInfo>();

                    #region initPath

                    initPath.Add(new() { point = new(99, 11), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-77, 11), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-90, 6), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-94, 7), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-95, 9), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-95, 11), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-95, 13), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-96, 33), bloonScale = 1, moabScale = 1 });

                    initPath.Add(new() { point = new(-109, 27), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-114, 21), bloonScale = 0, moabScale = 0 });
                    initPath.Add(new() { point = new(-200, 12), bloonScale = 0, moabScale = 0 });

                    #endregion

                    _path3 = initPath.ToArray();
                }

                return _path3;
            }
        }

        public static PointInfo[] Path4 {
            get {
                if (_path4 == null) {
                    var initPath = new List<PointInfo>();

                    #region initPath

                    initPath.Add(new() { point = new(109, 42), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-78, 41), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-92, 36), bloonScale = 1, moabScale = 1 });

                    initPath.Add(new() { point = new(-109, 27), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-114, 21), bloonScale = 0, moabScale = 0 });
                    initPath.Add(new() { point = new(-200, 12), bloonScale = 0, moabScale = 0 });

                    #endregion

                    _path4 = initPath.ToArray();
                }

                return _path4;
            }
        }

        public static PointInfo[] Path5 {
            get {
                if (_path5 == null) {
                    var initPath = new List<PointInfo>();

                    #region initPath

                    initPath.Add(new() { point = new(107, 72), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-78, 73), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-95, 74), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-98, 73), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-98, 71), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-98, 69), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-98, 66), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-98, 63), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-95, 31), bloonScale = 1, moabScale = 1 });

                    initPath.Add(new() { point = new(-109, 27), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-114, 21), bloonScale = 0, moabScale = 0 });
                    initPath.Add(new() { point = new(-200, 12), bloonScale = 0, moabScale = 0 });

                    #endregion

                    _path5 = initPath.ToArray();
                }

                return _path5;
            }
        }

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

        public static Polygon AreaLand {
            get {
                if (_areaLand == null) {
                    var initArea = new List<Vector2>();

                    initArea.Add(new(-86, -72));
                    initArea.Add(new(-67, -74));
                    initArea.Add(new(-49, -75));
                    initArea.Add(new(-28, -77));
                    initArea.Add(new(-11, -78));
                    initArea.Add(new(9, -78));
                    initArea.Add(new(27, -78));
                    initArea.Add(new(47, -75));
                    initArea.Add(new(64, -69));
                    initArea.Add(new(81, -62));
                    initArea.Add(new(80, -54));
                    initArea.Add(new(81, -37));
                    initArea.Add(new(82, -24));
                    initArea.Add(new(82, -10));
                    initArea.Add(new(81, 3));
                    initArea.Add(new(79, 13));
                    initArea.Add(new(78, 18));
                    initArea.Add(new(78, 24));
                    initArea.Add(new(86, 45));
                    initArea.Add(new(82, 59));
                    initArea.Add(new(81, 67));
                    initArea.Add(new(83, 74));
                    initArea.Add(new(82, 80));
                    initArea.Add(new(82, 92));
                    initArea.Add(new(72, 90));
                    initArea.Add(new(65, 91));
                    initArea.Add(new(42, 90));
                    initArea.Add(new(36, 90));
                    initArea.Add(new(-8, 91));
                    initArea.Add(new(-91, 89));
                    initArea.Add(new(-86, -72));

                    _areaLand = new(initArea);
                }

                return _areaLand;
            }
        }

        public static string Name = "PVZGarden";
    }
}