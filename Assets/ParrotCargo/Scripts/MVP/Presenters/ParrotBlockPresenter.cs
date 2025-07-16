using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class ParrotBlockPresenter : IInitializable
{
    private readonly ParrotBlock _model;
    private readonly ParrotsBlockView _view;
    private readonly DraggableParrotBlock _draggableParrotBlock;
    private readonly List<ParrotPresenter> _parrotPresenters;

    private IReadOnlyList<ShipPresenter> _ships;
    private IReadOnlyList<PalletPresenter> _pallets;

    public ReactiveCommand ReleasedBlock = new ReactiveCommand();

    public ParrotBlockPresenter(ParrotBlock parrotBlock, ParrotsBlockView parrotsBlockView, DraggableParrotBlock draggableParrotBlock)
    {
        _model = parrotBlock;
        _view = parrotsBlockView;
        _draggableParrotBlock = draggableParrotBlock;
        _parrotPresenters = new List<ParrotPresenter>();

        Initialize();
        Subscribe();
    }

    public bool IsBlockReleased { get; private set; }

    public void Initialize()
    {
        foreach (ParrotView view in _view.Parrots)
        {
            var parrot = new Parrot();
            _model.AddParrot(parrot);
            _parrotPresenters.Add(new ParrotPresenter(view, parrot));
        }
    }

    public void SetShipTargets(IReadOnlyList<ShipPresenter> ships)
    {
        _ships = ships;
    }

    public void SetPalletTargets(IReadOnlyList<PalletPresenter> pallets)
    {
        _pallets = pallets;
    }

    //public ParrotsBlockView GetParrotsBlockView()
    //{
    //    return _parrotsBlockView;
    //}

    private void Subscribe()
    {
        _draggableParrotBlock.MoveCommand.Subscribe(targetPosition =>
        {
            Vector3 newPosition = new Vector3(targetPosition.x, _view.StartPosition.y + _draggableParrotBlock.YFlyingOffset, targetPosition.z);

            _view.MoveBlock(newPosition);
            _view.ScanBags();
        });

        _draggableParrotBlock.StopMoving.Subscribe(pickBag =>
        {
            _view.StopMoveBlock();
            _view.TryPickBags();
            _view.CarryBags();
        });

        _view.BlockMoving
            .Subscribe(newPosition => { _model.MoveParrots(newPosition); });
        _view.Movable
            .Subscribe(movable => { _model.ChangeMovable(movable); });
        _view.ReleasingBlock
            .Subscribe(parrotBlock => { ReleasingBlock(); });
        _view.SearchingRecievers
            .Subscribe(parrotBlock => { AddingRecievers(); });
    }

    private void ReleasingBlock()
    {
        IsBlockReleased = true;
        ReleasedBlock.Execute();
    }


    private void AddingRecievers()
    {
        List<BaseShipView> shipViews = new List<BaseShipView>();
        List<PalletView> palletViews = new List<PalletView>();

        if (_ships != null)
        {
            foreach (var ship in _ships)
                shipViews.Add(ship.ShipView);
        }

        if(_pallets != null)
        {
            foreach (var pallet in _pallets)
                palletViews.Add(pallet.PalletView);
        }

        _view.SetReceivers(shipViews,palletViews);
    }
}
