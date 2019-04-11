using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { movement, item };

public class PlayerState : MonoBehaviour
{
    //set in editor;
    [SerializeField] Transform head;

    //config
    [SerializeField] private float actionsRange; //TODO dosent fit into the class

    //state
    public State CurrentState { get; set; }
    public bool Action { get; private set; }
    public bool Cancel { get; private set; }
    public bool Quit { get; private set; }
    public bool IsInElevator { get; private set; }
    public GameObject SelectedObject { get; private set; }


    private void Awake()
    {
        CurrentState = State.movement;
    }

    void Update()
    {
        RaycastHit hit;
        Physics.Raycast(head.position, head.forward, out hit, actionsRange);
        if(hit.collider)
        {
            if (hit.collider.GetComponent<FlashingObject>())
            {
                SelectedObject = hit.collider.gameObject;
            }
            else
            {
                SelectedObject = null;
            }
        }
        else
        {
            SelectedObject = null;
        }

        Action = Input.GetButtonDown("Fire1");
        Cancel = Input.GetButtonDown("Cancel");
        Quit = Input.GetButtonDown("Fire2");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("elevator"))
        {
            IsInElevator = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("elevator"))
        {
            IsInElevator = false;
        }
    }
}
