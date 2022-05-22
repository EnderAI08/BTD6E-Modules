using System;
using System.Drawing;
using System.Linq;

using Assets.Scripts.Data;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.UI_New.InGame;
using Assets.Scripts.Utils;

using HarmonyLib;

using Maps.Maps;
using Maps.Util;

using MelonLoader;

using NinjaKiwi.Common;

using SixthTiers.Utils;

using UnityEngine;

using Image = UnityEngine.UI.Image;

[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
[assembly: MelonInfo(typeof(Maps.Entry), "Maps", "1.5", "1330 Studios LLC")]
namespace Maps {
    class Entry : MelonMod {
        public static AssetBundle SceneBundle;
        public override void OnApplicationStart() {
            SceneBundle = AssetBundle.LoadFromMemory(Properties.Resources.map);
            MelonLogger.Msg(ConsoleColor.Red, "Maps Loaded!");
        }

        [HarmonyPatch(typeof(InGame), "Update")]
        public sealed class Update_Patch {
            private static float lastX, lastY;
            private static bool run;

            [HarmonyPostfix]
            public static void Postfix() {
                var cursorPosition = InGame.instance.inputManager.cursorPositionWorld;
                if (Input.GetKeyDown(KeyCode.LeftBracket)) {
                    run = !run;
                    if (lastX != cursorPosition.x || lastY != cursorPosition.y) {
                        Console.WriteLine("initArea.Add(new(" + Math.Round(cursorPosition.x) + ", " + Math.Round(cursorPosition.y) + "));");
                        lastX = cursorPosition.x;
                        lastY = cursorPosition.y;
                    }
                }
                if (Input.GetKeyDown(KeyCode.RightBracket)) {
                    run = !run;
                    if (lastX != cursorPosition.x || lastY != cursorPosition.y) {
                        Console.WriteLine("initPath.Add(new(){ point = new(" + Math.Round(cursorPosition.x) + ", " + Math.Round(cursorPosition.y) + "), bloonScale = 1, moabScale = 1 });");
                        lastX = cursorPosition.x;
                        lastY = cursorPosition.y;
                    }
                }

                if (Input.GetKeyDown(KeyCode.KeypadDivide)) {
                    run = !run;
                    if (lastX != cursorPosition.x || lastY != cursorPosition.y) Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                }
            }
        }

        [HarmonyPatch(typeof(Game), nameof(Game.Awake))]
        public sealed class GameAwake {
            [HarmonyPostfix]
            public static void Postfix() {
                var ms = ScriptableObjectSingleton<GameData>._instance.mapSet.Maps.items;

                for (var i = 0; i < ms.Length; i++) {
                    ms[i].isDebug = ms[i].isBrowserOnly = false;
                }

                var impls = new MapImpl[] { BigFoot.VALUE, Daffodils.VALUE, Shipwreck.VALUE, PVZGarden.VALUE, CursedMeadows.VALUE, EggHunt.VALUE };

                ScriptableObjectSingleton<GameData>._instance.mapSet.Maps.items = ms.AddAll(impls.Select(impl => impl.GetCreated()).ToArray());
            }
        }

        [HarmonyPatch(typeof(ResourceLoader), "LoadSpriteFromSpriteReferenceAsync")]
        public sealed class ResourceLoader_Patch {
            [HarmonyPostfix]
            public static void Postfix(SpriteReference reference, Image image) {
                if (reference != null) {
                    var bitmap = Properties.Resources.ResourceManager.GetObject(reference.guidRef) as byte[];
                    if (bitmap != null) {
                        var texture = new Texture2D(0, 0);
                        ImageConversion.LoadImage(texture, bitmap);
                        image.canvasRenderer.SetTexture(texture);
                        image.sprite = Sprite.Create(texture, new(0, 0, texture.width, texture.height),
                            new(), 10.2f);
                    } else {
                        var b = Properties.Resources.ResourceManager.GetObject(reference.guidRef);
                        if (b != null) {
                            var bm = (byte[])new ImageConverter().ConvertTo(b, typeof(byte[]));
                            var texture = new Texture2D(0, 0);
                            ImageConversion.LoadImage(texture, bm);
                            image.canvasRenderer.SetTexture(texture);
                            image.sprite = Sprite.Create(texture, new(0, 0, texture.width, texture.height),
                                new(), 10.2f);
                        }
                    }
                }
            }
        }
    }
}
