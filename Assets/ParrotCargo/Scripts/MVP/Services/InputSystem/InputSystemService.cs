using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using Zenject;

public class InputSystemService : BaseService
{
    [SerializeField] private InputAction _mouseClick;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _dragSpeed;

    private Plane _dragPlane;
    private Vector3 _dragOffset;
    private float _dragY;

    private Vector3 _velocity;
    private IDraggable _draggableObject;
    private Transform _draggableTransform;
    private Coroutine _movingBlockCoroutine;

    [Inject] private AudioService _audioService;

    public override void Initialize()
    {
        _velocity = Vector3.zero;

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
                _draggableTransform = draggable.transform;
                _draggableObject = draggable;
                _dragY = _draggableTransform.position.y;
                _dragPlane = new Plane(Vector3.up, new Vector3(0f, _dragY, 0f));
                _dragOffset = _draggableTransform.position - hit.point;
                _dragOffset.y = 0f;

                _audioService.OnBirdPickedSound();
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

            if (_dragPlane.Raycast(ray, out float enter))
            {
                Vector3 target = ray.GetPoint(enter) + _dragOffset;
                target.y = _dragY;

                _draggableObject.SetDraggable(true);
                _draggableObject.MoveCommand.Execute(target);
            }

            yield return null;
        }
    }

    private void OnDestroy()
    {
        _mouseClick.performed -= MousePressed;
        _mouseClick.canceled -= MousePressCanceled;
    }
}