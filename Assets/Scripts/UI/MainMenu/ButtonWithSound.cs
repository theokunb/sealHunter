using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonWithSound : MonoBehaviour
{
    [SerializeField] private SoundsContainer _soundContainer;

    public void Play(AudioClip clip)
    {
        if(_soundContainer.TryGetWeaponAudioSource(out AudioSource audioSource))
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
