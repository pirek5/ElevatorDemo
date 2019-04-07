using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using Zenject;

public class ElevatorController : MonoBehaviour
{
    //config
    [SerializeField] private float elevatorSpeed = 10f;
    [SerializeField] private float elevatorMoveDelay = 3f;
    [SerializeField] private float autoComeBackTo0FloorDelay = 5f;

    //set in editor
    [SerializeField] private Transform elevator;
    [SerializeField] private Transform player;
    [SerializeField] private Vector3[] floorPositions;

    //state
    public int CurrentFloor { get; private set; }
    public bool IsMoving { get; private set; }

    //dependencies
    [Inject] private FirstPersonController firstPersonController;
    [Inject] private ElevatorDoorController elevatorDoorController;
    [Inject] private PlayerState playerState;
    [Inject] private ElevatorSounds elevatorSounds;

    public void GoToFloor(int floor)
    {
        if(floor == CurrentFloor || IsMoving) { return; }

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

        if(playerState.IsInElevator)
        {
            firstPersonController.InMovingElevator = true;
            player.SetParent(elevator);
        }
        elevatorSounds.PlaySound(Sound.elevatorMoves);
        while (fractionOfJourney < 1)
        {
            fractionOfJourney += Time.deltaTime * elevatorSpeed / distance;
            elevator.localPosition = Vector3.Lerp(currentPosition, endPos, fractionOfJourney);
            yield return null;
        }
        elevatorSounds.PlaySound(Sound.elevatorBoing);
        
        if (playerState.IsInElevator)
        {
            firstPersonController.InMovingElevator = false;
            player.SetParent(null);
        }
        
        CurrentFloor = floor;
        IsMoving = false;
        OpenElevatorDoors();
    }

    public void CallElevator(int floor)
    {
        if(floor == CurrentFloor)
        {
            elevatorDoorController.Open();
        }
        else if(!IsMoving)
        {
            GoToFloor(floor);
        }
        else
        {
            StartCoroutine(WaitForEndOfMovingAndGoToFloor(floor));
        }
    }

    public void OpenElevatorDoors()
    {
        StopAllCoroutines(); //in case elevator waits to move

        if (!IsMoving)
        {
            elevatorDoorController.Open();
        }
    }

    public void CloseElevatorDoors()
    {
        elevatorDoorController.Close();  
    }

    public void EnableElevatorAutoBack()
    {
        StartCoroutine(ElevatorAutoBack());
    }

    private IEnumerator ElevatorAutoBack()
    {
        if(CurrentFloor != 0)
        {
            float t = 0;
            while(t< autoComeBackTo0FloorDelay)
            {
                t += Time.deltaTime;
                yield return null;
            }
            GoToFloor(0);
        }
    }

    private IEnumerator WaitForEndOfMovingAndGoToFloor(int floor)
    {
        while (IsMoving)
        {
            yield return null;
        }
        GoToFloor(floor);
    }

}
