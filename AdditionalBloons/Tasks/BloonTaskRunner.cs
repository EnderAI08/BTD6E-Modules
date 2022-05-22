
using AdditionalBloons.Utils;

using Assets.Scripts.Simulation.Bloons;
using Assets.Scripts.Simulation.Track;
using V3 = Assets.Scripts.Simulation.SMath.Vector3;

using static Assets.Scripts.Models.Bloons.Behaviors.StunTowersInRadiusActionModel;

namespace AdditionalBloons.Tasks {
    internal sealed class BloonTaskRunner {
        public static BloonQueue bloonQueue = new();

        // Key of bloontask which stores all needed info, Value of KeyValuePair with key of amount sent out of total and value of time spent
        private static Dictionary<BloonTask, KeyValuePair<int, int>> bloonTasks = new();
        private static System.Random rand = new(DateTime.Now.Millisecond);

        private static readonly Dictionary<int, int> burnQueue = new(); // tower id key, tick value
        private static readonly Func<double, double> burnFunc = (tiers) => 1 + Math.Pow(2.71828183, 0.37701 * tiers);
        private static bool HasSpawnedFireBAD;

        internal static void Run() {
            var fps = Application.targetFrameRate.Map(60, 165, 60, 144);

            if (InGame.instance?.bridge != null) {

                foreach (var bloon in InGame.instance.bridge.GetAllBloons()) {
                    var bloonPosition = bloon.position;

                    #region Flame BAD Burn

                    if (bloon.Def.icon.guidRef.Contains("FireBADIcon")) {
                        for (var i = 0; i < InGame.instance.bridge.GetAllTowers().Count; i++) {
                            var tts = InGame.instance.bridge.GetAllTowers()[i];

                            if (tts.tower.towerModel.baseId.StartsWith("Ice") || tts.tower.towerModel.baseId.StartsWith("Gwen"))
                                continue;

                            if (Vector3.Distance(bloonPosition, tts.tower.Position.ToUnity()) <= 140) {
                                if (burnQueue.ContainsKey(tts.id)) {
                                    var tierSums = tts.Def.tiers.Sum();
                                    var ticks = burnQueue[tts.id];
                                    var seconds = (int)Math.Round(burnFunc(tierSums));
                                    var timeSpent = (int)Math.Round(ticks / (double)fps);

                                    if (timeSpent <= seconds) {
                                        if (ticks % fps == 0) {
                                            tts.sim.simulation.CreateTextEffect(new(tts.tower.Position.ToVector3()), "3dcdbc19136c60846ab944ada06695c0", 10, $"{seconds - timeSpent}s", false);
                                        } else if (ticks % fps == fps / 2) {
                                            var tm = tts.Def.CloneCast();
                                            tm.behaviors = tm.behaviors.Add(new DisplayModel($"FIREABAD_DM__{ticks}", "Assets/Monkeys/Gwendolin/Graphics/Effects/FireballPlacement/GwenFireballPlacementFX.prefab", 1,
                                                new(0, 0, 0), 1, false, 0));
                                            tts.tower.UpdateRootModel(tm);
                                        }
                                    }

                                    if (timeSpent > seconds) {
                                        tts.tower.towerModel.cost = 1;
                                        tts.tower.worth = 1;
                                        InGame.instance.SellTower(tts);
                                    }

                                    burnQueue[tts.id]++;
                                } else {
                                    burnQueue.Add(tts.id, 0);
                                }
                            }
                        }
                    }

                    #endregion

                    #region Cop BAD Jail

                    if (bloon.Def.icon.guidRef.Contains("CopBADIcon")) {
                        for (var i = 0; i < InGame.instance.bridge.GetAllTowers().Count; i++) {
                            var tts = InGame.instance.bridge.GetAllTowers()[i];

                            if (Vector3.Distance(bloonPosition, tts.tower.Position.ToUnity()) <= 140) {
                                var tierSums = tts.Def.tiers.Sum();

                                if (rand.Next(50000 / 7 * (tierSums+1)) == 0) {
                                    tts.sim.simulation.CreateTextEffect(new(tts.tower.Position.ToVector3()), "3dcdbc19136c60846ab944ada06695c0", 10, "Jailed!", false);
                                    var freeze = new TowerFreezeMutator("JailBars", true);
                                    tts.tower.AddMutatorIncludeSubTowers(freeze, (15/(tierSums+1)) * 60, true, true, false, true, -1);
                                    break;
                                }
                            }
                        }

                        var result = rand.Next(75000);
                        if (result == 0) {
                            bloon.sim.simulation.CreateTextEffect(new V3(bloonPosition), "3dcdbc19136c60846ab944ada06695c0", 10, "You've been caught speeding!\nPay a 5% fine!", false);
                            bloon.sim.SetCash(bloon.sim.GetCash(-1) * .95, -1);
                        } else if (result == 1) {
                            bloon.sim.simulation.CreateTextEffect(new V3(bloonPosition), "3dcdbc19136c60846ab944ada06695c0", 10, "You've been caught speeding!\nPay a 10% fine!", false);
                            bloon.sim.SetCash(bloon.sim.GetCash(-1) * .90, -1);
                        } else if (result == 2) {
                            bloon.sim.simulation.CreateTextEffect(new V3(bloonPosition), "3dcdbc19136c60846ab944ada06695c0", 10, "You've been caught speeding!\nGive me your lives!", false);
                            bloon.sim.simulation.Health -= 10;
                        }
                    }

                    #endregion
                }

                #region Bloon Spawner

                //Add to dictionary from queue
                while (bloonQueue.Count > 0) bloonTasks.Add(bloonQueue.Dequeue(), new KeyValuePair<int, int>(0, 0));

                if (bloonTasks.Count > 0) {
                    List<BloonTask> remove = new();
                    Dictionary<BloonTask, KeyValuePair<int, int>> update = new();

                    foreach (var entry in bloonTasks) {
                        //exception for it updatesBetween is 0 so they get sent at the same time
                        bool shouldContinue = true;
                        if (entry.Key.updatesBetween is 0) {
                            for (int i = 0; i < entry.Key.amount; i++) {
                                InGame.instance.bridge.simulation.map.spawner.Emit(entry.Key.model,
                                    InGame.instance.bridge.simulation.GetCurrentRound(), 0);
                                remove.Add(entry.Key);
                                shouldContinue = false;
                            }
                        }

                        if (!shouldContinue) break;

                        //timing for if should be sent
                        if (entry.Value.Key < entry.Key.amount && (entry.Value.Value % entry.Key.updatesBetween == 0))
                            InGame.instance.bridge.simulation.map.spawner.Emit(entry.Key.model,
                                InGame.instance.bridge.simulation.GetCurrentRound(), 0);

                        //if expired, remove
                        if (entry.Value.Value > entry.Key.amount * entry.Key.updatesBetween) {
                            remove.Add(entry.Key);
                        } else {
                            //if it was sent add to the key
                            var keyNewVal = entry.Value.Key;
                            if (entry.Value.Value % entry.Key.updatesBetween == 0)
                                keyNewVal++;
                            //update it with new timing
                            var kvp = new KeyValuePair<int, int>(keyNewVal, entry.Value.Value + 1);
                            update.Add(entry.Key, kvp);
                        }
                    }

                    //remove entries in the list from the dictionary
                    foreach (var toRemove in remove) bloonTasks.Remove(toRemove);
                    bloonTasks = update;
                }

                #endregion
            }
        }

