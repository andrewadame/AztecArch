using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlyrCtrlr : MonoBehaviour
{
    Rigidbody2D plyrRgdBdy;     //Rigidbody of player
    Vector3 dir;                //Direction of player
    public float mveSpd;        //Movement speed
    Vector2 input;              //Input from player

    private void Awake()
    {
        plyrRgdBdy = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {

        //WASD/ARROW KEYS
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Direction player is moving
        dir = new Vector3(input.x, input.y).normalized;

    }

    private void FixedUpdate()
    {
        //Player move
        plyrRgdBdy.velocity = dir * mveSpd;
        
        //Attack!

        ///////////

    }
}
