using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestInputSystem : BaseService
{
    [SerializeField] private InputAction _mouseClick;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _dragSpeed;

    private Vector3 _velocity = Vector3.zero;

    public override void Initialize()
    {
        _mouseClick.Enable();
        _mouseClick.performed += MousePressed;
    }

    private void MousePressed(InputAction.CallbackContext ctx)
    {
        Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent(out Draggable draggable))
                StartCoroutine(Drag(draggable));
        }
    }

    private IEnumerator Drag(Draggable draggable)
    {
        float initialDistance = Vector3.Distance(draggable.transform.position, _camera.transform.position);

        while (_mouseClick.ReadValue<float>() != 0)
        {
            Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);

            Vector3 direction = ray.GetPoint(initialDistance) - draggable.transform.position;
            draggable.MoveCommand.Execute(Vector3.SmoothDamp(draggable.transform.position, ray.GetPoint(initialDistance), ref _velocity, _dragSpeed));

            yield return null;
        }
    }
}
