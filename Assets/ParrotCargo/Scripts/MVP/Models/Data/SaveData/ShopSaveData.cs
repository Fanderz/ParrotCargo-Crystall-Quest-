using System;

[Serializable]
public class ShopSaveData
{
    public TypeShopItem Type;
    public bool IsPurchased;
    public bool isActive;
}

public class NullableShopSaveData : ShopSaveData
{

}
