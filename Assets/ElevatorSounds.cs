using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ElevatorSounds : MonoBehaviour
{
    [SerializeField] private AudioClip elevatorBoing;
    [SerializeField] private AudioClip elevatorRide;

    [Inject] AudioSource audioSource;

    public void PlayBoing()
    {
        audioSource.Stop();
        audioSource.clip = elevatorBoing;
        audioSource.Play();
    }

    public void PlayElevatorRide()
    {
        audioSource.Stop();
        audioSource.clip = elevatorRide;
        audioSource.Play();
    }
}
