using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MenuColors")]
public class SCR_object : ScriptableObject
{
    public Color MainNonHit;
    public Color Mainhit;
    public Color SubHit;
    public Color SubNonHit;

    public Color PlayerTurn;
    public Color nonPlayerTurn;
}
