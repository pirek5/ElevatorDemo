using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTriggerCollider : MonoBehaviour
{
    Transform transportedObject;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<PlayerActions>())
        {
            print("player in elevator");
            transportedObject = collision.collider.transform;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerActions>())
        {
            print("player outside elevator");
            transportedObject = null;
        }
    }
}
