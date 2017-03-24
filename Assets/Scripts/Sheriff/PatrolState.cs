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
        agent.location = eLocation.SheriffsOffice;
    }

    public override void Execute(Sheriff agent)
    {
        if (agent.location == eLocation.SheriffsOffice)
            agent.location = eLocation.Undertakers;
        else if (agent.location == eLocation.Undertakers)
            agent.location = eLocation.Cemetery;
        else if (agent.location == eLocation.Cemetery)
            agent.location = eLocation.Shack;
        else if (agent.location == eLocation.Shack)
            agent.location = eLocation.Bank;
        else if (agent.location == eLocation.Bank)
            agent.location = eLocation.Mine;
        else if (agent.location == eLocation.Mine)
            agent.location = eLocation.SheriffsOffice;
    }

    public override void Exit(Sheriff agent)
    {

    }
}

