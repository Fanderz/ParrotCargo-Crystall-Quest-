using System.Collections.Generic;

using UnityEngine;

using UniRx;
using Zenject;
using Cysharp.Threading.Tasks;
using System;

namespace Assets.Scripts.MVP.Services.Spawners
{
    public class CrystallBagSpawner : BaseSpawner<BaseCrystallBagView>
    {
        [SerializeField] private float _zSpawnOffset = -1f;
        [SerializeField] private float _ySpawnOffset = 6f;
        [SerializeField] private int _respawnDelay = 500;

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
                SpawnBagOnPallet(pallet);
        }

        private void SpawnBagOnPallet(PalletView pallet)
        {
            if (pallet == null || pallet.HaveBag)
                return;

            Vector3 startPosition = new Vector3(pallet.transform.position.x, pallet.transform.position.y + _ySpawnOffset, pallet.transform.position.z + _zSpawnOffset);
            var crystallBagView = SpawnObject(startPosition);

            if (crystallBagView == null)
                return;

            pallet.OnTakeBag(crystallBagView);

            crystallBagView.Releasing.Subscribe(_ =>
            {
                Release(crystallBagView);
            });

            var crystallBag = new BaseCrystallBag(startPosition);
            var crystallBagPresenter = new CrystallBagPresenter(crystallBagView, crystallBag);
            crystallBagPresenter.Initialize();

            crystallBagPresenter.BagPicked.Subscribe(_ =>
            {
                pallet.RemoveBag();
                RespawnPalletNextFrame(pallet).Forget();
            });

            _crystallBagPresenters.Add(crystallBagPresenter);
        }

        private async UniTaskVoid RespawnPalletNextFrame(PalletView pallet)
        {
            await UniTask.NextFrame();

            if (pallet != null && pallet.HaveBag == false)
                SpawnBagOnPallet(pallet);
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
