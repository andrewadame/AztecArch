using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }
    public IdleState idleState { get; private set; }
    public MoveState moveState { get; private set; }

    public Rigidbody2D playerRigidBody;

    public Animator Anim { get; private set; }

    [SerializeField]
    private PlayerData playerData;

    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        StateMachine = new PlayerStateMachine();
        playerData = new PlayerData();
        idleState = new IdleState(this, StateMachine, playerData, "");
        moveState = new MoveState(this, StateMachine, playerData, "");
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

}