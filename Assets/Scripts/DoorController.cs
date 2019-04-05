using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    //config
    [SerializeField] private float autoCloseTime;

    //cached
    private Animator[] doorAnimators;
    private IEnumerator currentCoroutine;

    //debug
    public bool open = false;
    public bool close = false;

    void Awake()
    {
        doorAnimators = GetComponentsInChildren<Animator>();
        if (doorAnimators == null)
        {
            Debug.LogError("door animators cant be assigned!");
        }
    }

    private void Update()
    {
        if (open)
        {
            open = false;
            OpenDoor();
        }

        if (close)
        {
            close = false;
            CloseDoor();
        }
    }

    public void OpenDoor()
    {
        if (doorAnimators[0].GetCurrentAnimatorStateInfo(0).IsName("Door Open")) { return; } // doors already opening

        foreach (var door in doorAnimators)
        {
            float t = 1f - door.GetCurrentAnimatorStateInfo(0).normalizedTime; //time of clip starting point
            door.Play("Door Open", -1, Mathf.Clamp(t, 0f, 1f));
        }
        currentCoroutine = AutoCloseDoor();
        StartCoroutine(currentCoroutine);
    }

    public void CloseDoor()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine); //if door closed by player disables autoclosing
        }

        if(doorAnimators[0].GetCurrentAnimatorStateInfo(0).IsName("Door Close")) { return; } // doors already closing

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
            OpenDoor();
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
        CloseDoor();
    }
}
