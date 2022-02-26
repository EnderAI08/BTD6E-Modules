using Assets.Scripts.Data.MapSets;
using Assets.Scripts.Models.Map;
using Assets.Scripts.Models.Map.Gizmos;
using Assets.Scripts.Models.Map.Triggers;
using Assets.Scripts.Simulation.SMath;
using Assets.Scripts.Unity.Map;
using Assets.Scripts.Unity.UI_New;

using HarmonyLib;

using Il2CppSystem.Collections.Generic;

using Maps.Util;

using UnityEngine.SceneManagement;

using Vector2 = Assets.Scripts.Simulation.SMath.Vector2;

namespace Maps.Maps {
    public class Daffodils : Instanced<Daffodils>, MapImpl {
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
        public class MapLoad_Patch {
            [HarmonyPrefix]
            public static bool Prefix(MapLoader __instance, string map, Il2CppSystem.Action<MapModel> loadedCallback) {
                if (map.Equals(Name)) {
                    __instance.currentMapName = map;
                    var mapModel = new MapModel(map, new AreaModel[] {
                            new("Whole", AreaWhole, 0, AreaType.land),
                            new("Track", AreaTrack, 0, AreaType.track),
                            new("Water", AreaWellWater, 0, AreaType.water),
                            new("Land1", AreaLand1, 0, AreaType.land),
                            new("Land2", AreaLand2, 0, AreaType.land),
                            new("Tree1", AreaTree1, 15, AreaType.unplaceable, isBlocker: true),
                            new("Tree2", AreaTree2, 15, AreaType.unplaceable, isBlocker: true),
                            new("Tree3", AreaTree3, 15, AreaType.unplaceable, isBlocker: true),
                            new("Tree4", AreaTree4, 15, AreaType.unplaceable, isBlocker: true)
                        }, new BlockerModel[0], new CoopAreaLayoutModel[] {
                            new(new CoopAreaModel[] {new(0, AreaWhole, new())}, AreaLayoutType.FREE_FOR_ALL)
                        },
                        new PathModel[] {
                            new("Path1", Path1, true, false, new(-1000, -1000, -1000), new(-1000, -1000, -1000), null, null),
                        }, new RemoveableModel[0], new MapGizmoModel[0], 0
                        , new("", new("", new string[] { "Path1" }), new("", new string[] { "Path1" }))
                        , new MapEventModel[0], 1);
                    loadedCallback.Invoke(mapModel);

                    SceneManager.LoadScene(map, new LoadSceneParameters {
                        loadSceneMode = LoadSceneMode.Additive,
                        localPhysicsMode = LocalPhysicsMode.None
                    });

                    return false;
                }

                return true;
            }

            [HarmonyPatch(typeof(UI), nameof(UI.DestroyAndUnloadMapScene))]
            public class MapClear_Patch {
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

        private static PointInfo[] _path1 = null;
        private static Polygon _areaWhole = null;
        private static Polygon _areaTrack = null;
        private static Polygon _areaTree1 = null;
        private static Polygon _areaTree2 = null;
        private static Polygon _areaTree3 = null;
        private static Polygon _areaTree4 = null;
        private static Polygon _areaWellWater = null;
        private static Polygon _areaLand1 = null;
        private static Polygon _areaLand2 = null;

