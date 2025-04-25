namespace Assets.Scripts.MVP.Services.Spawners
{
    class CrystallBagSpawner : BaseSpawner<BaseCrystallBagView>
    {
        protected override void Awake()
        {
            base.Awake();
            FirstSpawn();
        }

        private void FirstSpawn()
        {
            for (int i = 0; i < ObjectsMaxCount; i++)
                SpawnObject();
        }
    }
}
