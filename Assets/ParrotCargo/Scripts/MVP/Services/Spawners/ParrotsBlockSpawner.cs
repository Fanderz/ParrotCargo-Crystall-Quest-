using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParrotsBlockSpawner : BaseSpawner<ParrotsBlockView>
{
    //_xIncrement = 7.7f;
    private Camera _camera;
    private List<ParrotBlockPresenter> _parrotBlockPresenters;

    public List<ParrotBlockPresenter> Spawn()
    {
        _parrotBlockPresenters = new List<ParrotBlockPresenter>();

        for (int i = 0; i < ObjectsMaxCount; i++)
        {
            var parrotBlockView = SpawnObject(Parent);
            parrotBlockView.transform.position = new Vector3(SpawnPoints[0].position.x + _xOffset, SpawnPoints[0].position.y, SpawnPoints[0].position.z);
            parrotBlockView.Initialize();
            var parrotBlock = new ParrotBlock(parrotBlockView.GetComponent<Transform>());
            var parrotBlockPresenter = new ParrotBlockPresenter(parrotBlock, parrotBlockView);

            _parrotBlockPresenters.Add(parrotBlockPresenter);

            IncreaseOffset(ref _xOffset, IncrementX);
        }

        return _parrotBlockPresenters;
    }
}
