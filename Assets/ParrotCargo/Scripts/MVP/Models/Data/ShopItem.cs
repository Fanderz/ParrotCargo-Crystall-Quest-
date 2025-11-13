using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup _grid;
    [SerializeField] private TextMeshProUGUI _headerText;

    private string _name;
    private List<Button> _buttons;
    private List<Image> _buttonImages;

    public string Name => _name;
    public IReadOnlyList<Button> Buttons => _buttons;
    public IReadOnlyList<Image> Images => _buttonImages;

    public void Initialize(ShopItemValues shopItemValues)
    {
        _buttons = new List<Button>();
        _buttonImages = new List<Image>();
        _headerText.text = shopItemValues.ItemHeader;
        _name = shopItemValues.ItemName;

        for (int i = 0; i < shopItemValues.ItemChildCount; i++)
        {
            Button button = Instantiate(shopItemValues.ChildItemPrefab, _grid.transform);
            Image buttonImage = button.GetComponent<Image>();
            _buttons.Add(button);
            _buttonImages.Add(buttonImage);
        }
    }
}
