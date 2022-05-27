using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnerController : MonoBehaviour
{
    public GameObject[] Spawners;
    public GameObject[] Objects;
    public float[] SpawnTierRatio;
    public GameObject tempSpawn;
    ItemSpawnerController spawnerController;

    public GameObject selected1,
                      selected2;
    public DungeonGrid DungeonGrid1,
                       DungeonGrid2;
    public Material normal, 
                    highlight;
    public bool finishedSpawning = false;

    private void Awake()
    {
        spawnerController = FindObjectOfType<ItemSpawnerController>();
        Objects = new GameObject[Spawners.Length];
        for(int i = 0; i < Spawners.Length; i++)
        {
            GameObject temp = Instantiate(spawnerController.GetItemFromTier(SpawnTierRatio), Spawners[i].transform);
            temp.GetComponent<SpawnObject>().spawnerParent = Spawners[i];
            Objects[i] = temp;
        }
        finishedSpawning = true;
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
       
        if(obj == selected1)
        {
            selected1 = null;
        }
        if (obj == selected2)
        {
            selected2 = null;
        } 
        Objects[del] = Instantiate(spawnerController.GetItemFromTier(SpawnTierRatio), Spawners[del].transform);
    }
}
