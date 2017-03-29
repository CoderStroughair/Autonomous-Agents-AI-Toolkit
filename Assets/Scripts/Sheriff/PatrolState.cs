using UnityEngine;
using System.Collections;
using System;

class PatrolState : State<Sheriff>
{
    static readonly PatrolState instance = new PatrolState();

    public static PatrolState Instance
    {
        get
        {
            return instance;
        }
    }

    static PatrolState() { }
    private PatrolState() { }

    public override void Enter(Sheriff agent)
    {
        if (agent.destination == eLocation.UNSET)
            agent.location = eLocation.SheriffsOffice;
        agent.destination = eLocation.SheriffsOffice;
    }

    public override void Execute(Sheriff agent)
    {
        float random = UnityEngine.Random.Range(0.0f, 1.0f);
        if (random > 0.6)
            return;
        if (agent.location == eLocation.SheriffsOffice)
            agent.destination = eLocation.Cemetery;
        else if (agent.location == eLocation.Cemetery)
            agent.destination = eLocation.Shack;
        else if (agent.location == eLocation.Shack)
            agent.destination = eLocation.Mine;
        else
            agent.destination = eLocation.SheriffsOffice;
    }

    public override void Exit(Sheriff agent)
    {

    }
}

