using Cysharp.Threading.Tasks;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    [SerializeField] private Sprite _filledStarSprite;

    private PanelAnimationView _panelAnimationView;

    private List<ShopItemView> _shopItems;

    public IReadOnlyList<ShopItemView> ShopItems => _shopItems;

    private void Awake()
    {
        _panelAnimationView = GetComponent<PanelAnimationView>();
    }

    public void Initialize(List<ShopItemView> shopItems)
    {
        _shopItems = shopItems;
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

            await UniTask.Delay(500);
            
            gameObject.SetActive(false);
        }
    }
}
