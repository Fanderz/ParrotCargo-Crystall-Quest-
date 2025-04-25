using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParrotSpawner : BaseSpawner<ParrotView>
{
    [SerializeField] private int _minSpawnedParrots = 1;
    //[SerializeField] private int _maxSpawnedParrots = 4;
    //[SerializeField] private ParrotView _parrotViewPrefab;

    public List<ParrotPresenter> Spawn()
    {
        List<ParrotPresenter> parrotPresenters = new List<ParrotPresenter>();
        var randomCountParrots = Random.Range(_minSpawnedParrots, ObjectsMaxCount);

        for(int i = 0; i < randomCountParrots; i++)
        {
            var parrot = new Parrot();
            var parrotView = SpawnObject();
            var parrotPresenter = new ParrotPresenter(parrotView, parrot);

            parrotPresenters.Add(parrotPresenter);
        }

        return parrotPresenters;
    }
}
