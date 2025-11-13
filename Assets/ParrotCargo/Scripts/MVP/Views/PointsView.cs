using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

public class PointsView : BaseScoreView
{
    //[SerializeField] private TextMeshProUGUI _pointsView;

    //private bool _isIncreasingPoints;

    //private WaitForSeconds _smoothWait;

    //private Coroutine _smoothIncreaserPointsCoroutine;

    //public ReactiveCommand<int> PointsChanged = new ReactiveCommand<int>();

    //public void Initialize(float smoothWait)
    //{
    //    _smoothWait = new WaitForSeconds(smoothWait);
    //}

    //public void SetScore(int value)
    //{
    //    _isIncreasingPoints = true;

    //    if (_smoothIncreaserPointsCoroutine != null)
    //        StopCoroutine(_smoothIncreaserPointsCoroutine);

    //    if (_smoothWait != null)
    //        _smoothIncreaserPointsCoroutine = StartCoroutine(SmoothIncreaser(_pointsView, Convert.ToInt32(_pointsView.text), value));
    //    else
    //        _pointsView.text = value.ToString();

    //    PointsChanged.Execute(value);
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
