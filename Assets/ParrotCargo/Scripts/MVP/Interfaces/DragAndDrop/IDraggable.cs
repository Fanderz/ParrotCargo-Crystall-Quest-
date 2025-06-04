using UniRx;
using UnityEngine;

public interface IDraggable
{
    public ReactiveCommand<Vector3> MoveCommand { get; }
}
