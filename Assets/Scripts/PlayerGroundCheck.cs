using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    PlayerContoller playerContoller;
    private void Awake()
    {
        playerContoller = GetComponentInParent<PlayerContoller>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerContoller.gameObject)
            return;
        playerContoller.SetGroundedState(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerContoller.gameObject)
            return;
        playerContoller.SetGroundedState(false);

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == playerContoller.gameObject)
            return;
        playerContoller.SetGroundedState(true);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == playerContoller.gameObject)
            return;
        playerContoller.SetGroundedState(true);
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == playerContoller.gameObject)
            return;
        playerContoller.SetGroundedState(false);
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject == playerContoller.gameObject)
            return;
        playerContoller.SetGroundedState(true);
    }
}
