using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnCtrlr : MonoBehaviour
{
    private Rigidbody2D enRgdBdy;     //Rigidbody of player
    private int dir;                //Direction of player
    //private int lkDir = 0;              //Looking direction of player
    //private bool mvng = false;          //Is enemy moving
    [SerializeField]
    private float dist;
    private PlyrCtrlr plyr;

    public float mveSpd;                //Movement speed
    public float mxHlth;                //Max amount of health
    private float hlth;                 //Current amount of health
    public enum enState { chase, atk };
    public enState crntState;

    public float atkDmg;                //Attack Damage to Enemy
    public float atkRng;

    public float ifrmeTme;              //Max IFrames
    private float iframes;              //Current IFrames

    //public GameObject melCol;

    Animator anim;                      //Animator
    SpriteRenderer rend;                //Sprite Renderer
    string crntAnim;                    //Current Animation State

    private void Awake()
    {
        plyr = FindObjectOfType<PlyrCtrlr>();
        enRgdBdy = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();

        hlth = mxHlth;

        //FOR TESTING ONLY
        crntState = enState.chase;

    }

    private void Update()
    {
        if (iframes > 0)
        {
            iframes -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        switch (crntState)
        {
            case (enState.chase):
                Chase();
                break;
            case (enState.atk):
                Attack();
                break;
        }
    }

    void Chase()
    {
        dist = Vector2.Distance(transform.position, plyr.transform.position);
        if (plyr.transform.position.y < transform.position.y)
        {
            dir = 0;
        }
        else if (plyr.transform.position.x > transform.position.x)
        {
            dir = 1;
            rend.flipX = false;
        }
        else if (plyr.transform.position.x < transform.position.x)
        {
            dir = 1;
            rend.flipX = true;
        }
        else if (plyr.transform.position.y > transform.position.y)
        {
            dir = 2;
        }

        if (dist > atkRng)
        {
            Vector3 direction = plyr.transform.position - transform.position;
            enRgdBdy.AddForce(direction * mveSpd * Time.deltaTime);
        }
        else
        {
            /*
            if (clDwn <= 0)
            {
                crntState = enState.atk;
            }
            */
        }

    }

    void Attack()
    {

    }

    public void Dmgd(float amt)
    {
        if (iframes <= 0)
        {
            hlth -= amt;

            if (hlth <= 0)
            {
                Die();
            }
        }
    }
    void Die()
    {
        gameObject.SetActive(false);
    }
}