        public static PointInfo[] Path1 {
            get {
                if (_path1 == null) {
                    var initPath = new List<PointInfo>();

                    #region initPath

                    initPath.Add(new() { point = new(150, -93), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(56, -92), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(30, -81), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(20, -68), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(11, -51), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(6, -27), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(6, -20), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(6, 51), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-8, 75), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-21, 80), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-38, 77), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-48, 70), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-58, 57), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-62, 46), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-62, -50), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-67, -71), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-76, -85), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-92, -92), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-106, -92), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-118, -86), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-127, -76), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-135, -61), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-136, -46), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-131, -32), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-123, -22), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-114, -14), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-103, -14), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-91, -12), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(24, -12), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(43, -10), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(57, -1), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(66, 9), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(71, 24), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(72, 37), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(74, 50), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(78, 60), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(86, 71), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(96, 76), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(107, 81), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(121, 81), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(150, 82), bloonScale = 1, moabScale = 1 });

                    #endregion

                    _path1 = initPath.ToArray();
                }

                return _path1;
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

        public static Polygon AreaTree1 {
            get {
                if (_areaTree1 == null) {
                    var initArea = new List<Vector2>();
                    initArea.Add(new(-93, -30));
                    initArea.Add(new(-110, -37));
                    initArea.Add(new(-116, -51));
                    initArea.Add(new(-115, -71));
                    initArea.Add(new(-97, -74));
                    initArea.Add(new(-84, -70));
                    initArea.Add(new(-76, -56));
                    initArea.Add(new(-80, -40));
                    initArea.Add(new(-92, -31));
                    initArea.Add(new(-93, -30));
                    _areaTree1 = new(initArea);
                }

                return _areaTree1;
            }
        }

        public static Polygon AreaTree2 {
            get {
                if (_areaTree2 == null) {
                    var initArea = new List<Vector2>();
                    initArea.Add(new(-150, 44));
                    initArea.Add(new(-135, 50));
                    initArea.Add(new(-126, 61));
                    initArea.Add(new(-115, 62));
                    initArea.Add(new(-103, 68));
                    initArea.Add(new(-95, 75));
                    initArea.Add(new(-94, 84));
                    initArea.Add(new(-97, 94));
                    initArea.Add(new(-99, 108));
                    initArea.Add(new(-104, 111));
                    initArea.Add(new(-120, 111));
                    initArea.Add(new(-130, 105));
                    initArea.Add(new(-138, 99));
                    initArea.Add(new(-139, 91));
                    initArea.Add(new(-147, 89));
                    initArea.Add(new(-150, 89));
                    initArea.Add(new(-150, 44));
                    _areaTree2 = new(initArea);
                }

                return _areaTree2;
            }
        }

        public static Polygon AreaTree3 {
            get {
                if (_areaTree3 == null) {
                    var initArea = new List<Vector2>();
                    initArea.Add(new(-17, 61));
                    initArea.Add(new(-30, 62));
                    initArea.Add(new(-42, 53));
                    initArea.Add(new(-47, 39));
                    initArea.Add(new(-46, 26));
                    initArea.Add(new(-40, 19));
                    initArea.Add(new(-28, 16));
                    initArea.Add(new(-15, 19));
                    initArea.Add(new(-9, 25));
                    initArea.Add(new(-7, 33));
                    initArea.Add(new(-11, 53));
                    initArea.Add(new(-13, 59));
                    initArea.Add(new(-17, 61));
                    _areaTree3 = new(initArea);
                }

                return _areaTree3;
            }
        }

        public static Polygon AreaTree4 {
            get {
                if (_areaTree4 == null) {
                    var initArea = new List<Vector2>();
                    initArea.Add(new(150, -4));
                    initArea.Add(new(145, -10));
                    initArea.Add(new(141, -20));
                    initArea.Add(new(140, -28));
                    initArea.Add(new(126, -32));
                    initArea.Add(new(122, -43));
                    initArea.Add(new(122, -61));
                    initArea.Add(new(123, -73));
                    initArea.Add(new(137, -75));
                    initArea.Add(new(150, -71));
                    initArea.Add(new(150, -4));
                    _areaTree4 = new(initArea);
                }

                return _areaTree4;
            }
        }

        public static Polygon AreaWellWater {
            get {
                if (_areaWellWater == null) {
                    var initArea = new List<Vector2>();
                    initArea.Add(new(75, -32));
                    initArea.Add(new(65, -26));
                    initArea.Add(new(53, -26));
                    initArea.Add(new(43, -33));
                    initArea.Add(new(38, -43));
                    initArea.Add(new(38, -56));
                    initArea.Add(new(43, -67));
                    initArea.Add(new(53, -74));
                    initArea.Add(new(64, -73));
                    initArea.Add(new(75, -66));
                    initArea.Add(new(81, -55));
                    initArea.Add(new(81, -44));
                    initArea.Add(new(75, -32));
                    _areaWellWater = new(initArea);
                }

                return _areaWellWater;
            }
        }

        public static Polygon AreaLand1 {
            get {
                if (_areaLand1 == null) {
                    var initArea = new List<Vector2>();
                    initArea.Add(new(-68, -19));
                    initArea.Add(new(-102, -19));
                    initArea.Add(new(-113, -24));
                    initArea.Add(new(-120, -29));
                    initArea.Add(new(-125, -37));
                    initArea.Add(new(-129, -49));
                    initArea.Add(new(-129, -56));
                    initArea.Add(new(-126, -64));
                    initArea.Add(new(-124, -71));
                    initArea.Add(new(-118, -76));
                    initArea.Add(new(-112, -82));
                    initArea.Add(new(-105, -84));
                    initArea.Add(new(-96, -84));
                    initArea.Add(new(-87, -82));
                    initArea.Add(new(-80, -77));
                    initArea.Add(new(-74, -72));
                    initArea.Add(new(-70, -66));
                    initArea.Add(new(-68, -60));
                    initArea.Add(new(-68, -18));
                    initArea.Add(new(-68, -19));
                    _areaLand1 = new(initArea);
                }

                return _areaLand1;
            }
        }

        public static Polygon AreaLand2 {
            get {
                if (_areaLand2 == null) {
                    var initArea = new List<Vector2>();
                    initArea.Add(new(-54, -5));
                    initArea.Add(new(0, -4));
                    initArea.Add(new(0, 52));
                    initArea.Add(new(-5, 61));
                    initArea.Add(new(-12, 68));
                    initArea.Add(new(-20, 72));
                    initArea.Add(new(-29, 72));
                    initArea.Add(new(-36, 70));
                    initArea.Add(new(-45, 66));
                    initArea.Add(new(-52, 57));
                    initArea.Add(new(-54, 48));
                    initArea.Add(new(-54, -5));
                    _areaLand2 = new(initArea);
                }

                return _areaLand2;
            }
        }

        public static Polygon AreaTrack {
            get {
                if (_areaTrack == null) {
                    var initArea = new List<Vector2>();
                    initArea.Add(new(150, 75));
                    initArea.Add(new(101, 72));
                    initArea.Add(new(86, 59));
                    initArea.Add(new(80, 47));
                    initArea.Add(new(79, 28));
                    initArea.Add(new(76, 11));
                    initArea.Add(new(65, -3));
                    initArea.Add(new(50, -16));
                    initArea.Add(new(43, -17));
                    initArea.Add(new(14, -20));
                    initArea.Add(new(14, -41));
                    initArea.Add(new(17, -56));
                    initArea.Add(new(24, -66));
                    initArea.Add(new(35, -76));
                    initArea.Add(new(48, -82));
                    initArea.Add(new(64, -85));
                    initArea.Add(new(86, -86));
                    initArea.Add(new(119, -85));
                    initArea.Add(new(150, -86));
                    initArea.Add(new(150, -101));
                    initArea.Add(new(49, -100));
                    initArea.Add(new(35, -94));
                    initArea.Add(new(23, -86));
                    initArea.Add(new(10, -71));
                    initArea.Add(new(3, -54));
                    initArea.Add(new(0, -37));
                    initArea.Add(new(0, -18));
                    initArea.Add(new(-54, -18));
                    initArea.Add(new(-56, -54));
                    initArea.Add(new(-57, -66));
                    initArea.Add(new(-62, -79));
                    initArea.Add(new(-72, -89));
                    initArea.Add(new(-88, -98));
                    initArea.Add(new(-105, -99));
                    initArea.Add(new(-118, -94));
                    initArea.Add(new(-131, -85));
                    initArea.Add(new(-137, -75));
                    initArea.Add(new(-142, -60));
                    initArea.Add(new(-142, -46));
                    initArea.Add(new(-139, -34));
                    initArea.Add(new(-133, -21));
                    initArea.Add(new(-123, -12));
                    initArea.Add(new(-114, -6));
                    initArea.Add(new(-99, -3));
                    initArea.Add(new(-69, -4));
                    initArea.Add(new(-67, 54));
                    initArea.Add(new(-57, 70));
                    initArea.Add(new(-50, 80));
                    initArea.Add(new(-41, 85));
                    initArea.Add(new(-24, 86));
                    initArea.Add(new(-12, 85));
                    initArea.Add(new(1, 76));
                    initArea.Add(new(8, 68));
                    initArea.Add(new(12, 54));
                    initArea.Add(new(14, 44));
                    initArea.Add(new(13, -4));
                    initArea.Add(new(40, -3));
                    initArea.Add(new(52, 5));
                    initArea.Add(new(61, 13));
                    initArea.Add(new(64, 23));
                    initArea.Add(new(65, 35));
                    initArea.Add(new(66, 50));
                    initArea.Add(new(71, 63));
                    initArea.Add(new(78, 72));
                    initArea.Add(new(89, 81));
                    initArea.Add(new(103, 87));
                    initArea.Add(new(120, 88));
                    initArea.Add(new(150, 89));
                    _areaTrack = new(initArea);
                }

                return _areaTrack;
            }
        }

        public static string Name = "Daffodils";
    }
}