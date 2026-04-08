using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
using Zenject;

public class GameStarterService : BaseService
{
    [SerializeField] private SmoothLoaderService _loaderService;
    [SerializeField] private CrystallBagsService _bagsService;
    [SerializeField] private ShipsService _shipsService;
    [SerializeField] private PalletService _palletService;
    [SerializeField] private ParrotsBlockService _parrotsBlockService;

    [SerializeField] private LevelsService _levelsService;
    [SerializeField] private TypeGameService _typeGameService;

    public override void Initialize()
    {
    }

    public void StartGame()
    {
        Debug.Log("<size=50>Вызвался GameStarterService.StartGame</size>");
        Time.timeScale = 1f;
        YG2.InterstitialAdvShow();

        _palletService.OnStartGame();
        _shipsService.OnStartGame();
        _bagsService.OnStartGame();
        _parrotsBlockService.OnStartGame();
    }
}
