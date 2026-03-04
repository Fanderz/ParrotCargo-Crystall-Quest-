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

    public void Add—ountBag—ollected()
    {
        if (_typeGameService.CurrentTypeGame == TypeGame.EndlessTypeGame)
            return;

        _currentLevel.Add—ountBag—ollected();
        _levelProgressView.UpdateCountBag—ollectedView(_currentLevel);

        if (_currentLevel.TryFinishLevel())
            _gameWinView.SetActive(true);
    }
}
