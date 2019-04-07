using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Door : MonoBehaviour
{
    //dependencies
    [Inject] private ElevatorController elevatorController;
    [Inject] private ElevatorSounds elevatorSounds;

    //state
    private bool photocellEnabled;

    private void OnTriggerStay(Collider collider)
    {
        print("triggered something");
        if (collider.GetComponent<PlayerActions>() && photocellEnabled) // auto opening works only for player, more universal aproach would be using for example layers or tags
        {
            print("trigerred player");
            elevatorController.OpenElevatorDoors();
            photocellEnabled = false;
        }
    }

    public void DoorOpening()
    {
        elevatorSounds.PlaySound(Sound.doors);
    }

    public void DoorOpened()
    {
        elevatorSounds.StopSound();
    }

    public void DoorCloseing()
    {
        elevatorSounds.PlaySound(Sound.doors);
        photocellEnabled = true;
    }

    public void DoorClosed()
    {
        photocellEnabled = false;
        elevatorController.EnableElevatorAutoBack();
        elevatorSounds.StopSound();
    }
    
}
