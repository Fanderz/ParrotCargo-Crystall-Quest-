using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartingUIService : BaseService
{
    [SerializeField] private List<PanelAnimationView> _panelsAnimationView;
    [SerializeField] private Button _buttonStartGame;

    public override void Initialize()
    {
        _buttonStartGame.onClick.AddListener(() =>
        {
            foreach (var panelAnimationView in _panelsAnimationView)
                panelAnimationView.Hide();
        });

        foreach (var panelAnimationView in _panelsAnimationView)
            panelAnimationView.Show();
    }
}
