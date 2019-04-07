using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Instructions : MonoBehaviour
{
    //dependencies
    [Inject] PlayerState playerState;


    void Update()
    {
        if (playerState.Cancel)
        {
            CloseInstructions();
        }
    }

    private void CloseInstructions()
    {
        this.gameObject.SetActive(false);
    }
}
