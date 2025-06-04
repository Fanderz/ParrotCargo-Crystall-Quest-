using UniRx;
using UnityEngine;

public class Draggable : MonoBehaviour, IDraggable
{
    private ReactiveCommand<Vector3> Dragging = new ReactiveCommand<Vector3>();

    public ReactiveCommand<Vector3> MoveCommand => Dragging;

}
