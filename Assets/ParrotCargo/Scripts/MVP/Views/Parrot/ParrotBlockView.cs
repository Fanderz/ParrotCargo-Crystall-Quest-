using UnityEngine;
using UniRx;

public class ParrotsBlockView : MonoBehaviour
{
    private InputSystemService _inputService;

    public void Initialize(InputSystemService inputSystemService)
    {
        _inputService = inputSystemService;
        _inputService.MoveCommand.Subscribe(newPosition => { MoveBlock(newPosition); });
    }

    private void MoveBlock(Vector3 newPosition)
    {
    }

}
