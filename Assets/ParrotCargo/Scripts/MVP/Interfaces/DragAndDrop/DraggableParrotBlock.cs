using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class DraggableParrotBlock : MonoBehaviour, IDraggable
{
    [SerializeField] private float _yDraggingOffset;

    public float YFlyingOffset => _yDraggingOffset;

    public bool IsDragging { get; private set; } 

    public ReactiveCommand<Vector3> MoveCommand { get; } = new ReactiveCommand<Vector3>();
    public ReactiveCommand StopMoving { get; } = new ReactiveCommand();

    public void SetDraggable(bool value)
    {
        IsDragging = value;
    }
}
