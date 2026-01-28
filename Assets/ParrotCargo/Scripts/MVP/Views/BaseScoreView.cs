using System;
using System.Collections;

using UnityEngine;
using Unity.VisualScripting;

using UniRx;
using TMPro;
using DG.Tweening;

public class BaseScoreView : MonoBehaviour
{
    [SerializeField] protected float _durationIncrease = 1f;
    [SerializeField] protected TextMeshProUGUI valueView;

    protected int targetValue;

    protected WaitForSeconds smoothWait;

    public ReactiveCommand<int> ValueChanged = new ReactiveCommand<int>();

    public void Initialize(float inputSmoothWait)
    {
        smoothWait = new WaitForSeconds(inputSmoothWait);
    }

    public void ChangeValue(int value)
    {
        if (this.IsDestroyed() == false)
        {
            if (gameObject.activeSelf)
                UpdateCount(valueView, value);
            else
                valueView.text = value.ToString();
        }
    }

    public void UpdateCount(TextMeshProUGUI textView, int countValue)
    {
        int addedValue = int.Parse(textView.text);

        DOTween.To(() => int.Parse(textView.text), x => { addedValue = x; textView.text = addedValue.ToString(); }, countValue, _durationIncrease);
    }

    private void OnDestroy()
    {
        valueView.text = targetValue.ToString();
    }
}
