using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundsContainer : MonoBehaviour
{
    [SerializeField] private AudioSource _backgroundAudioSource;
    [SerializeField] private List<AudioSource> _enemyAudioSources;
    [SerializeField] private List<AudioSource> _weaponAudioSources;


    private void Start()
    {

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

    
    private void Pause(List<AudioSource> audioSources)
    {
        foreach(var element in audioSources)
        {
            Pause(element);
        }
    }

    private void Pause(AudioSource audioSource)
    {
        if(audioSource.isPlaying == true)
        {
            audioSource.Pause();
        }
    }

    public void PauseAll()
    {
        Pause(_backgroundAudioSource);
        Pause(_weaponAudioSources);
        Pause(_enemyAudioSources);
    }

    private void UnPause(List<AudioSource> audioSources)
    {
        foreach (var element in audioSources)
        {
            element?.UnPause();
        }
    }

    public void PauseGameSounds()
    {
        Pause(_weaponAudioSources);
        Pause(_enemyAudioSources);
    }

    public void UnPauseAll()
    {
        UnPause(_weaponAudioSources);
        UnPause(_enemyAudioSources);
        _backgroundAudioSource?.UnPause();
    }
}
