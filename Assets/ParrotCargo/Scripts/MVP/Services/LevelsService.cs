using System.Collections.Generic;
using UnityEngine;

using YG;

public class LevelsService : BaseService
{
    [Header("Settings")]
    [SerializeField] private List<Level> _levels;
    [Header("References")]
    [SerializeField] private LevelProgressView _levelProgressView;

    public override void Initialize()
    {
        var currentNumberLevel = YG2.saves.currentNumberLevel;

        var level = _levels.Find(level => level.NumberLevel == currentNumberLevel);

        if (level == null)
            Debug.Log("Не найден уровень! Проверьте создан ли уровень " + currentNumberLevel);

        _levelProgressView.UpdateNumverLevelView(level);
        _levelProgressView.UpdateCountBagСollectedView(level);
    }
}
