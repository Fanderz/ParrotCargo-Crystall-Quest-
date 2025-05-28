using UnityEngine;
using UniRx;

public class ParrotsBlockView : MonoBehaviour
{
    private InputSystemService _inputService;
    private RectTransform _rectTransform;

    public ReactiveCommand<Vector2> BlockMoving = new ReactiveCommand<Vector2>();

    public void Initialize(InputSystemService inputSystemService)
    {
        _inputService = inputSystemService;
        _inputService.MoveCommand.Subscribe(newPosition => { MoveBlock(newPosition); });

        _rectTransform = GetComponent<RectTransform>();
    }

    private void MoveBlock(Vector2 newPosition)
    {
        transform.SetParent(null, true);
        _rectTransform.anchoredPosition = newPosition;
        BlockMoving.Execute(_rectTransform.anchoredPosition);
    }
}
