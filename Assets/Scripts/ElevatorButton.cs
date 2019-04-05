using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ElevatorButton : MonoBehaviour, IButton
{
    //set in editor
    [SerializeField] private GameObject button;

    //dependencies
    [Inject] ItemManager itemManager;

    public void EnableButton()
    {
        itemManager.ShowItem(button);
    }
}
