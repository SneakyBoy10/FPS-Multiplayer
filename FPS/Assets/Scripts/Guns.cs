using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class Guns : ScriptableObject
{
    public string name;
    public string description;
    public string type;

    public Sprite logo;

    public int damage;
    public int range;
}