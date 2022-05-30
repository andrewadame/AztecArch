using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlyrCtrlr : MonoBehaviour
{
    private Rigidbody2D plyrRgdBdy;     //Rigidbody of player
    //public int plyrNm;
    private Vector3 directionMoving;                //Direction of player
    private int lkDir = 0;              //Looking direction of player
    private bool mvng = false;          //Is player moving

    public float mveSpd;                //Movement speed
    private Vector2 input;              //Input from player
    public string inputXNme;
    public string inputYNme;
    public float mxHlth;                //Max amount of health
    [SerializeField]
    private float hlth;                 //Current amount of health
    Item itm;

    public float atkDmg;                //Attack Damage to Enemy
    public float knockbackAmount = 2.0f;
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


    enum Direction { None, Up, Down, Left, Right, DownLeft, DownRight, UpLeft, UpRight };
    private Direction lastDirection = Direction.Down;


    private Direction lastDirectionFacing = Direction.Down;


    private void Awake()
    {
        plyrRgdBdy = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        itm = FindObjectOfType<Item>();

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
        //Player Input
        input = new Vector2(Input.GetAxisRaw(inputXNme), Input.GetAxisRaw(inputYNme));

        //Attack check
        if (Input.GetKeyDown(KeyCode.F))
        {
            isAtkPrssd = true;
        }
        //////////////////////////////////////////////////////////

        //Direction player is moving
        directionMoving = new Vector2(input.x, input.y).normalized;
        var directionFacing = calculateDirectionMoving(directionMoving);
        if (directionFacing != lastDirectionFacing && directionFacing != Direction.None)
        {
            lastDirectionFacing = directionFacing;
            Debug.Log(lastDirectionFacing);
        }

        //Player movement
        mvng = (input.x != 0 || input.y != 0);

        //Animation Conditions
        anim.SetInteger("dir", lkDir);
        anim.SetBool("mov", mvng);

    }

    private Direction calculateDirectionMoving(Vector2 inputs)
    {
        var retVal = Direction.None;
        if (input.x < 0 && input.y < 0)
            retVal = Direction.DownLeft;
        else if (input.x < 0 && input.y > 0)
            retVal = Direction.UpLeft;
        else if (input.x > 0 && input.y < 0)
            retVal = Direction.DownRight;
        else if (input.x > 0 && input.y > 0)
            retVal = Direction.UpRight;
        else if (input.x > 0 && input.y == 0)
            retVal = Direction.Right;
        else if (input.x < 0 && input.y == 0)
            retVal = Direction.Left;
        else if (input.x == 0 && input.y > 0)
            retVal = Direction.Up;
        else if (input.x == 0 && input.y < 0)
            retVal = Direction.Down;
        return retVal;
    }

    private void FixedUpdate()
    {
        //Player move
        plyrRgdBdy.velocity = directionMoving * mveSpd;


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
        if (isAtkPrssd)
        {
            isAtkPrssd = false;

            if (!isAtk)
            {
                isAtk = true;
                Debug.Log("Attacking");
                if (lkDir == 0)
                {
                    ChangeAnimationState(PLYRDWNATK);
                }
                else if (lkDir == 1)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If Enemy hits...
        if (collision.tag == "Enemy")
        {
            Vector2 direction = this.transform.position - collision.transform.position;
            this.transform.Translate(direction * knockbackAmount);
        }
        //If player touches item
        if(collision.tag == "Pick-Up")
        {
            //Grab Item Holder
            itm = collision.GetComponent<ItmHolder>().itm;
            //Add to Attack Damage
            atkDmg += itm.atkAdd;
            //Add to Max Health
            mxHlth += itm.hlthAdd;
            collision.gameObject.SetActive(false);

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
