using System.Linq;
using System.Collections.Generic;

using UnityEngine;

using UniRx;
using Zenject;

public class ParrotBlockPresenter
{
    private readonly ParrotBlock _model;
    private readonly ParrotsBlockView _view;
    private readonly DraggableParrotBlock _draggableParrotBlock;

    private PalletPresenter _tempPallet;

    private readonly List<ParrotPresenter> _parrotPresenters;

    private IReadOnlyList<ShipPresenter> _ships;
    private IReadOnlyList<PalletPresenter> _pallets;
    private BoxCollider _draggableCollider;

    public ReactiveCommand ChangingActiveCommand = new ReactiveCommand();
    //public ReactiveCommand SittingWithBag = new ReactiveCommand();
    public ReactiveCommand PickedBagsCommand = new ReactiveCommand();
    public ReactiveCommand GameOverCommand = new ReactiveCommand();

    public ReactiveCommand ParrotDroppedBagSoundCommand = new ReactiveCommand();
    public ReactiveCommand PickedParrotSoundCommand = new ReactiveCommand();

    public ParrotBlockPresenter(ParrotBlock parrotBlock, ParrotsBlockView parrotsBlockView)
    {
        _model = parrotBlock;
        _view = parrotsBlockView;
        _draggableParrotBlock = _view.GetComponent<DraggableParrotBlock>();
        _draggableCollider = _view.GetComponent<BoxCollider>();
        _parrotPresenters = new List<ParrotPresenter>();
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

            presenter.DroppedBag.Subscribe(parrot => { presenter.TargetPallet.TakeBag(presenter.CrystallBag); ParrotDroppedBagSoundCommand.Execute(); });
            presenter.ChangedActive.Subscribe(block => { ReleasingBlock(); });
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

    private void Subscribe()
    {
        _draggableParrotBlock.MoveCommand = new();
        _draggableParrotBlock.MoveCommand.Subscribe(target =>
        {
            Vector3 targetPosition = new Vector3(target.x, _model.StartPosition.y + _draggableParrotBlock.YFlyingOffset, target.z);
            MoveBlock(targetPosition);
            ScanBags();
        });

        _draggableParrotBlock.StopMoving = new();
        _draggableParrotBlock.StopMoving.Subscribe(pickBag =>
        {
            _draggableParrotBlock.SetDraggable(false);
            StopBlock();
            TryPickBags();
            TryCarryBags();
        });

        _view.BlockMoving.Subscribe(newPosition => { _model.MoveParrots(newPosition); });
        _view.Movable.Subscribe(isBlockMovable => { _model.ChangeMovable(isBlockMovable); });
        _view.SearchingRecievers.Subscribe(parrot => { TryCarryBagFromTempPallet(parrot); });
    }

    private void ReleasingBlock()
    {
        IsBlockReleased = _parrotPresenters.TrueForAll(parrotPresenter => parrotPresenter.isActive == false);

        if (IsBlockReleased)
        {
            _view.Release();
            ChangingActiveCommand.Execute();
        }
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

        PalletPresenter targetPallet = _pallets.ToList().Find(pallet => pallet.HaveCourier == false);

        if (targetPallet == null)
            return new NullablePalletPresenter(new NullablePalletView(), null);

        return targetPallet;
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
            PickedBagsCommand.Execute();
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
            _draggableCollider.enabled = false;

            foreach (ParrotPresenter parrotPresenter in _parrotPresenters.FindAll(parrot => parrot.isActive))
            {
                PalletPresenter targetPallet;
                ShipPresenter targetShip;

                List<ShipPresenter> targetShips = GetShipsMatchedBag(parrotPresenter);

                targetShip = GetSmallerEmptyShip(targetShips);
                targetPallet = GetTargetPallet(targetShip, out bool isTargetShip);

                if(targetPallet is NullablePalletPresenter)
                {
                    GameOverCommand.Execute();
                    return;
                }

                targetPallet.SetCourier(true);
                parrotPresenter.CarryBag(targetPallet, isTargetShip);
            }
        }
    }

    private void TryCarryBagFromTempPallet(ParrotView parrot)
    {
        if (parrot.HaveBag)
        {
            PalletPresenter targetPallet;
            ShipPresenter targetShip = null;

            ParrotPresenter parrotPresenter = _parrotPresenters.Find(presenter => presenter.GetView() == parrot);
            List<ShipPresenter> targetShips = GetShipsMatchedBag(parrotPresenter);

            targetShip = GetSmallerEmptyShip(targetShips);

            if (targetShip != null && targetShip.IsStopped && targetShip.isGoingToRelease == false)
            {
                targetPallet = targetShip.GetEmptyPallet();

                if (targetPallet == null)
                    return;

                parrotPresenter.CarryBag(targetPallet, true);
                targetPallet.SetCourier(true);
            }
            else
                return;
        }
    }

    private ShipPresenter GetSmallerEmptyShip(List<ShipPresenter> ships)
    {
        if (ships.Count == 0)
            return new NullableShipPresenter(null, null);

        return ships.OrderBy(ship => ship.EmptyPalletsCnt > 0).FirstOrDefault();
    }

    private PalletPresenter GetTargetPallet(ShipPresenter targetShip, out bool isTargetShip)
    {
        PalletPresenter targetPallet;
        isTargetShip = false;

        if (targetShip != null && targetShip.IsStopped && targetShip.isGoingToRelease == false)
        {
            targetPallet = targetShip.GetEmptyPallet();

            if (targetPallet == null)
                 targetPallet = _tempPallet = GetEmptyTempPallet();
            else
                isTargetShip = true;
        }
        else
        {
            targetPallet = _tempPallet = GetEmptyTempPallet();
        }

        return targetPallet;
    }
}
