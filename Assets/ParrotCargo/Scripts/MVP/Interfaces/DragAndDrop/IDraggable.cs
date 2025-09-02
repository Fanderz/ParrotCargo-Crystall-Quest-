using UniRx;
using UnityEngine;

public interface IDraggable
{
    public bool IsDragging { get; }

    public ReactiveCommand<Vector3> MoveCommand { get; }
    public ReactiveCommand StopMoving { get; }

    public void SetDraggable(bool value);
}
