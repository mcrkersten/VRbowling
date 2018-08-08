using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class HitByRay : MonoBehaviour {

    public SCR_object colors;
    public SpriteRenderer[] subSprites;
    public SpriteRenderer mainSprite;

    [Header("SelectPlayers")]
    public GameObject mainMenu;
    public GameObject playersMenu;
    public GameObject laneMenu;
    public GameObject[] lanes;

    [Header("Lane")]
    public Transform position;
    public GameObject player;
    public int playerAmount;
    public PinMachine machine;

    [Header("Hands")]
    public GameObject[] hands;

    // Use this for initialization
    void Start () {
        mainSprite = this.gameObject.GetComponent<SpriteRenderer>();
	}

    public void HitActive()
    {
        mainSprite.color = colors.Mainhit;
        for (int i = 0; i < subSprites.Length; i++)
        {
            subSprites[i].color = colors.SubHit;
        }
    }

    public void HitNotActive()
    {
        mainSprite.color = colors.MainNonHit;
        for (int i = 0; i < subSprites.Length; i++)
        {
            subSprites[i].color = colors.SubNonHit;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SelectPlayers()
    {
        mainMenu.SetActive(false);
        playersMenu.SetActive(true);
    }

    public void Players()
    {
        for(int i = 0; i < lanes.Length; i++)
        {
            lanes[i].GetComponent<ScoreCalculator>().GameStart(playerAmount);
        }
        laneMenu.SetActive(true);
        playersMenu.SetActive(false);
    }

    public void Lane()
    {
        player.transform.position = position.position;
        Lasers();    
    }

    public void Lasers()
    {
        //Turn lasers off
        for(int i = 0; i < hands.Length; i++)
        {
            hands[i].GetComponent<SteamVR_LaserPointer>().thickness = 0f;
        }
        //Turn MAchine on.
        machine.ActivateMachine();
        laneMenu.SetActive(false);
    }
}
