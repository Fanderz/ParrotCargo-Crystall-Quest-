using System.Collections.Generic;

using UnityEngine;

public abstract class BaseSpawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected int ObjectsMaxCount;
    [SerializeField] protected Transform Parent;
    [SerializeField] protected List<T> Prefab;
    [SerializeField] protected List<Transform> SpawnPoints;

    protected int SpawnedObjectsCount;

    protected BasePool<T> Pool;

    protected abstract void CreatePool();

    protected virtual void Awake()
    {
        if (Pool == null)
            CreatePool();

        SpawnedObjectsCount = 0;
    }


    public T SpawnObject(Vector3 startPosition, Transform parent = null)
    {
        if (parent != null)
            Parent = parent;

        var randomIndex = Random.Range(0, Prefab.Count);
        var prefab = Prefab[randomIndex];
        
        var  obj = Pool.Get(prefab, Parent);

        if (obj != null)
        {
            obj.gameObject.SetActive(true);
            SetSpawnPosition(obj, startPosition);
        }

        return obj;
    }

    public T CreateObjects()
    {
        T obj = null;

        foreach (var prefab in Prefab)
        {
            obj = Pool.CreateObject(prefab, transform);

            if (obj != null)
                obj.gameObject.SetActive(false);
        }

        return obj;
    }

    protected virtual void Release(T obj)
    {
        Pool.Release(obj);
    }

    protected void SetSpawnPosition(T obj, Vector3 position)
    {
        obj.transform.position = position;
    }
}
