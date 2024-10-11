using System.Collections.Generic;
using UnityEngine;

public class SoundsContainer : MonoBehaviour
{
    public const float VolumeValue = 0.1f;

    [SerializeField] private AudioSource _backgroundAudioSource;
    [SerializeField] private List<AudioSource> _enemyAudioSources;
    [SerializeField] private List<AudioSource> _weaponAudioSources;

    private void OnEnable()
    {
        InitVolume();
    }

    private void OnDisable()
    {
        Mute();
    }

    public void Mute()
    {
        _backgroundAudioSource.volume = 0;
        Mute(_enemyAudioSources);
        Mute(_weaponAudioSources);
    }

    public void UnMute()
    {
        _backgroundAudioSource.volume = VolumeValue;
        UnMute(_enemyAudioSources);
        UnMute(_weaponAudioSources);
    }

    private void InitVolume()
    {
        _backgroundAudioSource.volume = PlayerPrefs.GetFloat(Constants.Strings.Volume, VolumeValue);

        InitVolume(_enemyAudioSources);
        InitVolume(_weaponAudioSources);
    }

    private void InitVolume(IEnumerable<AudioSource> audioSources)
    {
        foreach (var audioSource in audioSources)
        {
            audioSource.volume = PlayerPrefs.GetFloat(Constants.Strings.Volume, VolumeValue);
        }
    }

    private void Mute(IEnumerable<AudioSource> audioSources)
    {
        foreach(var audioSource in audioSources)
        {
            audioSource.volume = 0;
        }
    }

    private void UnMute(IEnumerable<AudioSource> audioSources)
    {
        foreach (var audioSource in audioSources)
        {
            audioSource.volume = VolumeValue;
        }
    }

    public bool TryGetBackgroundAudioSource(out AudioSource audioSource)
    {
        audioSource = _backgroundAudioSource;
        return audioSource != null;
    }

    public bool TryGetEnemyAudioSource(out AudioSource audioSource)
    {
        audioSource = GetAvailableAudioSource(_enemyAudioSources);
        return audioSource != null;
    }

    public bool TryGetWeaponAudioSource(out AudioSource audioSource)
    {
        audioSource = GetAvailableAudioSource(_weaponAudioSources);
        return audioSource != null;
    }

    private AudioSource GetAvailableAudioSource(List<AudioSource> audioSources)
    {
        foreach (AudioSource source in audioSources)
        {
            if (source.isPlaying == false)
            {
                return source;
            }
        }

        return null;
    }
}
