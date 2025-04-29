using UnityEngine;
using Rand = UnityEngine.Random;
using System.Collections.Generic;

public class BaseSpawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected List<T> Prefab;
    [SerializeField] protected int ObjectsMaxCount;

    protected int SpawnedObjectsCount;

    protected BasePool<T> Pool;

    protected virtual void Awake()
    {
        if (Pool == null)
            Pool = new BasePool<T>(ObjectsMaxCount/*, this.transform*/);

        SpawnedObjectsCount = 0;
    }


    public T SpawnObject(Transform parent)
    {
        if (Pool == null)
            Pool = new BasePool<T>(ObjectsMaxCount);

        var testCnt = Rand.Range(0, Prefab.Count);

        var obj = Pool.Get(Prefab[testCnt], parent);

        if (obj != null)
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
