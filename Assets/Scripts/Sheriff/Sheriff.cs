using UnityEngine;
using System.Collections;


class Sheriff : Agent
{
    private StateMachine<Sheriff> stateMachine;

    public delegate void Showdown(Vector3 location);
    public static event Showdown OnArrival;


    public void Awake()
    {
        this.stateMachine = new StateMachine<Sheriff>();
        this.stateMachine.Init(this, PatrolState.Instance);
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    override public void FixedUpdate()
    {
        bool movement = doMovement();
        OnArrival(this.transform.position);
        if (!movement)
            return;

        this.stateMachine.Update();



    }
}

