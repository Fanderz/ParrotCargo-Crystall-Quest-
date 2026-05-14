using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

using UnityEngine;

public class ModelAnimationView : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _finishPosition;
    [SerializeField] private float _durationMoving;

    public TweenerCore<Vector3, Vector3, VectorOptions> Show()
    {
        transform.DOKill();
        return transform.DOMove(_finishPosition, _durationMoving).SetUpdate(true);
    }

    public TweenerCore<Vector3, Vector3, VectorOptions> Hide()
    {
        transform.DOKill();
        return transform.DOMove(_startPosition, _durationMoving).SetUpdate(true);
    }

    private void Start()
    {
        transform.position = _startPosition;
    }
}
