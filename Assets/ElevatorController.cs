﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using Zenject;

public class ElevatorController : MonoBehaviour
{
    //config
    [SerializeField] private float elevatorSpeed = 10f;
    [SerializeField] private float elevatorMoveDelay = 3f;

    //set in editor
    [SerializeField] private Transform elevator;
    [SerializeField] private Transform player;
    [SerializeField] private Vector3[] floorPositions;

    //state
    int currentFloor;
    public bool IsMoving { get; private set; }

    //dependencies
    [Inject] private ElevatorDoorController elevatorDoorController;
    [Inject] private PlayerState playerState;

    public void GoToFloor(int floor)
    {
        if(floor == currentFloor || IsMoving) { return; }

        CloseElevatorDoors();
        StartCoroutine(ElevatorMovement(elevator.localPosition, floorPositions[floor], floor));
    }

    IEnumerator ElevatorMovement(Vector3 startPos, Vector3 endPos, int floor)
    {
        float t = 0f;
        while(t< elevatorMoveDelay)
        {
            t += Time.deltaTime;
            yield return null;
        }

        IsMoving = true;
        float distance = Vector3.Distance(startPos, endPos);
        var currentPosition = startPos;
        var fractionOfJourney = 0f;

        playerState.inMovingElevator = true;
        player.SetParent(elevator);
        while (fractionOfJourney < 1)
        {
            fractionOfJourney += Time.deltaTime * elevatorSpeed / distance;
            elevator.localPosition = Vector3.Lerp(currentPosition, endPos, fractionOfJourney);
            yield return null;
        }
        playerState.inMovingElevator = false;
        player.SetParent(null);
        //MovingUp = false;

        
        currentFloor = floor;
        IsMoving = false;
        OpenElevatorDoors();
    }

    public void CallElevator(int floor)
    {
        if(floor == currentFloor)
        {
            elevatorDoorController.Open();
        }
        else
        {
            GoToFloor(floor);
        }
    }

    public void OpenElevatorDoors()
    {
        StopAllCoroutines(); //in case elevator waits tomove

        if (!IsMoving)
        {
            elevatorDoorController.Open();
        }
    }

    public void CloseElevatorDoors()
    {
        elevatorDoorController.Close();
    }

}
