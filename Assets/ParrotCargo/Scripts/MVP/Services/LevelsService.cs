using System.Collections.Generic;
using UnityEngine;

using Zenject;
using YG;

public class LevelsService : BaseService
{
    [Header("References")]
    [SerializeField] private LevelProgressView _levelProgressView;
    [SerializeField] private GameWinView _gameWinView;
    [SerializeField] private List<Level> _levels;
    [SerializeField] private LevelsView _levelsView;

    [Inject] private TypeGameService _typeGameService;
    [Inject] private PlayerProgressService _playerProgressService;

    private LevelsProgressPresenter _levelsProgressPresenter;

    public LevelsProgressPresenter LevelsProgressPresenter => _levelsProgressPresenter;

    public override void Initialize()
    {
        _levelsProgressPresenter = new LevelsProgressPresenter(_levelProgressView, _typeGameService, _gameWinView);
    }

    public void StartLevel(Level enryLevele)
    {
        if (enryLevele == null)
            Debug.Log("Не найден уровень! Проверьте создан ли уровень " + enryLevele.NumberLevel);

        _levelsProgressPresenter.Initialize(enryLevele);
    }

    public void NextLevel()
    {
        var nextLevel = YG2.saves.currentNumberLevel + 1;
        var currentLevel = _levels.Find(level => level.NumberLevel == nextLevel);

        if (currentLevel == null)
            Debug.Log("Не найден уровень! Проверьте создан ли уровень " + nextLevel);

        _levelsView.OnLoadedLevel(currentLevel);
        _playerProgressService.SaveLevel();
        _levelsProgressPresenter.Initialize(currentLevel);
    }

    public bool TryNextLevel()
    {
        var nextLevel = YG2.saves.currentNumberLevel + 1;

        if (nextLevel <= _levels.Count)
            return true;

        return false;
    }
}
