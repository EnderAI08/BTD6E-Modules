namespace PetTowers {
    public abstract class ITower<T> : ITower {
        public virtual void Initialize(ref GameModel gameModel) {
        }

        public virtual TowerContainer GetTower(GameModel gameModel) {
            Console.WriteLine($"{typeof(T).FullName} isn't overriding GetTower method. This isn't good :(");
            return default;
        }
    }

    public interface ITower {
        public void Initialize(ref GameModel gameModel);
        public TowerContainer GetTower(GameModel gameModel);
    }
}
