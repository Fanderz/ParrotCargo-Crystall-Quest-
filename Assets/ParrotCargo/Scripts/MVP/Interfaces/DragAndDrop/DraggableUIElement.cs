using UnityEngine;

public class DraggableUIElement : MonoBehaviour, IDraggableUI
{
    public RectTransform RectTransform => GetComponent<RectTransform>();
}
