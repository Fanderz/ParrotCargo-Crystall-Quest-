using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private List<ParrotsBlockView> _defaultParrotView;
    [Space]
    [Header("Модель корабля")]
    [SerializeField] private BaseShipView _defaultShipView;

}
