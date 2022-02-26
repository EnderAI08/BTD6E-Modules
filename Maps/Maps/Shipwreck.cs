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

using SixthTiers.Utils;

using UnityEngine.SceneManagement;

using Vector2 = Assets.Scripts.Simulation.SMath.Vector2;

namespace Maps.Maps {
    public class Shipwreck : Instanced<Shipwreck>, MapImpl {
        private static MapDetails map;
        public void Create(out MapDetails mapDetails) {
            map = new() {
                id = Name,
                isDebug = false,
                difficulty = MapDifficulty.Advanced,
                coopMapDivisionType = CoopDivision.FREE_FOR_ALL,
                unlockDifficulty = MapDifficulty.Beginner,
                mapMusic = "MusicDarkA",
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

                    PointInfo[] Path2 = Path1.Reversed();
                    var mapModel = new MapModel(map, new AreaModel[] {
                            new("Whole", AreaWhole, 0, AreaType.water),
                            new("UP1", AreaUp1, 0, AreaType.unplaceable),
                            new("UP2", AreaUp2, 0, AreaType.unplaceable),
                            new("UP3", AreaUp3, 0, AreaType.unplaceable),
                            new("UP4", AreaUp4, 0, AreaType.unplaceable),
                            new("Crate", AreaCrate, 0, AreaType.land),
                            new("Barrel", AreaBarrel, 0, AreaType.land),
                            new("Rock1", AreaSRock, 0, AreaType.land),
                            new("Rock2", AreaMRock, 0, AreaType.land),
                            new("Rock3", AreaLRock, 0, AreaType.land),
                            new("Rock4", AreaTRock1, 0, AreaType.land),
                            new("Rock5", AreaTRock2, 0, AreaType.land),
                            new("WatchTower", AreaWatchTowerTop, 0, AreaType.land),
                            new("LeftShip", AreaLeftShip, 0, AreaType.land),
                            new("RightShip", AreaRightShip, 0, AreaType.land),
                            new("Straight1", AreaStraight1, 0, AreaType.track)
                    }, new BlockerModel[0], new CoopAreaLayoutModel[]
                        {
                            new(new CoopAreaModel[] {new(0, AreaWhole, new())}, AreaLayoutType.FREE_FOR_ALL)
                        },
                        new PathModel[] {
                            new("Path1", Path1, true, false, new(-1000, -1000, -1000), new(-1000, -1000, -1000), null, null),
                            new("Path2", Path2, true, false, new(-1000, -1000, -1000), new(-1000, -1000, -1000), null, null)
                    }, new RemoveableModel[0], new MapGizmoModel[0], 0
                    , new("", new("", new string[] { "Path1", "Path2" }), new("", new string[] { "Path1", "Path2" }))
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
        private static Polygon _areaLeftShip = null;
        private static Polygon _areaRightShip = null;
        private static Polygon _areaCrate = null;
        private static Polygon _areaBarrel = null;
        private static Polygon _areaSRock = null;
        private static Polygon _areaLRock = null;
        private static Polygon _areaMRock = null;
        private static Polygon _areaTRock1 = null;
        private static Polygon _areaTRock2 = null;
        private static Polygon _areaWatchTowerTop = null;
        private static Polygon _areaUp1 = null;
        private static Polygon _areaUp2 = null;
        private static Polygon _areaUp3 = null;
        private static Polygon _areaUp4 = null;
        private static Polygon _areaStraight1 = null;

        public static PointInfo[] Path1 {
            get {
                if (_path1 == null) {
                    var initPath = new List<PointInfo>();

                    #region initPath
                    initPath.Add(new() { point = new(-37, 125), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-36, 92), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-40, 78), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-41, 69), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-45, 60), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-64, 52), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-81, 46), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-101, 41), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-120, 24), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-129, 3), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-133, -10), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-131, -22), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-125, -40), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-119, -54), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-110, -63), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-97, -72), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-79, -74), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-65, -72), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-51, -65), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-41, -54), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-39, -36), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-38, -19), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-37, 0), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-38, 20), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-36, 37), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-28, 49), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(-7, 52), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(5, 46), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(10, 31), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(10, 13), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(12, -4), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(12, -20), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(12, -35), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(14, -50), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(18, -63), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(31, -70), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(47, -73), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(67, -73), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(83, -66), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(102, -55), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(112, -41), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(121, -26), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(123, 1), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(117, 14), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(111, 25), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(104, 37), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(90, 47), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(74, 50), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(60, 51), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(43, 54), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(33, 58), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(26, 65), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(23, 71), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(19, 82), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(17, 96), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(16, 105), bloonScale = 1, moabScale = 1 });
                    initPath.Add(new() { point = new(16, 125), bloonScale = 1, moabScale = 1 });

                    #endregion

                    _path1 = initPath.ToArray();
                }

                return _path1;
            }
        }

        #region AreaWhole

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

        #endregion

        #region AreaLeftShip

        public static Polygon AreaLeftShip {
            get {
                if (_areaLeftShip == null) {
                    var initArea = new List<Vector2>();
                    initArea.Add(new(-54, 36));
                    initArea.Add(new(-71, 34));
                    initArea.Add(new(-92, 30));
                    initArea.Add(new(-107, 26));
                    initArea.Add(new(-107, -47));
                    initArea.Add(new(-89, -53));
                    initArea.Add(new(-75, -55));
                    initArea.Add(new(-56, -57));
                    initArea.Add(new(-54, -57));
                    initArea.Add(new(-53, -48));
                    initArea.Add(new(-57, -39));
                    initArea.Add(new(-55, -35));
                    initArea.Add(new(-57, -32));
                    initArea.Add(new(-56, -30));
                    initArea.Add(new(-57, -24));
                    initArea.Add(new(-56, -22));
                    initArea.Add(new(-56, -14));
                    initArea.Add(new(-57, -4));
                    initArea.Add(new(-58, 7));
                    initArea.Add(new(-59, 8));
                    initArea.Add(new(-58, 12));
                    initArea.Add(new(-58, 15));
                    initArea.Add(new(-58, 18));
                    initArea.Add(new(-57, 21));
                    initArea.Add(new(-57, 25));
                    initArea.Add(new(-55, 26));
                    initArea.Add(new(-53, 35));
                    initArea.Add(new(-54, 36));
                    _areaLeftShip = new(initArea);
                }

                return _areaLeftShip;
            }
        }

        #endregion

        #region AreaRightShip

        public static Polygon AreaRightShip {
            get {
                if (_areaRightShip == null) {
                    var initArea = new List<Vector2>();
                    initArea.Add(new(35, -52));
                    initArea.Add(new(36, -44));
                    initArea.Add(new(37, -39));
                    initArea.Add(new(36, -35));
                    initArea.Add(new(39, -32));
                    initArea.Add(new(37, -28));
                    initArea.Add(new(37, -24));
                    initArea.Add(new(37, -20));
                    initArea.Add(new(36, -14));
                    initArea.Add(new(36, -9));
                    initArea.Add(new(36, -6));
                    initArea.Add(new(38, 0));
                    initArea.Add(new(39, 5));
                    initArea.Add(new(38, 11));
                    initArea.Add(new(36, 10));
                    initArea.Add(new(36, 16));
                    initArea.Add(new(37, 21));
                    initArea.Add(new(37, 25));
                    initArea.Add(new(36, 30));
                    initArea.Add(new(35, 32));
                    initArea.Add(new(35, 35));
                    initArea.Add(new(36, 40));
                    initArea.Add(new(63, 39));
                    initArea.Add(new(83, 30));
                    initArea.Add(new(96, 21));
                    initArea.Add(new(106, 7));
                    initArea.Add(new(111, -4));
                    initArea.Add(new(111, -7));
                    initArea.Add(new(104, -21));
                    initArea.Add(new(94, -33));
                    initArea.Add(new(79, -42));
                    initArea.Add(new(66, -49));
                    initArea.Add(new(49, -52));
                    initArea.Add(new(35, -53));
                    initArea.Add(new(35, -52));
                    _areaRightShip = new(initArea);
                }

                return _areaRightShip;
            }
        }

        #endregion

        #region AreaCrate

        public static Polygon AreaCrate {
            get {
                if (_areaCrate == null) {
                    var initArea = new List<Vector2>();
                    initArea.Add(new(-82, 85));
                    initArea.Add(new(-70, 99));
                    initArea.Add(new(-58, 85));
                    initArea.Add(new(-70, 73));
                    initArea.Add(new(-82, 85));
                    _areaCrate = new(initArea);
                }

                return _areaCrate;
            }
        }

        #endregion

        #region AreaBarrel

        public static Polygon AreaBarrel {
            get {
                if (_areaBarrel == null) {
                    var initArea = new List<Vector2>();
                    initArea.Add(new(-22, 73));
                    initArea.Add(new(-24, 84));
                    initArea.Add(new(-19, 93));
                    initArea.Add(new(-16, 99));
                    initArea.Add(new(3, 94));
                    initArea.Add(new(4, 88));
                    initArea.Add(new(4, 83));
                    initArea.Add(new(0, 77));
                    initArea.Add(new(-2, 73));
                    initArea.Add(new(-6, 68));
                    initArea.Add(new(-22, 73));
                    _areaBarrel = new(initArea);
                }

                return _areaBarrel;
            }
        }

        #endregion

        #region AreaSRock

        public static Polygon AreaSRock {
            get {
                if (_areaSRock == null) {
                    var initArea = new List<Vector2>();
                    initArea.Add(new(33, 87));
                    initArea.Add(new(34, 92));
                    initArea.Add(new(39, 93));
                    initArea.Add(new(42, 86));
                    initArea.Add(new(37, 83));
                    initArea.Add(new(33, 87));
                    _areaSRock = new(initArea);
                }

                return _areaSRock;
            }
        }

        #endregion

        #region AreaLRock

        public static Polygon AreaLRock {
            get {
                if (_areaLRock == null) {
                    var initArea = new List<Vector2>();
                    initArea.Add(new(53, 81));
                    initArea.Add(new(54, 89));
                    initArea.Add(new(66, 93));
                    initArea.Add(new(73, 91));
                    initArea.Add(new(79, 83));
                    initArea.Add(new(76, 72));
                    initArea.Add(new(63, 71));
                    initArea.Add(new(53, 82));
                    initArea.Add(new(88, 69));
                    initArea.Add(new(53, 81));
                    _areaLRock = new(initArea);
                }

                return _areaLRock;
            }
        }

        #endregion

        #region AreaMRock

        public static Polygon AreaMRock {
            get {
                if (_areaMRock == null) {
                    var initArea = new List<Vector2>();
                    initArea.Add(new(87, 69));
                    initArea.Add(new(96, 79));
                    initArea.Add(new(102, 69));
                    initArea.Add(new(97, 63));
                    initArea.Add(new(87, 69));
                    _areaMRock = new(initArea);
                }

                return _areaMRock;
            }
        }

        #endregion

        #region AreaTRock1

        public static Polygon AreaTRock1 {
            get {
                if (_areaTRock1 == null) {
                    var initArea = new List<Vector2>();
                    initArea.Add(new(-95, -90));
                    initArea.Add(new(-104, -93));
                    initArea.Add(new(-105, -98));
                    initArea.Add(new(-99, -108));
                    initArea.Add(new(-89, -104));
                    initArea.Add(new(-87, -98));
                    initArea.Add(new(-93, -91));
                    initArea.Add(new(-95, -90));
                    _areaTRock1 = new(initArea);
                }

                return _areaTRock1;
            }
        }

        #endregion

        #region AreaTRock2

        public static Polygon AreaTRock2 {
            get {
                if (_areaTRock2 == null) {
                    var initArea = new List<Vector2>();
                    initArea.Add(new(-76, -99));
                    initArea.Add(new(-72, -91));
                    initArea.Add(new(-64, -86));
                    initArea.Add(new(-60, -88));
                    initArea.Add(new(-56, -93));
                    initArea.Add(new(-55, -101));
                    initArea.Add(new(-63, -107));
                    initArea.Add(new(-69, -105));
                    initArea.Add(new(-75, -100));
                    initArea.Add(new(-76, -99));
                    _areaTRock2 = new(initArea);
                }

                return _areaTRock2;
            }
        }

        #endregion

        #region AreaWatchTowerTop

        public static Polygon AreaWatchTowerTop {
            get {
                if (_areaWatchTowerTop == null) {
                    var initArea = new List<Vector2>();
                    initArea.Add(new(2, -85));
                    initArea.Add(new(-4, -87));
                    initArea.Add(new(-9, -91));
                    initArea.Add(new(-11, -97));
                    initArea.Add(new(-10, -103));
                    initArea.Add(new(-8, -108));
                    initArea.Add(new(-1, -111));
                    initArea.Add(new(6, -110));
                    initArea.Add(new(10, -106));
                    initArea.Add(new(13, -100));
                    initArea.Add(new(12, -93));
                    initArea.Add(new(10, -89));
                    initArea.Add(new(4, -85));
                    initArea.Add(new(2, -85));
                    _areaWatchTowerTop = new(initArea);
                }

                return _areaWatchTowerTop;
            }
        }

        #endregion

        #region AreaUp1

        public static Polygon AreaUp1 {
            get {
                if (_areaUp1 == null) {
                    var initArea = new List<Vector2>();
                    initArea.Add(new(32, 97));
                    initArea.Add(new(28, 81));
                    initArea.Add(new(44, 81));
                    initArea.Add(new(60, 64));
                    initArea.Add(new(79, 66));
                    initArea.Add(new(99, 56));
                    initArea.Add(new(112, 63));
                    initArea.Add(new(103, 82));
                    initArea.Add(new(86, 84));
                    initArea.Add(new(76, 98));
                    initArea.Add(new(55, 100));
                    initArea.Add(new(44, 96));
                    initArea.Add(new(31, 98));
                    _areaUp1 = new(initArea);
                }

                return _areaUp1;
            }
        }

        #endregion

        #region AreaUp2

        public static Polygon AreaUp2 {
            get {
                if (_areaUp2 == null) {
                    var initArea = new List<Vector2>();
                    initArea.Add(new(-95, -85));
                    initArea.Add(new(-108, -87));
                    initArea.Add(new(-112, -100));
                    initArea.Add(new(-102, -111));
                    initArea.Add(new(-95, -113));
                    initArea.Add(new(-85, -109));
                    initArea.Add(new(-82, -107));
                    initArea.Add(new(-72, -113));
                    initArea.Add(new(-66, -115));
                    initArea.Add(new(-56, -114));
                    initArea.Add(new(-47, -106));
                    initArea.Add(new(-50, -88));
                    initArea.Add(new(-59, -80));
                    initArea.Add(new(-73, -81));
                    initArea.Add(new(-83, -90));
                    initArea.Add(new(-89, -89));
                    initArea.Add(new(-98, -83));
                    initArea.Add(new(-110, -89));
                    initArea.Add(new(-95, -85));
                    _areaUp2 = new(initArea);
                }

                return _areaUp2;
            }
        }

        #endregion

        #region AreaUp3

        public static Polygon AreaUp3 {
            get {
                if (_areaUp3 == null) {
                    var initArea = new List<Vector2>();
                    initArea.Add(new(6, -68));
                    initArea.Add(new(-2, -73));
                    initArea.Add(new(-4, -77));
                    initArea.Add(new(-15, -86));
                    initArea.Add(new(-20, -100));
                    initArea.Add(new(-12, -115));
                    initArea.Add(new(-2, -115));
                    initArea.Add(new(16, -115));
                    initArea.Add(new(21, -103));
                    initArea.Add(new(17, -86));
                    initArea.Add(new(12, -75));
                    initArea.Add(new(10, -70));
                    initArea.Add(new(6, -68));
                    _areaUp3 = new(initArea);
                }

                return _areaUp3;
            }
        }

        #endregion

        #region AreaUp1

        public static Polygon AreaUp4 {
            get {
                if (_areaUp4 == null) {
                    var initArea = new List<Vector2>();
                    initArea.Add(new(95, -77));
                    initArea.Add(new(86, -77));
                    initArea.Add(new(78, -89));
                    initArea.Add(new(77, -101));
                    initArea.Add(new(84, -112));
                    initArea.Add(new(92, -115));
                    initArea.Add(new(102, -115));
                    initArea.Add(new(112, -114));
                    initArea.Add(new(115, -103));
                    initArea.Add(new(118, -92));
                    initArea.Add(new(112, -80));
                    initArea.Add(new(106, -77));
                    initArea.Add(new(91, -78));
                    initArea.Add(new(95, -77));
                    _areaUp4 = new(initArea);
                }

                return _areaUp4;
            }
        }

        #endregion

        #region AreaStraight1

        public static Polygon AreaStraight1 {
            get {
                if (_areaStraight1 == null) {
                    var initArea = new List<Vector2>();

                    #region initArea

                    initArea.Add(new(-44, 115));
                    initArea.Add(new(-43, 84));
                    initArea.Add(new(-47, 70));
                    initArea.Add(new(-63, 63));
                    initArea.Add(new(-91, 55));
                    initArea.Add(new(-120, 43));
                    initArea.Add(new(-128, 26));
                    initArea.Add(new(-139, 6));
                    initArea.Add(new(-140, -10));
                    initArea.Add(new(-133, -29));
                    initArea.Add(new(-128, -55));
                    initArea.Add(new(-115, -68));
                    initArea.Add(new(-94, -79));
                    initArea.Add(new(-69, -79));
                    initArea.Add(new(-50, -76));
                    initArea.Add(new(-36, -62));
                    initArea.Add(new(-33, -47));
                    initArea.Add(new(-31, -17));
                    initArea.Add(new(-31, 10));
                    initArea.Add(new(-29, 36));
                    initArea.Add(new(-16, 43));
                    initArea.Add(new(-3, 44));
                    initArea.Add(new(3, 34));
                    initArea.Add(new(6, 7));
                    initArea.Add(new(7, -17));
                    initArea.Add(new(10, -47));
                    initArea.Add(new(12, -63));
                    initArea.Add(new(20, -72));
                    initArea.Add(new(35, -78));
                    initArea.Add(new(59, -79));
                    initArea.Add(new(84, -77));
                    initArea.Add(new(103, -67));
                    initArea.Add(new(116, -53));
                    initArea.Add(new(124, -38));
                    initArea.Add(new(127, -20));
                    initArea.Add(new(127, 3));
                    initArea.Add(new(122, 15));
                    initArea.Add(new(113, 35));
                    initArea.Add(new(105, 47));
                    initArea.Add(new(93, 52));
                    initArea.Add(new(82, 55));
                    initArea.Add(new(62, 57));
                    initArea.Add(new(41, 62));
                    initArea.Add(new(35, 65));
                    initArea.Add(new(30, 75));
                    initArea.Add(new(27, 90));
                    initArea.Add(new(24, 106));
                    initArea.Add(new(24, 115));
                    initArea.Add(new(23, 115));
                    initArea.Add(new(9, 115));
                    initArea.Add(new(9, 103));
                    initArea.Add(new(13, 88));
                    initArea.Add(new(17, 69));
                    initArea.Add(new(25, 54));
                    initArea.Add(new(38, 48));
                    initArea.Add(new(64, 46));
                    initArea.Add(new(90, 34));
                    initArea.Add(new(104, 22));
                    initArea.Add(new(114, 5));
                    initArea.Add(new(117, -15));
                    initArea.Add(new(112, -36));
                    initArea.Add(new(94, -55));
                    initArea.Add(new(70, -66));
                    initArea.Add(new(44, -67));
                    initArea.Add(new(26, -63));
                    initArea.Add(new(18, -40));
                    initArea.Add(new(18, -20));
                    initArea.Add(new(17, 6));
                    initArea.Add(new(16, 26));
                    initArea.Add(new(13, 46));
                    initArea.Add(new(6, 58));
                    initArea.Add(new(-16, 60));
                    initArea.Add(new(-31, 52));
                    initArea.Add(new(-38, 42));
                    initArea.Add(new(-44, 22));
                    initArea.Add(new(-42, -7));
                    initArea.Add(new(-44, -39));
                    initArea.Add(new(-45, -54));
                    initArea.Add(new(-60, -67));
                    initArea.Add(new(-80, -71));
                    initArea.Add(new(-100, -63));
                    initArea.Add(new(-112, -55));
                    initArea.Add(new(-120, -42));
                    initArea.Add(new(-123, -25));
                    initArea.Add(new(-126, -4));
                    initArea.Add(new(-122, 14));
                    initArea.Add(new(-111, 30));
                    initArea.Add(new(-96, 38));
                    initArea.Add(new(-76, 43));
                    initArea.Add(new(-60, 48));
                    initArea.Add(new(-50, 53));
                    initArea.Add(new(-38, 62));
                    initArea.Add(new(-32, 71));
                    initArea.Add(new(-30, 87));
                    initArea.Add(new(-29, 103));
                    initArea.Add(new(-30, 115));
                    initArea.Add(new(-44, 115));

                    #endregion

                    _areaStraight1 = new(initArea);
                }

                return _areaStraight1;
            }
        }

        #endregion

        public static string Name = "Shipwreck";
    }
}
