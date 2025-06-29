using System.Collections.Generic;
using UnityEngine;

public class ParrotsBlockService : BaseService
{
    [SerializeField] private ParrotsBlockSpawner _parrotsBlockSpawner;

    private List<ParrotBlockPresenter> _parrotBlockPresenters;

    public IReadOnlyList<ParrotBlockPresenter> ParrotBlockPresenters => _parrotBlockPresenters;

    public override void Initialize()
    {
        _parrotBlockPresenters = new List<ParrotBlockPresenter>();
        _parrotBlockPresenters = _parrotsBlockSpawner.Spawn();
    }
}
