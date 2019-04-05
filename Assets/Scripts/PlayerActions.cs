using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerActions : MonoBehaviour
{
    //dependencies
    [Inject] InputPlayerActions input;

    // Update is called once per frame
    void Update()
    {
        if(input.SelectedObject == null) { return; }

        if (input.Action && input.SelectedObject.GetComponent(typeof(IButton)))
        {                
                var button = input.SelectedObject.GetComponent(typeof(IButton)) as IButton;
                button.EnableButton();
        }
    }
}
