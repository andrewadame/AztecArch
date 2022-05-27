using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnerController : MonoBehaviour
{
    public GameObject[] Spawners;
    public GameObject[] Objects;
    float[] SpawnTierRatio;
    public GameObject tempSpawn;
    public GameObject[] Tier1,Tier2,Tier3;

    public GameObject selected1,
                      selected2;

    public DungeonGrid DungeonGrid1,
                       DungeonGrid2;
    public Material normal, 
                    highlight;


    private void Awake()
    {
        Objects = new GameObject[Spawners.Length];
        for(int i = 0; i < Spawners.Length; i++)
        {
            GameObject temp = Instantiate(tempSpawn, Spawners[i].transform);
            temp.GetComponent<SpawnObject>().spawnerParent = Spawners[i];
            Objects[i] = temp;
        }
    }

    public int GetSpawnerPosition(GameObject selecteded)
    {
        for(int i = 0; i < Spawners.Length; i++)
        {
            if(Objects[i] == selecteded)
            {
                return i;
            }
        }
        return 0;
    }

    public void RemoveObject(GameObject obj)
    {
        int del = GetSpawnerPosition(obj);
        Objects[del] = new GameObject();
        if(obj == selected1)
        {
            selected1 = null;
        }
        if (obj == selected2)
        {
            selected2 = null;
        }
        
    }
}
