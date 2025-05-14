using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MVP.Services.Spawners
{
    class CrystallBagSpawner : BaseSpawner<BaseCrystallBagView>
    {
        public List<CrystallBagPresenter> Spawn(Transform parent)
        {
            List<CrystallBagPresenter> crystallBagPresenters = new List<CrystallBagPresenter>();

            for (int i = 0; i < ObjectsMaxCount; i++)
            {
                var crystallBag = new BaseCrystallBag();
                var crystallBagView = SpawnObject(parent);
                var crystallBagPresenter = new CrystallBagPresenter(crystallBagView, crystallBag);

                crystallBagPresenters.Add(crystallBagPresenter);
            }

            return crystallBagPresenters;
        }
    }
}
