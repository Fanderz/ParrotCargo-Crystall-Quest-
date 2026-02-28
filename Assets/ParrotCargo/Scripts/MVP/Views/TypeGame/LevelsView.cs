using UnityEngine;

using Cysharp.Threading.Tasks;

public class LevelsView : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PanelAnimationView _panelAnimationView;

    public async void SetActive(bool isActive)
    {
        if (isActive)
            _panelAnimationView.Show();
        else
            _panelAnimationView.Hide();

        if(isActive == false)
            await UniTask.Delay(1000);

        gameObject.SetActive(isActive);
    }
}
