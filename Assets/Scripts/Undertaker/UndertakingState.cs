using System;
using UnityEngine;

public sealed class UndertakingState : State<Undertaker>
{
    static readonly UndertakingState instance = new UndertakingState();

    public static UndertakingState Instance{
        get{
            return instance;
        }
    }

    static UndertakingState() { }
    private UndertakingState() { }

    public override void Enter(Undertaker agent)
    {
        agent.location = eLocation.Undertakers;
        Debug.Log("The Undertaker has begun work.");
    }

    public override void Execute(Undertaker agent)
    {
        if(agent.body != null)
        {
            Debug.Log("The Undertaker is bringing the body to the Cemetary");
            agent.location = eLocation.Cemetery;
            agent.body.location = eLocation.Cemetery;

            agent.BuryBody();
        }
    }

    public override void Exit(Undertaker agent)
    {
    }
}

