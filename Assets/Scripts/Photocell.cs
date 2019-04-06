using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Photocell : MonoBehaviour
{
    //dependencies
    [Inject] private ElevatorController elevatorController;

    //state
    private bool photocellEnabled;

    private void OnTriggerStay(Collider collider)
    {
        if (collider.GetComponent<PlayerActions>() && photocellEnabled) // auto opening works only for player, more universal aproach would be using for example layers or tags
        {
            elevatorController.OpenElevatorDoors();
            photocellEnabled = false;
        }
    }

    public void DoorOpening()
    {
        photocellEnabled = true;
    }

    public void DoorClosed()
    {
        photocellEnabled = false;
        elevatorController.EnableElevatorAutoBack();
    }
    
}
