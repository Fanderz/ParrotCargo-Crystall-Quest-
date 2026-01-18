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
        private List<PalletView> _pallets;

        public IReadOnlyList<CrystallBagPresenter> CrystallBags => _crystallBagPresenters;

        public void Initialize()
        {
            _crystallBagPresenters = new List<CrystallBagPresenter>();
            _pallets = new List<PalletView>();

            foreach (Transform transformPallet in SpawnPoints)
                _pallets.Add(transformPallet.GetComponent<PalletView>());
        }

        public void Spawn()
        {
            List<PalletView> emptyPallets = _pallets.FindAll(pallet => pallet.HaveBag == false);

            foreach (PalletView pallet in emptyPallets)
            {
                Vector3 startPosition = new Vector3(pallet.transform.position.x, pallet.transform.position.y + _ySpawnOffset, pallet.transform.position.z + _zSpawnOffset);
                var crystallBagView = SpawnObject(startPosition);
                pallet.OnTakeBag(crystallBagView);

                crystallBagView.Releasing.Subscribe(bag => 
                { 
                    Release(crystallBagView); 
                });;

                var crystallBag = new BaseCrystallBag(startPosition);
                var crystallBagPresenter = new CrystallBagPresenter(crystallBagView, crystallBag);
                crystallBagPresenter.Initialize();

                crystallBagPresenter.BagPicked.Subscribe(picked => 
                { 
                    pallet.RemoveBag();
                    Spawn(); 
                });

                _crystallBagPresenters.Add(crystallBagPresenter);
            }
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
