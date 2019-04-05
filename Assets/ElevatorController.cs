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

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
