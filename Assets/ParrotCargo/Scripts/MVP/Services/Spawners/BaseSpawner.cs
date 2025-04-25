using UnityEngine;
using Rand = UnityEngine.Random;
using System;
using System.Collections.Generic;

public class BaseSpawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected List<T> Prefab;
    [SerializeField] protected int ObjectsMaxCount;

    protected int SpawnedObjectsCount;

    protected BasePool<T> Pool;

    public event Action<int> ChangedSpawnedCounter;
    public event Action<int> ChangedCreatedCounter;
    public event Action<int> ChangedActiveCounter;

    protected virtual void Awake()
    {
        Pool = new BasePool<T>(ObjectsMaxCount, this.transform);
        SpawnedObjectsCount = 0;
    }


    public T SpawnObject()
    {
        var obj = Pool.Get(Prefab[Rand.Range(0, Prefab.Count)]);

        if(obj != null)
        {
            obj.gameObject.SetActive(true);
        }

        return obj;
    }

    protected virtual void Release(T obj)
    {
        Pool.Release(obj);
    }
}
