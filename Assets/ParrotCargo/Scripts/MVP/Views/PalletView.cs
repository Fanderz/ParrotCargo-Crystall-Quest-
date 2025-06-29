using UnityEngine;

public class PalletView : MonoBehaviour
{
    public bool IsEmpty { get; private set; } = true;

    public void ChangeEmpty(bool value)
    {
        IsEmpty = value;
    }
}
