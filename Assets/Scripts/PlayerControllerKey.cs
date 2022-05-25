using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerKey : MonoBehaviour
{
    public DungeonGrid DungeonGrid1,
                       DungeonGrid2;
    Vector2Int pos1 = new Vector2Int(0, 0),
               pos2 = new Vector2Int(0, 0);



    private void Update()
    {

        if (DungeonGrid1.EnableKeyControlls)   //building mode 
        {
            pos1 = DungeonGrid1.GetSelectedPosition();
            GetPlayerInput1();
            SwitchSelected(DungeonGrid1, pos1);
        }

        if (DungeonGrid2.EnableKeyControlls) {
            pos2 = DungeonGrid2.GetSelectedPosition();
            GetPlayerInput2();
            SwitchSelected(DungeonGrid2, pos2);
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

    public void GetPlayerInput1()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            pos1 = DungeonGrid1.GetSelectedPosition() + new Vector2Int(0, 1);

        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            pos1 = DungeonGrid1.GetSelectedPosition() + new Vector2Int(0, -1);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            pos1 = DungeonGrid1.GetSelectedPosition() + new Vector2Int(1, 0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            pos1 = DungeonGrid1.GetSelectedPosition() + new Vector2Int(-1, 0);
        }

        if (DungeonGrid1.selected != null)
        {
            DungeonGrid1.selected.GetComponent<Tile>().UnSelect();
        }
    }

    public void GetPlayerInput2()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            pos2 = DungeonGrid2.GetSelectedPosition() + new Vector2Int(0, 1);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            pos2 = DungeonGrid2.GetSelectedPosition() + new Vector2Int(0, -1);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            pos2 = DungeonGrid2.GetSelectedPosition() + new Vector2Int(1, 0);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            pos2 = DungeonGrid2.GetSelectedPosition() + new Vector2Int(-1, 0);
        }

        if (DungeonGrid2.selected != null)
        {
            DungeonGrid2.selected.GetComponent<Tile>().UnSelect();
        }
    }
}
