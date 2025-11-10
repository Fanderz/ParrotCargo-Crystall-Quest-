using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Slider _loadingSlider;
    [SerializeField] private List<GameObject> _unDestroyableObjects;
    [SerializeField] [Range(0, 1)] private float _stopProgress;
    [SerializeField] [Range(0, 1)] private float _progressMultiplier;

    private float _currentValue;
    //private float _targetValue;
    private AsyncOperation _sceneLoadingOperation;
    private Coroutine _smoothLoadingCoroutine;

    private void Awake()
    {
        _currentValue = 0;
        //_targetValue = 0;

        _loadingSlider.value = _currentValue;
    }

    private void Start()
    {
        foreach (GameObject obj in _unDestroyableObjects)
            DontDestroyOnLoad(obj);
    }

    private void FixedUpdate()
    {
        if (_sceneLoadingOperation != null)
        {
            if (_sceneLoadingOperation.progress == _stopProgress)
            {
                if (_smoothLoadingCoroutine != null)
                    StopCoroutine(_smoothLoadingCoroutine);
            }
        }
    }

    public void LoadScene(int sceneIndex)
    {
        if (sceneIndex <= 0)
            throw new ArgumentOutOfRangeException();

        if (_smoothLoadingCoroutine != null)
        {
            StopCoroutine(_smoothLoadingCoroutine);
            _smoothLoadingCoroutine = null;
        }

        _smoothLoadingCoroutine = StartCoroutine(SmoothLoadingCoroutine(sceneIndex));
    }

    private IEnumerator SmoothLoadingCoroutine(int sceneIndex)
    {
        _sceneLoadingOperation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
        _sceneLoadingOperation.allowSceneActivation = false;

        while (_sceneLoadingOperation.progress <= 1)
        {
            _loadingSlider.value = _sceneLoadingOperation.progress;

            if (_sceneLoadingOperation.progress == _stopProgress)
            {
                _loadingSlider.value = 1f;
                _sceneLoadingOperation.allowSceneActivation = true;
                yield return new WaitForSeconds(0.001f);
                break;
            }

            Debug.Log("Progress: " + _sceneLoadingOperation.progress);

            yield return null;
        }
    }
}
