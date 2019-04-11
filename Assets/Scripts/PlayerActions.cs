using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using Zenject;

public class PlayerActions : MonoBehaviour
{
    //dependencies
    [Inject] PlayerState playerState;
    [Inject] ItemManager itemManager;
    [Inject] FirstPersonController firstPersonController;

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

        if (playerState.Quit)
        {
            QuitApplication();
        }
    }

    private void MovementActions()
    {
        if (!firstPersonController.isActiveAndEnabled) firstPersonController.enabled = true;

        if (playerState.Action && playerState.SelectedObject.GetComponent(typeof(IButton)))
        {
            var button = playerState.SelectedObject.GetComponent(typeof(IButton)) as IButton;
            button.EnableButton();
        }
    }

    private void ItemActions()
    {
        if (firstPersonController.isActiveAndEnabled) firstPersonController.enabled = false;

        if (playerState.Cancel)
        {
            itemManager.PutAwayItem();
        }
    }

    private void QuitApplication()
    {
        Application.Quit();
        Debug.Log("exit");
    }

}
