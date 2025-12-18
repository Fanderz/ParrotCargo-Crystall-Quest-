using System;
using System.Collections;

using UnityEngine;
using Unity.VisualScripting;

using UniRx;
using TMPro;

public class BaseScoreView : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI valueView;

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
            targetValue = value;

            if (smoothIncreaserValueCoroutine != null)
                StopCoroutine(smoothIncreaserValueCoroutine);

            if (smoothWait != null && this.gameObject.activeInHierarchy)
                smoothIncreaserValueCoroutine = StartCoroutine(SmoothChanger(valueView, Convert.ToInt32(valueView.text), value));
            else
                valueView.text = value.ToString();

            ValueChanged.Execute(value);
        }
    }

    private IEnumerator SmoothChanger(TextMeshProUGUI textView, int startValue, int endValue)
    {
        while (startValue != endValue && this.gameObject.activeInHierarchy)
        {
            if (startValue < endValue)
                startValue += 1;

            if (startValue > endValue)
                startValue -= 1;

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
