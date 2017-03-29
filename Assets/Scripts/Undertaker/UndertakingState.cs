using System;
using UnityEngine;

public sealed class UndertakingState : State<Agent>
{
    static readonly UndertakingState instance = new UndertakingState();

    public static UndertakingState Instance{
        get{
            return instance;
        }
    }

    static UndertakingState() { }
    private UndertakingState() { }

    public override void Enter(Agent agent)
    {
        if (agent.destination == eLocation.UNSET)
            agent.location = eLocation.Undertakers;
        agent.destination = eLocation.Undertakers;
        Debug.Log("The Undertaker has begun work.");
    }

    public override void Execute(Agent agent )
    {
        Undertaker uAgent = agent as Undertaker;
        if(uAgent.body != null)
        {
            if (uAgent.destination != eLocation.Cemetery)
            {
                Debug.Log("The Undertaker is bringing the body to the Cemetary");
                uAgent.destination = eLocation.Cemetery;
                uAgent.body.destination = eLocation.Cemetery;
            }
            uAgent.body.transform.position = uAgent.transform.position;
            uAgent.body.location = uAgent.location;
            if (uAgent.location == eLocation.Cemetery)
                uAgent.BuryBody();
        }
    }

    public override void Exit(Agent agent)
    {
    }
}

