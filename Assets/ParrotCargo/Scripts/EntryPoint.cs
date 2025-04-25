using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private List<BaseService> _services;

    private void Awake()
    {
        var parrotsBlockService = GetService<ParrotsBlockService>();
        parrotsBlockService.SetInputService(GetService<InputSystemService>());

        foreach (var service in _services)
            service.Initialize();
    }

    private T GetService<T>() where T : BaseService
    {
        var service = _services.Find(service => service is T);
        return (T)service;
    }
}
