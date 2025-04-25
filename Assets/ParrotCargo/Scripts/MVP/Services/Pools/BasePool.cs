using System.Collections.Generic;
using UnityEngine;

public class BasePool<T> where T : MonoBehaviour
{
    private int _poolMaxSize;

    private List<T> _objects;
    private Transform _parent;

    public BasePool(int maxSize, Transform parent)
    {
        _objects = new List<T>();
        _poolMaxSize = maxSize;
        _parent = parent;
    }

    public int Count => _objects.Count;

    public int ActiveCount => _objects.FindAll(finded => finded.gameObject.activeSelf == true).Count;

    public T Get(T prefab)
    {
        T result = null;

        if (_objects.FindAll(obj => obj.gameObject.activeSelf == false).Count > 0)
        {
            result = _objects.Find(obj => obj.gameObject.activeSelf == false);
        }
        else
        {
            if (_objects.Count < _poolMaxSize)
            {
                result = Create(prefab, _parent);
                _objects.Add(result);
            }
        }

        return result;
    }

    public void Release(T obj)
    {
        var finded = _objects.Find(finded => finded.Equals(obj));

        if (finded != null)
            finded.gameObject.SetActive(false);
    }

    private T Create(T prefab, Transform parent = null)
    {
        return Object.Instantiate(prefab, parent);
    }
}
