using UnityEngine;

using UniRx;

public class Ship
{
    private int _palletsCount;

    public ReactiveCommand PalletsCntChanged = new ReactiveCommand();

    public Ship(Vector3 pointOnFilled, int activePalletsCount)
    {
        TargetOnFilled = pointOnFilled;
        _palletsCount = activePalletsCount;
    }

    public Vector3 TargetOnFilled { get; private set; }
    public bool isGoingToRelease { get; private set; }

    public void Initialize(int activePalletsCount)
    {
        for (int i = 0; i < activePalletsCount; i++)
            AddPallet();
    }

    public void AddPallet()
    {
        _palletsCount++;
        PalletsCntChanged.Execute();
    }

    public void SetGoingToRelease(bool value)
    {
        isGoingToRelease = value;
    }
}
