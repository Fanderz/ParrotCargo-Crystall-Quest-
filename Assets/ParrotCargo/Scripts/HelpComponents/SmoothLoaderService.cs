using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using YG;
using UniRx;
using Zenject;
using Cysharp.Threading.Tasks;

public class SmoothLoaderService : BaseService
{
    [SerializeField] private Slider _loadingSlider;
    [SerializeField] private List<GameObject> _startingUI;
    [SerializeField] private GameObject _gameUI;
    [SerializeField] private GameObject _startUIButtons;
    [SerializeField] private float _startProgress;
    [SerializeField] private int _waitLoadingMiliseconds;
    [SerializeField][Range(0, 1)] private float _stopProgress;
    [SerializeField][Range(0, 1)] private float _progressMultiplier;

    public ReactiveCommand LoadingCompletedCommand = new();

    [Inject] private AudioService _audioService;

    public override void Initialize()
    {
        YG2.onCloseInterAdv += LoadCompleted;
    }

    public async void Loading()
    {
        _startProgress = 0;
        _loadingSlider.value = 0;

        YG2.InterstitialAdvShow();

        while (_startProgress != _stopProgress)
        {
            _startProgress += _progressMultiplier;
            _loadingSlider.value = _startProgress;
            await UniTask.Delay(_waitLoadingMiliseconds);
        }
    }

    private void LoadCompleted()
    {
        YG2.onCloseInterAdv -= LoadCompleted;

        _loadingSlider.gameObject.SetActive(false);
        _startUIButtons.gameObject.SetActive(true);

        foreach (GameObject obj in _startingUI)
            obj?.SetActive(false);

        _gameUI?.SetActive(true);

        _audioService.OnGameStarted();
        LoadingCompletedCommand.Execute();
    }
}
