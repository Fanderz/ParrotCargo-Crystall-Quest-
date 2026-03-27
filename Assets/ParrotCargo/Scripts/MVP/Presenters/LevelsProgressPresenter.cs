public class LevelsProgressPresenter
{
    private LevelProgressView _levelProgressView;
    private TypeGameService _typeGameService;
    private GameWinView _gameWinView;
    private Level _currentLevel;

    public LevelsProgressPresenter(LevelProgressView levelProgressView, TypeGameService typeGameService, GameWinView gameWinView)
    {
        _levelProgressView = levelProgressView;
        _typeGameService = typeGameService;
        _gameWinView = gameWinView;
    }

    public void Initialize(Level currentLevel)
    {
        _currentLevel = currentLevel;

        _levelProgressView.SetActive(true);
        _levelProgressView.UpdateNumverLevelView(_currentLevel);
        _levelProgressView.UpdateCountBag—ollectedView(_currentLevel);
    }

    public void Add—ountBag—ollected(TypeCrystallBag bagType)
    {
        if (_typeGameService.CurrentTypeGame == TypeGame.EndlessTypeGame)
            return;

        if (IsBagSatisfiesLevel(bagType))
        {
            _currentLevel.Add—ountBag—ollected();
            _levelProgressView.UpdateCountBag—ollectedView(_currentLevel);

            if (_currentLevel.TryFinishLevel())
                _gameWinView.SetActive(true);
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
