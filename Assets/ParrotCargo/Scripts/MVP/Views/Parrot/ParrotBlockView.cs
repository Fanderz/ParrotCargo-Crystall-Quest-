using UnityEngine;
using UniRx;
using System.Collections.Generic;

public class ParrotsBlockView : MonoBehaviour
{
    [SerializeField] private float _zOffsetOnPick;
    [SerializeField] private List<ParrotView> _parrots;

    private Draggable _draggable;
    private float _zValue;

    public ReactiveCommand<Vector3> BlockMoving = new ReactiveCommand<Vector3>();

    private void Awake()
    {
        _draggable = GetComponent<Draggable>();
    }

    public void Initialize()
    {
        ActivateRandomParrots();
        _zValue = transform.position.z + _zOffsetOnPick;

        _draggable.MoveCommand.Subscribe(newPosition => { MoveBlock(newPosition); });
    }

    private void MoveBlock(Vector3 newPosition)
    {
        transform.position = new Vector3(newPosition.x, newPosition.y, _zValue);
        BlockMoving.Execute(transform.position);
    }

    private void ActivateRandomParrots()
    {
        var activeParrotsCount = Random.Range(1, _parrots.Count);

        for (int i = 0; i < activeParrotsCount; i++)
        {
            _parrots[Random.Range(0, _parrots.Count)].SetActive(true);
        }
    }
}
