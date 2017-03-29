using UnityEngine;

public sealed class RobState : GlobalState<Bandit> {
	
	static readonly RobState instance = new RobState();
	
	public static RobState Instance {
		get {
			return instance;
		}
	}
	
	static RobState() {}
	private RobState() {}

    public override void Enter(Bandit agent)
    {
        if (agent.destination == eLocation.UNSET)
            agent.location = eLocation.Bank;
        agent.destination = eLocation.Bank;
        Debug.Log("Keith has arrived at the Bank...");
    }

    public override void Execute(Bandit agent)
    {
        agent.RobGold();
        agent.LeaveGlobalState();
    }

    public override void Exit(Bandit agent)
    {
        //Debug.Log("Keith is leaving the Cemetery");
    }
}
