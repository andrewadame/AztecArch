using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGrid : MonoBehaviour
{
    public int height = 7,
        width = 5,
        spaceBetween;

    public GameObject selected; //will swap out but required temporarily 
    public GameObject floor;
    public GameObject road;
    public GameObject Border;
     
    public GameObject[,] tiles;
    public Vector3 startPos;

    public bool EnableKeyControlls = false; //toggle between build/play
    public bool SelectMode = true;          //Choosing Tiles
    public bool hasDropped = false;         //set block
    public bool thisBoard  = true;          //governs which board to move to;


    private void Awake()
    {
        tiles = new GameObject[height, width];
        CreateGrid();

    }

    //Fill grid in with 'grass'/default block 
    void CreateGrid()
    {
        for(int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                GameObject temp = Instantiate(floor, startPos+ transform.position + new Vector3(spaceBetween * j, spaceBetween * i, 0) ,transform.rotation);
                temp.GetComponent<Tile>().gridParent = this;
                tiles[i, j] = temp;
            }
        }
        CreateRoad();
    }


    //Adds road in grid
    void CreateRoad()
    {   bool spawned = true;
        int currentRoadIndex = Mathf.Max(width / 2);
        int lastRoadIndex = Mathf.Max(width / 2); ;
        SwapTiles(0, currentRoadIndex);
        SwapTiles(height-1, currentRoadIndex);
        for (int i = 1; i < height-1; i++)
        {
            //Places a road randomly in row "sometimes"
            if (Random.value < 0.5f && !spawned)
            {
                int pos = Random.Range(0, width - 1);
                SwapTiles(i,pos);
                lastRoadIndex = currentRoadIndex;
                currentRoadIndex = pos;
                spawned = true;
            }
            //Connects roads vertically 
            else 
            {
                spawned = false;
                SwapTiles(i, currentRoadIndex);
            }

            //Connects roads horizontally 
            if (i > 0)
            {
                if (spawned && currentRoadIndex > lastRoadIndex)
                {
                    for (int last = lastRoadIndex; last <= currentRoadIndex; last++)
                    {
                        SwapTiles(i, last);
                    }
                }
                if (spawned && currentRoadIndex < lastRoadIndex)
                {
                    for (int last = lastRoadIndex; last >= currentRoadIndex; last--)
                    {
                        SwapTiles(i, last);
                    }
                }
            }
            else { }
        }

        lastRoadIndex = currentRoadIndex;
        currentRoadIndex = Mathf.Max(width / 2);
        Debug.Log(currentRoadIndex + " current and last " + lastRoadIndex + " height: " + height);
        if ( currentRoadIndex > lastRoadIndex)
        {
            for (int last = lastRoadIndex; last <= currentRoadIndex; last++)
            {
                SwapTiles(height-2, last);
            }
        }
        if ( currentRoadIndex < lastRoadIndex)
        {
            for (int last = lastRoadIndex; last >= currentRoadIndex; last--)
            {
                SwapTiles(height -2, last);
            }
        }
    }

    //Returns position in array of selected object or 0,0 default
    public Vector2Int GetSelectedPosition()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (selected == tiles[i, j]) return new Vector2Int(i, j);
            }
        }
        return new Vector2Int(0, 0);
    }

    //Changes out floor to road
    void SwapTiles(int height, int width)
    {
        GameObject old = tiles[height,width];
        tiles[height, width] = Instantiate(road, old.transform.position, old.transform.rotation);
        tiles[height, width].GetComponent<Tile>().gridParent = this;
        Destroy(old);
    }

    //Used to tile in place of gameobject
    public void SwapTiles(GameObject tile)
    {
        GameObject old = selected;
        Vector2Int oldPosition = GetSelectedPosition();
        tiles[oldPosition.x, oldPosition.y] = tile;
        tile = selected;
        Tile temp = tile.AddComponent<Tile>();
        temp.SetDungeon(this);
        Destroy(old);
    }


    //Please no mouse is devil
    private void OnMouseEnter()
    {
        EnableKeyControlls = false;
    }

    private void OnMouseExit()
    {
        EnableKeyControlls = true;
    }
}
