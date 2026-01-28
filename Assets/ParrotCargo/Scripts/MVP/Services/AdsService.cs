using YG;
using UniRx;
using Zenject;

public class AdsService : BaseService
{
    private string _rewardID;

    [Inject] private PlayerProgressService _playerProgressService;

    public override void Initialize()
    {
        _rewardID = "Coins";
    }

    public void ShowRewardAd()
    {
        YG2.RewardedAdvShow(_rewardID, () => _playerProgressService.OnReward());
    }
}
