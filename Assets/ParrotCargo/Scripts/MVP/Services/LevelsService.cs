using UnityEngine;

using Zenject;

public class LevelsService : BaseService
{
    [Header("References")]
    [SerializeField] private LevelProgressView _levelProgressView;
    [SerializeField] private GameWinView _gameWinView;

    [Inject] private TypeGameService _typeGameService;

    private LevelsProgressPresenter _levelsProgressPresenter;

    public LevelsProgressPresenter LevelsProgressPresenter => _levelsProgressPresenter;

    public override void Initialize()
    {
        _levelsProgressPresenter = new LevelsProgressPresenter(_levelProgressView, _typeGameService, _gameWinView);
    }

    public void Initialize(Level enryLevele)
    {
        if (enryLevele == null)
            Debug.Log("Не найден уровень! Проверьте создан ли уровень " + enryLevele.NumberLevel);

        _levelsProgressPresenter.Initialize(enryLevele);
    }
}
