using YG;
using UniRx;
using Zenject;

public class AdsService : BaseService
{
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
