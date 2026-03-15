using UnityEngine;
using UnityEngine.UI;

public class TypeGameService : BaseService
{
    [Header("UI")]
    [SerializeField] private Button _endlessTypeGame;
    [SerializeField] private Button _levelsTypeGame;

    [SerializeField] private TypeGame _currentTypeGame;

    public TypeGame CurrentTypeGame => _currentTypeGame;

    public override void Initialize()
    {
        _endlessTypeGame.onClick.AddListener(() => { _currentTypeGame = TypeGame.EndlessTypeGame; });
        _levelsTypeGame.onClick.AddListener(() => { _currentTypeGame = TypeGame.LevelsTypeGame; });
    }

    public void SetTypeGame(TypeGame typeGame)
        => _currentTypeGame = typeGame;
}
