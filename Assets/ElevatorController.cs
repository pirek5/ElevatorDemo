using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    //set in editor
    [SerializeField] private Transform elevator;
    [SerializeField] private Vector3[] floorPositions;

    //state
    int currentFloor;

    

    void GoToFloor(int floor)
    {
        if(floor == currentFloor) { return; }
        StartCoroutine(ElevatorMovement(elevator.position, floorPositions[floor]));
    }

    IEnumerator ElevatorMovement(Vector3 startPos, Vector3 endPos)
    {
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
