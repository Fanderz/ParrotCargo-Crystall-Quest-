using UniRx;
using UnityEngine;

public interface IDraggable
{
    public bool IsDragging { get; }

    public ReactiveCommand<Vector3> MoveCommand { get; set; }
    public ReactiveCommand StopMoving { get; set; }

    public void SetDraggable(bool value);
}
