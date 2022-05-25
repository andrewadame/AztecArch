using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnCtrlr : MonoBehaviour
{
    Rigidbody2D enRgdBdy;     //Rigidbody of enemy
    Vector3 dir;                //Direction of enemy
    public float mveSpd;        //Movement speed

    PlyrCtrlr Player;

    public float mxHlth;
    float htlh;

    public float atkDmg;

    public float ifrmeTme;
    float iframes;

    public GameObject melCol;

    private void Awake()
    {
        enRgdBdy = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        //Direction enemy is moving
        //dir = new Vector3(input.x, input.y).normalized;

    }

    private void FixedUpdate()
    {
        if (iframes > 0)
        {
            iframes = Time.deltaTime;
        }

        //Enemy move
        enRgdBdy.velocity = dir * mveSpd;

        //Attack!

        ///////////

    }

    public void Dmgd(float amt)
    {
        if (iframes <= 0)
        {
            iframes = ifrmeTme;
            htlh -= amt;
            if (htlh <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {

    }
}
