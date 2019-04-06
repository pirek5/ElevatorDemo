using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using Zenject;

public class ItemManager : MonoBehaviour
{
    //set in editor
    [SerializeField] private GameObject hud;

    //dependencies
    [Inject] private FirstPersonController firstPersonController;
    [Inject] PlayerState playerState;

    public GameObject currentItem;

    public void ShowItem(GameObject item)
    {
        firstPersonController.enabled = false;
        currentItem = item;
        item.SetActive(true);
        playerState.CurrentState = State.item;
        hud.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void PutAwayItem()
    {
        if(currentItem == null) { return; }

        firstPersonController.enabled = true;
        currentItem.SetActive(false);
        currentItem = null;
        playerState.CurrentState = State.movement;
        hud.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
