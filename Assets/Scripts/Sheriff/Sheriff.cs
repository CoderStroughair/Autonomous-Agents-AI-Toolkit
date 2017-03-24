using UnityEngine;
using System.Collections;


class Sheriff : Agent
{
    private StateMachine<Sheriff> stateMachine;

    public delegate void Showdown(eLocation sheriffLocation);
    public static event Showdown OnArrival;

    public void Awake()
    {
        this.stateMachine = new StateMachine<Sheriff>();
        this.stateMachine.Init(this, PatrolState.Instance);
    }

    override public void FixedUpdate()
    {
        TilingEngine controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<TilingEngine>();
        if (!controller.characterMovement)
            return;

        this.stateMachine.Update();
        Vector2 mapLoc = getPosition();
        this.transform.position = new Vector3(mapLoc.x, mapLoc.y, 0);

        OnArrival(this.location);
    }
}

