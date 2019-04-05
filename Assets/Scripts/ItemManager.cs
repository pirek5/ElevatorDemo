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
    [Inject] FirstPersonController fpsController;
    [Inject] InputPlayerActions inputPlayerActions;

    public GameObject currentItem;

    public void ShowItem(GameObject item)
    {
        currentItem = item;
        item.SetActive(true);
        inputPlayerActions.CurrentState = State.item;
        fpsController.enabled = false;
        hud.SetActive(false);
        Cursor.visible = true;
    }

    public void PutAwayItem()
    {
        if(currentItem == null) { return; }

        currentItem.SetActive(false);
        currentItem = null;
        inputPlayerActions.CurrentState = State.movement;
        fpsController.enabled = true;
        hud.SetActive(true);
        Cursor.visible = false;
    }
}
