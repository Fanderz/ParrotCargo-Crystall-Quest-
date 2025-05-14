using System.Collections.Generic;
using UnityEngine;

public class ParrotsBlockService : BaseService
{
    [SerializeField] private ParrotsBlockSpawner _parrotsBlockSpawner;
    [SerializeField] private InputSystemService _inputSystemService;

    private List<ParrotBlockPresenter> _parrotBlockPresenters;

    public override void Initialize()
    {
        _parrotBlockPresenters = new List<ParrotBlockPresenter>();
        _parrotBlockPresenters = _parrotsBlockSpawner.Spawn(_inputSystemService, Parent);
    }

    public void SetInputService(InputSystemService inputSystem)
    {
        _inputSystemService = inputSystem;
    }
}
