using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MVP.Services.Spawners
{
    class CrystallBagSpawner : BaseSpawner<BaseCrystallBagView>
    {
        //_xIncrement = 3.35f;
        [SerializeField] private float _yIncrement = 3.2f;

        private float _yOffset = 0f;

        public List<CrystallBagPresenter> Spawn()
        {
            List<CrystallBagPresenter> crystallBagPresenters = new List<CrystallBagPresenter>();

            for (int i = 1; i <= ObjectsMaxCount; i++)
            {
                var crystallBag = new BaseCrystallBag();
                var crystallBagView = SpawnObject(Parent);
                crystallBagView.transform.position = new Vector3(SpawnPoints[0].position.x + _xOffset, SpawnPoints[0].position.y + _yOffset, SpawnPoints[0].position.z);
                var crystallBagPresenter = new CrystallBagPresenter(crystallBagView, crystallBag);

                crystallBagPresenters.Add(crystallBagPresenter);

                IncreaseOffset(ref _xOffset, IncrementX);

                if (i % 5 == 0)
                {
                    IncreaseOffset(ref _yOffset, -_yIncrement);
                    _xOffset = 0f;
                }
            }

            return crystallBagPresenters;
        }
    }
}
