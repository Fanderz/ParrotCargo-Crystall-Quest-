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

    public void StartLevel(Level level)
    {
        InitializeLevelsProgressPresenter(level);
        _typeGameService.OnStartGame();
    }

    public void ReloadCurrentLevel()
    {
        var currentLevel = _levels.Find(level => level.NumberLevel == YG2.saves.currentNumberLevel);

        InitializeLevelsProgressPresenter(currentLevel);

        _levelsView.OnLoadedLevel(currentLevel);
    }

    public void NextLevel()
    {
        var nextLevel = _levels.Find(level => level.NumberLevel == YG2.saves.currentNumberLevel);

        InitializeLevelsProgressPresenter(nextLevel);

        _gameWinView.SetActive(false);
        _levelsView.OnLoadedLevel(nextLevel);
    }

    public void SaveProgressLevels()
    {
        if(TryNextLevel())
            _playerProgressService.SaveLevel();
    }

    public bool TryNextLevel()
    {
        int currentLevel = YG2.saves.currentNumberLevel;
        int nextLevel = currentLevel++;

        if (nextLevel <= _levels.Count)
            return true;

        return false;
    }

    private void InitializeLevelsProgressPresenter(Level currentLevel)
    {
        if (currentLevel == null)
            Debug.Log("Не найден уровень! Проверьте создан ли уровень " + currentLevel.NumberLevel);

        currentLevel.ResetCollectedBags();
        _levelsProgressPresenter.Initialize(currentLevel);
    }
}
