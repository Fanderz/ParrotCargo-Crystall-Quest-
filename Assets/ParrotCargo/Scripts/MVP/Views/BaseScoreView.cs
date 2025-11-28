using System;
using System.Collections;
using UnityEngine;
using UniRx;
using TMPro;
using Unity.VisualScripting;

public class BaseScoreView : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI valueView;

    protected bool isIncreasingValue;
    protected int targetValue;

    protected WaitForSeconds smoothWait;

    protected Coroutine smoothIncreaserValueCoroutine;

    public ReactiveCommand<int> ValueChanged = new ReactiveCommand<int>();

    public void Initialize(float inputSmoothWait)
    {
        smoothWait = new WaitForSeconds(inputSmoothWait);
    }

    public void ChangeValue(int value)
    {
        if (this.IsDestroyed() == false)
        {
            isIncreasingValue = true;
            targetValue = value;

            if (smoothIncreaserValueCoroutine != null)
                StopCoroutine(smoothIncreaserValueCoroutine);

            if (smoothWait != null && this.gameObject.activeInHierarchy)
                smoothIncreaserValueCoroutine = StartCoroutine(SmoothIncreaser(valueView, Convert.ToInt32(valueView.text), value));
            else
                valueView.text = value.ToString();
            
            ValueChanged.Execute(value);
        }
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
        valueView.text = targetValue.ToString();
    }
}
