using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Photocell : MonoBehaviour
{
    //dependencies
    [Inject] private ElevatorDoorController doorController;

    //state
    private bool photocellEnabled;

    private void OnTriggerStay(Collider collider)
    {
        if (collider.GetComponent<PlayerActions>() && photocellEnabled) // auto opening works only for player, more universal aproach would be using for example layers or tags
        {
            doorController.AutoOpenDoor();
            photocellEnabled = false;
        }
    }

    public void EnablePhotocell()
    {
        photocellEnabled = true;
    }

    public void DisablePhotocell()
    {
        photocellEnabled = false;
    }
    
}
