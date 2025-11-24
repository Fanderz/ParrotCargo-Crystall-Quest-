using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

public class BaseScoreView : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI valueView;

    protected bool isIncreasingValue;

    protected WaitForSeconds smoothWait;

    protected Coroutine smoothIncreaserValueCoroutine;

    public ReactiveCommand<int> ValueChanged = new ReactiveCommand<int>();

    public void Initialize(float inputSmoothWait)
    {
        smoothWait = new WaitForSeconds(inputSmoothWait);
    }

    public void ChangeValue(int value)
    {
        isIncreasingValue = true;

        if (smoothIncreaserValueCoroutine != null)
            StopCoroutine(smoothIncreaserValueCoroutine);

        if (smoothWait != null && this.gameObject.activeInHierarchy)
            smoothIncreaserValueCoroutine = StartCoroutine(SmoothIncreaser(valueView, Convert.ToInt32(valueView.text), value));
        else
            valueView.text = value.ToString();

        ValueChanged.Execute(value);
    }

    private IEnumerator SmoothIncreaser(TextMeshProUGUI textView, int startValue, int endValue)
    {
        while (startValue != endValue && this.gameObject.activeInHierarchy)
        {
            startValue += 1;
            textView.text = startValue.ToString();

            yield return smoothWait;
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
