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
    [SerializeField] private GameObject _background;
    [SerializeField] private float _startProgress;
    [SerializeField] private int _waitLoadingMiliseconds;
    [SerializeField][Range(0, 1)] private float _stopProgress;
    [SerializeField][Range(0, 1)] private float _progressMultiplier;

    public ReactiveCommand LoadingCompletedCommand = new();

    [Inject] private AudioService _audioService;
    [Inject] private TypeGameService _typeGameService;

    public override void Initialize()
    {
        YG2.onCloseInterAdv += Loading;
    }

    public async void Loading()
    {
        foreach (GameObject obj in _startingUI)
            obj?.SetActive(false);

        _loadingSlider?.gameObject.SetActive(true);

        _startProgress = 0;
        _loadingSlider.value = 0;

        while (_startProgress < _stopProgress)
        {
            _startProgress += _progressMultiplier;
            _loadingSlider.value = _startProgress;
            await UniTask.Delay(_waitLoadingMiliseconds);
        }

        LoadCompleted();
    }

    private void LoadCompleted()
    {
        YG2.onCloseInterAdv -= Loading;

        _loadingSlider.gameObject.SetActive(false);
        _background.SetActive(false);
        _gameUI.SetActive(true);
        
        _audioService.OnGameStarted();
        LoadingCompletedCommand.Execute();
    }
}
