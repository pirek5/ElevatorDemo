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

    //cached
    private IEnumerator currentCoroutine;

    public void Open()
    {
        //do not proceed if doors already opening or opened
        if (doorAnimators[0].GetCurrentAnimatorStateInfo(0).IsName("Door Open")) { return; } 

        foreach (var door in doorAnimators)
        {
            float t = 1f - door.GetCurrentAnimatorStateInfo(0).normalizedTime; //time of clip starting point
            door.Play("Door Open", -1, Mathf.Clamp(t, 0f, 1f));
        }
        currentCoroutine = AutoCloseDoor();
        StartCoroutine(currentCoroutine);
    }

    public void Close()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine); //if door closed by player disables autoclosing
        }

        if(doorAnimators[0].GetCurrentAnimatorStateInfo(0).IsName("Door Close")) { return; } // doors already closing or closed

        foreach (var door in doorAnimators)
        {
            float t = 1f - door.GetCurrentAnimatorStateInfo(0).normalizedTime; //time of clip starting point
            door.Play("Door Close", -1, Mathf.Clamp(t, 0f, 1f));
        }
    }

    public void AutoOpenDoor()
    {
        if (doorAnimators[0].GetCurrentAnimatorStateInfo(0).IsName("Door Close"))
        {
            Open();
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
