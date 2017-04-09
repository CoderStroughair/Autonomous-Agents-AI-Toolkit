using UnityEngine;
using System.Collections.Generic;


class Sheriff : Agent
{
    private StateMachine<Sheriff> stateMachine;

    public delegate void Showdown(Vector3 location);
    public static event Showdown OnArrival;

    public static Sheriff s;


    public void Awake()
    {
        this.stateMachine = new StateMachine<Sheriff>();
        this.stateMachine.Init(this, PatrolState.Instance);
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        s = this;
        Miner.OnSmell += RespondToMinersSmell;
    }

    override public void FixedUpdate()
    {
        bool movement = doMovement();
        OnArrival(this.transform.position);
        if (!movement)
            return;
        List<Vector2> a = new List<Vector2>();
        List<Agent> g = controller.AgentSee(this, 2, new Vector3(0, 0, 0), ref a);
        foreach(Agent agent in g)
        {
            if (agent.agentName == "Bandit")
                OnArrival(agent.transform.position);
        }
        this.stateMachine.Update();
    }

    protected void RespondToMinersSmell(Vector2 gridLocation, int radius)
    {
        if (Vector2.Distance(GetGridPosition(), gridLocation) <= radius)
        {
            Debug.Log("The Sheriff is not impressed with how bad the Miner smells today.");
        }
    }
}