        internal static bool Damage(ref Bloon __instance, ref float totalAmount, Tower tower) {
            var model = __instance.bloonModel;

            if (model.icon.guidRef.Equals("FireBADIcon")) {
                if (tower == null)
                    return true;

                if (tower.towerModel.baseId.StartsWith("Ice")) {
                    totalAmount *= 2.2f;
                    __instance.Move(-0.01f);
                } else if (!tower.towerModel.baseId.StartsWith("Gwen")) {
                    __instance.Move(0.005f);
                } else {
                    totalAmount--;
                }
            }

            if (model.icon.guidRef.Equals("CoconutIcon")) {
                var healthPercent = model.maxHealth / __instance.health / 5f;

                if (__instance.spawnRound > 60)
                    healthPercent /= 2f;
                if (__instance.spawnRound > 100)
                    healthPercent /= 5f;

                __instance.Move(healthPercent);
                totalAmount = 1;
            }

            return true;
        }

        internal static bool Emit(ref Spawner __instance) {

            if (__instance.CurrentRound == 139 && !HasSpawnedFireBAD) {
                HasSpawnedFireBAD = true;
                var fireBAD = BloonCreator.bloons.Find(a => a.icon.guidRef.Equals("CopBADIcon", StringComparison.Ordinal));
                if (fireBAD != null) {
                    __instance.Emit(fireBAD, __instance.currentRound.ValueInt, 0);
                }
                return false;
            }

            if (!__instance.isSandbox) {
                int i;
                if (__instance.CurrentRound < 50)
                    i = rand.Next(100);
                else if (__instance.CurrentRound < 100)
                    i = rand.Next(50);
                else if (__instance.CurrentRound < 200)
                    i = rand.Next(20);
                else
                    i = rand.Next(10);


                if (i == 4) {
                    if (BloonCreator.bloons == null || BloonCreator.bloons.Count! > 0 || BloonCreator.bloons.Any(a => a.icon.guidRef.Equals("CoconutIcon")))
                        return true;
                    var coconut = BloonCreator.bloons.Find(a => a.icon.guidRef.Equals("CoconutIcon", StringComparison.Ordinal))?.CloneCast();
                    if (coconut != null) {
                        if (__instance.currentRound.ValueInt > 20) coconut.maxHealth = Math.Min(__instance.currentRound.ValueInt, 50);
                        __instance.Emit(coconut, __instance.currentRound.ValueInt, 0);
                    }
                }
            }

            return true;
        }

        internal static void Quit() {
            bloonQueue.Clear();
            bloonTasks.Clear();
            rand = new(DateTime.Now.Millisecond);
            HasSpawnedFireBAD = false;
            GC.Collect();
        }
    }
}