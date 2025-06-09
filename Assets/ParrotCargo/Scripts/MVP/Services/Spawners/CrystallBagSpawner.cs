using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MVP.Services.Spawners
{
    class CrystallBagSpawner : BaseSpawner<BaseCrystallBagView>
    {
        //_xIncrement = 15f;
        [SerializeField] private float _zIncrement = 3.2f;

        private float _zOffset = 0f;

        public List<CrystallBagPresenter> Spawn()
        {
            List<CrystallBagPresenter> crystallBagPresenters = new List<CrystallBagPresenter>();

            for (int i = 1; i <= ObjectsMaxCount; i++)
            {
                var crystallBag = new BaseCrystallBag();
                var crystallBagView = SpawnObject(Parent);
                crystallBagView.transform.position = new Vector3(SpawnPoints[0].position.x + _xOffset, SpawnPoints[0].position.y, SpawnPoints[0].position.z + _zOffset);
                var crystallBagPresenter = new CrystallBagPresenter(crystallBagView, crystallBag);

                crystallBagPresenters.Add(crystallBagPresenter);

                IncreaseOffset(ref _xOffset, IncrementX);

                if (i % 5 == 0)
                {
                    IncreaseOffset(ref _zOffset, -_zIncrement);
                    _xOffset = 0f;
                }
            }

            return crystallBagPresenters;
        }
    }
}
