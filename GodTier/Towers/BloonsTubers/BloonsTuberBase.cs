namespace GodlyTowers.Towers.BloonsTubers;

internal abstract class BloonsTuberBase {
    public static string Name { get; }
    public static string Description { get; }
    public static string CharactersBase { get; }

    public static List<TowerModel> TowerModels { get; } = new();
    public static List<UpgradeModel> UpgradeModels { get; } = new();

    public static void InitializeTowers(ref GameModel model) {}
    public static ShopTowerDetailsModel GetShopDetailsModel() { return null; }
}
