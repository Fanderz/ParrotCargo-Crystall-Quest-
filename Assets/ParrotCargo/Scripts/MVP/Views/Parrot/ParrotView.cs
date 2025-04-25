using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;

public class ParrotView : EventTrigger
{
    public ReactiveCommand<Vector3> MoveCommand = new ReactiveCommand<Vector3>();

    public override void OnMove(AxisEventData eventData)
    {
        MoveCommand.Execute(eventData.moveVector);
    }

    public void UpdatePosition(Vector3 newPosition)
        => gameObject.transform.position = newPosition;
}
