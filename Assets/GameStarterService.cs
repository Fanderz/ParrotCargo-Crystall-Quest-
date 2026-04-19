using UnityEngine;
using YG;
using Zenject;

public class GameStarterService : BaseService
{
    //[SerializeField] private SmoothLoaderService _loaderService;
    [SerializeField] private CrystallBagsService _bagsService;
    [SerializeField] private ShipsService _shipsService;
    [SerializeField] private PalletService _palletService;
    [SerializeField] private ParrotsBlockService _parrotsBlockService;

    //[SerializeField] private LevelsService _levelsService;
    //[SerializeField] private TypeGameService _typeGameService;

    [Inject] private PauseService _pauseService;

    public override void Initialize()
    {
    }

    public void StartGame()
    {
        _pauseService.ResetAll();
        YG2.InterstitialAdvShow();
        Debug.Log($"[GameStarterService] after InterstialAdvShow TimeScale: {Time.timeScale}");

        _palletService.OnStartGame();
        _shipsService.OnStartGame();
        _bagsService.OnStartGame();
        _parrotsBlockService.OnStartGame();

        Debug.Log($"[GameStarterService] after StartGame TimeScale: {Time.timeScale}");
    }
}
