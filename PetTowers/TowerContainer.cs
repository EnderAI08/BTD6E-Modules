namespace PetTowers;
public struct TowerContainer {
    public List<TowerModel> towers;
    public List<UpgradeModel> upgrades;
    public ShopTowerDetailsModel shop;

    public TowerContainer() {
        towers = new List<TowerModel>();
        upgrades = new List<UpgradeModel>();
        shop = default;
    }
}
