using System.Collections.Generic;

using UnityEngine;

using UniRx;
using Zenject;

public class ShipPresenter
{
    private BaseShipView _view;
    private Ship _model;
    private bool _isGameOverSequenceStarted;

    private List<PalletPresenter> _palletPresenters;
    private Vector3 _pointOnFilled;

    public int EmptyPalletsCnt => _palletPresenters.FindAll(pallet => pallet.HaveCourier == false && pallet.isEmpty && pallet.isActive).Count;
    public bool isGoingToRelease => _model.isGoingToRelease;
    public bool IsStopped => _view != null ? _view.IsStopped() : false;

    public ReactiveCommand Releasing = new ReactiveCommand();
    public ReactiveCommand PlayAudio = new ReactiveCommand();

    [Inject]
    public ShipPresenter(BaseShipView view, Ship model)
    {
        _view = view;
        _model = model;

        _palletPresenters = new List<PalletPresenter>();
    }

    public void Initialize(int activePalletsCnt, ShipStopPoint stopPoint)
    {
        _view.Initialize(stopPoint);

        _model.PalletsCntChanged.Subscribe(exec => 
        _view.ActivatePallet()
        );
        
        _model.Initialize(activePalletsCnt);

        foreach (PalletView palletView in _view.PalletViews)
        {
            Pallet pallet = new Pallet();

            PalletPresenter palletPresenter = new PalletPresenter(palletView, pallet);
            palletPresenter.TakedBag.Subscribe(pallet => { CheckShipFilled(); });

            _palletPresenters.Add(palletPresenter);
        }

        _view.ShipStopped.Subscribe(exec => PlayAudio.Execute());
    }

    public PalletPresenter GetEmptyPallet()
    {
        PalletPresenter pallet = _palletPresenters.Find(presenter => presenter.isActive && presenter.isEmpty && presenter.HaveCourier == false); ;

        return pallet != null ? pallet : new NullablePalletPresenter(null, null);
    }

    public BaseShipView GetView()
    {
        return _view;
    }

    private void CheckShipFilled()
    {
        if (_isGameOverSequenceStarted)
            return;

        if (_palletPresenters.FindAll(palletPresenter => palletPresenter.isActive).TrueForAll(pallet => pallet.isEmpty == false))
        {
            _model.SetGoingToRelease(true);
            _view.SetDestination(_model.TargetOnFilled, _model.isGoingToRelease);
            Releasing.Execute();
        }
    }

    public void AddPallet()
    {
        _model.AddPallet();
    }

    public void PrepareGameOverSequence()
    {
        _isGameOverSequenceStarted = true;
        _model.SetGoingToRelease(false);
        _view.PrepareGameOverSequence();
    }

    public void StartGameOverSinking(float sinkTargetY, float sinkDuration)
    {
        PrepareGameOverSequence();
        _view.StartGameOverSinking(sinkTargetY, sinkDuration);
    }
}

public class NullableShipPresenter : ShipPresenter
{
    public NullableShipPresenter(BaseShipView view, Ship model) : base(view, model)
    {
    }
}
