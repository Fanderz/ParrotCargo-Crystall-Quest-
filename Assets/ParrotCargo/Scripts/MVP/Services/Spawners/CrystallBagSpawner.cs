using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;

namespace Assets.Scripts.MVP.Services.Spawners
{
    class CrystallBagSpawner : BaseSpawner<BaseCrystallBagView>
    {
        [SerializeField] private float _zSpawnOffset = -1f;
        [SerializeField] private float _ySpawnOffset = 6f;

        private DiContainer _container;
        private List<CrystallBagPresenter> _crystallBagPresenters;

        public IReadOnlyList<CrystallBagPresenter> CrystallBags => _crystallBagPresenters;

        public void Initialize()
        {
            _crystallBagPresenters = new List<CrystallBagPresenter>();
        }

        public void Spawn()
        {
            //List<CrystallBagPresenter> crystallBagPresenters = new List<CrystallBagPresenter>();

            foreach (var spawnPoint in SpawnPoints)
            {
                PalletView pallet = spawnPoint.GetComponent<PalletView>();

                if (pallet.HaveBag == false)
                {
                    Vector3 startPosition = new Vector3(pallet.transform.position.x, pallet.transform.position.y + _ySpawnOffset, pallet.transform.position.z + _zSpawnOffset);
                    var crystallBagView = SpawnObject(startPosition);
                    crystallBagView.Releasing.Subscribe(bag => { Release(crystallBagView); });

                    pallet.TakeBag(crystallBagView);
                    crystallBagView.Picked.Subscribe(picked => { pallet.RemoveBag(); });

                    var crystallBag = new BaseCrystallBag(startPosition);
                    var crystallBagPresenter = new CrystallBagPresenter(crystallBagView, crystallBag);

                    _crystallBagPresenters.Add(crystallBagPresenter);
                }
            }

            //return crystallBagPresenters;
        }

        protected override void CreatePool()
        {
            if (Pool == null)
                Pool = new BasePool<BaseCrystallBagView>(ObjectsMaxCount, Parent, _container);
        }

        [Inject]
        private void Construct(DiContainer container)
        {
            _container = container;
            CreatePool();
        }
    }
}
