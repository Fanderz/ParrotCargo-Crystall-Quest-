using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BasePool<T> where T : MonoBehaviour
{
    private int _poolMaxSize;

    private readonly Transform _parent;

    private List<T> _objects;
    private DiContainer _container;

    public BasePool(int maxSize, Transform parent, DiContainer container)
    {
        _objects = new List<T>();

        _poolMaxSize = maxSize;
        _parent = parent;
        _container = container;
    }

    public int Count => _objects.Count;

    public int ActiveCount => _objects.FindAll(finded => finded.gameObject.activeSelf == true).Count;

    public T Get(T prefab, Transform parent)
    {
        foreach (T obj in _objects)
        {
            if (obj.gameObject.activeSelf == false)
            {
                obj.transform.SetParent(parent);
                return obj;
            }
        }

        return CreateObject(prefab, parent);
    }

    public T CreateObject(T prefab, Transform parent)
    {
        if (_objects.Count >= _poolMaxSize)
            return null;

        T result = Create(prefab, parent);
        _objects.Add(result);

        return result;
    }

    public void Release(T obj)
    {
        if (_objects.Contains(obj))
            obj.gameObject.SetActive(false);
    }

    private T Create(T prefab, Transform parent = null)
    {
        return _container.InstantiatePrefabForComponent<T>(prefab, parent);
    }
}
