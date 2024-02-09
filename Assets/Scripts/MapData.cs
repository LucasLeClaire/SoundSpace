using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "Game/NewMap")]

public class MapData : ScriptableObject
{
    public string data;
    public int maxHealth;
    public int difficulty = 1;
    //TODO Image

}
