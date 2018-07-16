using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitByRay : MonoBehaviour {

    public SCR_object colors;
    public SpriteRenderer[] subSprites;
    public SpriteRenderer mainSprite;

    [Header("SelectLane")]
    public GameObject mainMenu;
    public GameObject lanesMenu;

    [Header("Lane")]
    public Transform position;
    public GameObject player;

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

    public void Quite()
    {
        Application.Quit();
    }

    public void SelectLane()
    {
        mainMenu.SetActive(false);
        lanesMenu.SetActive(true);
    }

    public void Lane()
    {
        player.transform.position = position.position;
        lanesMenu.SetActive(false);
    }
}
