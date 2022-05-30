using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu]
public class ItemTierObject : ScriptableObject
{
    public string TierName;
    public GameObject[] TierDrops;
}
