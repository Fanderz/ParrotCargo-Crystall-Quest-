using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardView : MonoBehaviour
{
    private PanelAnimationView _panelAnimationView;

    private void Awake()
    {
        _panelAnimationView = GetComponent<PanelAnimationView>();
    }

    public async void ChangeActive()
    {
        var isActive = gameObject.activeSelf;

        if (isActive == false)
        {
            gameObject.SetActive(true);
            _panelAnimationView.Show();
        }
        else
        {
            _panelAnimationView.Hide();

            await UniTask.Delay(1000);

            gameObject.SetActive(false);
        }
    }
}
