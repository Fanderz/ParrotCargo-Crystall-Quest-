using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

public class ParrotBlockPresenter
{
    private readonly ParrotBlock _model;
    private readonly ParrotsBlockView _view;
    private readonly DraggableParrotBlock _draggableParrotBlock;
    private readonly List<ParrotPresenter> _parrotPresenters;

    private IReadOnlyList<ShipPresenter> _ships;
    private IReadOnlyList<PalletPresenter> _pallets;
    private List<IDisposable> _disposables;

    public ReactiveCommand ChangingActive = new ReactiveCommand();

    public ParrotBlockPresenter(ParrotBlock parrotBlock, ParrotsBlockView parrotsBlockView)
    {
        _model = parrotBlock;
        _view = parrotsBlockView;
        _draggableParrotBlock = _view.GetComponent<DraggableParrotBlock>();
        _parrotPresenters = new List<ParrotPresenter>();
        _disposables = new List<IDisposable>();
    }

    public bool IsBlockReleased { get; private set; }

    public void Initialize()
    {
        foreach (ParrotView view in _view.Parrots)
        {
            Parrot parrot = new Parrot();
            _model.AddParrot(parrot);

            ParrotPresenter presenter = new ParrotPresenter(view, parrot);
            presenter.Initialize();

            _parrotPresenters.Add(presenter);

            presenter.DroppedBag.Subscribe(parrot => { presenter.TargetPallet.TakeBag(presenter.CrystallBag); }).AddTo(_disposables);
            presenter.ChangedActive.Subscribe(block => { ReleasingBlock(); }).AddTo(_disposables);
        }

        Subscribe();
    }

    public void SetShipTargets(IReadOnlyList<ShipPresenter> ships)
    {
        _ships = ships;
    }

    public void SetPalletTargets(IReadOnlyList<PalletPresenter> pallets)
    {
        _pallets = pallets;
    }

    public void Dispose()
    {
        _disposables.ForEach(disposable => disposable.Dispose());
    }

    private void Subscribe()
    {
        _draggableParrotBlock.MoveCommand.Subscribe(target =>
        {
            Vector3 targetPosition = new Vector3(target.x, _model.StartPosition.y + _draggableParrotBlock.YFlyingOffset, target.z);

            MoveBlock(targetPosition);
            ScanBags();
        }).AddTo(_disposables);

        _draggableParrotBlock.StopMoving.Subscribe(pickBag =>
        {
            StopBlock();
            TryPickBags();
            TryCarryBags();
        }).AddTo(_disposables);

        _view.BlockMoving.Subscribe(newPosition => { _model.MoveParrots(newPosition); }).AddTo(_disposables);
        _view.Movable.Subscribe(isBlockMovable => { _model.ChangeMovable(isBlockMovable); }).AddTo(_disposables);

        _view.Activation.Subscribe(isBlockActive => { _parrotPresenters.ForEach(presenter => presenter.SetActive(isBlockActive)); });
    }

    private void ReleasingBlock()
    {
        IsBlockReleased = _parrotPresenters.TrueForAll(parrotPresenter => parrotPresenter.isActive == false);

        if (IsBlockReleased)
            ChangingActive.Execute();
    }

    private List<ShipPresenter> GetShipsMatchedBag(ParrotPresenter parrotPresenter)
    {
        if (_ships == null)
            throw new ZenjectException("Injected List<ShipPresenter> in ParrotBlockPresenter is null");

        return _ships.ToList().FindAll(ship => ship.isGoingToRelease == false && parrotPresenter.IsBagExistsShip(ship.GetView()));
    }

    private PalletPresenter GetEmptyTempPallet()
    {
        if (_pallets == null)
            throw new ZenjectException("Injected List<PalletPresenter> in ParrotBlockPresenter is null");

        return _pallets.FirstOrDefault(pallet => pallet.isEmpty && pallet.HaveCourier == false);
    }

    #region Движение блока и каждого попугая
    private void MoveBlock(Vector3 targetPosition)
    {
        _view.MoveBlock(targetPosition);
        MoveEachParrot();
    }

    private void StopBlock()
    {
        _view.StopMoveBlock();
        MoveEachParrot();
    }

    private void MoveEachParrot() =>
        _parrotPresenters.ForEach(parrot => parrot.OnBlockMoving(_view.IsMoving));
    #endregion

    private void ScanBags()
    {
        if (_view.IsMoving)
            _view.ScanBags();
    }

    private void TryPickBags()
    {
        if (_view.CanPickBag)
        {
            _view.PickBags();
        }
        else
        {
            _view.ReturnToBase();
            _parrotPresenters.ForEach(parrot => parrot.ReturnParrotOnStart());
        }
    }

    private void TryCarryBags()
    {
        if (_view.EachParrotHaveBag)
        {
            _draggableParrotBlock.enabled = false;

            foreach (ParrotPresenter parrotPresenter in _parrotPresenters)
            {
                bool isTargetShip = false;
                PalletPresenter targetPallet;
                ShipPresenter targetShip;

                List<ShipPresenter> targetShips = GetShipsMatchedBag(parrotPresenter);
                targetShip = GetSmallerEmptyShip(targetShips);

                //targetShip = targetShips.Find(ship => ship.EmptyPalletsCnt > 0);

                if (targetShip != null)
                {
                    targetPallet = targetShip.GetEmptyPallet();
                    isTargetShip = true;
                }
                else
                {
                    targetPallet = GetEmptyTempPallet();
                }

                targetPallet.SetCourier(true);
                parrotPresenter.CarryBag(targetPallet, isTargetShip);
            }
        }
    }

    private ShipPresenter GetSmallerEmptyShip(List<ShipPresenter> ships)
    {
        return ships.OrderBy(ship => ship.EmptyPalletsCnt > 0).FirstOrDefault();
    }
}
