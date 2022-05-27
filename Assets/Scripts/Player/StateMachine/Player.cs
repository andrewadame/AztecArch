using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }
    public IdleState idleState { get; private set; }
    public MoveState moveState { get; private set; }
    public AttackState attackState { get; private set; }


    public Rigidbody2D playerRigidBody;
    public SpriteRenderer spriteRenderer;

    private Vector2 direction;

    public Animator Anim { get; private set; }

    [SerializeField]
    private PlayerData playerData;

    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        StateMachine = new PlayerStateMachine();
        idleState = new IdleState(this, StateMachine, playerData, "idle");
        moveState = new MoveState(this, StateMachine, playerData, "walk");
        attackState = new AttackState(this, StateMachine, playerData, "attack");

    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        StateMachine.Initiazlize(idleState);
    }


    private void Update()
    {
        StateMachine.currentState.LogicUpdate();
    }

    public void setDirection(Vector2 direction)
    {
        this.direction = direction;
        if (direction.x < 0)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
        Anim.SetFloat("Position X", direction.x);
        Anim.SetFloat("Position Y", direction.y);

    }
    public Vector2 getDirection()
    {
        return direction;
    }

}