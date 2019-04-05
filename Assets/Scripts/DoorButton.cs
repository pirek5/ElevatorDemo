using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour, IButton
{
    [SerializeField] DoorController door;

    public void EnableButton()
    {
        door.OpenDoor();
    }
}
