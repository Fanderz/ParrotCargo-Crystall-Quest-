using UnityEngine;
using System.Collections.Generic;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private List<BaseService> _services;

    private void Awake()
    {
        foreach (var service in _services)
            service.Initialize();
    }

    private T GetService<T>() where T : BaseService
    {
        var service = _services.Find(service => service is T);
        return (T)service;
    }
}
