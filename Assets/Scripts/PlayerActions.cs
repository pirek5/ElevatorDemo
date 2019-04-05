using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerActions : MonoBehaviour
{
    //dependencies
    [Inject] InputPlayerActions input;

    void Update()
    {
        if(input.SelectedObject == null) { return; }

        if(input.CurrentState == State.movement)
        {
            MovementActions();
        }
        else if (input.CurrentState == State.item)
        {
            ItemActions();
        }

    }

    private void MovementActions()
    {
        if (input.Action && input.SelectedObject.GetComponent(typeof(IButton)))
        {
            var button = input.SelectedObject.GetComponent(typeof(IButton)) as IButton;
            button.EnableButton();
        }
    }

    private void ItemActions()
    {
        // possibility to cancel
    }

}
