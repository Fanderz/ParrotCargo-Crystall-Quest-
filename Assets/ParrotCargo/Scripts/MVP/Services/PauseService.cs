using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseService : BaseService
{
    private bool _pausedBySettings;
    private bool _pausedByGameWinLose;
    private bool _pausedByRewardAdv;

    public bool IsPaused => _pausedBySettings || _pausedByGameWinLose || _pausedByRewardAdv;

    public override void Initialize()
    {
    }

    public void SetPausedBySettings(bool value)
    {
        _pausedBySettings = value;
        ApplyPause();
    }

    public void SetPausedByWinLose(bool value)
    {
        _pausedByGameWinLose = value;
        ApplyPause();
    }

    public void SetPausedByRewardAdv(bool value)
    {
        _pausedByRewardAdv = value;
        ApplyPause();
    }

    public void ResetAll()
    {
        _pausedBySettings = false;
        _pausedByGameWinLose = false;
        _pausedByRewardAdv = false;
        ApplyPause();
    }

    private void ApplyPause()
    {
        Time.timeScale = IsPaused ? 0f : 1f;
        Debug.Log($"[PauseService] TimeScale {Time.timeScale}");
    }
}
