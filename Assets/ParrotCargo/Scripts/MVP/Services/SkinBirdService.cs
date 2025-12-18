using System.Collections.Generic;

using UnityEngine;

using YG;

public class SkinBirdService : BaseService
{
    [Header("Settings")]
    [SerializeField] private List<Bird> _birdsPrefab;

    public Bird CurrentBird { get; private set; }

    public override void Initialize()
    {
        var savedCurrentBird = YG2.saves.currentTypeBird;
        CurrentBird = _birdsPrefab.Find(bird => bird.TypeBird == savedCurrentBird);
    }
}
