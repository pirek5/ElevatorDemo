using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerActions : MonoBehaviour
{
    //dependencies
    [Inject] PlayerState playerState;
    [Inject] ItemManager itemManager;

    void Update()
    {
        if(playerState.SelectedObject != null && playerState.CurrentState == State.movement)
        {
            MovementActions();
        }
        else if (playerState.CurrentState == State.item)
        {
            ItemActions();
        }
    }

    private void MovementActions()
    {
        if (playerState.Action && playerState.SelectedObject.GetComponent(typeof(IButton)))
        {
            var button = playerState.SelectedObject.GetComponent(typeof(IButton)) as IButton;
            button.EnableButton();
        }
    }

    private void ItemActions()
    {
        if (playerState.Cancel)
        {
            itemManager.PutAwayItem();
        }
    }

}
