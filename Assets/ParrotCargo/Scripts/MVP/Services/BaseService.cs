using UnityEngine;
using Zenject;

public abstract class BaseService : MonoBehaviour, IInitializable
{
    public abstract void Initialize();
    public abstract void Reset();
}
