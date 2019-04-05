using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Photocell : MonoBehaviour
{
    //dependencies
    [Inject] private DoorController doorController;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<PlayerActions>()) // auto opening works only for player, more universal aproach would be using for example layers or tags
        {
            doorController.AutoOpenDoor();
        }
    }
}
