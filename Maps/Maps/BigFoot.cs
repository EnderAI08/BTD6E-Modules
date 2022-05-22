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
    public class BigFoot : Instanced<BigFoot>, MapImpl {
        private static MapDetails map;
        public void Create(out MapDetails mapDetails) {
            map = new() {
                id = Name,
                isDebug = false,
                difficulty = MapDifficulty.Advanced,
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
            public static bool Prefix(MapLoader __instance) {
                if (__instance.currentMapName.Equals(Name)) {
                    var mapModel = new MapModel(__instance.currentMapName, new AreaModel[] {
                            new("Whole", AreaWhole, new UnhollowerBaseLib.Il2CppReferenceArray<Polygon>(0), 0, AreaType.land),
                            new("Track", AreaTrack, new UnhollowerBaseLib.Il2CppReferenceArray<Polygon>(0), 0, AreaType.track),
                            new("Land1", AreaLand1, new UnhollowerBaseLib.Il2CppReferenceArray<Polygon>(0), 0, AreaType.land),
                            new("Water", AreaWellWater, new UnhollowerBaseLib.Il2CppReferenceArray<Polygon>(0), 0, AreaType.water),
                            new("Tree1", AreaTree1, new UnhollowerBaseLib.Il2CppReferenceArray<Polygon>(0), 15, AreaType.unplaceable, isBlocker: true),
                            new("Tree2", AreaTree2, new UnhollowerBaseLib.Il2CppReferenceArray<Polygon>(0), 15, AreaType.unplaceable, isBlocker: true),
                            new("Tree3", AreaTree3, new UnhollowerBaseLib.Il2CppReferenceArray<Polygon>(0), 15, AreaType.unplaceable, isBlocker: true),
                            new("Tree4", AreaTree4, new UnhollowerBaseLib.Il2CppReferenceArray<Polygon>(0), 15, AreaType.unplaceable, isBlocker: true)
                        }, new BlockerModel[0], new CoopAreaLayoutModel[] {
                            new(new CoopAreaModel[] {new(0, AreaWhole, new())}, AreaLayoutType.FREE_FOR_ALL)
                        },
                        new PathModel[] {
                            new("Path1", Path1, true, false, new(-1000, -1000, -1000), new(-1000, -1000, -1000), null,
                                null),
                        }, new RemoveableModel[0], new MapGizmoModel[0], 0
                        , new("", new("", new string[] { "Path1" }), new("", new string[] { "Path1" }))
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

        public static PointInfo[] Path1 {
            get {
                if (_path1 == null) {
                    var initPath = new List<PointInfo>();

                    #region initPath

                    initPath.Add(new() { point = new(-150, 88), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-128, 65), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-120, 73), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-111, 81), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-102, 89), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-91, 92), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-79, 94), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-64, 92), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-50, 85), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-38, 79), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-26, 76), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-12, 72), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(1, 71), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(10, 68), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(22, 69), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(32, 70), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(44, 67), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(52, 62), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(61, 63), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(75, 62), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(84, 62), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(90, 60), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(98, 43), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(103, 32), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(108, 25), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(110, 12), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(112, 4), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(115, -7), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(117, -18), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(122, -31), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(124, -42), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(150, -57), bloonScale = 1, moabScale = 1 });

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
                    initArea.Add(new(-18, 115));
                    initArea.Add(new(-15, 113));
                    initArea.Add(new(-13, 105));
                    initArea.Add(new(-5, 100));
                    initArea.Add(new(16, 101));
                    initArea.Add(new(25, 103));
                    initArea.Add(new(35, 106));
                    initArea.Add(new(43, 106));
                    initArea.Add(new(44, 115));
                    initArea.Add(new(55, 115));
                    initArea.Add(new(66, 105));
                    initArea.Add(new(74, 105));
                    initArea.Add(new(82, 109));
                    initArea.Add(new(86, 109));
                    initArea.Add(new(88, 102));
                    initArea.Add(new(90, 95));
                    initArea.Add(new(92, 89));
                    initArea.Add(new(96, 85));
                    initArea.Add(new(104, 81));
                    initArea.Add(new(110, 79));
                    initArea.Add(new(116, 82));
                    initArea.Add(new(120, 83));
                    initArea.Add(new(122, 75));
                    initArea.Add(new(124, 69));
                    initArea.Add(new(128, 66));
                    initArea.Add(new(127, 62));
                    initArea.Add(new(125, 54));
                    initArea.Add(new(124, 45));
                    initArea.Add(new(126, 39));
                    initArea.Add(new(133, 38));
                    initArea.Add(new(136, 48));
                    initArea.Add(new(136, 56));
                    initArea.Add(new(138, 62));
                    initArea.Add(new(144, 66));
                    initArea.Add(new(144, 53));
                    initArea.Add(new(143, 41));
                    initArea.Add(new(146, 38));
                    initArea.Add(new(150, 37));
                    initArea.Add(new(150, 115));
                    initArea.Add(new(-18, 115));
                    _areaTree1 = new(initArea);
                }

                return _areaTree1;
            }
        }

        public static Polygon AreaTree2 {
            get {
                if (_areaTree2 == null) {
                    var initArea = new List<Vector2>();
                    initArea.Add(new(-112, 115));
                    initArea.Add(new(-115, 110));
                    initArea.Add(new(-116, 103));
                    initArea.Add(new(-138, 107));
                    initArea.Add(new(-142, 98));
                    initArea.Add(new(-142, 93));
                    initArea.Add(new(-150, 93));
                    initArea.Add(new(-150, 115));
                    initArea.Add(new(-112, 115));
                    _areaTree2 = new(initArea);
                }

                return _areaTree2;
            }
        }

        public static Polygon AreaTree3 {
            get {
                if (_areaTree3 == null) {
                    var initArea = new List<Vector2>();
                    initArea.Add(new(-100, -38));
                    initArea.Add(new(-108, -41));
                    initArea.Add(new(-123, -45));
                    initArea.Add(new(-126, -48));
                    initArea.Add(new(-126, -54));
                    initArea.Add(new(-123, -59));
                    initArea.Add(new(-119, -60));
                    initArea.Add(new(-120, -67));
                    initArea.Add(new(-120, -76));
                    initArea.Add(new(-118, -79));
                    initArea.Add(new(-114, -76));
                    initArea.Add(new(-99, -77));
                    initArea.Add(new(-97, -75));
                    initArea.Add(new(-93, -75));
                    initArea.Add(new(-93, -71));
                    initArea.Add(new(-96, -67));
                    initArea.Add(new(-91, -67));
                    initArea.Add(new(-91, -64));
                    initArea.Add(new(-88, -61));
                    initArea.Add(new(-85, -56));
                    initArea.Add(new(-85, -50));
                    initArea.Add(new(-87, -46));
                    initArea.Add(new(-91, -46));
                    initArea.Add(new(-94, -48));
                    initArea.Add(new(-98, -45));
                    initArea.Add(new(-98, -43));
                    initArea.Add(new(-93, -46));
                    initArea.Add(new(-94, -41));
                    initArea.Add(new(-98, -38));
                    initArea.Add(new(-103, -36));
                    initArea.Add(new(-100, -38));
                    _areaTree3 = new(initArea);
                }

                return _areaTree3;
            }
        }

        public static Polygon AreaTree4 {
            get {
                if (_areaTree4 == null) {
                    var initArea = new List<Vector2>();
                    initArea.Add(new(150, -115));
                    initArea.Add(new(150, -94));
                    initArea.Add(new(140, -101));
                    initArea.Add(new(136, -107));
                    initArea.Add(new(127, -98));
                    initArea.Add(new(122, -105));
                    initArea.Add(new(106, -105));
                    initArea.Add(new(89, -106));
                    initArea.Add(new(86, -110));
                    initArea.Add(new(82, -109));
                    initArea.Add(new(73, -100));
                    initArea.Add(new(56, -113));
                    initArea.Add(new(47, -96));
                    initArea.Add(new(37, -90));
                    initArea.Add(new(28, -97));
                    initArea.Add(new(18, -106));
                    initArea.Add(new(14, -93));
                    initArea.Add(new(9, -86));
                    initArea.Add(new(-1, -80));
                    initArea.Add(new(-10, -82));
                    initArea.Add(new(-18, -85));
                    initArea.Add(new(-22, -89));
                    initArea.Add(new(-22, -101));
                    initArea.Add(new(-35, -97));
                    initArea.Add(new(-43, -101));
                    initArea.Add(new(-47, -106));
                    initArea.Add(new(-54, -104));
                    initArea.Add(new(-67, -96));
                    initArea.Add(new(-76, -100));
                    initArea.Add(new(-88, -107));
                    initArea.Add(new(-85, -101));
                    initArea.Add(new(-89, -99));
                    initArea.Add(new(-104, -85));
                    initArea.Add(new(-108, -85));
                    initArea.Add(new(-125, -98));
                    initArea.Add(new(-129, -98));
                    initArea.Add(new(-142, -89));
                    initArea.Add(new(-138, -82));
                    initArea.Add(new(-136, -72));
                    initArea.Add(new(-136, -67));
                    initArea.Add(new(-149, -55));
                    initArea.Add(new(-146, -43));
                    initArea.Add(new(-144, -29));
                    initArea.Add(new(-149, -18));
                    initArea.Add(new(-150, -115));
                    initArea.Add(new(150, -115));
                    _areaTree4 = new(initArea);
                }

                return _areaTree4;
            }
        }

        public static Polygon AreaWellWater {
            get {
                if (_areaWellWater == null) {
                    var initArea = new List<Vector2>();
                    initArea.Add(new(-19, -19));
                    initArea.Add(new(-20, -9));
                    initArea.Add(new(-14, 2));
                    initArea.Add(new(-2, 7));
                    initArea.Add(new(12, 6));
                    initArea.Add(new(24, -2));
                    initArea.Add(new(34, -22));
                    initArea.Add(new(36, -27));
                    initArea.Add(new(37, -43));
                    initArea.Add(new(36, -49));
                    initArea.Add(new(28, -55));
                    initArea.Add(new(8, -46));
                    initArea.Add(new(0, -44));
                    initArea.Add(new(-8, -38));
                    initArea.Add(new(-19, -17));
                    initArea.Add(new(-19, -19));
                    _areaWellWater = new(initArea);
                }

                return _areaWellWater;
            }
        }

        public static Polygon AreaLand1 {
            get {
                if (_areaLand1 == null) {
                    var initArea = new List<Vector2>();
                    initArea.Add(new(-102, 16));
                    initArea.Add(new(-105, 17));
                    initArea.Add(new(-114, 34));
                    initArea.Add(new(-114, 46));
                    initArea.Add(new(-111, 57));
                    initArea.Add(new(-106, 67));
                    initArea.Add(new(-95, 76));
                    initArea.Add(new(-84, 81));
                    initArea.Add(new(-58, 76));
                    initArea.Add(new(-51, 71));
                    initArea.Add(new(-21, 59));
                    initArea.Add(new(5, 54));
                    initArea.Add(new(23, 56));
                    initArea.Add(new(36, 56));
                    initArea.Add(new(48, 49));
                    initArea.Add(new(61, 16));
                    initArea.Add(new(60, -5));
                    initArea.Add(new(66, -43));
                    initArea.Add(new(80, -48));
                    initArea.Add(new(90, -54));
                    initArea.Add(new(107, -60));
                    initArea.Add(new(111, -73));
                    initArea.Add(new(104, -81));
                    initArea.Add(new(82, -81));
                    initArea.Add(new(75, -75));
                    initArea.Add(new(61, -67));
                    initArea.Add(new(44, -59));
                    initArea.Add(new(29, -54));
                    initArea.Add(new(-19, -18));
                    initArea.Add(new(-29, -10));
                    initArea.Add(new(-37, 1));
                    initArea.Add(new(-53, 6));
                    initArea.Add(new(-63, 6));
                    initArea.Add(new(-86, 5));
                    initArea.Add(new(-100, 15));
                    initArea.Add(new(-102, 16));
                    _areaLand1 = new(initArea);
                }

                return _areaLand1;
            }
        }

        public static Polygon AreaTrack {
            get {
                if (_areaTrack == null) {
                    var initArea = new List<Vector2>();
                    initArea.Add(new(-150, 65));
                    initArea.Add(new(-136, 51));
                    initArea.Add(new(-136, 41));
                    initArea.Add(new(-134, 31));
                    initArea.Add(new(-132, 21));
                    initArea.Add(new(-126, 8));
                    initArea.Add(new(-118, -3));
                    initArea.Add(new(-110, -10));
                    initArea.Add(new(-98, -15));
                    initArea.Add(new(-87, -17));
                    initArea.Add(new(-77, -16));
                    initArea.Add(new(-62, -14));
                    initArea.Add(new(-54, -13));
                    initArea.Add(new(-48, -15));
                    initArea.Add(new(-36, -28));
                    initArea.Add(new(-30, -39));
                    initArea.Add(new(-24, -47));
                    initArea.Add(new(-18, -55));
                    initArea.Add(new(-12, -61));
                    initArea.Add(new(-7, -63));
                    initArea.Add(new(1, -69));
                    initArea.Add(new(10, -73));
                    initArea.Add(new(19, -76));
                    initArea.Add(new(23, -76));
                    initArea.Add(new(34, -81));
                    initArea.Add(new(41, -86));
                    initArea.Add(new(48, -88));
                    initArea.Add(new(57, -92));
                    initArea.Add(new(65, -96));
                    initArea.Add(new(75, -101));
                    initArea.Add(new(84, -104));
                    initArea.Add(new(103, -104));
                    initArea.Add(new(110, -102));
                    initArea.Add(new(117, -99));
                    initArea.Add(new(124, -94));
                    initArea.Add(new(131, -88));
                    initArea.Add(new(131, -79));
                    initArea.Add(new(132, -69));
                    initArea.Add(new(132, -62));
                    initArea.Add(new(150, -71));
                    initArea.Add(new(150, -41));
                    initArea.Add(new(128, -31));
                    initArea.Add(new(125, -22));
                    initArea.Add(new(124, -11));
                    initArea.Add(new(122, 5));
                    initArea.Add(new(120, 11));
                    initArea.Add(new(114, 14));
                    initArea.Add(new(113, 25));
                    initArea.Add(new(112, 33));
                    initArea.Add(new(106, 39));
                    initArea.Add(new(104, 42));
                    initArea.Add(new(101, 49));
                    initArea.Add(new(99, 57));
                    initArea.Add(new(96, 63));
                    initArea.Add(new(93, 69));
                    initArea.Add(new(86, 71));
                    initArea.Add(new(74, 72));
                    initArea.Add(new(67, 72));
                    initArea.Add(new(58, 71));
                    initArea.Add(new(54, 72));
                    initArea.Add(new(46, 76));
                    initArea.Add(new(36, 80));
                    initArea.Add(new(23, 79));
                    initArea.Add(new(12, 76));
                    initArea.Add(new(3, 76));
                    initArea.Add(new(-4, 77));
                    initArea.Add(new(-12, 80));
                    initArea.Add(new(-25, 85));
                    initArea.Add(new(-34, 89));
                    initArea.Add(new(-45, 93));
                    initArea.Add(new(-51, 97));
                    initArea.Add(new(-75, 102));
                    initArea.Add(new(-95, 101));
                    initArea.Add(new(-109, 97));
                    initArea.Add(new(-115, 90));
                    initArea.Add(new(-121, 82));
                    initArea.Add(new(-125, 76));
                    initArea.Add(new(-150, 93));
                    initArea.Add(new(-150, 65));
                    _areaTrack = new(initArea);
                }

                return _areaTrack;
            }
        }

        public static string Name = "BigFoot";
    }
}