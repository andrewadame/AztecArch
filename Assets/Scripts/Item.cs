using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public string itmNme;
    public string itmDesc;

    public int atkAdd;
    public int hlthAdd;
    
}
