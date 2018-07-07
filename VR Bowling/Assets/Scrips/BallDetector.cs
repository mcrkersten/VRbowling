using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDetector : MonoBehaviour {

    public int waitForSeconds;
    public Animator cleanAlley;
    public Animator pinMachine;

    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            StartCoroutine(BallDetected());
        }
    }

    IEnumerator BallDetected()
    {
        yield return new WaitForSeconds(waitForSeconds);
        pinMachine.SetTrigger("Attach");
        cleanAlley.SetTrigger("Activated");
    }
}
