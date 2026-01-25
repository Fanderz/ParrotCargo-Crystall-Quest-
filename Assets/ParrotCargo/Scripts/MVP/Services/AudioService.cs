using UnityEngine;
using UnityEngine.Audio;
using YG;

public class AudioService : BaseService
{
    [Header("AudioMixer Params")]
    [SerializeField] private AudioMixerGroup _mixerGroup;
    [SerializeField] private string _mixerMusicParameterName;
    [SerializeField] private string _mixerEffectsParameterName;
    [Space]
    [Header("AudioSources")]
    [SerializeField] private AudioSource _shipStoppingSound;
    [SerializeField] private AudioSource _seaSound;
    [SerializeField] private AudioSource _crystallsDroppedSound;
    [SerializeField] private AudioSource _birdSound;

    public override void Initialize()
    {
        SetMusicVolume(YG2.saves.playerSettings.Music);
        SetEffectsVolume(YG2.saves.playerSettings.Sound);
    }

    public void SetMusicVolume(float volume)
    {
        ChangeLoud(_mixerMusicParameterName, volume);
    }

    public void SetEffectsVolume(float volume)
    {
        ChangeLoud(_mixerEffectsParameterName, volume);
    }


    public void OnGameStarted()
    {
        if (!_seaSound.isPlaying)
            _seaSound.Play();
    }

    public void OnShipStateChangedSound()
    {
        if (_shipStoppingSound.isPlaying)
            _shipStoppingSound.Stop();

        _shipStoppingSound.PlayOneShot(_shipStoppingSound.clip);
    }

    public void OnBagDroppedSound()
    {
        if (_crystallsDroppedSound.isPlaying)
            _crystallsDroppedSound.Stop();

        _crystallsDroppedSound.PlayOneShot(_crystallsDroppedSound.clip);
    }

    public void OnBirdPickedSound()
    {
        if (_birdSound.isPlaying)
            _birdSound.Stop();

        _birdSound.PlayOneShot(_birdSound.clip);
    }

    private void ChangeLoud(string mixerParameter, float volume)
    {
        _mixerGroup.audioMixer.SetFloat(mixerParameter, Mathf.Log10(volume) * 20);
    }
}
