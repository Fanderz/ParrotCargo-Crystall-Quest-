using YG;
using UniRx;
using Zenject;
using UnityEngine;
using UnityEngine.UI;

public class AdsService : BaseService
{
    [SerializeField] private Button _startGameButton;

    private string _rewardID;

    [Inject] private PlayerProgressService _playerProgressService;

    public string Id => _rewardID;

    public override void Initialize()
    {
        _rewardID = "Coins";
    }

    private void OnEnable()
    {
        YG2.onRewardAdv += _playerProgressService.OnReward;
        _startGameButton.onClick.AddListener(() => YG2.InterstitialAdvShow());
    }

    public void ShowRewardAd()
    {
        YG2.RewardedAdvShow(_rewardID);
    }

    private void OnDisable()
    {
        YG2.onRewardAdv += _playerProgressService.OnReward;
    }
}
