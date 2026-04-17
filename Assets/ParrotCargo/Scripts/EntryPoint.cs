using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class EntryPoint : MonoInstaller
{
    [SerializeField] private List<BaseService> _services;

    public ReactiveCommand BindingsEnded = new ReactiveCommand();

    private void Awake()
    {
        Time.timeScale = 1f;
        Debug.Log($"[EntryPoint.Awake] TimeScale: {Time.timeScale}");
    }

    public override void InstallBindings()
    {
        foreach (var service in _services)
        {
            Container.Bind(service.GetType()).FromInstance(service).AsSingle().IfNotBound();
            Container.Bind<IInitializable>().To(service.GetType()).FromResolve();
        }

        BindingsEnded.Execute();
    }

    private T GetService<T>() where T : BaseService
    {
        var service = _services.Find(service => service is T);
        return (T)service;
    }
}
