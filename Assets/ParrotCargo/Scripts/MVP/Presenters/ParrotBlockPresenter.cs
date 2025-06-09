using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

public class ParrotBlockPresenter
{
    private ParrotBlock _parrotBlock;
    private ParrotsBlockView _parrotsBlockView;
    private List<ParrotPresenter> _parrotPresenters;

    private Coroutine _scanningCoroutine;

    public ParrotBlockPresenter(ParrotBlock parrotBlock, ParrotsBlockView parrotsBlockView)
    {
        _parrotBlock = parrotBlock;
        _parrotsBlockView = parrotsBlockView;

        Initialize();
        Subscribe();
    }

    private void Subscribe()
    {
        _parrotsBlockView.BlockMoving.Subscribe(newPosition => { _parrotBlock.MoveParrots(newPosition); });
        _parrotsBlockView.Movable.Subscribe(movable => { _parrotBlock.ChangeMovable(movable); });
        //_parrotBlock.Pickable.Subscribe(pickable => { _parrotsBlockView.PickBags(); });
    }

    private void Initialize()
    {
        _parrotPresenters = new List<ParrotPresenter>();

        foreach (ParrotView view in _parrotsBlockView.Parrots)
        {
            var parrot = new Parrot();
            _parrotPresenters.Add(new ParrotPresenter(view, parrot));
        }
    }

    //private IEnumerator ScanBags()
    //{
    //    while (_parrotBlock.IsMoving)
    //    {
    //        foreach (var parrotPresenter in _parrotPresenters)
    //        {
    //            parrotPresenter.SearchBags();
    //        }

    //        _parrotBlock.SetPickable(_parrotPresenters.TrueForAll(value => value.CanParrotPick == true));

    //        yield return null;
    //    }
    //}
}
