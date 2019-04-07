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
        elevatorSounds.PlaySoundFromDifferentAudioSource(Sound.button, audioSource);
        itemManager.PutAwayItem();
    }

    public void OnDoorOpenPressed()
    {
        elevatorController.OpenElevatorDoors();
        elevatorSounds.PlaySoundFromDifferentAudioSource(Sound.button, audioSource);
        itemManager.PutAwayItem();
    }

    public void OnDoorClosePressed()
    {
        elevatorController.CloseElevatorDoors();
        elevatorSounds.PlaySoundFromDifferentAudioSource(Sound.button, audioSource);
        itemManager.PutAwayItem(); 
    }

    private void Update()
    {
        var floorNumber = elevatorController.CurrentFloor;
        if(floorNumber >= 0)
        {
            floorNumberDisplay.text = floorNumber.ToString();
        }
    }
}
