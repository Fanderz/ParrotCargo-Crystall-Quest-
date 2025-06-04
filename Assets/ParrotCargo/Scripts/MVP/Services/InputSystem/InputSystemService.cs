using UniRx;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class InputSystemService : BaseService
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Camera _camera;
    [SerializeField] private InputAction _mouseClick;
    [SerializeField] private LayerMask _draggableLayer;
    [SerializeField] private float _dragSpeed;

    private bool _isDragging;

    private Vector2 _pointerScreenPosition;
    private PlayerInput _playerInput;
    private IDraggable _currentDraggable;
    private Coroutine _draggingCoroutine;
    private Plane _canvasPlane;

    public ReactiveCommand<Vector2> MoveCommand = new ReactiveCommand<Vector2>();

    private bool IsClicked()
    {
        Ray ray = _camera.ScreenPointToRay(_pointerScreenPosition);

        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red, 1f);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, _draggableLayer))
        {
            if (hit.collider.TryGetComponent(out Draggable parrotBlock))
            {
                _currentDraggable = parrotBlock;
                return true;
            }
        }

        return false;
    }

    private IEnumerator Drag()
    {
        _isDragging = true;

        while (_isDragging && _currentDraggable != null)
        {
            Ray ray = _camera.ScreenPointToRay(_pointerScreenPosition);

            if (_canvasPlane.Raycast(ray, out float distance))
            {
                Vector3 targetPosition = ray.GetPoint(distance);
                _currentDraggable.MoveCommand.Execute(targetPosition);
            }

            yield return null;
        }
    }

    public override void Initialize()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();

        _playerInput.ParrotBlock.Point.performed += ctx =>
        {
            _pointerScreenPosition = ctx.ReadValue<Vector2>();
        };

        _playerInput.ParrotBlock.Press.started += press =>
        {
            if (IsClicked())
            {
                if (_currentDraggable != null)
                    _draggingCoroutine = StartCoroutine(Drag());
            }
        };

        _playerInput.ParrotBlock.Press.canceled += drop =>
        {
            _isDragging = false;
            _currentDraggable = null;

            if (_draggingCoroutine != null)
                StopCoroutine(_draggingCoroutine);
        };

        Vector3 planeNormal = _canvas.transform.forward;
        Vector3 planePoint = _canvas.transform.position;
        _canvasPlane = new Plane(planeNormal, planePoint);
    }
}
