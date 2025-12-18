using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class InputSystemService : BaseService
{
    [SerializeField] private InputAction _mouseClick;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _dragSpeed;

    private Vector3 _velocity = Vector3.zero;
    private IDraggable _draggableObject;
    private Transform _draggableTransform;
    private Coroutine _movingBlockCoroutine;

    public override void Initialize()
    {
        _mouseClick.Enable();
        _mouseClick.performed += MousePressed;
        _mouseClick.canceled += MousePressCanceled;
    }

    private void MousePressed(InputAction.CallbackContext ctx)
    {
        Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent(out DraggableParrotBlock draggable))
            {
                _draggableObject = draggable;
                _draggableTransform = draggable.transform;
                _movingBlockCoroutine = StartCoroutine(Drag());
            }
        }
    }

    private void MousePressCanceled(InputAction.CallbackContext ctx)
    {
        if (_movingBlockCoroutine != null)
        {
            StopCoroutine(_movingBlockCoroutine);
            _movingBlockCoroutine = null;
        }

        if (_draggableObject != null && _draggableObject.IsDragging)
        {
            _draggableObject.StopMoving.Execute();
            _draggableObject = null;
        }
    }

    private IEnumerator Drag()
    {
        float initialDistance = Vector3.Distance(_draggableTransform.position, _camera.transform.position);

        while (_mouseClick.ReadValue<float>() != 0)
        {
            Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            _draggableObject.SetDraggable(true);
            _draggableObject.MoveCommand.Execute(Vector3.SmoothDamp(_draggableTransform.position, ray.GetPoint(initialDistance), ref _velocity, _dragSpeed));

            yield return null;
        }
    }

    private void OnDestroy()
    {
        _mouseClick.performed -= MousePressed;
        _mouseClick.canceled -= MousePressCanceled;
    }
}
