using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    //config
    [SerializeField] private float autoCloseTime;

    //set in editor
    [SerializeField] private Animator door1Animator;
    [SerializeField] private Animator door2Animator;

    //cached
    private List<Animator> doorAnimators;
    private IEnumerator currentCoroutine;

    //debug
    public bool open = false;
    public bool close = false;

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

    void Awake()
    {
        doorAnimators = new List<Animator>() { door1Animator, door2Animator };
    }

    public void OpenDoor()
    {
        foreach(var door in doorAnimators)
        {
            door.SetTrigger("Open");
        }
        currentCoroutine = AutoCloseDoor();
        StartCoroutine(currentCoroutine);
    }

    public void CloseDoor()
    {
        StopCoroutine(currentCoroutine); //if door closed by player disables autoclosing

        foreach (var door in doorAnimators)
        {
            door.SetTrigger("Close");
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
