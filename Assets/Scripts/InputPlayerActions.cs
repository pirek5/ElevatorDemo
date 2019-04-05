using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayerActions : MonoBehaviour
{

    //set in editor;
    [SerializeField] Transform head;

    //config
    [SerializeField] private float actionsRange;

    //state
    public bool Action { get; private set; }
    public GameObject SelectedObject { get; private set; }

    //cached
    //private RaycastHit hit;

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
    }
}
