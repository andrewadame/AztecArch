using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerKey : MonoBehaviour
{
    public DungeonGrid DungeonGrid1,
                       DungeonGrid2;
    public ObjectSpawnerController spawnerController;

    Vector2Int pos1 = new Vector2Int(0, 0),
               pos2 = new Vector2Int(0, 0),
               obj1 = new Vector2Int(0, 0),
               obj2 = new Vector2Int(0, 0);
    int increment1 = 0,
        increment2 = 0;

    public float LerpSpeed;


    private void Update()
    {
        //Select mode so move around 
        if (DungeonGrid1.SelectMode)
        {
            GetPlayerInput1(DungeonGrid1);
            spawnerController.selected1 = spawnerController.Objects[increment1];
            DungeonGrid1.Border.transform.position
            = Vector3.Lerp(spawnerController.Objects[increment1].transform.position, DungeonGrid1.Border.transform.position, 1 - 1/LerpSpeed) ;
            pos1 = new Vector2Int(0,0);
            SwitchSelected(DungeonGrid1, pos1);
            Debug.Log(increment1);
        }
        //BuildMode so move around 
        else
        {
            if (DungeonGrid1.thisBoard) {
                GetPlayerInput1(DungeonGrid1);
                SwitchSelected(DungeonGrid1, pos1);
                DungeonGrid1.Border.transform.position = Vector3.Lerp(DungeonGrid1.selected.transform.position, DungeonGrid1.Border.transform.position, 1 - 1 / LerpSpeed);
                if (spawnerController.selected1)
                {
                    spawnerController.selected1.GetComponent<SpawnObject>().FollowTheLeader(DungeonGrid1);
                }
            }
            else
            {
                GetPlayerInput1(DungeonGrid2);
                SwitchSelected(DungeonGrid2, pos1);
                DungeonGrid1.Border.transform.position = Vector3.Lerp(DungeonGrid2.selected.transform.position, DungeonGrid1.Border.transform.position, 1 - 1 / LerpSpeed);

                if (spawnerController.selected1)
                {
                    spawnerController.selected1.GetComponent<SpawnObject>().FollowTheLeader(DungeonGrid2);
                }
            }

        }

        if (DungeonGrid2.SelectMode)
        {
            GetPlayerInput2(DungeonGrid2);
            spawnerController.selected2 = spawnerController.Objects[increment2];
            DungeonGrid2.Border.transform.position = Vector3.Lerp(spawnerController.Objects[increment2].transform.position, DungeonGrid2.Border.transform.position, 1 - 1 / LerpSpeed);
            pos2 = new Vector2Int(0, 0);
            SwitchSelected(DungeonGrid2, pos2);
            Debug.Log(increment2);
        }
        //BuildMode so move around 
        else
        {
            if (DungeonGrid2.thisBoard)
            {
                GetPlayerInput2(DungeonGrid2);
                SwitchSelected(DungeonGrid2, pos2);
                DungeonGrid2.Border.transform.position = Vector3.Lerp(DungeonGrid2.selected.transform.position, DungeonGrid2.Border.transform.position, 1 - 1 / LerpSpeed);
                if (spawnerController.selected2)
                {
                    spawnerController.selected2.GetComponent<SpawnObject>().FollowTheLeader(DungeonGrid2);
                }
            }
            else
            {
                GetPlayerInput2(DungeonGrid1);
                SwitchSelected(DungeonGrid1, pos2);
                DungeonGrid2.Border.transform.position = Vector3.Lerp(DungeonGrid1.selected.transform.position, DungeonGrid2.Border.transform.position, 1 - 1 / LerpSpeed);

                if (spawnerController.selected2)
                {
                    spawnerController.selected2.GetComponent<SpawnObject>().FollowTheLeader(DungeonGrid1);
                }
            }

        }


    }

    

    //Toggles selecteded on relative Grid on Pos passed
    public void SwitchSelected(DungeonGrid dungeonGrid, Vector2Int pos)
    {
        pos.y = Mathf.Clamp(pos.y, 0, dungeonGrid.width -1); 
        pos.x = Mathf.Clamp(pos.x, 0, dungeonGrid.height - 1); 
        dungeonGrid.tiles[ pos.x, pos.y].GetComponent<Tile>().Select();  
        dungeonGrid.selected = dungeonGrid.tiles[pos.x, pos.y];
    }


    //Controlls for player1, grid controlls which board they are droping at
    public void GetPlayerInput1(DungeonGrid grid)
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (!grid.SelectMode)
                pos1 = grid.GetSelectedPosition() + new Vector2Int(0, 1);
            else;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (!grid.SelectMode)
                pos1 = grid.GetSelectedPosition() + new Vector2Int(0, -1);
            else;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (grid.SelectMode)
                increment1 = Mathf.Clamp(spawnerController.GetSpawnerPosition(spawnerController.selected1) +1 , 0, spawnerController.Objects.Length);
            else
                pos1 = grid.GetSelectedPosition() + new Vector2Int(1, 0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (grid.SelectMode)
                increment1 = Mathf.Clamp(spawnerController.GetSpawnerPosition(spawnerController.selected1) - 1, 0, spawnerController.Objects.Length);
            else
                pos1 = grid.GetSelectedPosition() + new Vector2Int(-1, 0);
        }
        //Place item
        if (Input.GetKeyDown(KeyCode.E) && !grid.SelectMode)
        {
                spawnerController.selected1.GetComponent<SpawnObject>().pickedUp = false;
                spawnerController.selected1.GetComponent<SpawnObject>().DoDrop(grid);
                grid.hasDropped = true;
                DungeonGrid1.SelectMode = true;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (grid.SelectMode)
            {
                spawnerController.selected1.GetComponent<SpawnObject>().PickUp(grid);
                DungeonGrid1.SelectMode = false;
                DungeonGrid2.hasDropped = false;
            }
        }
        if (DungeonGrid1.selected != null)
        {
            DungeonGrid1.selected.GetComponent<Tile>().UnSelect();
        }
         increment1 = Mathf.Clamp(increment1, 0, spawnerController.Objects.Length -1);
    }

    public void GetPlayerInput2(DungeonGrid grid)
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!grid.SelectMode)
                pos2 = grid.GetSelectedPosition() + new Vector2Int(0, 1);
            else;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (!grid.SelectMode)
                pos2 = grid.GetSelectedPosition() + new Vector2Int(0, -1);
            else;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (grid.SelectMode)
                increment2 = Mathf.Clamp(spawnerController.GetSpawnerPosition(spawnerController.selected2) + 1, 0, spawnerController.Objects.Length);
            else
                pos2 = grid.GetSelectedPosition() + new Vector2Int(1, 0);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (grid.SelectMode)
                increment2 = Mathf.Clamp(spawnerController.GetSpawnerPosition(spawnerController.selected2) - 1, 0, spawnerController.Objects.Length);
            else
                pos2 = grid.GetSelectedPosition() + new Vector2Int(-1, 0);
        }
        if (Input.GetKeyDown(KeyCode.RightShift) && !grid.SelectMode)
        {
            spawnerController.selected2.GetComponent<SpawnObject>().pickedUp = false;
            spawnerController.selected2.GetComponent<SpawnObject>().DoDrop(grid);
            grid.hasDropped = true;
            DungeonGrid1.SelectMode = true;
        }
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            if (grid.SelectMode)
            {
                spawnerController.selected2.GetComponent<SpawnObject>().PickUp(grid);
                DungeonGrid2.SelectMode = false;
                DungeonGrid2.hasDropped = false;
            }
        }
        if (DungeonGrid2.selected != null)
        {
            DungeonGrid2.selected.GetComponent<Tile>().UnSelect();
        }
        increment2 = Mathf.Clamp(increment2, 0, spawnerController.Objects.Length - 1); 
    } 


    /* old code that was to italian
    public void GetPlayerInputSpawner1()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            increment = Mathf.Clamp(spawnerController.GetSpawnerPosition(spawnerController.selected1, DungeonGrid1) + 1, 0, spawnerController.Objects.Length);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            increment1 = Mathf.Clamp(spawnerController.GetSpawnerPosition(spawnerController.selected1, DungeonGrid1) -1 , 0, spawnerController.Objects.Length);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            increment = Mathf.Clamp(spawnerController.GetSpawnerPosition(spawnerController.selected1, DungeonGrid1) + 2, 0, spawnerController.Objects.Length);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            increment = Mathf.Clamp(spawnerController.GetSpawnerPosition(spawnerController.selected1, DungeonGrid1) - 2, 0, spawnerController.Objects.Length);
        }

        if (spawnerController.selected1 != null)
        {
            spawnerController.selected1 = spawnerController.Objects[0];
        }
        
        Debug.Log(increment);
        spawnerController.selected1 = spawnerController.Objects[increment];
        
    }

    public void GetPlayerInputSpawner2()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            increment1 = Mathf.Clamp(spawnerController.GetSpawnerPosition(spawnerController.selected2, DungeonGrid2) + 1, 0, spawnerController.Objects.Length);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            increment1 = Mathf.Clamp(spawnerController.GetSpawnerPosition(spawnerController.selected2, DungeonGrid2) -1, 0, spawnerController.Objects.Length);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            increment1 = Mathf.Clamp(spawnerController.GetSpawnerPosition(spawnerController.selected2, DungeonGrid2) + 2, 0, spawnerController.Objects.Length);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            increment1 = Mathf.Clamp(spawnerController.GetSpawnerPosition(spawnerController.selected2, DungeonGrid2) - 2, 0, spawnerController.Objects.Length);
        }
        if (spawnerController.selected2 != null)
        {
            spawnerController.selected2 = spawnerController.Objects[0];
        }
        Debug.Log(increment1);
        spawnerController.selected2 = spawnerController.Objects[increment1];
    }
    */
}
