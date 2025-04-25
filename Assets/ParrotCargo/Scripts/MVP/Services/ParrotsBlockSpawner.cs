using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrotsBlockSpawner : BaseSpawner<ParrotsBlockView>
{
    [SerializeField] private int _countParrotsBlock;
    [SerializeField] private ParrotSpawner _parrotSpawner;

    private List<ParrotBlockPresenter> _parrotBlockPresenters;

    protected override void Awake()
    {
        base.Awake();

        _parrotBlockPresenters = new List<ParrotBlockPresenter>();
    }

    public List<ParrotBlockPresenter> Spawn(InputSystemService inputService)
    {
        for (int i = 0; i < _countParrotsBlock; i++)
        {
            var parrotBlock = new ParrotBlock();
            var parrotBlockView = new ParrotsBlockView();
            parrotBlockView.Initialize(inputService);
            var parrotPresenters = _parrotSpawner.Spawn();
            var parrotBlockPresenter = new ParrotBlockPresenter(parrotBlock, parrotBlockView, parrotPresenters);

            _parrotBlockPresenters.Add(parrotBlockPresenter);
        }

        return _parrotBlockPresenters;
    }
}
