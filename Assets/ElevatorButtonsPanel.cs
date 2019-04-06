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

    public void OnFloorPushed(int floor)
    {
        elevatorController.GoToFloor(floor);
        itemManager.PutAwayItem();
    }

    public void OnDoorOpenPressed()
    {
        elevatorController.OpenElevatorDoors();
        itemManager.PutAwayItem();
    }

    public void OnDoorClosePressed()
    {
        elevatorController.CloseElevatorDoors();
        itemManager.PutAwayItem();
    }

    private void Update()
    {
        floorNumberDisplay.text = elevatorController.CurrentFloor.ToString();
    }
}
