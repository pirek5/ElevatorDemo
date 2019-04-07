using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using TMPro;

public class ElevatorButtonsPanel : MonoBehaviour
{
    //set in editor
    [SerializeField] private TextMeshProUGUI floorNumberDisplay;
    
    //dependencies
    [Inject] ElevatorController elevatorController;
    [Inject] ItemManager itemManager;
    [Inject] ElevatorSounds elevatorSounds;
    [Inject] AudioSource audioSource;

    public void OnFloorPushed(int floor)
    {
        elevatorController.GoToFloor(floor);
        itemManager.PutAwayItem();
        elevatorSounds.PlaySoundFromDifferentAudioSource(Sound.button, audioSource);
    }

    public void OnDoorOpenPressed()
    {
        elevatorController.OpenElevatorDoors();
        itemManager.PutAwayItem();
        elevatorSounds.PlaySoundFromDifferentAudioSource(Sound.button, audioSource);
    }

    public void OnDoorClosePressed()
    {
        elevatorController.CloseElevatorDoors();
        itemManager.PutAwayItem();
        elevatorSounds.PlaySoundFromDifferentAudioSource(Sound.button, audioSource);
    }

    private void Update()
    {
        floorNumberDisplay.text = elevatorController.CurrentFloor.ToString();
    }
}
