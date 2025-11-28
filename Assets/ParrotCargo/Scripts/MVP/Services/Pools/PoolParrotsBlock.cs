using UnityEngine;

using Zenject;

public class PoolParrotsBlock : BasePool<ParrotsBlockView>
{
    public PoolParrotsBlock(int maxSize, Transform parent, DiContainer container) 
        : base(maxSize, parent, container)
    {
    
    }

    protected override bool TryCheckGetObjectPool(ParrotsBlockView obj, ParrotsBlockView prefab)
        => obj.gameObject.activeSelf == false && obj.TypeParrotsBlock == prefab.TypeParrotsBlock;
}
