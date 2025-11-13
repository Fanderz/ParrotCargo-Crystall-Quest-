using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

public class CoinsView : BaseScoreView
{
    //[SerializeField] private TextMeshProUGUI _coinsView;

    //private bool _isIncreasingCoins;

    //private WaitForSeconds _smoothWait;

    //private Coroutine _smoothIncreaserCoinsCoroutine;

    //public ReactiveCommand<int> CoinsChanged = new ReactiveCommand<int>();

    //public void Initialize(float smoothWait)
    //{
    //    _smoothWait = new WaitForSeconds(smoothWait);
    //}

    //public void SetCoins(int value)
    //{
    //    _isIncreasingCoins = true;

    //    if (_smoothIncreaserCoinsCoroutine != null)
    //        StopCoroutine(_smoothIncreaserCoinsCoroutine);

    //    if (_smoothWait != null)
    //        _smoothIncreaserCoinsCoroutine = StartCoroutine(SmoothIncreaser(_coinsView, Convert.ToInt32(_coinsView.text), value));
    //    else
    //        _coinsView.text = value.ToString();

    //    CoinsChanged.Execute(value);
    //}

    //public void SetScore(int value)
    //{
    //    _scoreView.text = value.ToString();
    //    ScoreChanged.Execute(value);
    //}

    //private IEnumerator SmoothIncreaser(TextMeshProUGUI textView, int startValue, int endValue)
    //{
    //    while (startValue != endValue)
    //    {
    //        startValue += 1;
    //        textView.text = startValue.ToString();

    //        yield return _smoothWait;
    //    }  
    //}

    //private void OnDestroy()
    //{
    //    StopAllCoroutines();
    //}
}
