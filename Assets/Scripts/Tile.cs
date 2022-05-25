using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class Tile : MonoBehaviour
{ 
    public SpriteRenderer image;
    public DungeonGrid gridParent;     //Grid that you create 
    public Material normal,highlited;  

    private void OnMouseEnter()
    {
        Select();
        gridParent.EnableKeyControlls = false;
    }
    private void OnMouseExit()
    {
        UnSelect();
        gridParent.EnableKeyControlls = true;
    }

    //Controlls when over
    public void Select()
    {   
        if(gridParent.selected != null)
        {
            gridParent.selected.GetComponent<Tile>().UnSelect();
        }
        image.material.color = highlited.color;
        gridParent.selected = gameObject;
        
    }
    public void UnSelect()
    {
        image.material.color = normal.color;
    }

}
