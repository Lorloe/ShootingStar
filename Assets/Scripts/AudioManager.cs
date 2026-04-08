using System;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Shooting SFX")]
    [SerializeField] private AudioClip shootingSound;
    [SerializeField] [Range(0f, 1f)] private float shootingVolume = 1f;

    [Header("Damage Explosion SFX")]
    [SerializeField] private AudioClip damageExplosionSound;
    [SerializeField] [Range(0f, 1f)] private float damageExplosionVolume = 1f;

    void PlayAudioClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
        }
        else
        {
            Debug.LogWarning("Audio clip is not assigned in the AudioManager.");
        }
    }

    public void PlayShootingSFX()
    {
        if (shootingSound != null)
        {
            PlayAudioClip(shootingSound, shootingVolume);
        }
    }

    public void PlayDamageExplosionSFX()
    {
        if (damageExplosionSound != null)
        {
            PlayAudioClip(damageExplosionSound, damageExplosionVolume);
        }
    }
}
