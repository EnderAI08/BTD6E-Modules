namespace BTD6E_Module_Helper {
    internal static class TowerExtensions {
        public static string GetNameMod(this TowerModel towerModel, string name) {
            return name + (towerModel.tiers.Sum() > 0 ? $"-{towerModel.tiers[0]}{towerModel.tiers[1]}{towerModel.tiers[2]}" : "");
        }

        public static void SetIcons(this TowerModel towerModel, string id) {
            towerModel.icon = new SpriteReference(id);
            towerModel.portrait = new SpriteReference(id);
        }
    }
}
