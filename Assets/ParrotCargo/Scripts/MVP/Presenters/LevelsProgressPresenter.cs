using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UniRx;

public class LevelsProgressPresenter
{
    private LevelProgressView _levelProgressView;
    private TypeGameService _typeGameService;
    private GameWinView _gameWinView;
    private Level _currentLevel;

    private List<CrystallBagPresenter> _collectedCrystallBags;
    private bool _isLevelWinTriggered;

    public ReactiveCommand LevelWin = new();

    public LevelsProgressPresenter(LevelProgressView levelProgressView, TypeGameService typeGameService, GameWinView gameWinView)
    {
        _levelProgressView = levelProgressView;
        _typeGameService = typeGameService;
        _gameWinView = gameWinView;

        _collectedCrystallBags = new List<CrystallBagPresenter>();
    }

    public void Initialize(Level currentLevel)
    {
        _currentLevel = currentLevel;
        _isLevelWinTriggered = false;

        _levelProgressView.gameObject.SetActive(true);
        _levelProgressView.UpdateNumverLevelView(_currentLevel);
        _levelProgressView.UpdateCountBagCollectedView(_currentLevel);
    }

    public void AddCountBagCollected(TypeCrystallBag bagType)
    {
        if (IsBagSatisfiesLevel(bagType))
        {
            _currentLevel.AddCountBagCollected();
            _levelProgressView.UpdateCountBagCollectedView(_currentLevel);
        }
    }

    public async void TryFinishLevel()
    {
        if (_isLevelWinTriggered)
            return;

        if (_currentLevel.TryFinishLevel())
        {
            _isLevelWinTriggered = true;
            await UniTask.Delay(500);
            LevelWin.Execute();
        }
    }

    private bool IsBagSatisfiesLevel(TypeCrystallBag bagType)
    {
        if (_currentLevel.BagType == TypeCrystallBag.Other)
            return true;
        else if (_currentLevel.BagType == bagType)
            return true;
        else
            return false;
    }
}
