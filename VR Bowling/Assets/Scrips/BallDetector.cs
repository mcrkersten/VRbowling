using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDetector : MonoBehaviour {

    public int waitForSeconds;
    public Animator cleanAlley;
    public Animator pinMachineAnimator;
    public PinMachine pinMachine;
    private GameObject detectedBall;
    public Transform ballReturnPos;

    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            StartCoroutine(BallDetected());
            detectedBall = other.gameObject;
        }
    }

    public void ActivateLane()
    {
        pinMachineAnimator.SetTrigger("Detache");
    }

    IEnumerator BallDetected()
    {
        yield return new WaitForSeconds(waitForSeconds);
        if(pinMachine.on == false)
        {
            pinMachine.ActivateMachine();
        }
        else
        {
            pinMachineAnimator.SetTrigger("Attach");
            cleanAlley.SetTrigger("Activated");
        }
        if (detectedBall != null)
        {
            detectedBall.transform.position = ballReturnPos.position;
        }
    }
}
