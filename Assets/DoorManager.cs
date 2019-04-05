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
            float t = 1f - door.GetCurrentAnimatorStateInfo(0).normalizedTime; //time of clip start
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

        foreach (var door in doorAnimators)
        {
            float t = 1f - door.GetCurrentAnimatorStateInfo(0).normalizedTime; //time of clip start
            door.Play("Door Close", -1, Mathf.Clamp(t, 0f, 1f));
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
