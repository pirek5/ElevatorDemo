using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ElevatorDoorController : MonoBehaviour
{
    //config
    [SerializeField] private float autoCloseTime;

    //dependencies
    [Inject] private Animator[] doorAnimators;

    public void Open()
    {
        //do not proceed if doors already opening or opened
        if (doorAnimators[0].GetCurrentAnimatorStateInfo(0).IsName("Door Open")) { return; } 

        foreach (var door in doorAnimators)
        {
            float t = 1f - Mathf.Clamp(door.GetCurrentAnimatorStateInfo(0).normalizedTime, 0f, 1f); //time of clip starting point
            door.Play("Door Open", -1, t);
        }
        StartCoroutine(AutoCloseDoor());
    }

    public void Close()
    {
        StopAllCoroutines() ; //if door closed by player disables autoclosing

        if(doorAnimators[0].GetCurrentAnimatorStateInfo(0).IsName("Door Close")) { return; } // doors already closing or closed

        foreach (var door in doorAnimators)
        {
            float t = 1f - Mathf.Clamp(door.GetCurrentAnimatorStateInfo(0).normalizedTime, 0f, 1f); //time of clip starting point
            door.Play("Door Close", -1, t);
        }
    }

    private IEnumerator AutoCloseDoor()
    {
        float t = 0f;
        while(t< autoCloseTime)
        {
            t += Time.deltaTime;
            yield return null;
        }
        Close();
    }
}
