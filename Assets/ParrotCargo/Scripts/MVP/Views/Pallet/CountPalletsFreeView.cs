using UnityEngine;
using TMPro;

public class CountPalletsFreeView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countPalletFreeView;

    public void UpdateCountPalletFree(int value)
        => _countPalletFreeView.text = value.ToString();
}
