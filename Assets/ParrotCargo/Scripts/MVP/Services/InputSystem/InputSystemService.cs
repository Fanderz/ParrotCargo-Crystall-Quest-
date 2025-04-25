using UniRx;
using UnityEngine;
using System.Collections;

public class InputSystemService : BaseService
{
    private bool _isDragging;

    private Vector3 _currentScreenPosition;
    private PlayerInput _playerInput;
    private Camera _camera;

    public ReactiveCommand<Vector3> MoveCommand = new ReactiveCommand<Vector3>();

    private bool _isClicked 
    { 
        get
        {
            Ray ray = _camera.ScreenPointToRay(_currentScreenPosition);
            
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                return hit.collider.TryGetComponent(out ParrotsBlockView view);
            }

            return false;
        }
    }

    private Vector3 _worldPosition
    {
        get
        {
            float zPosition = _camera.WorldToScreenPoint(transform.position).z;
            return _camera.ScreenToWorldPoint(_currentScreenPosition + new Vector3(0,0,zPosition));
        }
    }

    private void Awake()
    {
    }

    private IEnumerator Drag()
    {
        _isDragging = true;
        //Vector3 offset = transform.position - _worldPosition;
        
        while(_isDragging)
        {
            MoveCommand.Execute(_worldPosition);
            //transform.position = _worldPosition + offset;
            yield return null;
        }
    }

    public override void Initialize()
    {
        _camera = Camera.main;
        _playerInput = new PlayerInput();
        _playerInput.ParrotBlock.Enable();
        _playerInput.ParrotBlock.Move.performed += context => { _currentScreenPosition = context.ReadValue<Vector2>(); };
        _playerInput.ParrotBlock.Press.performed += press => { if (_isClicked) StartCoroutine(Drag()); };
        _playerInput.ParrotBlock.Press.canceled += drop => { _isDragging = false; };
    }
}
