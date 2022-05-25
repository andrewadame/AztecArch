using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnCtrlr : MonoBehaviour
{
    private Rigidbody2D enRgdBdy;     //Rigidbody of player
    private float dir;                //Direction of player
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
    private bool isAtk = false;         //Is Enemy Attacking?
    private float atkDly = 0.3f;

    public float ifrmeTme;              //Max IFrames
    private float iframes;              //Current IFrames

    //public GameObject melCol;

    Animator anim;                      //Animator
    SpriteRenderer rend;                //Sprite Renderer
    string crntAnim;                    //Current Animation State

    //Animation States
    const string ENIDLE = "SlimeIdle";
    const string ENWLKUP = "SlimeMveUp";
    const string ENWLKSDE = "SlimeMveSde";
    const string ENWLKDWN = "SlimeMveDwn";
    const string ENUPATK = "SlmeAtkUp";
    const string ENSDEATK = "SlmeAtkSde";
    const string ENDWNATK = "SlimeAtkDwn";

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

        anim.SetFloat("dir", dir);
    }

    void Chase()
    {
        dist = Vector2.Distance(transform.position, plyr.transform.position);

        if (!isAtk)
        {
            if (plyr.transform.position.y < transform.position.y )
            {
                dir = 0;
                ChangeAnimationState(ENWLKDWN);
            }
            else if (plyr.transform.position.y > transform.position.y)
            {
                dir = 2;
                ChangeAnimationState(ENWLKUP);
            }
            else if (plyr.transform.position.x > transform.position.x)
            {
                dir = 1;
                ChangeAnimationState(ENWLKSDE);
                rend.flipX = false;
            }
            else if (plyr.transform.position.x < transform.position.x)
            {
                dir = 1;
                ChangeAnimationState(ENWLKSDE);
                rend.flipX = true;
            }
        }

        if (dist > atkRng)
        {
            Vector3 direction = plyr.transform.position - transform.position;
            enRgdBdy.AddForce(direction * mveSpd * Time.deltaTime);
        }
        else
        {
            
            //if (clDwn <= 0)
            //{
                crntState = enState.atk;
            //}
        }

    }

    void Attack()
    {
        if (!isAtk)
        {
            isAtk = true;
            if (dir == 0)
            {
                ChangeAnimationState(ENDWNATK);
            }
            else if (dir == 1)
            {
                ChangeAnimationState(ENSDEATK);
            }
            else
            {
                ChangeAnimationState(ENUPATK);
            }

            Invoke("AtkComp", atkDly);
        }

        crntState = enState.chase;
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
