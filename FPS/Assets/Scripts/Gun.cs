using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class Gun : ScriptableObject
{
    public string Name;
    public string description;
    public string type;

    public Sprite logo;

    public GameObject model;

    public int damage;
    public int range;
    public int fireRate;
}