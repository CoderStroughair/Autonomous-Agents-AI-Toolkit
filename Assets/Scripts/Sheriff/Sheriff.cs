using UnityEngine;
using System.Collections;


class Sheriff : Agent
{
    private StateMachine<Sheriff> stateMachine;

    public delegate void Showdown(eLocation sheriffLocation);
    public static event Showdown OnArrival;

    private bool first = true;

    public void Awake()
    {
        this.stateMachine = new StateMachine<Sheriff>();
        this.stateMachine.Init(this, PatrolState.Instance);
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    override public void FixedUpdate()
    {
        if (!controller.characterMovement)
            return;

        this.stateMachine.Update();
        Vector2 mapLoc = getPosition();
        this.transform.position = new Vector3(mapLoc.x, mapLoc.y, 0);

        if (first)
        {
            controller.pathfinder.solve(GetMapPosition(), new Vector2(0, 0));
            first = false;
        }
        else
            OnArrival(this.location);

    }
}

