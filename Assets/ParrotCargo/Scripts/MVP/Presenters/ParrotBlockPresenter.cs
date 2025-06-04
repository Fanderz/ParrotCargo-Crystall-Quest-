using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

public class ParrotBlockPresenter
{
    private ParrotBlock _parrotBlock;
    private ParrotsBlockView _parrotsBlockView;
    private List<ParrotPresenter> _parrotPresenters;

    public ParrotBlockPresenter(ParrotBlock parrotBlock, ParrotsBlockView parrotsBlockView/*, List<ParrotPresenter> parrotPresenters*/)
    {
        _parrotBlock = parrotBlock;
        _parrotsBlockView = parrotsBlockView;
        //_parrotPresenters = parrotPresenters;

        Subscribe();
    }

    private void Subscribe()
    {
        _parrotsBlockView.BlockMoving.Subscribe(newPosition => { _parrotBlock.MoveParrots(newPosition); });
    }
}
