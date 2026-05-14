using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

using Zenject;

public class GameOverShipView : MonoBehaviour
{
    [SerializeField] private int _countShootingWaves;
    [SerializeField] private float _shipsSinkTargetY = -500f;
    [SerializeField] private float _shipsSinkDuration = 3f;
    [SerializeField] private List<ParticleSystem> _particles;


    private ModelAnimationView _animationView;

    [Inject] private ShipsService _shipService;

    private void Awake()
    {
        _animationView = GetComponent<ModelAnimationView>();
    }

    //private void OnDisable()
    //{
    //    _animationView.Hide();
    //}

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }

    public async UniTask PlayGameOverSequenceAsync(CancellationToken ct)
    {
        gameObject.SetActive(true);
        _shipService.PrepareGameOverSequence();

        Tweener moveTween = _animationView.Show();

        await UniTask.WaitUntil(() => moveTween == null || !moveTween.IsActive() || !moveTween.IsPlaying(), cancellationToken: ct);

        _shipService.StartGameOverSinking(_shipsSinkTargetY, _shipsSinkDuration);

        for (int i = 0; i < _countShootingWaves; i++)
        {
            await FireSalvoAndWaitAsync(ct);

            await UniTask.Delay(500, cancellationToken: ct);
        }
    }

    private async UniTask FireSalvoAndWaitAsync(CancellationToken ct)
    {
        foreach (var p in _particles)
        {
            if (p != null)
            {
                p.Play();
                await UniTask.Delay(50, cancellationToken: ct);
            }
        }

        var allStopped = UniTask.WaitUntil(() => _particles.Where(p => p != null).All(p => !p.IsAlive(true)), cancellationToken: ct);

        var timeout = UniTask.Delay(50, cancellationToken: ct);

        await UniTask.WhenAny(allStopped, timeout);
    }
}
