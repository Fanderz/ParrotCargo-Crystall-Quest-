public class LevelsProgressPresenter
{
    private LevelProgressView _levelProgressView;
    private TypeGameService _typeGameService;
    private Level _currentLevel;

    public LevelsProgressPresenter(LevelProgressView levelProgressView, TypeGameService typeGameService)
    {
        _levelProgressView = levelProgressView;
        _typeGameService = typeGameService;
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
            return;
    }
}
