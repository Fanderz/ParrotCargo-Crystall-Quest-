using UnityEngine;
using Rand = UnityEngine.Random;
using System.Collections.Generic;

public class BaseSpawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected int ObjectsMaxCount;
    [SerializeField] protected float IncrementX;
    [SerializeField] protected Transform Parent;
    [SerializeField] protected List<T> Prefab;
    [SerializeField] protected List<Transform> SpawnPoints;

    protected int SpawnedObjectsCount;
    protected float _xOffset = 0f;

    protected BasePool<T> Pool;

    protected virtual void Awake()
    {
        if (Pool == null)
            Pool = new BasePool<T>(ObjectsMaxCount, Parent);

        SpawnedObjectsCount = 0;
    }


    public T SpawnObject(Transform parent)
    {
        if (Pool == null)
            Pool = new BasePool<T>(ObjectsMaxCount, Parent);

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

    protected void IncreaseOffset(ref float offset, float increment)
    {
        offset += increment;
    }
}
