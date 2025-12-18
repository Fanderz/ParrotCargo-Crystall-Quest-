using UnityEngine;
using System.Linq;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GameDefaultSettings", menuName = "ScriptableObject/GameDefaultSettings")]
public class GameDefaultSettings : ScriptableObject
{
    [Header("Временные платформы")]
    [SerializeField] private int _defaultTempPalletsCnt;
    [Space]
    [Header("Количество платформ на корабле")]
    [SerializeField] private int _defaultPlatformsCntOnShip;
    [Space]
    [Header("Модель попугая")]
    [SerializeField] private List<ParrotsBlockView> _defaultParrotBlockView;
    [Space]
    [Header("Модель корабля")]
    [SerializeField] private BaseShipView _defaultShipView;

    public int DefaultTempPalletsCnt => _defaultTempPalletsCnt;
    public int DefaultPlatformsCntOnShip => _defaultPlatformsCntOnShip;
    public IReadOnlyList<ParrotsBlockView> DefaultParrotBlockView => _defaultParrotBlockView;
    public BaseShipView DefaultShipView => _defaultShipView;

    public void FirstLoadingSetts(GameDefaultSettings defaultSettings)
    {
        _defaultTempPalletsCnt = defaultSettings.DefaultTempPalletsCnt;
        _defaultPlatformsCntOnShip = defaultSettings.DefaultPlatformsCntOnShip;
        _defaultParrotBlockView = defaultSettings.DefaultParrotBlockView.ToList();
        _defaultShipView = defaultSettings.DefaultShipView;
    }
}
