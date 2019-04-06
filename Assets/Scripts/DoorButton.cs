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
    
    public void EnableButton()
    {
        elevator.CallElevator(thisFloor);
    }
}
