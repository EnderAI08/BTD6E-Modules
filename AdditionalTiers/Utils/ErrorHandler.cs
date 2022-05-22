namespace AdditionalTiers.Utils;
public sealed class ErrorHandler : Instanced<ErrorHandler> {
    public bool Error;

    public readonly List<(string, int)> ModsWithErrors = new();

    public void Initialize() {
        MelonLogger.ErrorCallbackHandler += (namesection, _) => {
            Error = true;

            if (ModsWithErrors.Any(tuple => tuple.Item1 != null && tuple.Item1.Equals(namesection))) {
                var modTuple = ModsWithErrors.First(tuple => tuple.Item1.Equals(namesection));
                var position = ModsWithErrors.IndexOf(modTuple);
                var newModTuple = (namesection, modTuple.Item2 + 1);

                ModsWithErrors.Remove(modTuple);
                ModsWithErrors.Insert(position, newModTuple);
                return;
            }

            ModsWithErrors.Add((namesection, 1));
        };
        Logger13.ErrorEvent += (namesection, _) => {
            Error = true;

            if (ModsWithErrors.Any(tuple => tuple.Item1.Equals(namesection))) {
                var modTuple = ModsWithErrors.First(tuple => tuple.Item1.Equals(namesection));
                var position = ModsWithErrors.IndexOf(modTuple);
                var newModTuple = (namesection, modTuple.Item2 + 1);

                ModsWithErrors.Remove(modTuple);
                ModsWithErrors.Insert(position, newModTuple);
                return;
            }

            ModsWithErrors.Add((namesection, 1));
        };
    }

    public void OnGUI() {
        if (Error) {
            GUI.Box(new(5, 15, 825, 40 + ModsWithErrors.Count * 15), GUIContent.none);

            const string errorMessage = "Additional Tiers has detected an error, please restart the game and if this issue persists please make a ticket with 1330 Studios.";

            for (int j = 0; j < 4; j++) {
                int x = 10, y = 20;

                if (j < 2)
                    x += j % 2 == 0 ? -1 : 1;
                else
                    y += j % 2 == 0 ? -1 : 1;

                var outlineGUIColor = GUI.color;
                GUI.color = Color.black;
                var outlineGUIStyle = new GUIStyle { normal = { textColor = Color.white } };
                GUI.Label(new Rect(x, y, 100, 90), errorMessage, outlineGUIStyle);
                GUI.color = outlineGUIColor;
            }

            var mainErrGuiCol = GUI.color;
            GUI.color = new Color32(255, 50, 50, 255);
            var mainErrGuiStyle = new GUIStyle { normal = { textColor = Color.white } };
            GUI.Label(new Rect(10, 20, 100, 90), errorMessage, mainErrGuiStyle);
            GUI.color = mainErrGuiCol;

            for (var i = 0; i < ModsWithErrors.Count; i++) {
                if (ModsWithErrors[i].Item1 != null && ModsWithErrors[i].Item2 != 0) {
                    string modErrorMessage = $"Mod {ModsWithErrors[i].Item1.Replace('_', ' ')} has errored{(ModsWithErrors[i].Item2 > 1 ? $" x{ModsWithErrors[i].Item2}" : "")}!";

                    for (int j = 0; j < 4; j++) {
                        int x = 10, y = 40 + i * 15;

                        if (j < 2)
                            x += j % 2 == 0 ? -1 : 1;
                        else
                            y += j % 2 == 0 ? -1 : 1;

                        var outlineGUIColor = GUI.color;
                        GUI.color = Color.black;
                        var outlineGUIStyle = new GUIStyle { normal = { textColor = Color.white } };
                        GUI.Label(new Rect(x, y, 100, 90), modErrorMessage, outlineGUIStyle);
                        GUI.color = outlineGUIColor;
                    }

                    var errGuiCol = GUI.color;
                    GUI.color = new Color32(255, 50, 50, 255);
                    var errGuiStyle = new GUIStyle { normal = { textColor = Color.white } };
                    GUI.Label(new Rect(10, 40 + i * 15, 100, 90), modErrorMessage, errGuiStyle);
                    GUI.color = errGuiCol;
                }
            }
        }
    }
}