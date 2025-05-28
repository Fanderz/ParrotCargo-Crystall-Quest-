using UniRx;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InputSystemService : BaseService
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private float _dragSpeed;

    private bool _isDragging;

    private Vector3 _currentScreenPosition;
    private PlayerInput _playerInput;
    private Camera _camera;
    private GraphicRaycaster _graphicRaycaster;

    private IDraggableUI _currentDraggable;

    public ReactiveCommand<Vector2> MoveCommand = new ReactiveCommand<Vector2>();

    private bool IsClicked(out IDraggableUI draggable)
    {
        draggable = null;

        Ray ray = _camera.ScreenPointToRay(_currentScreenPosition);

        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red, 2f);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent(out IDraggableUI draggableObject))
            {
                draggable = draggableObject;
                return true;
            }
        }

        return false;
    }

    //private bool RaycastForDraggable(out IDraggableUI draggable)
    //{
    //    draggable = null;

    //    PointerEventData pointerData = new PointerEventData(EventSystem.current)
    //    {
    //        position = _currentScreenPosition
    //    };

    //    List<RaycastResult> results = new List<RaycastResult>();
    //    _graphicRaycaster.Raycast(pointerData, results);



    //    foreach (var value in results)
    //    {
    //        if (value.gameObject.TryGetComponent(out IDraggableUI draggableObject))
    //        {
    //            draggable = draggableObject;
    //            return true;
    //        }
    //    }

    //    return false;
    //}

    private IEnumerator Drag()
    {
        _isDragging = true;

        while (_isDragging && _currentDraggable != null)
        {
            bool haveLocalPoint = RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform, _currentScreenPosition, _camera, out Vector2 localPoint);

            if (haveLocalPoint)
            {
                Vector2 currentPosition = _currentDraggable.RectTransform.anchoredPosition;
                _currentDraggable.RectTransform.anchoredPosition = Vector2.Lerp(currentPosition, localPoint, Time.deltaTime * _dragSpeed);
                MoveCommand.Execute(localPoint);
            }

            yield return null;
        }
    }

    public override void Initialize()
    {
        _camera = Camera.main;
        _graphicRaycaster = _canvas.GetComponent<GraphicRaycaster>();
        _playerInput = new PlayerInput();
        _playerInput.ParrotBlock.Enable();

        _playerInput.ParrotBlock.Move.performed += context =>
        {
            _currentScreenPosition = context.ReadValue<Vector2>();
        };

        _playerInput.ParrotBlock.Press.started += press =>
        {
            if (IsClicked(out var foundedDraggable))
                _currentDraggable = foundedDraggable; StartCoroutine(Drag());
            //if (RaycastForDraggable(out var foundedDraggable))
            //    _currentDraggable = foundedDraggable; StartCoroutine(Drag());
        };

        _playerInput.ParrotBlock.Press.canceled += drop =>
        {
            _isDragging = false;
            _currentDraggable = null;
        };
    }
}
