using System.Collections.Generic;
using UnityEngine;

using Cysharp.Threading.Tasks;

public class GameWinView : MonoBehaviour
{
    [SerializeField] private List<PanelAnimationView> _panelAnimationViews;

    public async void SetActive(bool isActive)
    {
        HandlerPanelAnimation(isActive);

        if (isActive == false)
            await UniTask.Delay(1000);

        gameObject.SetActive(isActive);
    }

    private void HandlerPanelAnimation(bool isActive)
    {
        foreach (var panelAnimationView in _panelAnimationViews)
        {
            if (isActive)
                panelAnimationView.Show();
            else
                panelAnimationView.Hide();
        }
    }
}
