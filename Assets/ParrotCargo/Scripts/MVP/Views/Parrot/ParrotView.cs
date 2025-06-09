using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;
using System.Collections;

public class ParrotView : MonoBehaviour
{
    [SerializeField] private Transform _raycastPoint;
    [SerializeField] private Transform _bagPicker;
    [SerializeField] private LayerMask _pickableLayer;

    private BaseCrystallBagView _crystallBag;

    public ReactiveCommand<bool> PickingBag = new ReactiveCommand<bool>();

    public BaseCrystallBagView CrystallBag => _crystallBag;

    public bool CanPick { get; private set; }

    public void SetActive(bool value) => gameObject.SetActive(value);

    public void SearchBag()
    {
        Ray ray = new Ray(_raycastPoint.position, Vector3.forward);

        Debug.DrawRay(_raycastPoint.position, Vector3.forward, Color.yellow, 1f);

        if (Physics.Raycast(ray, out RaycastHit hit, 20f, _pickableLayer))
        {
            CanPick = hit.collider.TryGetComponent(out _crystallBag);

            Debug.Log($"Попадание в мешочек кристаллов: {CanPick}");
            Debug.Log($"Попадание в коллайдер: {hit.collider.name}");

            PickingBag.Execute(CanPick);
        }
    }

    public void PickBag()
    {
        if(_crystallBag != null)
        {
            _crystallBag.transform.SetParent(_bagPicker);
            _crystallBag.transform.position = _bagPicker.position;
        }
    }
}
