using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinMachine : MonoBehaviour {

    public Animator guard;
    public List<GameObject> pinPosition = new List<GameObject>();
    public GameObject pin;
    public bool turn;
    public ScoreCalculator calculator;
    private int frame1 = 0; //Score in throw 1
    private int frame2 = 0; //Score in throw 2
    public int frame = 0; //Gets edited in ScoreCalculator

    // Use this for initialization
    void Start () {
        turn = false;
        foreach (Transform pinPos in transform)
        {
            pinPosition.Add(pinPos.gameObject);

            GameObject newPin = Instantiate(pin, 
                pinPos.transform.position, 
                pinPos.transform.rotation, 
                pinPos.transform);

            pinPos.GetComponent<PinHolder>().pin = newPin;
            newPin.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    public void PinRelease()
    {
        foreach(GameObject pinPos in pinPosition)
        {
            pinPos.GetComponent<PinHolder>().pin.GetComponent<Rigidbody>().isKinematic = false;
            pinPos.GetComponent<PinHolder>().pin.transform.parent = null;           
        }
    }

    public void PinAttach()
    {
        if(turn == false) //Check of frame 1 of frame 2.
        {
            foreach (GameObject pinPos in pinPosition)
            {
                if (pinPos.GetComponent<PinHolder>().pin.GetComponent<Pin>().hasFallen == false)
                {
                    
                    pinPos.GetComponent<PinHolder>().pin.transform.parent = pinPos.transform;
                    pinPos.GetComponentInChildren<Rigidbody>().isKinematic = true;
                }
                else
                {
                    frame1++;
                    if(frame1 == 10)
                    {
                        ResetPins();
                        turn = false; //Frame 1 is actief na strike
                        calculator.ScoreCal(frame1, turn, frame);
                        frame1 = 0;
                        return;
                    }
                }
            }
            calculator.ScoreCal(frame1, turn, frame);
            turn = true; //Frame 2 is nu actief
        }
        else
        {
            foreach (GameObject pinPos in pinPosition)
            {
                if (pinPos.GetComponent<PinHolder>().pin.GetComponent<Pin>().hasFallen == false)
                {
                    pinPos.GetComponent<PinHolder>().pin.transform.parent = pinPos.transform;
                    pinPos.GetComponentInChildren<Rigidbody>().isKinematic = true;
                }
                else
                {
                    frame2++;
                }
                //Reset pins for new player              
            }
            ResetPins();
            calculator.ScoreCal(frame2, turn, frame);
            turn = false; //Frame 1 is nu actief

            //reset score
            frame1 = 0;
            frame2 = 0;
        }
    }

    public void ResetPins()
    {
        foreach (GameObject pinPos in pinPosition)
        {
            pinPos.GetComponent<PinHolder>().pin.GetComponent<Pin>().hasFallen = false;
            pinPos.GetComponent<PinHolder>().pin.transform.eulerAngles = new Vector3(0, 0, 0);
            pinPos.GetComponent<PinHolder>().pin.transform.position = pinPos.transform.position;
            pinPos.GetComponent<PinHolder>().pin.transform.parent = pinPos.transform;
            pinPos.GetComponentInChildren<Rigidbody>().isKinematic = true;
        }
    }

    public void TriggerGuard()
    {
        guard.SetTrigger("Activate");
    }
}
