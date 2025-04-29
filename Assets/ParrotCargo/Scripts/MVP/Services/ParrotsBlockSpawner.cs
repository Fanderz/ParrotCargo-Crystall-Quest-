using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrotsBlockSpawner : BaseSpawner<ParrotsBlockView>
{
    //[SerializeField] private int _countParrotsBlock;
    [SerializeField] private ParrotSpawner _parrotSpawner;

    private List<ParrotBlockPresenter> _parrotBlockPresenters;

    public List<ParrotBlockPresenter> Spawn(InputSystemService inputService, Transform parent)
    {
        _parrotBlockPresenters = new List<ParrotBlockPresenter>();

        for (int i = 0; i < ObjectsMaxCount; i++)
        {
            var parrotBlock = new ParrotBlock();
            var parrotBlockView = SpawnObject(parent);
            parrotBlockView.Initialize(inputService);
            var parrotPresenters = _parrotSpawner.Spawn(parrotBlockView.transform);
            var parrotBlockPresenter = new ParrotBlockPresenter(parrotBlock, parrotBlockView, parrotPresenters);

            _parrotBlockPresenters.Add(parrotBlockPresenter);
        }

        return _parrotBlockPresenters;
    }
}
