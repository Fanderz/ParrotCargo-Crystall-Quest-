using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Cysharp.Threading.Tasks;

public class SmoothLoaderService : MonoBehaviour
{
    [SerializeField] private Slider _loadingSlider;
    [SerializeField] private List<GameObject> _startingUI;
    [SerializeField] private GameObject _gameUI;
    [SerializeField] private GameObject _startUIButtons;
    [SerializeField] private float _startProgress;
    [SerializeField] private int _waitLoadingMiliseconds;
    [SerializeField] [Range(0, 1)] private float _stopProgress;
    [SerializeField] [Range(0, 1)] private float _progressMultiplier;

    public async void Loading()
    {
        while(_startProgress != _stopProgress)
        {
            _startProgress += _progressMultiplier;
            _loadingSlider.value = _startProgress;
            await UniTask.Delay(_waitLoadingMiliseconds);
        }

        LoadCompleted();
    }

    private void LoadCompleted()
    {
        _loadingSlider.gameObject.SetActive(false);
        _startUIButtons.gameObject.SetActive(true);

        foreach (GameObject obj in _startingUI)
            obj.SetActive(false);

        _gameUI.SetActive(true);
    }
}
