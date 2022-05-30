using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public bool pickedUp;
    public GameObject spawnerParent;
    DungeonGrid DungeonGrid;
    ObjectSpawnerController ObjectSpawnerController;

    private void Awake()
    {
        ObjectSpawnerController = FindObjectOfType<ObjectSpawnerController>();
    }


    // just no
    private void OnMouseDown()
    {
        if (pickedUp) PickUp(DungeonGrid);
        else DoDrop(DungeonGrid);
    }



    public void PickUp(DungeonGrid dungeonGrid)
    {
        FollowTheLeader(dungeonGrid);
        pickedUp = true;
        dungeonGrid.hasDropped = false;
    }

    //drop that bish tomato town
    public void DoDrop(DungeonGrid dungeonGrid)
    {
        gameObject.transform.position = dungeonGrid.selected.transform.position;
        ObjectSpawnerController.RemoveObject(gameObject);
    }

    //Makes the object follow border
    public void FollowTheLeader(DungeonGrid dungeon)
    {
        transform.position = dungeon.selected.transform.position;
    }
}
