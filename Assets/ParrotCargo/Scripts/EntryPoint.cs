using System.Collections.Generic;

using UnityEngine;
using YG;
using Zenject;

public class EntryPoint : MonoInstaller
{
    [SerializeField] private List<BaseService> _services;

    //public override void Start()
    //{
    //    YG2.StickyAdActivity(true);
    //}

    public override void InstallBindings()
    {
        foreach (var service in _services)
        {
            Container.Bind(service.GetType()).FromInstance(service).AsSingle().IfNotBound();
            Container.Bind<IInitializable>().To(service.GetType()).FromResolve();
        }
    }

    private T GetService<T>() where T : BaseService
    {
        var service = _services.Find(service => service is T);
        return (T)service;
    }
}
