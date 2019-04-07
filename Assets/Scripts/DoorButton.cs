using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DoorButton : MonoBehaviour, IButton
{
    //set in editor
    [SerializeField] private int thisFloor;

    //dependencies
    [Inject] private ElevatorController elevator;
    [Inject] private ElevatorSounds elevatorSounds;
    [Inject] private AudioSource audioSource;
    
    public void EnableButton()
    {
        elevatorSounds.PlaySoundFromDifferentAudioSource(Sound.button, audioSource);
        elevator.CallElevator(thisFloor);
    }
}
