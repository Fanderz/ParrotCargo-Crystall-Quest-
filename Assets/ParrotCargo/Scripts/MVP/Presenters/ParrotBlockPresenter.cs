using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ParrotBlockPresenter
{
    private ParrotBlock _parrotBlock;
    private ParrotsBlockView _parrotsBlockView;
    private List<ParrotPresenter> _parrotPresenters;

    public ParrotBlockPresenter(ParrotBlock parrotBlock, ParrotsBlockView parrotsBlockView, List<ParrotPresenter> parrotPresenters)
    {
        _parrotBlock = parrotBlock;
        _parrotsBlockView = parrotsBlockView;
        _parrotPresenters = parrotPresenters;
    }
}
