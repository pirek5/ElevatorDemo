using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum Sound { elevatorMoves, button, elevatorBoing, doors }

public class ElevatorSounds : MonoBehaviour
{
    [Inject] AudioSource audioSource;

    public SoundAudioClip[] soundAudioClipArray;

    [System.Serializable]
    public class SoundAudioClip
    {
        public Sound sound;
        public AudioClip audioClip;
    }

    public void PlaySound(Sound sound)
    {
        audioSource.Stop();
        audioSource.clip = GetAudioClip(sound);
        audioSource.Play();
    }

    public void StopSound()
    {
        audioSource.Stop();
    }

    public AudioClip GetAudioClip(Sound sound)
    {
        foreach (var soundAudioClip in soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }

        Debug.LogError("Cant find sound!");
        return null;
    }
}
