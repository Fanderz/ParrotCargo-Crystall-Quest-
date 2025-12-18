using UnityEngine;

using UniRx;

public class DraggableParrotBlock : MonoBehaviour, IDraggable
{
    [SerializeField] private float _yDraggingOffset;

    public float YFlyingOffset => _yDraggingOffset;

    public bool IsDragging { get; private set; } 

    public ReactiveCommand<Vector3> MoveCommand { get; set; }
    public ReactiveCommand StopMoving { get; set; }

    private void OnEnable()
    {
        MoveCommand = new();
        StopMoving = new();
    }

    private void OnDisable()
    {
        MoveCommand.Dispose();
        StopMoving.Dispose();
    }

    public void SetDraggable(bool value)
    {
        IsDragging = value;
    }
}
