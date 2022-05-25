using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlyrCtrlr : MonoBehaviour
{
    private Rigidbody2D plyrRgdBdy;     //Rigidbody of player
    private Vector3 dir;                //Direction of player
    private int lkDir = 0;              //Looking direction of player
    private bool mvng = false;          //Is player moving

    public float mveSpd;                //Movement speed
    private Vector2 input;              //Input from player
    public float mxHlth;                //Max amount of health
    private float hlth;                 //Current amount of health

    public float atkDmg;                //Attack Damage to Enemy
    private bool isAtkPrssd = false;    //Is Attack button pressed?
    private bool isAtk = false;         //Is Player Attacking?
    private float atkDly = 0.3f;

    public float ifrmeTme;              //Max IFrames
    private float iframes;              //Current IFrames

    //public GameObject melCol;

    Animator anim;                      //Animator
    SpriteRenderer rend;                //Sprite Renderer
    string crntAnim;                    //Current Animation State

    //Animation States
    const string PLYRIDLE = "PlyrIdle";
    const string PLYRWLKUP = "PlyrWlkUp";
    const string PLYRWLKSDE = "PlyrWlkSde";
    const string PLYRWLKDWN = "PlyrWlkDwn";
    const string PLYRUPATK = "PlyrUpAtk";
    const string PLYRSDEATK = "PlyrSdeAtk";
    const string PLYRDWNATK = "PlyrDwnAtk";

    private void Awake()
    {
        plyrRgdBdy = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();

        hlth = mxHlth;
        
    }

    // Update is called once per frame
    void Update()
    {
        //IFrames
        if (iframes > 0)
        {
            iframes -= Time.deltaTime;
        }

        /////////////////////////CONTROLS/////////////////////////
        //WASD/ARROW KEYS
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Attack check
        if (Input.GetKeyDown(KeyCode.F))
        {
            isAtkPrssd = true;
        }
        //////////////////////////////////////////////////////////

        //Direction player is moving
        dir = new Vector3(input.x, input.y).normalized;

        //Player movement
        mvng = (input.x != 0 || input.y != 0);

        //Animation Conditions
        anim.SetInteger("dir", lkDir);
        anim.SetBool("mov", mvng);

    }

    private void FixedUpdate()
    {
        //Player move
        plyrRgdBdy.velocity = dir * mveSpd;


        //Change direction and play animation
        if (!isAtk)
        {
            if (input.y < 0)
            {
                lkDir = 0;
                ChangeAnimationState(PLYRWLKDWN);
            }
            else if (input.y > 0)
            {
                lkDir = 2;
                ChangeAnimationState(PLYRWLKUP);
            }
            else if (input.x > 0)
            {
                lkDir = 1;
                ChangeAnimationState(PLYRWLKSDE);
                rend.flipX = false;
            }
            else if (input.x < 0)
            {
                lkDir = 1;
                ChangeAnimationState(PLYRWLKSDE);
                rend.flipX = true;
            }
            else
            {
                ChangeAnimationState(PLYRIDLE);
            }
        }

        //Attack and play animation
        if(isAtkPrssd)
        {
            isAtkPrssd = false;

            if(!isAtk)
            {
                isAtk = true;
                Debug.Log("Attacking");
                if (lkDir == 0)
                {
                    ChangeAnimationState(PLYRDWNATK);
                }
                else if(lkDir == 1)
                {
                    ChangeAnimationState(PLYRSDEATK);
                }
                else
                {
                    ChangeAnimationState(PLYRUPATK);
                }

                Invoke("AtkComp", atkDly);
            }
        }
    }

    //If Player takes damage
    public void Dmgd(float amt)
    {
        if (iframes <= 0)
        {
            iframes = ifrmeTme;
            hlth -= amt;
            if (hlth <= 0)
            {
                Die();
            }
        }
    }

    //If Player Dies
    public void Die()
    {
        gameObject.SetActive(false);
    }

    //Animation Manager
    void ChangeAnimationState(string newAnimation)
    {
        if (crntAnim == newAnimation) return;

        anim.Play(newAnimation);
        crntAnim = newAnimation;
    }

    void AtkComp()
    {
        isAtk = false;
    }
}
